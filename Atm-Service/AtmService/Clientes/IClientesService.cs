using AtmService.ODT;
using AtmServiceDominio.ODN.Tipos.Fiscal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Clientes
{
    public interface IClientesService
    {
        List<ClienteODT> ListarClientes();
        ClienteODT ObterCliente(int codCliente);
        List<ClienteODT> ListarClientesPorNomeOuCnfCnpj(string nomeCpfCnpj);
        List<ClienteODT> ListarClientes(int codCliente = 0, string nome = null, int? tiposDePessoa = null, int cnae = 0, string inscricaoEstadual = null,
            bool? microEmpresa = null, string telefone = null, string contato = null, string identidade = null, string cpf = null, int[] situacaoInscricaoEstadual = null,
            DateTime? dataNascimento = null, int[] estadosCivis = null, int[] codUfs = null, int[] codCidades = null, string razaoSocial = null);

        void Excluir(int codCliente);

        ClienteODT SalvarCliente(ClienteODT cliente);
        List<ItemListaODT> ListarTiposDePessoa();
        List<ItemListaODT> ListarEstadosCivis();
    }
}
