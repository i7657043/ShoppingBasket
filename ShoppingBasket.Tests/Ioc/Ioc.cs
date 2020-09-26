using Microsoft.Extensions.DependencyInjection;

namespace ShoppingBasket.Tests
{
    public class IocFixture
    {
        public IocFixture()
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection
                .AddTransient<IShoppingBasket, ShoppingBasket>() //Transient so a fresh Basket is created for each test
                .AddSingleton<IAlertService, AlertService>(); //Singleton as we can re-use the same instance of the Alert Service throughout execution

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}