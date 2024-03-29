﻿namespace CatalogService.Dtos
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public int CategoryId { get; set; }
    }
}
