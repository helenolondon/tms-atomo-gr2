using AtmService.ODT;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Enderecos
{
    public interface IEnderecosService
    {
        EnderecoODT ObterEnderecoPorCep(string cep);
        CidadeODT ObterCidadePorCodigo(int codCidade);
        List<CidadeODT> ObterCidadesPorEstado(int codEtado);
        List<BairroODT> ObterBairrosPorCidade(int codCidade);
        List<EstadoODT> ObterEstados();
        List<CidadeODT> ObterCidadesPorNome(string nomeCidade, string uf = null);
    }
}
