namespace Loyalty.Data.Entities
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { set; get; }
        public int ViewCount { set; get; }
        public DateTime DateCreated { set; get; }
        public int Quantily { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }



    }
}
