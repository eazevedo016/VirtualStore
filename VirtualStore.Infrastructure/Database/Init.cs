using VirtualStore.VirtualStore.Domain.Entities;
using VirtualStore.VirtualStore.Infrastructure.Context;


namespace VirtualStore.VirtualStore.Infrastructure.Data
{
    public class DbInit
    {
        public static void Init(ProdutoContext context)
        {
            context.Database.EnsureCreated();

            if (context.Produto.Any())
            {
                return;
            }

            var produtos = new Produto[]
            {
                new Produto { Nome = "SSD", Estoque = 20, Valor = 800 },
                new Produto { Nome = "Placa de Vídeo", Estoque = 2, Valor = 2600 },
                new Produto { Nome = "Memória RAM", Estoque = 15, Valor = 1500 },
                new Produto { Nome = "Fonte de Energia", Estoque = 5, Valor = 1000 },
                new Produto { Nome = "Processador AMD Ryzen 7", Estoque = 10, Valor = 2500 }
            };

            context.Produto.AddRange(produtos);
            context.SaveChanges();
        }
    }
}