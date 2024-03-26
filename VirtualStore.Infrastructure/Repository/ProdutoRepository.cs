using Microsoft.EntityFrameworkCore;
using VirtualStore.VirtualStore.Domain.Entities;
using VirtualStore.VirtualStore.Domain.Interfaces;
using VirtualStore.VirtualStore.Infrastructure.Context;

namespace VirtualStore.VirtualStore.Infrastructure.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoContext _context;
        public ProdutoRepository(ProdutoContext context)
        {
            _context = context;
        }
        public async Task Add(Produto produto)
        {
            try
            {
                await _context.Produto.AddAsync(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar o produto.", ex);
            }
        }

        public async Task Update(Produto produto)
        {
            try
            {
                _context.Produto.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException knfd)
            {
                throw new KeyNotFoundException("Erro ao atualizar o produto.", knfd);
            }
        }

        public async Task Delete(Produto produto)
        {

            try
            {
                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException knfd)
            {
                throw new KeyNotFoundException("Erro ao excluir o produto.", knfd);
            }
        }

        public async Task<List<Produto>> GetAllProdutos()
        {
            var produto = await _context.Produto.ToListAsync();
            if (produto == null)
                throw new KeyNotFoundException("Produto não encontrado.");

            return produto;

        }

        public async Task<List<Produto>> GetByField(string campo, bool ascendente)
        {
            IQueryable<Produto> query = _context.Produto;

            switch (campo.ToLower())
            {
                case "nome":
                    query = ascendente ? query.OrderBy(p => p.Nome) : query.OrderByDescending(p => p.Nome);
                    break;
                case "estoque":
                    query = ascendente ? query.OrderBy(p => p.Estoque) : query.OrderByDescending(p => p.Estoque);
                    break;
                case "valor":
                    query = ascendente ? query.OrderBy(p => p.Valor) : query.OrderByDescending(p => p.Valor);
                    break;
                default:
                    throw new ArgumentException("Campo de ordenação inválido.");
            }

            return await query.ToListAsync();
        }


        public async Task<Produto> GetById(int id)
        {

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
                throw new KeyNotFoundException("Produto não encontrado.");

            return produto;

        }

        public async Task<Produto> GetByName(string nome)
        {
            var produto = await _context.Produto.FirstOrDefaultAsync(p => p.Nome == nome);
            if (produto == null)
                throw new KeyNotFoundException("Produto não encontrado.");

            return produto;
        }
    }
}
