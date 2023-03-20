using VendingMachine.Business.DataLayer;
using VendingMachine.Business.Models;

namespace VendingMachine.DataAccess.InMemory
{
    internal class InMemoryProductRepository : IProductRepository
    {
        private static readonly ICollection<Product> Products = new List<Product>
        {
            new Product
            {
                Name = "Chocolate",
                Price = 9,
                Quantity = 0,
                ColumnId = 1
            },
            new Product
            {
                Name = "Chips",
                Price = 5,
                Quantity = 7,
                ColumnId = 2
            },
            new Product
            {
                Name = "Still Water",
                Price = 2,
                Quantity = 10,
                ColumnId = 3
            },
            new Product
            {
                Name = "Orange Juice",
                Price = 5,
                Quantity = 8,
                ColumnId = 4
            },
            new Product
            {
                Name = "Croissant",
                Price = 12,
                Quantity = 7,
                ColumnId = 5
            }
        };

        public IEnumerable<Product> GetAll()
        {
            return Products;
        }

        public Product GetByColumnId(int id)
        {
            return Products.FirstOrDefault(product => product.ColumnId == id);
        }

        public void Update(Product product)
        {
        }
    }
}
