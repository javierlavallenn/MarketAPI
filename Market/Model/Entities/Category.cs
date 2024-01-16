using System.Text.Json.Serialization;

namespace Market.Model.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
