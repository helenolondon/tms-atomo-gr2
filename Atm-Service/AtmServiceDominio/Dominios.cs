using AtmServiceDominio.ObjetosDeDominio;
using AtmServiceDominio.ObjetosDeDominio.Arquivos;
using AtmServiceDominio.ObjetosDeDominio.Bairros;
using AtmServiceDominio.ObjetosDeDominio.Cidades;
using AtmServiceDominio.ObjetosDeDominio.Clientes;
using AtmServiceDominio.ObjetosDeDominio.Cnaes;
using AtmServiceDominio.ObjetosDeDominio.Contatos;
using AtmServiceDominio.ObjetosDeDominio.Empresas;
using AtmServiceDominio.ObjetosDeDominio.Estados;
using AtmServiceDominio.ObjetosDeDominio.Fiscal;
using AtmServiceDominio.ObjetosDeDominio.Logradouros;
using AtmServiceDominio.ObjetosDeDominio.Marcas;
using AtmServiceDominio.ObjetosDeDominio.ProdutoTipoAgressividades;
using AtmServiceDominio.ObjetosDeDominio.ProdutoTipos;
using AtmServiceDominio.ObjetosDeDominio.Seguranca;
using AtmServiceDominio.ObjetosDeDominio.Sistema;
using AtmServiceDominio.ObjetosDeDominio.UnidadesDeMedida;
using AtmServiceDominio.ObjetosDeDominio.Usuarios;
using AtmServiceInfra;
using Microsoft.Extensions.Configuration;

namespace AtmServiceDominio
{
    public class Dominios
    {
        private readonly IConfiguration configuration;
        private IRepositorios reps;
        private IInfraResources InfraResources;

        IArquivosOD arquivosOD = null;
        IUsuariosOD usuariosOD = null;
        ISegurancaOD segurancaOD = null;
        IContatosOD contatosOD = null;
        ILogradourosOD logradourosOD = null;
        ICidadesOD cidadesOD = null;
        IBairrosOD bairrosOD = null;
        IEstadosOD estadosOD = null;
        ICnaesOD cnaesOD = null;
        IClientesOD clientesOD = null;
        IFiscalOD fiscalOD = null;
        ISistemaOD sistemaOD = null;
        IEmpresasOD empresasOD = null;
        IMarcasOD marcasOD = null;
        IProdutoTipoAgressividadeOD produtoTipoAgressividadesOD = null;
        IProdutoTiposOD produtoTiposOD = null;
        IUnidadesDeMedidaOD unidadesDeMedidaOD = null;

        public Dominios(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.InfraResources = new InfraResources();

            CriarRepositorios();
        }

        private void CriarRepositorios()
        {
            this.reps = new Repositorios(configuration);
        }

        public void Salvar()
        {
            reps.Salvar();
        }

        public IArquivosOD ArquivosOD
        {
            get
            {
                if (arquivosOD == null)
                {
                    arquivosOD = new ArquivosOD(reps.ArquivosRepositorio);
                    arquivosOD.NomePlural = "Arquivos";
                    arquivosOD.NomeSingular = "Arquivo";
                }

                return arquivosOD;
            }
        }

        public ISistemaOD SistemaOD
        {
            get
            {
                if (sistemaOD == null)
                {
                    sistemaOD = new SistemaOD(this.InfraResources);
                    sistemaOD.NomePlural = "Configurações de sistema";
                    sistemaOD.NomeSingular = "Configuração de sistema";
                }

                return sistemaOD;
            }
        }

        public IContatosOD ContatosOD
        {
            get
            {
                if (contatosOD == null)
                {
                    contatosOD = new ContatosOD(reps.ContatosRep);
                    contatosOD.NomePlural = "Contatos";
                    contatosOD.NomeSingular = "Contato";
                }

                return contatosOD;
            }
        }

        public ISegurancaOD SegurancaOD
        {
            get
            {
                if (segurancaOD == null)
                {
                    segurancaOD = new SegurancaOD(reps.SegurancaRep, reps.LoginInfoRep);
                    segurancaOD.NomePlural = "Logins";
                    segurancaOD.NomeSingular = "Login";
                }

                return segurancaOD;
            }
        }

        public IUsuariosOD UsuariosOD
        {
            get
            {
                if (usuariosOD == null)
                {
                    usuariosOD = new UsuariosOD(reps.UsuariosRep);
                    usuariosOD.NomeSingular = "Usuário";
                    usuariosOD.NomePlural = "Usuários";
                }

                return usuariosOD;
            }
        }

        public ILogradourosOD LogradourosOD
        {
            get
            {
                if (logradourosOD == null)
                {
                    logradourosOD = new LogradourosOD(reps.LogradourosRep);
                    logradourosOD.NomeSingular = "Endereço";
                    logradourosOD.NomePlural = "Endereços";
                }

                return logradourosOD;
            }
        }

        public ICidadesOD CidadesOD
        {
            get
            {
                if (cidadesOD == null)
                {
                    cidadesOD = new CidadesOD(reps.CidadesRepositorio);
                    cidadesOD.NomeSingular = "Cidade";
                    cidadesOD.NomePlural = "Cidades";
                }

                return cidadesOD;
            }
        }

        public IBairrosOD BairrosOD
        {
            get
            {
                if (bairrosOD == null)
                {
                    bairrosOD = new BairrosOD(reps.BairrosRepositorio);
                    bairrosOD.NomeSingular = "Bairro";
                    bairrosOD.NomePlural = "Bairros";
                }

                return bairrosOD;
            }
        }

        public IEstadosOD EstadosOD
        {
            get
            {
                if (estadosOD == null)
                {
                    estadosOD = new EstadosOD(reps.EstadosRepositorio);
                    estadosOD.NomeSingular = "Estado";
                    estadosOD.NomePlural = "Estados";
                }

                return estadosOD;
            }
        }

        public ICnaesOD CnaesOD
        {
            get
            {
                if (cnaesOD == null)
                {
                    cnaesOD = new CnaesOD(reps.CnaesRepositorio);
                    cnaesOD.NomeSingular = "Cnae";
                    cnaesOD.NomePlural = "Cnaes";
                }

                return cnaesOD;
            }
        }

        public IClientesOD ClientesOD
        {
            get
            {
                if (clientesOD == null)
                {
                    clientesOD = new ClientesOD(reps.ClientesRepositorio);
                    clientesOD.NomeSingular = "Cliente";
                    clientesOD.NomePlural = "Clientes";
                }

                return clientesOD;
            }
        }

        public IFiscalOD FiscalOD
        {
            get
            {
                if (fiscalOD == null)
                {
                    fiscalOD = new FiscalOD();
                }

                return fiscalOD;
            }
        }

        public IEmpresasOD EmpresasOD
        {
            get
            {
                if (empresasOD == null)
                {
                    empresasOD = new EmpresasOD(this.reps.EmpresasRepositorio);
                    empresasOD.NomeSingular = "Empresa";
                    empresasOD.NomePlural = "Empresas";
                }

                return empresasOD;
            }
        }

        public IMarcasOD MarcasOD
        {
            get
            {
                if (marcasOD == null)
                {
                    marcasOD = new MarcasOD(reps.MarcasRepositorio);
                    marcasOD.NomeSingular = "Marca";
                    marcasOD.NomePlural = "Marcas";
                }

                return marcasOD;
            }
        }

        public IProdutoTipoAgressividadeOD ProdutoTipoAgressividadesOD
        {
            get
            {
                if (produtoTipoAgressividadesOD == null)
                {
                    produtoTipoAgressividadesOD = new ProdutoTipoAgressividadesOD(reps.ProdutoTipoAgressividadesRepositorio);
                    produtoTipoAgressividadesOD.NomeSingular = "Tipo de produto agressividade";
                    produtoTipoAgressividadesOD.NomePlural = "Tipos de produto agressividade";
                }

                return produtoTipoAgressividadesOD;
            }
        }

        public IProdutoTiposOD ProdutoTiposOD
        {
            get
            {
                if (produtoTiposOD == null)
                {
                    produtoTiposOD = new ProdutoTiposOD(reps.ProdutoTiposRepositorio);
                    produtoTiposOD.NomeSingular = "Tipo de produto";
                    produtoTiposOD.NomePlural = "Tipos de produto";
                }

                return produtoTiposOD;
            }
        }

        public IUnidadesDeMedidaOD UnidadesDeMedidaOD
        {
            get
            {
                if (unidadesDeMedidaOD == null)
                {
                    unidadesDeMedidaOD = new UnidadesDeMedidaOD(reps.UnidadesDeMedidaRepositorio);
                    unidadesDeMedidaOD.NomeSingular = "Unidade de medida";
                    unidadesDeMedidaOD.NomePlural = "Unidades de medida";
                }

                return unidadesDeMedidaOD;
            }
        }

    }
}
