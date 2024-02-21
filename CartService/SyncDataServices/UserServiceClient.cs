using CartService.Dtos;
using System.Text.Json;

namespace CartService.SyncDataServices
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;
        public UserServiceClient(HttpClient httpClient) 
        {
            _httpClient = httpClient; 
        }
        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5063/api/users/id/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                // Log the status code if the request fails
                Console.WriteLine($"Error: {response.StatusCode}");
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"JSON Response: {json}"); // Log the JSON response
            try
            {
                var userDto = JsonSerializer.Deserialize<UserDto>(json);
                Console.WriteLine("Getting user's name!");
                Console.WriteLine(userDto.Name);
                return userDto;
            }
            catch (Exception ex)
            {
                // Log any exceptions during deserialization
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return null;
            }
        }
    }
}
