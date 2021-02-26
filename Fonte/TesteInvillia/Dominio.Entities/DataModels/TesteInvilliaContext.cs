using DTO.Ferramentas;
using Microsoft.EntityFrameworkCore;

namespace Dominio.Entities.DataModels
{
    public partial class TesteInvilliaContext : DbContext
    {
        public TesteInvilliaContext()
        {
        }

        public TesteInvilliaContext(DbContextOptions<TesteInvilliaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Jogo> Jogo { get; set; }
        public virtual DbSet<LogErro> LogErro { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<VinculoUsuarioRole> VinculoUsuarioRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfiguracaoString.Conexao["DefaultConnection"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogo>(entity =>
            {
                entity.Property(e => e.Nome).IsRequired();

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Jogo)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Jogo_Usuario");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Descricao).IsRequired();

                entity.Property(e => e.NomeRole).IsRequired();
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Nome).IsRequired();

                entity.Property(e => e.Senha).IsRequired();

                entity.HasOne(d => d.IdCriadoUsuarioNavigation)
                    .WithMany(p => p.InverseIdCriadoUsuarioNavigation)
                    .HasForeignKey(d => d.IdCriadoUsuario)
                    .HasConstraintName("FK_Usuario_Usuario");
            });

            modelBuilder.Entity<VinculoUsuarioRole>(entity =>
            {
                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.VinculoUsuarioRole)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VinculoUsuarioRole_Role");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.VinculoUsuarioRole)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VinculoUsuarioRole_Usuario");
            });
        }
    }
}
