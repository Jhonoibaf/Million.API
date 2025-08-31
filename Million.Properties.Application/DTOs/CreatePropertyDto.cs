
namespace Million.Properties.Application.DTOs

{
    public class CreatePropertyDto
    {
        public string _id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
