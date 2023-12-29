namespace Market.Models.Request.Product
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Brand{ get; set; }
        public string Category { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public bool IsPublished { get; set; }
    }
}
