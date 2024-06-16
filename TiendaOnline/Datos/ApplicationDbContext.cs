using Microsoft.EntityFrameworkCore;
using TiendaOnline.Models;

namespace TiendaOnline.Datos
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Marca> Marcas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modelo>()
                .HasOne(p => p.Marca)
                .WithMany(b => b.Modelos)
                .HasForeignKey(p => p.MarcaIDMARCA);

            modelBuilder.Entity<Producto>()
                .HasOne(v => v.Modelo)
                .WithMany(m => m.Productos)
                .HasForeignKey(v => v.ModeloIDMODELO);

            // Datos semilla
            modelBuilder.Entity<Marca>().HasData(
                new Marca { IDMARCA = 1, NOM_MARCA = "Samsung" },
                new Marca { IDMARCA = 2, NOM_MARCA = "Apple" },
                new Marca { IDMARCA = 3, NOM_MARCA = "Xiaomi" }
            );

            modelBuilder.Entity<Modelo>().HasData(
                //SAMSUNG
                new Modelo { IdModelo  = 1, Nom_Modelo = "Galaxy S23 Ultra", MarcaIDMARCA = 1 },
                new Modelo { IdModelo  = 2, Nom_Modelo = " Galaxy Z Fold 4", MarcaIDMARCA = 1 },
                new Modelo { IdModelo  = 3, Nom_Modelo = "Galaxy Watch 5 Pro", MarcaIDMARCA = 1 },
                new Modelo { IdModelo = 4, Nom_Modelo = "Galaxy Buds 2 Pro", MarcaIDMARCA = 1 },
                new Modelo { IdModelo = 5, Nom_Modelo = "QN90A Neo QLED", MarcaIDMARCA = 1 },
                //APPLE
                new Modelo { IdModelo  = 6, Nom_Modelo = "iPhone 14 Pro Max", MarcaIDMARCA = 2 },
                new Modelo { IdModelo  = 7, Nom_Modelo = "Watch Series 8", MarcaIDMARCA = 2 },
                new Modelo { IdModelo  = 8, Nom_Modelo = "AirPods Pro", MarcaIDMARCA = 2 },
                new Modelo { IdModelo = 9, Nom_Modelo = "AirPods Max", MarcaIDMARCA = 2 },
                new Modelo { IdModelo = 10, Nom_Modelo = "iPhone SE", MarcaIDMARCA = 2 },
                //XIAOMI
                new Modelo { IdModelo  = 11, Nom_Modelo = "Xiaomi 13 Pro", MarcaIDMARCA = 3 },
                new Modelo { IdModelo  = 12, Nom_Modelo = "Redmi Note 12 Pro", MarcaIDMARCA = 3 },
                new Modelo { IdModelo = 13, Nom_Modelo = "True Wireless Earphones 2 Pro", MarcaIDMARCA = 3 },
                new Modelo { IdModelo = 14, Nom_Modelo = "TV Q1 75", MarcaIDMARCA = 3 },
                new Modelo { IdModelo  = 15, Nom_Modelo = "Redmi Buds 4 Pro", MarcaIDMARCA = 3 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
