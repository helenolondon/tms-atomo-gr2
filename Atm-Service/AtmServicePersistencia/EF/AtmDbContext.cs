using AtmServiceInfra.ODP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace AtmServiceInfra.EF
{
    public class AtmDbContext: DbContext
    {
        private readonly string connectionString;

        public DbSet<ArquivoODP> Arquivos { get; set; }
        public DbSet<ContatoODP> Contatos { get; set; }
        public DbSet<LoginODP> Logins { get; set; }
        public DbSet<LoginInfoODP> LoginInfo { get; set; }
        public DbSet<SequenciaODP> SequenciaInfo { get; set; }
        public DbSet<UsuarioODP> Usuarios { get; set; }
        public DbSet<PaisODP> Paises { get; set; }
        public DbSet<LogradouroODP> Logradouros { get; set; }
        public DbSet<BairroODP> Bairros { get; set; }
        public DbSet<CidadeODP> Cidades { get; set; }
        public DbSet<EstadoODP> Estados { get; set; }
        public DbSet<CnaeODP> Cnaes { get; set; }
        public DbSet<ClienteODP> Clientes { get; set; }
        public DbSet<ClientePrecoTarefaODP> ClientesPrecosTarefas { get; set; }
        public DbSet<EmpresaODP> Empresas { get; set; }
        public DbSet<MarcaODP> Marcas { get; set; }
        public DbSet<ProdutoTipoAgressividadeODP> ProdutoTipoAgressividades { get; set; }
        public DbSet<ProdutoTipoODP> ProdutoTipos { get; set; }
        public DbSet<UnidadeDeMedidaODP> UnidadesDeMedida { get; set; }
        public DbSet<TipoVeiculoODP> TipoVeiculos { get; set; }
        public DbSet<ProdutoGrupoODP> ProdutoGrupos { get; set; }
        public DbSet<ProdutoODP> Produtos { get; set; }
        public DbSet<ServicoCategoriaODP> ServicoCategorias { get; set; }

        public AtmDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseMySql(connectionString);
            }

            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginODP>()
                .HasOne<UsuarioODP>(l => l.UsuarioAlteracao)
                .WithMany()
                .HasForeignKey(l => l.UsuarioAlt)
                .HasPrincipalKey(u => u.Login);
            
            modelBuilder.Entity<LoginODP>()
                .HasOne<UsuarioODP>(l => l.UsuarioCadastro)
                .WithMany()
                .HasForeignKey(l => l.UsuarioCad)
                .HasPrincipalKey(u => u.Login);

            modelBuilder.Entity<ProdutoODP>()
                .HasOne(p => p.UnidadeDeMedida)
                .WithMany()
                .HasForeignKey(p => p.CodUnidadeDeMedida);

            modelBuilder.Entity<ProdutoODP>()
                .HasOne(p => p.ProdutoTipo)
                .WithMany()
                .HasForeignKey(p => p.CodProdutoTipo);

            modelBuilder.Entity<ProdutoODP>()
                .HasOne(p => p.Marca)
                .WithMany()
                .HasForeignKey(p => p.CodMarca);

            modelBuilder.Entity<ProdutoODP>()
                .HasOne(p => p.ProdutoGrupo)
                .WithMany()
                .HasForeignKey(p => p.CodProdutoGrupo);
        }
    }
}
