using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.DataLayer;
using VendingMachine.Business.Models;
using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Business.UseCases
{
    internal class LookUseCase : IUseCase
    {
        private readonly IProductRepository productRepository;
        private readonly IShelfView shelfView;

        public string Name => "look";

        public string Description => "Get access to products visualisation.";

        public bool CanExecute => true;

        public LookUseCase(IProductRepository productRepository, IShelfView shelfView)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.shelfView = shelfView ?? throw new ArgumentNullException(nameof(shelfView));
        }

        public void Execute()
        {
            IEnumerable<Product> products = productRepository.GetAll();
            var availableProducts = products.Where(p => p.Quantity > 0);

            shelfView.DisplayProducts(availableProducts);
        }
    }
}
