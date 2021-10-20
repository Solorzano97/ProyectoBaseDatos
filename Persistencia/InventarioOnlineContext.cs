using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistencia
{
    public class InventarioOnlineContext : IdentityDbContext<usuario>
    {
        public InventarioOnlineContext(DbContextOptions options) : base(options){ //puente para hacer las inyecciones

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
        }

          
        
        public DbSet<bodega> bodega{get;set;}
        public DbSet<categoria> categoria{get;set;}
        public DbSet<detalle> detalle{get;set;}
        public DbSet<empleado> empleado{get;set;}
        public DbSet<existencias> existencias{get;set;}
        public DbSet<factura> factura{get;set;}
        public DbSet<indexTemporada> indexTemporada{get;set;}
        public DbSet<metodoPago> metodoPago{get;set;}
        public DbSet<movimientos> movimientos{get;set;}
        public DbSet<producto> producto{get;set;}
        public DbSet<proveedor> proveedor{get;set;}
        public DbSet<rol> rol{get;set;}
        public DbSet<temporada> temporada{get;set;}
        public DbSet<tipoMovimiento> tipoMovimiento{get;set;}
        public DbSet<cliente> cliente{get;set;}
    }
}