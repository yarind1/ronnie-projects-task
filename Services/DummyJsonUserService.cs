using System.Net.Http.Json;
using UserDataCollector.Models;

namespace UserDataCollector.Services;

public class DummyJsonUserService : IUserSourceService
{
    private readonly HttpClient _httpClient = new();
    private const int PageSize = 100;

    public async Task<List<User>> GetUsersAsync()
    {
        var allUsers = new List<User>();
        int skip = 0;
        int total = int.MaxValue;

        while (allUsers.Count < total)
        {
            var url = $"https://dummyjson.com/users?limit={PageSize}&skip={skip}";
            var response = await _httpClient.GetFromJsonAsync<DummyJsonResponse>(url);

            if (response?.Users == null)
                break;

            total = response.Total;
            skip += PageSize;

            allUsers.AddRange(response.Users.Select(u => new User
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                SourceId = $"dummyjson:{u.Id}"
            }));
        }

        return allUsers;
    }

    private class DummyJsonResponse
    {
        public List<DummyJsonUser>? Users { get; set; }
        public int Total { get; set; }
    }

    private class DummyJsonUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
