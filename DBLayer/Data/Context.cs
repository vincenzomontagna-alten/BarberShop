using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DBLayer.Data
{

    public class Context : DbContext
    {
        public DbSet<Product> Products { get; set; }

        private IConfiguration _configuration;

        public Context(DbContextOptions<Context> options, IConfiguration config) : base(options)
        {
            _configuration = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("SqlServerConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.ToTable("prodotto");
                entity.Property(p => p.Nome)
                .HasColumnName("Nome");
                entity.Property(p => p.Quantita)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Quantita");
                entity.Property(p => p.QuantitaMinimaRichiesta)
                .IsRequired(false)
                .HasColumnName("QuantitaMinimaRichiesta");
               
            });

           
        }
    }
}
