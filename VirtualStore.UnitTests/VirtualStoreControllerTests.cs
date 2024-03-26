using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VirtualStore.VirtualStore.API.Controllers;
using VirtualStore.VirtualStore.Domain.DTO;
using VirtualStore.VirtualStore.Domain.Entities;
using VirtualStore.VirtualStore.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VirtualStore.VirtualStore.API.Tests
{
    
    public class ProdutoControllerTests
    {
        private Mock<IProdutoService> _produtoServiceMock;
        private ProdutoController _produtoController;

       
        public ProdutoControllerTests()
        {
            _produtoServiceMock = new Mock<IProdutoService>();
            _produtoController = new ProdutoController(_produtoServiceMock.Object);
        }

        [Fact]
        public async Task AddProduto_ValidProduto_ReturnsOkResult()
        {
            // Arrange
            var produtoDTO = new ProdutoDTO
            {
                Nome = "Criação de novo produto para atender ao teste unitário AddProduto",
                Estoque = 4,
                Valor = 20
            };

            // Act
            var result = await _produtoController.AddProduto(produtoDTO) as  OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtoDTO, result.Value);
        }


       
        public async Task AddProduto_NegativeValue_ReturnsOkResult()
        {
            // Arrange
            var produtoDTO = new ProdutoDTO
            {
                Nome = "Criação de novo produto para atender ao teste unitário AddProduto",
                Estoque = 4,
                Valor = -20
            };

            // Act
            var result = await _produtoController.AddProduto(produtoDTO) as BadRequestObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result?.Value);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            // Act
            var result = await _produtoController.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task GetProdutoById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var id = 50;

            // Act
            var result = await _produtoController.GetProdutoById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task GetProdutoByName_ExistingName_ReturnsOkResult()
        {

            // Arrange
            var produtoDTO = new ProdutoDTO
            {
                Nome = "Criação de novo produto para atender ao teste  GetProdutoByName",
                Estoque = 4,
                Valor = 20
            };
            await _produtoController.AddProduto(produtoDTO);


            // Act
            var resultGetByNome = await _produtoController.GetProdutoByName(produtoDTO.Nome);

            // Assert
            Assert.NotNull(resultGetByNome);
            Assert.IsType<OkObjectResult>(resultGetByNome);

        }

        [Fact]
        public async Task GetProdutoByName_NameNotExists_ReturnsOkResult()
        {

            // Arrange
            var produtoDTO = new ProdutoDTO
            {
                Nome = "Este nome nao existe no banco de dados",
                Estoque = 4,
                Valor = 20
            };
            
            // Act
            var resultGetByNome = await _produtoController.GetProdutoByName(produtoDTO.Nome);

            // Assert
            Assert.NotNull(resultGetByNome);
            Assert.IsType<OkObjectResult>(resultGetByNome);

        }
    }
}
