using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace TP_02.Models
{
    public class LogisticaContext : DbContext
    {
        public LogisticaContext(DbContextOptions<LogisticaContext> options)
            : base(options)
        {
        }

        // DbSets para as entidades
        public DbSet<BL> BLs { get; set; }
        public DbSet<Container> Containers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da relação BL 1:N Container
            modelBuilder.Entity<Container>()
                .HasOne(c => c.BL)
                .WithMany(b => b.Containers)
                .HasForeignKey(c => c.BL_ID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurações adicionais (opcional)
            modelBuilder.Entity<BL>()
                .Property(b => b.Numero)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Container>()
                .Property(c => c.Numero)
                .IsRequired()
                .HasMaxLength(11);

            modelBuilder.Entity<Container>()
                .Property(c => c.Tipo)
                .IsRequired();

            modelBuilder.Entity<Container>()
                .Property(c => c.Tamanho)
                .IsRequired();
        }
    }
}
