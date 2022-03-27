using System.ComponentModel.DataAnnotations.Schema;

namespace Loyalty.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public DateTime DateCreated { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        Product Product { get; set; }
    }
}
