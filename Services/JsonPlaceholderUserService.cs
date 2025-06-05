using System.Net.Http.Json;
using UserDataCollector.Models;

namespace UserDataCollector.Services;

public class JsonPlaceholderUserService : IUserSourceService
{
    private readonly HttpClient _httpClient = new();

    public async Task<List<User>> GetUsersAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<JsonPlaceholderUser>>(
            "https://jsonplaceholder.typicode.com/users"
        );

        if (response == null)
            return new List<User>();

        return response.Select(user => new User
        {
            FirstName = user.Name.Split(' ').FirstOrDefault() ?? "",
            LastName = user.Name.Split(' ').Skip(1).FirstOrDefault() ?? "",
            Email = user.Email,
            SourceId = $"jsonplaceholder:{user.Id}"
        }).ToList();
    }

    private class JsonPlaceholderUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
