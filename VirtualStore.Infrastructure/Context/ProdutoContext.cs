using Microsoft.EntityFrameworkCore;
using VirtualStore.VirtualStore.Domain.Entities;

namespace VirtualStore.VirtualStore.Infrastructure.Context
{
    public class ProdutoContext : DbContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options)
        {

        }

        public ProdutoContext()
        {
         
        }
        public DbSet<Produto> Produto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            
        }


    }
}
