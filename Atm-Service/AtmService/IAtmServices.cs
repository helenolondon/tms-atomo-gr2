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
using AtmService.Seguranca;
using AtmService.TipoVeiculos;
using AtmService.UnidadesDeMedida;
using AtmService.Usuarios;

namespace AtmService
{
    public interface IAtmServices
    {
        IArquivosService ArquivosService { get; }
        IContatosService ContatosService { get; }
        ISegurancaService SegurancaService { get; }
        IUsuariosService UsuariosService { get; }
        IEnderecosService EnderecosService { get; }
        IFiscalService FiscalService { get; }
        IClientesService ClientesService { get; }
        IEmpresasService EmpresasService { get; }

        IMarcasService MarcasService { get; }
        IProdutoTipoAgressividadesService ProdutoTipoAgressividadesService { get; }
        IProdutoTiposService ProdutoTiposService { get; }
        IUnidadesDeMedidaService UnidadesDeMedidaService { get; }
        ITipoVeiculosService TipoVeiculosService { get; }
        IProdutosService ProdutosService { get; }
        IProdutoGruposService ProdutoGruposService { get; }

        /// <summary>
        /// Salva operações em memória.
        /// Todas as vezes que os dados nos repositórios mudarem, é preciso execuar Commit()
        /// </summary>
        void Commit();
    }
}