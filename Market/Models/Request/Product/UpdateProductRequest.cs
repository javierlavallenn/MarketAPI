﻿namespace Market.Models.Request.Product
{
    public class UpdateProductRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public bool IsPublished { get; set; }
    }
}
