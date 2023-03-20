using System;
using VendingMachine.Business.AcquisitionExceptions;
using VendingMachine.Business.Authentication;
using VendingMachine.Business.DataLayer;
using VendingMachine.Business.Models;
using VendingMachine.Business.Payment;
using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Business.UseCases
{
    internal class BuyUseCase : IUseCase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IPaymentService paymentService;
        private readonly IProductRepository productRepository;
        private readonly IBuyView buyView;

        public string Name => "buy";

        public string Description => "Get access to products acquisition.";

        public bool CanExecute => !authenticationService.IsUserAuthenticated;

        public BuyUseCase(IAuthenticationService authenticationService, IPaymentService paymentService, IProductRepository productRepository, IBuyView buyView)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.buyView = buyView ?? throw new ArgumentNullException(nameof(buyView));
        }

        public void Execute()
        {
            int productId = buyView.RequestProductId();

            Product productToBeDispensed = productRepository.GetByColumnId(productId);

            if (productToBeDispensed == null)
            {
                throw new InvalidColumnIdException(productId);
            }

            if (productToBeDispensed.Quantity == 0)
            {
                throw new InsufficientStockException(productToBeDispensed.Name);
            }

            paymentService.Execute(productToBeDispensed.Price);

            DecrementStock(productToBeDispensed);
            productRepository.Update(productToBeDispensed);

            buyView.DispenseProduct(productToBeDispensed.Name);
        }

        private static void DecrementStock(Product product)
        {
            product.Quantity--;
        }
    }
}
