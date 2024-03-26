using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VirtualStore.IntegrationTests.Base;
using VirtualStore.IntegrationTests.Infra;
using VirtualStore.VirtualStore.Domain.Entities;
using Xunit;

namespace VirtualStore.IntegrationTests.Tests
{
    public class ProdutoTest : TestBase
    {
        public ProdutoTest(WebAppFactory factory) : base(factory) { }
        
        [Fact]
        public async Task GetProduto()
        {
            // Act
            var response = await _client.GetAsync("/produtos");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task GetProdutoById()
        {
            // Act
            var response = await _client.GetAsync("/produtos/4");

            var produto = new Produto()
            {
                Nome = "SSD",
                Estoque = 20,
                Valor = 800
            };

           
            var res = await response.Content.ReadFromJsonAsync<Produto>();

            // Assert
            Assert.Equal(res.Nome, produto.Nome);
            response.EnsureSuccessStatusCode(); // Status Code 200-299

        }


        [Fact]
        public async Task CreateProduto()
        {
            var produto = new Produto()
            {
                Nome = "Produto Teste",
                Estoque = 10,
                Valor = 5800
            };

           
            var stringPayload = JsonConvert.SerializeObject(produto);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/produtos", httpContent);

        
            
            var res = await response.Content.ReadFromJsonAsync<Produto>();

            Assert.Equal(res.Nome, produto.Nome);

            response.EnsureSuccessStatusCode(); // Status Code 200-299

        }




    }
}
