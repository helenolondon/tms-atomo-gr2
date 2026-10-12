using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Fiscal
{
    public interface IFiscalOD
    {
        List<EnumOV> ObterTiposDeSituacaoDeInscricaoEstadual();
    }
}
