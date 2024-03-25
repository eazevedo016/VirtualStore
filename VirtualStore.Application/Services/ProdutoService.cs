using VirtualStore.VirtualStore.Domain.DTO;
using VirtualStore.VirtualStore.Domain.Entities;
using VirtualStore.VirtualStore.Domain.Interfaces;
using VirtualStore.VirtualStore.Infrastructure.Repository;

namespace VirtualStore.VirtualStore.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;


        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task AddProduto(ProdutoDTO produtoDTO)
        {
            if (produtoDTO.Valor < 0)
            {
                throw new ArgumentException("O valor do produto não pode ser negativo.", nameof(produtoDTO.Valor));
            }

            var produto = new Produto
            {
                Nome = produtoDTO.Nome,
                Estoque = produtoDTO.Estoque,
                Valor = produtoDTO.Valor
            };

            await _produtoRepository.Add(produto);
        }

        public async Task UpdateProduto(int id, ProdutoDTO produtoDTO)
        {
            var produto = await _produtoRepository.GetById(id);


            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            if (produtoDTO.Valor < 0)
            {
                throw new ArgumentException("O valor do produto não pode ser negativo.", nameof(produtoDTO.Valor));
            }

            produto.Nome = produtoDTO.Nome;
            produto.Estoque = produtoDTO.Estoque;
            produto.Valor = produtoDTO.Valor;

            await _produtoRepository.Update(produto);
        }

        public async Task DeleteProduto(int id)
        {
            var produto = await _produtoRepository.GetById(id);
            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado");
            }

            await _produtoRepository.Delete(produto);
        }

        public async Task<List<ProdutoDTO>> GetAllProdutos()
        {
            var produtos = await _produtoRepository.GetAllProdutos();
            return produtos.Select(p => new ProdutoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Estoque = p.Estoque,
                Valor = p.Valor
            }).ToList();
        }

        public async Task<ProdutoDTO> GetProdutoById(int id)
        {
            var produto = await _produtoRepository.GetById(id);
            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Estoque = produto.Estoque,
                Valor = produto.Valor
            };
        }

        public async Task<ProdutoDTO> GetProdutoByName(string nome)
        {
            var produto = await _produtoRepository.GetByName(nome);
            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Estoque = produto.Estoque,
                Valor = produto.Valor
            };
        }


    }
}
