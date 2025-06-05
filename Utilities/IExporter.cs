using UserDataCollector.Models;

namespace UserDataCollector.Utilities;

public interface IExporter
{
    void Export(List<User> users, string folderPath);
}
