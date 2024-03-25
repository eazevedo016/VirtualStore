using VirtualStore.VirtualStore.Domain.DTO;

namespace VirtualStore.VirtualStore.Domain.Interfaces
{
    public interface IProdutoService
    {
        Task AddProduto(ProdutoDTO produtoDTO);
        Task UpdateProduto(int id, ProdutoDTO produtoDTO);
        Task DeleteProduto(int id);
        Task<List<ProdutoDTO>> GetAllProdutos();
        Task<ProdutoDTO> GetProdutoById(int id);
        Task<ProdutoDTO> GetProdutoByName(string nome);
    }
}
