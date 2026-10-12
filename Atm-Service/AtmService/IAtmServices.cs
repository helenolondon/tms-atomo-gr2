using AtmService.Arquivos;
using AtmService.Clientes;
using AtmService.Contatos;
using AtmService.Empresas;
using AtmService.Enderecos;
using AtmService.Fiscal;
using AtmService.Seguranca;
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

        /// <summary>
        /// Salva operações em memória.
        /// Todas as vezes que os dados nos repositórios mudarem, é preciso execuar Commit()
        /// </summary>
        void Commit();
    }
}