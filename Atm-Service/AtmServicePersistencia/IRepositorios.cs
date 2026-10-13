/* Gateway, Unity Of Work de repositorios conterá referências para os outros repositórios
 * 
 * 
 *
 * */

using AtmServiceInfra.Cidades;
using AtmServiceInfra.Bairros;
using AtmServiceInfra.Contatos;
using AtmServiceInfra.Logradouros;
using AtmServiceInfra.Seguranca;
using AtmServiceInfra.Estados;
using AtmServiceInfra.Cnaes;
using AtmServiceInfra.Clientes;
using AtmServiceInfra.Clientes.PrecosTarefas;
using AtmServiceInfra.Arquivos;
using AtmServiceInfra.Empresas;
using AtmServiceInfra.Marcas;
using AtmServiceInfra.ProdutoTipoAgressividades;
using AtmServiceInfra.ProdutoTipos;

namespace AtmServiceInfra
{
    public interface IRepositorios
    {
        IArquivosRepositorio ArquivosRepositorio { get; }
        IContatosRepositorio ContatosRep { get; }
        ISegurancaRepositorio SegurancaRep { get; }
        IUsuariosRepositorio UsuariosRep { get; }
        ILoginInfoRepositorio LoginInfoRep { get; }
        ILogradourosRepositorio LogradourosRep { get; }
        ICidadesRepositorio CidadesRepositorio { get; }
        IBairrosRepositorio BairrosRepositorio { get; }
        IEstadosRepositorio EstadosRepositorio { get; }
        ICnaesRepositorio CnaesRepositorio { get; }
        IClientesRepositorio ClientesRepositorio { get; }
        IEmpresasRepositorio EmpresasRepositorio { get; }
        IMarcasRepositorio MarcasRepositorio { get; }
        AtmServiceInfra.ProdutoTipoAgressividades.IProdutoTipoAgressividadeRepositorio ProdutoTipoAgressividadesRepositorio { get; }
        IProdutoTiposRepositorio ProdutoTiposRepositorio { get; }

        void Salvar();
    }
}
