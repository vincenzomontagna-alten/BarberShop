// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using DBLayer.Models;
using DBLayer.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;
using esercizio_barber_shop;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using ServiceLayer;
using DBLayer.Interfaces;
using DBLayer.Repositories;

namespace esercizio_barber_shop

{
    class Program
    { 
        public static void Main(string[] args)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //crei la configurazione creando prima un configuration builder,
            //ovvero un oggetto che crea la configurazione dopo che lanci il metodo build()
            var builder = new ConfigurationBuilder()
                .AddJsonFile(System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\appsettings.json"));

            IConfiguration config = builder.Build();
            //creazione del services provider. basta creare un oggetto services collectio,
            // registrare i servizi e chiamare su quell' oggetto il metodo buildservicesprovider.
            // da quel momento puoi passare le dipendenze tra i servizi registrati all' interno dei costruttori
            var services = new ServiceCollection();
            ConfigureServices(services, config);
            services
                .AddSingleton<Start>()
                .BuildServiceProvider()
                .GetService<Start>()
                .Execute();

        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IConfiguration>(config);
            services.AddDbContext<Context>();
            services.AddSingleton<ProductService>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<ProductAdoRepository>();
            services.AddSingleton<ProductAdoService>();
                
        }
    }
}

