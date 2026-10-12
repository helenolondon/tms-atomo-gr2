using AtmService.ODT;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Fiscal
{
    public interface IFiscalService
    {
        List<CnaeODT> ListarCnaes(string filtro);
        CnaeODT ObterCnaePorCodigo(int codCnae);
        List<CnaeODT> ObterCnaesPorCodigo(int codCnaeInicial, int codCnaeFinal);
        List<ItemListaODT> ObterTiposDeSituacaoDeInscricaoEstadual();
    }
}
