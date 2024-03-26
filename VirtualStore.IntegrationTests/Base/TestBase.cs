using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStore.IntegrationTests.Infra;

namespace VirtualStore.IntegrationTests.Base
{
    public class TestBase : IClassFixture<WebAppFactory>
    {
        public readonly WebAppFactory _factory;

        public HttpClient _client;

        public TestBase(WebAppFactory factory) {  
            _factory = factory;
            _client = _factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:49863");
        }
    }
}
