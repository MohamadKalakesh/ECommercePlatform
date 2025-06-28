using ProductService.Domain.Common;

namespace ProductService.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public decimal Price { get; private set; }

        private Product() { }   //EF needs parameterless constructor to create object by reflection

        public Product(string name, string description, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
        }

        public void UpdateDetails(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
