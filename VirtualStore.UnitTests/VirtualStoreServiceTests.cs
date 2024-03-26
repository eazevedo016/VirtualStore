using Moq;
using VirtualStore.VirtualStore.Domain.DTO;
using VirtualStore.VirtualStore.Domain.Entities;
using VirtualStore.VirtualStore.Domain.Interfaces;
using VirtualStore.VirtualStore.Domain.Services;


namespace VirtualStore.UnitTests
{
    public class VirtualStoreServiceTests
    {
        private Mock<IProdutoRepository> _mockProdutoRepository;
        private ProdutoService _produtoService;



        /* métodos a serem testados
         
        Task AddProduto(ProdutoDTO produtoDTO);
        Task UpdateProduto(int id, ProdutoDTO produtoDTO);
        Task DeleteProduto(int id);
        Task<List<ProdutoDTO>> GetAllProdutos();
        Task<ProdutoDTO> GetProdutoById(int id);
        Task<ProdutoDTO> GetProdutoByName(string nome);
        
        */

        public VirtualStoreServiceTests()
        {
            _mockProdutoRepository = new Mock<IProdutoRepository>();
            _produtoService = new ProdutoService(_mockProdutoRepository.Object);
        }

        [Fact]
        public async Task AddProduto_WithValidProdutoDTO_CallsRepositoryAdd()
        {
            // Arrange
            var produtoDTO = new ProdutoDTO
            {
                Nome = "Produto Teste para o AddProduto Test",
                Estoque = 10,
                Valor = 30
            };

            // Act
            await _produtoService.AddProduto(produtoDTO);

            // Assert
            _mockProdutoRepository.Verify(pr => pr.Add(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProduto_WithValidProdutoDTO_CallsRepositoryUpdate()
        {
            // Arrange
            var produtoDTO = new ProdutoDTO
            {
                Id = 1, 
                Nome = "Produto Teste1 para o UpdateProduto Test",
                Estoque = 5,
                Valor = 40.50
            };

            var produtoExistente = new Produto
            {
                Id = produtoDTO.Id,
                Nome = "Produto2 Teste1 para o UpdateProduto Test",
                Estoque = 6,
                Valor = 54.50
            };

            // Act
            _mockProdutoRepository.Setup(pr => pr.GetById(produtoDTO.Id)).ReturnsAsync(produtoExistente);
            await _produtoService.UpdateProduto(produtoDTO.Id, produtoDTO);

            // Assert
            _mockProdutoRepository.Verify(pr => pr.Update(It.IsAny<Produto>()), Times.Once);
        }



        [Fact]
        public async Task GetAllProdutos_ReturnsListOfProdutoDTO()
        {
            // Arrange
            var produtosMock = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Mouse", Estoque = 2, Valor = 250 },
                new Produto { Id = 2, Nome = "Mousepad", Estoque = 3, Valor = 100 },
                new Produto { Id = 3, Nome = "Teclado", Estoque = 1, Valor = 350 }
            };

            _mockProdutoRepository.Setup(pr => pr.GetAllProdutos()).ReturnsAsync(produtosMock);

            // Act
            var produtosDTO = await _produtoService.GetAllProdutos();

            // Assert
            Assert.NotNull(produtosDTO);
            Assert.IsType<List<ProdutoDTO>>(produtosDTO);
            Assert.Equal(produtosMock.Count, produtosDTO.Count);

            for (int i = 0; i < produtosMock.Count; i++)
            {
                Assert.Equal(produtosMock[i].Id, produtosDTO[i].Id);
                Assert.Equal(produtosMock[i].Nome, produtosDTO[i].Nome);
                Assert.Equal(produtosMock[i].Estoque, produtosDTO[i].Estoque);
                Assert.Equal(produtosMock[i].Valor, produtosDTO[i].Valor);
            }
        }

        [Fact]
        public async Task GetProdutoById_WithValidId_ReturnsProdutoDTO()
        {
            // Arrange
            var produtoMock = new Produto
            {
                Id = 1,
                Nome = "Produto Teste para o GetById Test",
                Estoque = 1,
                Valor = 10
            };

            _mockProdutoRepository.Setup(pr => pr.GetById(produtoMock.Id)).ReturnsAsync(produtoMock);

            // Act
            var produtoDTO = await _produtoService.GetProdutoById(produtoMock.Id);

            // Assert
            Assert.NotNull(produtoDTO);
            Assert.IsType<ProdutoDTO>(produtoDTO);
            Assert.Equal(produtoMock.Id, produtoDTO.Id);
            Assert.Equal(produtoMock.Nome, produtoDTO.Nome);
            Assert.Equal(produtoMock.Estoque, produtoDTO.Estoque);
            Assert.Equal(produtoMock.Valor, produtoDTO.Valor);
        }

        [Fact]
        public async Task GetProdutoByName_WithValidName_ReturnsProdutoDTO()
        {
            // Arrange
            var produtoMock = new Produto
            {
                Id = 1,
                Nome = "Produto Teste para o GetByName Test",
                Estoque = 2,
                Valor = 15
            };

            _mockProdutoRepository.Setup(pr => pr.GetByName(produtoMock.Nome)).ReturnsAsync(produtoMock);

            // Act
            var produtoDTO = await _produtoService.GetProdutoByName(produtoMock.Nome);

            // Assert
            Assert.NotNull(produtoDTO);
            Assert.IsType<ProdutoDTO>(produtoDTO);
            Assert.Equal(produtoMock.Id, produtoDTO.Id);
            Assert.Equal(produtoMock.Nome, produtoDTO.Nome);
            Assert.Equal(produtoMock.Estoque, produtoDTO.Estoque);
            Assert.Equal(produtoMock.Valor, produtoDTO.Valor);
        }


        [Fact]
        public void AddProduto_WithNegativeValue_ThrowsArgumentException()
        {
            // Arrange
            var produtoDTO = new ProdutoDTO
            {
                Nome = "Produto Teste",
                Estoque = 10,
                Valor = -50
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => _produtoService.AddProduto(produtoDTO));
        }



       

    }
}