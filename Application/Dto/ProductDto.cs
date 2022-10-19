

namespace Application.Dto
{
    public class ProductDto : BaseDto
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
