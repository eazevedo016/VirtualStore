using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    options.UseSqlServer("Server=SPWBRJ142QM0S;Database=VirtualStoreDB;Integrated Security=True;TrustServerCertificate=True;");
                });
            });
            builder.UseEnvironment("Development");
        }
    }
}
