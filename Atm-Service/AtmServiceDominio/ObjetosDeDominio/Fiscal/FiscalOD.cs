using AtmServiceDominio.ODN;
using AtmServiceDominio.ODN.Tipos.Fiscal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Fiscal
{
    public class FiscalOD: IFiscalOD
    {
        public List<EnumOV> ObterTiposDeSituacaoDeInscricaoEstadual()
        {
            return new List<EnumOV>() 
            {
                new EnumOV((int)SituacaoInscricaoEstadual.Contribuinte, "Contribuinte"),
                new EnumOV((int)SituacaoInscricaoEstadual.NaoContribuinte, "Não contribuinte"),
                new EnumOV((int)SituacaoInscricaoEstadual.Isento, "Isento")
            };
        }
    }
}
