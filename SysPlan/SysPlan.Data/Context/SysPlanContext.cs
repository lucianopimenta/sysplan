using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SysPlan.Core.Interface;
using SysPlan.Data.Entities;

namespace SysPlan.Data.Context
{
    public class SysPlanContext: DbContext, IDbContext
    {
        public SysPlanContext() { }

        public SysPlanContext(DbContextOptions<SysPlanContext> options) : base(options)
        {
            Database.SetCommandTimeout(300);
        }

        public virtual DbSet<Filme> Filme { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filme>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pk_filme");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Catalogo)
                    .HasMaxLength(100)
                    .IsRequired()
                    .HasColumnName("catalogo");

                entity.Property(e => e.Nome)
                   .HasMaxLength(50)
                   .IsRequired()
                   .HasColumnName("nome");

                entity.Property(e => e.Ano)
                   .IsRequired()
                   .HasColumnName("ano");

                entity.Property(e => e.Imagem)
                   .IsRequired()
                   .HasColumnName("imagem");

                entity.Property(e => e.Slogan)
                   .HasMaxLength(500)
                   .IsRequired()
                   .HasColumnName("slogan");

                entity.Property(e => e.VisaoGeral)
                   .IsRequired()
                   .HasColumnName("visao_geral");

                entity.Property(e => e.Classificacao)
                   .IsRequired()
                   .HasColumnName("classificacao");

                entity.Property(e => e.DataCriacao)
                    .IsRequired()
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("data_criacao");

                entity.Property(e => e.DataAtualizacao)
                    .HasColumnName("data_atualizacao");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                   .HasName("pk_usuario");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Login)
                    .HasMaxLength(100)
                    .IsRequired()
                    .HasColumnName("login");

                entity.Property(e => e.Senha)
                   .HasMaxLength(50)
                   .IsRequired()
                   .HasColumnName("senha");

                entity.Property(e => e.Ativo)
                   .IsRequired()
                   .HasColumnName("ativo");

                entity.Property(e => e.DataCriacao)
                    .IsRequired()
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("data_criacao");

                entity.Property(e => e.DataAtualizacao)
                    .HasColumnName("data_atualizacao");

            });
        }

        #region Transação
            public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

        public void Commit()
        {
            Database.CommitTransaction();
        }

        public DbContext Ctx()
        {
            return this;
        }

        public void Rollback()
        {
            Database.RollbackTransaction();
        }
        #endregion
    }
}
