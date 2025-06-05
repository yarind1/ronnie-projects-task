using System.Net.Http.Json;
using UserDataCollector.Models;

namespace UserDataCollector.Services;

public class ReqResUserService : IUserSourceService
{
    private static readonly HttpClient _httpClient = new();

    public async Task<List<User>> GetUsersAsync()
    {
        var users = new List<User>();
        int page = 1;
        int totalPages = 1;

        do
        {
            var url = $"https://reqres.in/api/users?page={page}";
            Console.WriteLine($"ReqRes: {url}");

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ReqResResponse>(url);

                if (response?.Data == null)
                {
                    Console.WriteLine("ReqRes returned no data.");
                    break;
                }

                totalPages = response.TotalPages;

                users.AddRange(response.Data.Select(u => new User
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    SourceId = $"reqres:{u.Id}"
                }));
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Failed to fetch from ReqRes (Page {page}): {ex.Message}");
                break;
            }

            page++;
        }
        while (page <= totalPages);

        return users;
    }

    private class ReqResResponse
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public List<ReqResUser>? Data { get; set; }
    }

    private class ReqResUser
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}
