namespace Market.Model.Request
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
