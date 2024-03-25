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
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o produto.", ex);
            }
        }

        public async Task Delete(Produto produto)
        {
            try
            {
                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir o produto.", ex);
            }
        }

        public async Task<List<Produto>> GetAllProdutos()
        {
            try
            {
                return await _context.Produto.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter todos os produtos.", ex);
            }
        }

        public async Task<Produto> GetById(int id)
        {
            try
            {
                return await _context.Produto.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter o produto pelo ID.", ex);
            }
        }

        public async Task<Produto> GetByName(string nome)
        {
            try
            {
                return await _context.Produto.FirstOrDefaultAsync(p => p.Nome == nome);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter o produto pelo nome.", ex);
            }
        }

    }
}
