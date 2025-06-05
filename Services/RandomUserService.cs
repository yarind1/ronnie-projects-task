using System.Net.Http.Json;
using UserDataCollector.Models;

namespace UserDataCollector.Services;

public class RandomUserService : IUserSourceService
{
    private readonly HttpClient _httpClient = new();

    private const int USER_COUNT = 5000;

    public async Task<List<User>> GetUsersAsync()
    {
        var url = $"https://randomuser.me/api/?results={USER_COUNT}";
        var response = await _httpClient.GetFromJsonAsync<RandomUserResponse>(url);

        if (response == null || response.Results == null)
            return new List<User>();

        return response.Results.Select(user => new User
        {
            FirstName = user.Name.First,
            LastName = user.Name.Last,
            Email = user.Email,
            SourceId = $"randomuser:{user.Login.Uuid}"
        }).ToList();

    }

    // Classes to deserialize JSON response
    private class RandomUserResponse
    {
        public List<RandomUser>? Results { get; set; }
    }

    private class RandomUser
    {
        public Name Name { get; set; }
        public string Email { get; set; }
        public Login Login { get; set; }
    }

    private class Name
    {
        public string First { get; set; }
        public string Last { get; set; }
    }

    private class Login
    {
        public string Uuid { get; set; }
    }
}