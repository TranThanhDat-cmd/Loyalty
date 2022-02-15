using Loyalty.Data.Enums;

namespace Loyalty.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsShowOnHome { get; set; }
        public CategoryStatus Status { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
