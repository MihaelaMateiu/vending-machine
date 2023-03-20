using System.Collections.Generic;
using System.Configuration;
using VendingMachine.Business;
using VendingMachine.Business.Authentication;
using VendingMachine.Business.DataLayer;
using VendingMachine.Business.Payment;
using VendingMachine.Business.PresentationLayer;
using VendingMachine.Business.UseCases;
using VendingMachine.DataAccess.LiteDB;
using VendingMachine.Presentation;
using VendingMachine.Presentation.PaymentView;
using VendingMachine.Presentation.UseCasesView;

namespace VendingMachine
{
    internal class Bootstrapper
    {
        public void Run()
        {
            VendingMachineApplication vendingMachineApplication = BuildApplication();
            vendingMachineApplication.Run();
        }

        private static VendingMachineApplication BuildApplication()
        {
            IMainView mainView = new MainView();
            ILoginView loginView = new LoginView();
            IShelfView shelfView = new ShelfView();
            IBuyView buyView = new BuyView();
            ICashPaymentTerminal cashPaymentTerminal = new CashPaymentTerminal();
            ICardPaymentTerminal cardPaymentTerminal = new CardPaymentTerminal();

            List<IPaymentAlgorithm> paymentAlgorithms = new List<IPaymentAlgorithm>()
            {
                new CashPayment(cashPaymentTerminal),
                new CardPayment(cardPaymentTerminal)
            };

            IAuthenticationService authenticationService = new AuthenticationService();
            IPaymentService paymentService = new PaymentService(buyView, paymentAlgorithms);
            //IProductRepository productRepository = new InMemoryProductRepository();
            IProductRepository productRepository = new LiteDbProductRepository(ConfigurationManager.ConnectionStrings["ProductsLiteDB"].ConnectionString);


            List<IUseCase> useCases = new List<IUseCase>
            {
                new LoginUseCase(authenticationService, loginView),
                new LogoutUseCase(authenticationService),
                new LookUseCase(productRepository, shelfView),
                new BuyUseCase(authenticationService, paymentService, productRepository, buyView)
            };

            return new VendingMachineApplication(useCases, mainView);
        }
    }
}