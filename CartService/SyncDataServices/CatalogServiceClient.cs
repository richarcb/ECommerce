using CartService.Dtos;
using System.Text.Json;

namespace CartService.SyncDataServices
{
    public class CatalogServiceClient : ICatalogServiceClient
    {
        private readonly HttpClient _httpClient;
        public CatalogServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5277/api/products/{productId}");
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
                ProductDto productDto = JsonSerializer.Deserialize<ProductDto>(json);
                Console.WriteLine("Getting product's name!");
                Console.WriteLine(productDto.Name);
                return productDto;
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
