using Microsoft.AspNetCore.Mvc;
using VirtualStore.VirtualStore.Domain.DTO;
using VirtualStore.VirtualStore.Domain.Interfaces;


namespace VirtualStore.VirtualStore.API.Controllers
{
    [Route("produtos")]
    [ApiController]

    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;   
        }

        [HttpPost]
        public async Task<ActionResult> AddProduto([FromBody] ProdutoDTO produtoDTO)
        {
            try
            {
                await _produtoService.AddProduto(produtoDTO);
                return Ok(produtoDTO); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] ProdutoDTO produtoDTO)
        {
            try
            {
                await _produtoService.UpdateProduto(id, produtoDTO);
                return NoContent();
            } catch (KeyNotFoundException)
            {
                return NotFound("Produto não encontrado");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                await _produtoService.DeleteProduto(id);
                return NoContent();
            } catch (KeyNotFoundException)
            {
                return NotFound("Produto não encontrado");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _produtoService.GetAllProdutos();
            return Ok(produtos);
        }


        [HttpGet("ordenar")]
        public async Task<IActionResult> GetProdutosOrdered([FromQuery] string campo, [FromQuery] bool asc = true)
        {
            try
            {
                var produtos = await _produtoService.GetProdutosOrderByField(campo, asc);
                return Ok(produtos);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno ao processar a solicitação");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProdutoById(int id)
        {

            try
            {
                var produto = await _produtoService.GetProdutoById(id);
                return Ok(produto);
            } catch (KeyNotFoundException)
            {
                return NotFound("Produto não encontrado");
            }
            
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> GetProdutoByName(string nome)
        {

            try
            {
                var produto = await _produtoService.GetProdutoByName(nome);
                return Ok(produto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Produto não encontrado");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno ao processar a solicitação");
            }

        }
    }
}
