using AtmService.Arquivos;
using AtmService.Clientes;
using AtmService.Contatos;
using AtmService.Empresas;
using AtmService.Enderecos;
using AtmService.Fiscal;
using AtmService.Marcas;
using AtmService.ProdutoTipoAgressividades;
using AtmService.ProdutoTipos;
using AtmService.Produtos;
using AtmService.ProdutoGrupos;
using AtmService.ServicoCategorias;
using AtmService.Seguranca;
using AtmService.TipoVeiculos;
using AtmService.UnidadesDeMedida;
using AtmService.Usuarios;
using AtmServiceDominio;
using Microsoft.Extensions.Configuration;

namespace AtmService
{
    public class AtmServices : IAtmServices
    {
        private readonly IConfiguration configuration;

        Dominios dom;

        private IArquivosService arquivosService;
        private IContatosService contatosService;
        private ISegurancaService segurancaService;
        private IUsuariosService usuariosService;
        private IEnderecosService enderecosService;
        private IFiscalService fiscalService;
        private IClientesService clientesService;
        private IEmpresasService empresasService;
        private IMarcasService marcasService;
        private IProdutoTipoAgressividadesService produtoTipoAgressividadesService;
        private IProdutoTiposService produtoTiposService;
        private IUnidadesDeMedidaService unidadesDeMedidaService;
        private ITipoVeiculosService tipoVeiculosService;
        private IProdutosService produtosService;
        private IProdutoGruposService produtoGruposService;
        private IServicoCategoriasService servicoCategoriasService;

        public AtmServices(IConfiguration configuration)
        {
            this.configuration = configuration;

            CriarDominios();
        }

        private void CriarDominios()
        {
            this.dom =  new Dominios(configuration);
        }

        public void Commit()
        {
            dom.Salvar();
        }

        public IContatosService ContatosService
        {
            get
            {
                if (this.contatosService == null)
                {
                    this.contatosService = new ContatosService(dom.ContatosOD);
                }

                return this.contatosService;
            }
        }

        public IArquivosService ArquivosService
        {
            get
            {
                if (this.arquivosService == null)
                {
                    this.arquivosService = new ArquivosService(dom.ArquivosOD);
                }

                return this.arquivosService;
            }
        }

        public ISegurancaService SegurancaService
        {
            get
            {
                if (this.segurancaService == null)
                {
                    this.segurancaService = new SegurancaService(dom.SegurancaOD);
                }

                return this.segurancaService;
            }
        }

        public IUsuariosService UsuariosService
        {
            get
            {
                if (this.usuariosService == null)
                {
                    this.usuariosService = new UsuariosService(dom.UsuariosOD, dom.SegurancaOD, dom.SistemaOD);
                }

                return this.usuariosService;
            }
        }

        public IEnderecosService EnderecosService
        {
            get
            {
                if (this.enderecosService == null)
                {
                    this.enderecosService = new EnderecosService(dom.LogradourosOD, dom.CidadesOD, dom.BairrosOD,
                        dom.EstadosOD);
                }

                return this.enderecosService;
            }
        }

        public IFiscalService FiscalService
        {
            get
            {
                if (this.fiscalService == null)
                {
                    this.fiscalService = new FiscalService(dom.CnaesOD, dom.FiscalOD);
                }

                return this.fiscalService;
            }
        }

        public IClientesService ClientesService
        {
            get
            {
                if (this.clientesService == null)
                {
                    this.clientesService = new ClientesService(dom.ClientesOD);
                }

                return this.clientesService;
            }
        }

        public IEmpresasService EmpresasService
        {
            get
            {
                if (this.empresasService == null)
                {
                    this.empresasService = new EmpresasService(dom.EmpresasOD, dom.CnaesOD);
                }

                return this.empresasService;
            }
        }

        public IMarcasService MarcasService
        {
            get
            {
                if (this.marcasService == null)
                {
                    this.marcasService = new MarcasService(dom.MarcasOD);
                }

                return this.marcasService;
            }
        }

        public IProdutoTipoAgressividadesService ProdutoTipoAgressividadesService
        {
            get
            {
                if (this.produtoTipoAgressividadesService == null)
                {
                    this.produtoTipoAgressividadesService = new ProdutoTipoAgressividadesService(dom.ProdutoTipoAgressividadesOD);
                }

                return this.produtoTipoAgressividadesService;
            }
        }

        public IProdutoTiposService ProdutoTiposService
        {
            get
            {
                if (this.produtoTiposService == null)
                {
                    this.produtoTiposService = new ProdutoTiposService(dom.ProdutoTiposOD);
                }

                return this.produtoTiposService;
            }
        }

        public IUnidadesDeMedidaService UnidadesDeMedidaService
        {
            get
            {
                if (this.unidadesDeMedidaService == null)
                {
                    this.unidadesDeMedidaService = new UnidadesDeMedidaService(dom.UnidadesDeMedidaOD);
                }

                return this.unidadesDeMedidaService;
            }
        }

        public ITipoVeiculosService TipoVeiculosService
        {
            get
            {
                if (this.tipoVeiculosService == null)
                {
                    this.tipoVeiculosService = new TipoVeiculosService(dom.TipoVeiculosOD);
                }

                return this.tipoVeiculosService;
            }
        }

        public IProdutosService ProdutosService
        {
            get
            {
                if (this.produtosService == null)
                {
                    this.produtosService = new ProdutosService(dom.ProdutosOD);
                }

                return this.produtosService;
            }
        }

        public IProdutoGruposService ProdutoGruposService
        {
            get
            {
                if (this.produtoGruposService == null)
                {
                    this.produtoGruposService = new ProdutoGruposService(dom.ProdutoGruposOD);
                }

                return this.produtoGruposService;
            }
        }

        public IServicoCategoriasService ServicoCategoriasService
        {
            get
            {
                if (this.servicoCategoriasService == null)
                {
                    this.servicoCategoriasService = new ServicoCategoriasService(dom.ServicoCategoriasOD);
                }

                return this.servicoCategoriasService;
            }
        }
    }
}
