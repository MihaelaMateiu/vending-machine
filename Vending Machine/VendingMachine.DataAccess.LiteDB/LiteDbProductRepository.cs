using LiteDB;
using VendingMachine.Business.DataLayer;
using VendingMachine.Business.Models;


namespace VendingMachine.DataAccess.LiteDB
{
    internal class LiteDbProductRepository : IProductRepository
    {
        private readonly ILiteCollection<Product> collection;

        public LiteDbProductRepository(string connectionstring)
        {
            if (string.IsNullOrEmpty(connectionstring))
            {
                throw new ArgumentNullException(nameof(connectionstring));
            }

            LiteDatabase database = new LiteDatabase(connectionstring);
            collection = database.GetCollection<Product>();

            //var products = new List<Product>
            //{
            //    new Product
            //    {
            //        Name = "Chocolate",
            //        Price = 9,
            //        Quantity = 0,
            //        ColumnId = 1
            //    },
            //    new Product
            //    {
            //        Name = "Chips",
            //        Price = 5,
            //        Quantity = 7,
            //        ColumnId = 2
            //    },
            //    new Product
            //    {
            //        Name = "Still Water",
            //        Price = 2,
            //        Quantity = 10,
            //        ColumnId = 3
            //    },
            //    new Product
            //    {
            //        Name = "Orange Juice",
            //        Price = 5,
            //        Quantity = 8,
            //        ColumnId = 4
            //    },
            //    new Product
            //    {
            //        Name = "Croissant",
            //        Price = 12,
            //        Quantity = 7,
            //        ColumnId = 5
            //    }
            //};

            //collection.InsertBulk(products);
        }

        public IEnumerable<Product> GetAll()
        {
            return collection.FindAll();
        }

        public Product GetByColumnId(int id)
        {
            return collection.FindOne(product => product.ColumnId == id);
        }

        public void Update(Product product)
        {
            collection.Update(product);
        }
    }
}


