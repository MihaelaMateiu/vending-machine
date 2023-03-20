using VendingMachine.Business.Models;
using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Presentation.UseCasesView
{
    internal class ShelfView : DisplayBase, IShelfView
    {
        public void DisplayProducts(IEnumerable<Product> products)
        {
            if (!HasProducts(products))
            {
                DisplayLine("No available products. Please try again later.", ConsoleColor.Red);
                return;
            }

            DisplayLine($"{"Id",-5} {"Name",-15} {"Quantity",-11} {"Price"}", ConsoleColor.White);
            Console.WriteLine();

            foreach (Product product in products)
            {
                DisplayLine($"{product.ColumnId,-5} {product.Name,-15} {product.Quantity,-11} {product.Price}", ConsoleColor.White);
            }
        }

        private static bool HasProducts(IEnumerable<Product> products)
        {
            bool isNullOrEmpty = products == null || !products.Any();

            return !isNullOrEmpty;
        }
    }
}
