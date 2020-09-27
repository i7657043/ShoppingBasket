using Microsoft.Extensions.DependencyInjection;
using ShoppingBasketChallenge.Alerts;
using ShoppingBasketChallenge.Basket;

namespace ShoppingBasketChallenge.Tests
{
    public class IocFixture
    {
        public IocFixture()
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection
                .AddTransient<IShoppingBasket, ShoppingBasket>() //Transient so a fresh Basket is created for each test
                .AddSingleton<IAlertService, AlertService>();    //Singleton as its more efficient to use the same instance of the Alert Service throughout test execution

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}