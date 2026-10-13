using AtmServiceInfra.Arquivos;
using AtmServiceInfra.Bairros;
using AtmServiceInfra.Cidades;
using AtmServiceInfra.Clientes;
using AtmServiceInfra.Clientes.PrecosTarefas;
using AtmServiceInfra.Cnaes;
using AtmServiceInfra.Contatos;
using AtmServiceInfra.EF;
using AtmServiceInfra.Empresas;
using AtmServiceInfra.Erros;
using AtmServiceInfra.Estados;
using AtmServiceInfra.Logradouros;
using AtmServiceInfra.Marcas;
using AtmServiceInfra.ProdutoTipos;
using AtmServiceInfra.Seguranca;
using AtmServiceInfra.TipoVeiculos;
using AtmServiceInfra.UnidadesDeMedida;
using Microsoft.Extensions.Configuration;

namespace AtmServiceInfra
{
    public class Repositorios : IRepositorios
    {
        private readonly IConfiguration configuration;
        private AtmDbContext _context;

        private IArquivosRepositorio arquivosRepositorio;
        private IContatosRepositorio contatosRep;
        private ISegurancaRepositorio segurancaRep;
        private IUsuariosRepositorio usuariosRep;
        private ILoginInfoRepositorio loginInfoRep;
        private ILogradourosRepositorio logradourosRepositorio;
        private ICidadesRepositorio cidadesRepositorio;
        private IBairrosRepositorio bairrosRepositorio;
        private IEstadosRepositorio estadosRepositorio;
        private ICnaesRepositorio cnaesRepositorio;
        private IClientesRepositorio clientesRepositorio;
        private IEmpresasRepositorio empresasRepositorio;
        private IMarcasRepositorio marcasRepositorio;
        private AtmServiceInfra.ProdutoTipoAgressividades.IProdutoTipoAgressividadeRepositorio produtoTipoAgressividadesRepositorio;
        private IProdutoTiposRepositorio produtoTiposRepositorio;
        private IUnidadesDeMedidaRepositorio unidadesDeMedidaRepositorio;
        private ITipoVeiculosRepositorio tipoVeiculosRepositorio;

        public Repositorios(IConfiguration configuration)
        {
            this.configuration = configuration;

            CriaDbContext();
        }

        private void CriaDbContext()
        {
            var connstr = this.configuration.GetConnectionString("atm-connection-str");
            
            if (string.IsNullOrEmpty(connstr))
            {
                throw new PersistenciaExcecao("Connection String não foi configurada");
            }

            this._context = new AtmDbContext(connstr);
        }

        public IArquivosRepositorio ArquivosRepositorio
        {
            get
            {
                if (this.arquivosRepositorio == null)
                {
                    this.arquivosRepositorio = new ArquivosRepositorio(_context, this.configuration.GetValue<string>("uploadedFilesFolder"));
                }

                return this.arquivosRepositorio;
            }
        }

        public IContatosRepositorio ContatosRep
        {
            get
            {
                if (this.contatosRep == null)
                {
                    this.contatosRep = new ContatosRepositorio(_context);
                }

                return this.contatosRep;
            }
        }

        public ISegurancaRepositorio SegurancaRep
        {
            get
            {
                if (this.segurancaRep == null)
                {
                    this.segurancaRep = new SegurancaRepositorio(_context);
                }

                return this.segurancaRep;
            }            
        }

        public IUsuariosRepositorio UsuariosRep
        {
            get
            {
                if (this.usuariosRep == null)
                {
                    this.usuariosRep = new UsuariosRepositorio(_context);
                }

                return this.usuariosRep;
            }
        }

        public ILoginInfoRepositorio LoginInfoRep
        {
            get
            {
                if (this.loginInfoRep == null)
                {
                    this.loginInfoRep = new LoginInfoRepositorio(_context);
                }

                return this.loginInfoRep;
            }
        }

        public ILogradourosRepositorio LogradourosRep
        {
            get
            {
                if (this.logradourosRepositorio == null)
                {
                    this.logradourosRepositorio = new LogradourosRepositorio(_context);
                }

                return this.logradourosRepositorio;
            }
        }

        public ICidadesRepositorio CidadesRepositorio
        {
            get
            {
                if (this.cidadesRepositorio == null)
                {
                    this.cidadesRepositorio = new CidadesRepositorio(_context);
                }

                return this.cidadesRepositorio;
            }
        }

        public IBairrosRepositorio BairrosRepositorio
        {
            get
            {
                if (this.bairrosRepositorio == null)
                {
                    this.bairrosRepositorio = new BairrosRepositorio(_context);
                }

                return this.bairrosRepositorio;
            }
        }

        public IEstadosRepositorio EstadosRepositorio
        {
            get
            {
                if (this.estadosRepositorio == null)
                {
                    this.estadosRepositorio = new EstadosRepositorio(_context);
                }

                return this.estadosRepositorio;
            }
        }

        public ICnaesRepositorio CnaesRepositorio
        {
            get
            {
                if (this.cnaesRepositorio == null)
                {
                    this.cnaesRepositorio = new CnaesRepositorio(_context);
                }

                return this.cnaesRepositorio;
            }
        }

        public IClientesRepositorio ClientesRepositorio
        {
            get
            {
                if (this.clientesRepositorio == null)
                {
                    this.clientesRepositorio = new ClientesRepositorio(_context);
                }

                return this.clientesRepositorio;
            }
        }

        public IEmpresasRepositorio EmpresasRepositorio
        {
            get
            {
                if (this.empresasRepositorio == null)
                {
                    this.empresasRepositorio = new EmpresasRepositorio(_context);
                }

                return this.empresasRepositorio;
            }
        }

        public IMarcasRepositorio MarcasRepositorio
        {
            get
            {
                if (this.marcasRepositorio == null)
                {
                    this.marcasRepositorio = new MarcasRepositorio(_context);
                }

                return this.marcasRepositorio;
            }
        }

        public AtmServiceInfra.ProdutoTipoAgressividades.IProdutoTipoAgressividadeRepositorio ProdutoTipoAgressividadesRepositorio
        {
            get
            {
                if (this.produtoTipoAgressividadesRepositorio == null)
                {
                    this.produtoTipoAgressividadesRepositorio = new AtmServiceInfra.ProdutoTipoAgressividades.ProdutoTipoAgressividadeRepositorio(_context);
                }

                return this.produtoTipoAgressividadesRepositorio;
            }
        }

        public IProdutoTiposRepositorio ProdutoTiposRepositorio
        {
            get
            {
                if (this.produtoTiposRepositorio == null)
                {
                    this.produtoTiposRepositorio = new ProdutoTiposRepositorio(_context);
                }

                return this.produtoTiposRepositorio;
            }
        }

        public IUnidadesDeMedidaRepositorio UnidadesDeMedidaRepositorio
        {
            get
            {
                if (this.unidadesDeMedidaRepositorio == null)
                {
                    this.unidadesDeMedidaRepositorio = new UnidadesDeMedidaRepositorio(_context);
                }

                return this.unidadesDeMedidaRepositorio;
            }
        }

        public ITipoVeiculosRepositorio TipoVeiculosRepositorio
        {
            get
            {
                if (this.tipoVeiculosRepositorio == null)
                {
                    this.tipoVeiculosRepositorio = new TipoVeiculosRepositorio(_context);
                }

                return this.tipoVeiculosRepositorio;
            }
        }

        /// <summary>
        /// Salva as alterações feitas em todos os repositórios
        /// </summary>
        public void Salvar()
        {
            this._context.SaveChanges();
        }
    }
}
