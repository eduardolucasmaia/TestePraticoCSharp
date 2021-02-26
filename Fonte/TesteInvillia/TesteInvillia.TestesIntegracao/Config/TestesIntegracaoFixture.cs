using System;
using System.Net.Http;
using Xunit;

namespace TesteInvillia.TestesIntegracao.Config
{
    [CollectionDefinition(nameof(TestesIntegracaoApiFixtureCollection))]
    public class TestesIntegracaoApiFixtureCollection : ICollectionFixture<TestesIntegracaoFixture<StartupApiTests>>
    {

    }

    public class TestesIntegracaoFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly TesteInvilliaAppFactory<TStartup> Factory;
        public HttpClient Client;

        public TestesIntegracaoFixture()
        {
            var clientOptions = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions()
            {
                HandleCookies = false,
                BaseAddress = new Uri("https://localhost:44318"),
                AllowAutoRedirect = true,
                MaxAutomaticRedirections = 7
            };

            Factory = new TesteInvilliaAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
        }
        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}