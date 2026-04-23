
using GoodBurger.IntegrationTests;
using Reqnroll;

namespace GoodBurger.FunctionalTests.Hooks;

[Binding]
public sealed class TestHooks
{
    public static CustomWebApplicationFactory Factory { get; private set; }
    public static HttpClient Client { get; private set; }

    [BeforeTestRun]
    public static void Setup()
    {
        Factory = new CustomWebApplicationFactory();
        Client = Factory.CreateClient();
    }
}
