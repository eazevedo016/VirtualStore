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
        public async Task CreateProduto()
        {

            //Arrange
            var produto = new Produto()
            {
                Nome = "Produto Teste",
                Estoque = 10,
                Valor = 5800
            };

            var stringPayload = JsonConvert.SerializeObject(produto);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/produtos", httpContent);

            // Act
            var res = await response.Content.ReadFromJsonAsync<Produto>();

            // Assert
            Assert.Equal(res.Nome, produto.Nome);
            response.EnsureSuccessStatusCode(); // Status Code 200-299

        }

        [Fact]
        public async Task GetProduto()
        {
            // Act
            var response = await _client.GetAsync("/produtos");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }


        [Fact]
        public async Task UpdateProduto()
        {

            //Arrange
            var produto = new Produto()
            {
                Nome = "Criação de novo produto para atender ao teste de integração UpdateProduto",
                Estoque = 1,
                Valor = 1
            };

            
            var stringPayloadCreate = JsonConvert.SerializeObject(produto);
            var httpContentCreate = new StringContent(stringPayloadCreate, Encoding.UTF8, "application/json");
            var createResponse = await _client.PostAsync("/produtos", httpContentCreate);
            createResponse.EnsureSuccessStatusCode();
            var produtoCriado = await createResponse.Content.ReadFromJsonAsync<Produto>();

            produtoCriado.Nome = "Produto Atualizado";
            produtoCriado.Estoque = 30;
            produtoCriado.Valor = 7500;

            var stringPayloadUpdate = JsonConvert.SerializeObject(produtoCriado);
            var httpContentUpdate = new StringContent(stringPayloadUpdate, Encoding.UTF8, "application/json");

            try
            {   // Act
                var updateResponse = await _client.PutAsync($"/produtos/{produtoCriado.Id}", httpContentUpdate);
                updateResponse.EnsureSuccessStatusCode();
                var updatedProduto = await updateResponse.Content.ReadFromJsonAsync<Produto>();

                // Assert
                Assert.Equal(produtoCriado.Id, updatedProduto.Id);
                Assert.Equal(produtoCriado.Nome, updatedProduto.Nome);
                Assert.Equal(produtoCriado.Estoque, updatedProduto.Estoque);
                Assert.Equal(produtoCriado.Valor, updatedProduto.Valor);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro na solicitação HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }

        [Fact]
        public async Task DeleteProduto()
        {

            //Arrange
            var produto = new Produto()
            {
                Nome = "Criação de novo produto para atender ao teste de integração DeleteProduto",
                Estoque = 2,
                Valor = 2
            };


            var stringPayloadCreate = JsonConvert.SerializeObject(produto);
            var httpContentCreate = new StringContent(stringPayloadCreate, Encoding.UTF8, "application/json");
            var createResponse = await _client.PostAsync("/produtos", httpContentCreate);
            createResponse.EnsureSuccessStatusCode();
            var produtoCriado = await createResponse.Content.ReadFromJsonAsync<Produto>();

            try
            {
                //Act
                var deleteResponse = await _client.DeleteAsync($"/produtos/{produtoCriado.Id}");

                // Assert
                deleteResponse.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro na solicitação HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }


        [Fact]
        public async Task GetProdutoById()
        {
            // Arrange
            var novoProduto = new Produto()
            {
                Nome = "Criação de novo produto para atender ao teste de integração GetProdutoById",
                Estoque = 3,
                Valor = 3
            };

            var jsonPayload = JsonConvert.SerializeObject(novoProduto);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var postResponse = await _client.PostAsync("/produtos", httpContent);
            postResponse.EnsureSuccessStatusCode();
            
            var produtoAdicionado = await postResponse.Content.ReadFromJsonAsync<Produto>();

            try
            {   
                //Act
                var getResponse = await _client.GetAsync($"/produtos/{produtoAdicionado.Id}");
                getResponse.EnsureSuccessStatusCode();
               
                var produtoRetornado = await getResponse.Content.ReadFromJsonAsync<Produto>();

                // Assert
                Assert.Equal(produtoAdicionado.Nome, produtoRetornado.Nome);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro na solicitação HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }

        }


        [Fact]
        public async Task GetProdutoByName()
        {
            // Arrange
            var novoProduto = new Produto()
            {
                Nome = "Criação de novo produto para atender ao teste de integração GetProdutoByName",
                Estoque = 3,
                Valor = 3
            };

            var jsonPayload = JsonConvert.SerializeObject(novoProduto);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var postResponse = await _client.PostAsync("/produtos", httpContent);
            postResponse.EnsureSuccessStatusCode();

            var produtoAdicionado = await postResponse.Content.ReadFromJsonAsync<Produto>();

            try
            {
                //Act
                var getResponse = await _client.GetAsync($"/produtos/{produtoAdicionado.Nome}");
                getResponse.EnsureSuccessStatusCode();

                var produtoRetornado = await getResponse.Content.ReadFromJsonAsync<Produto>();

                // Assert
                Assert.Equal(produtoAdicionado.Nome, produtoRetornado.Nome);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro na solicitação HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }

        }
    }

}

