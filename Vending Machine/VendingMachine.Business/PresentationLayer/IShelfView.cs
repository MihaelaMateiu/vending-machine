using VendingMachine.Business.Models;
using System.Collections.Generic;

namespace VendingMachine.Business.PresentationLayer
{
    internal interface IShelfView
    {
        public void DisplayProducts(IEnumerable<Product> products);
    }
}
