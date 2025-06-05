using System.Text;
using UserDataCollector.Models;

namespace UserDataCollector.Utilities;

public class CsvExporter : IExporter
{
    public void Export(List<User> users, string folderPath)
    {
        var path = Path.Combine(folderPath, "users.csv");

        var sb = new StringBuilder();
        sb.AppendLine("FirstName,LastName,Email,SourceId");

        foreach (var user in users)
        {
            sb.AppendLine($"{Escape(user.FirstName)},{Escape(user.LastName)},{Escape(user.Email)},{Escape(user.SourceId)}");
        }

        File.WriteAllText(path, sb.ToString());
    }

    private string Escape(string value)
    {
        // Handle commas/quotes
        if (value.Contains(',') || value.Contains('"'))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }
        return value;
    }
}
