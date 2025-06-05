using UserDataCollector.Models;

namespace UserDataCollector.Services;

public interface IUserSourceService
{
  Task<List<User>> GetUsersAsync();
}