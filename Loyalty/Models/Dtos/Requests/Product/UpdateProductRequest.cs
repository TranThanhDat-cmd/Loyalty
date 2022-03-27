namespace Loyalty.Models.Dtos.Requests.Product
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { set; get; }
        public int Quantily { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
