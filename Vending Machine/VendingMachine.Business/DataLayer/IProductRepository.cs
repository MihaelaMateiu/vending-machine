using System.Collections.Generic;
using VendingMachine.Business.Models;

namespace VendingMachine.Business.DataLayer
{
    internal interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product GetByColumnId(int id);

        void Update(Product product);
    }
}
