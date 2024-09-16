using Entities;
using System.Data.Entity;

namespace Data
{
    public class ContextDb : DbContext
    {
        public ContextDb() : base("name=MyDatabase")
        {
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Areas> Areas { get; set; }
        public DbSet<AreaUsuarios> AreaUsuarios { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaUsuarios>()
            .HasKey(au => new { au.IdArea, au.IdUsuario });

            modelBuilder.Entity<AreaUsuarios>()
           .HasIndex(au => au.IdUsuario)
           .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
