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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Prenotation> Prenotations { get; set; }
        public DbSet<ProductPrenotation> ProductPrenotationCouples { get; set; }

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

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.ToTable("Cliente");
                entity.Property(c => c.Name)
                .HasColumnName("Nome")
                .IsRequired(false);
                entity.Property(c => c.Surname)
                .HasColumnName("Cognome")
                .IsRequired(false);
            });

            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.ToTable("Trattamento");
                entity.Property(c => c.Name)
                .HasColumnName("Nome")
                .IsRequired(false);
                entity.Property(c => c.Cost)
                .HasColumnName("Costo")
                .IsRequired(false);
                entity.Property(c => c.Duration)
                .HasColumnName("DurataInMinuti")
                .IsRequired(false);

            });

            modelBuilder.Entity<Prenotation>(entity =>
            { 
                entity.HasKey(p => p.Id);
                entity.ToTable("Prenotazione");
                entity.Property(p => p.DayAndHour)
                .IsRequired(false)
                .HasColumnName("DataEOra");
                entity.Property(p => p.CustomerId)
                .IsRequired(false)
                .HasColumnName("IdCliente");
                entity.Property(p => p.TreatmentId)
                .IsRequired(false)
                .HasColumnName("IdTrattamento");
                entity.HasOne(p => p.Customer)
                .WithMany(c => c.Prenotations)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(p => p.Treatment)
                .WithMany(c => c.Prenotations)
                .HasForeignKey(p => p.TreatmentId)
                .OnDelete(DeleteBehavior.Cascade);
            }
            );

            modelBuilder.Entity<ProductPrenotation>(entity =>
            {
                entity.HasKey(pp => new { pp.ProductId, pp.PrenotationId });
                entity.ToTable("ProdottoPrenotazione");
                entity.Property(p => p.ProductId)
                .HasColumnName("IdProdotto")
                .IsRequired();
                entity.Property(p => p.PrenotationId)
               .HasColumnName("IdPrenotazione")
               .IsRequired();
                entity.HasOne<Product>(pp => pp.Product)
                .WithMany(p => p.ProductPrenotationCouples)
                .HasForeignKey(pp => pp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<Prenotation>(pp => pp.Prenotation)
                .WithMany(p => p.ProductPrenotationCouples)
                .HasForeignKey(pp => pp.PrenotationId)
                .OnDelete(DeleteBehavior.Cascade); 
            });
        }
    }
}
