using System.Text.Json;
using UserDataCollector.Models;

namespace UserDataCollector.Utilities;

public class JsonExporter : IExporter
{
    public void Export(List<User> users, string folderPath)
    {
        var path = Path.Combine(folderPath, "users.json");
        var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }
}
