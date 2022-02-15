using Loyalty.Data.Enums;

namespace Loyalty.Models.Dtos.Requests.Category
{
    public class UpdateCategoryRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsShowOnHome { get; set; }
        public CategoryStatus Status { get; set; }
    }
}
