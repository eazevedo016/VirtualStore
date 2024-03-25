using VirtualStore.VirtualStore.Domain.Entities;

namespace VirtualStore.VirtualStore.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task Add(Produto produto);
        Task Delete(Produto produto);
        Task Update(Produto produto);
        Task<List<Produto>> GetAllProdutos();
        Task<Produto> GetById(int id);
        Task<Produto> GetByName(string nome);
    }
}
