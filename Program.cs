using UserDataCollector.Models;
using UserDataCollector.Services;
using UserDataCollector.Utilities;


Console.WriteLine("Enter the folder path to save the file:");
var folderPath = Console.ReadLine();

if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
{
    Console.WriteLine("Invalid folder path.");
    return;
}

Console.WriteLine("Enter the file format (json/csv):");
var format = Console.ReadLine()?.ToLower();

if (format != "json" && format != "csv")
{
    Console.WriteLine("Invalid format. Only 'json' and 'csv' are supported.");
    return;
}

// Create services list
List<IUserSourceService> services = new()
{
    new RandomUserService(),
    new JsonPlaceholderUserService(),
    new DummyJsonUserService(),
    new ReqResUserService()
};

//Fetch users in parallel
Console.WriteLine("Fetching users from all sources...");

var userTasks = services.Select(service => service.GetUsersAsync()).ToList();
var results = await Task.WhenAll(userTasks);

List<User> allUsers = results.SelectMany(x => x).ToList();

Console.WriteLine($"Fetched {allUsers.Count} users.");

//Export to file
IExporter exporter = format == "csv"
    ? new CsvExporter()
    : new JsonExporter();

exporter.Export(allUsers, folderPath);

Console.WriteLine($"Exported to {format.ToUpper()} file at {folderPath}");
