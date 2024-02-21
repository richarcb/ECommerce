using System.Text.Json.Serialization;

namespace CartService.Dtos
{
    public class ProductDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("inventory")]
        public int Inventory { get; set; }
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }
    }
}
