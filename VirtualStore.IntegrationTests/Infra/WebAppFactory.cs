using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using VirtualStore.VirtualStore.Infrastructure.Context;

namespace VirtualStore.IntegrationTests.Infra
{
    public class WebAppFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ProdutoContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);
                services.AddDbContext<ProdutoContext>((container, options) =>
                {
                    options.UseNpgsql("Host=localhost;Database=VirtualStoreDB;Username=postgres;Password=admin;CommandTimeout=120;");
                });
            });
            builder.UseEnvironment("Development");
        }
    }
}
