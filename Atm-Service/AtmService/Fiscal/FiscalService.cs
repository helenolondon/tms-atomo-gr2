using AtmService.ODT;
using AtmService.Utils;
using AtmServiceDominio.ObjetosDeDominio.Cnaes;
using AtmServiceDominio.ObjetosDeDominio.Fiscal;
using System.Collections.Generic;

namespace AtmService.Fiscal
{
    public class FiscalService : AtmService, IFiscalService
    {
        private ICnaesOD cnaesOD = null;
        private IFiscalOD fiscalOD = null;

        public FiscalService(ICnaesOD cnaesOD, IFiscalOD fiscalOD)
        {
            this.cnaesOD = cnaesOD;
            this.fiscalOD = fiscalOD;
        }

        public List<CnaeODT> ListarCnaes(string filtro)
        {
            var odns = this.cnaesOD.ObterCnaes(filtro);

            if(odns == null)
            {
                return null;
            }

            var odts = new List<CnaeODT>();
            odts.FromListaODN(odns);

            return odts;
        }

        public CnaeODT ObterCnaePorCodigo(int codCnae)
        {
            var odn = this.cnaesOD.ObterCnaePorId(codCnae);

            if(odn == null)
            {
                return null;
            };

            return CnaeODT.NewFromODN(odn);
        }

        public List<CnaeODT> ObterCnaesPorCodigo(int codCnaeInicial, int codCnaeFinal)
        {
            var odns = this.cnaesOD.ObterCnaesPorId(codCnaeInicial, codCnaeFinal);
            var odts = new List<CnaeODT>();

            if (odns != null && odns.Count > 0)
            {
                odts.FromListaODN(odns);
            };

            return odts;        
        }

        public List<ItemListaODT> ObterTiposDeSituacaoDeInscricaoEstadual()
        {
            var tiposOV = this.fiscalOD.ObterTiposDeSituacaoDeInscricaoEstadual();
            var lista = new List<ItemListaODT>();

            if (tiposOV != null)
            {
                foreach (var ov in tiposOV)
                {
                    lista.Add(new ItemListaODT(ov));
                }
            };

            return lista;
        }
    }
}
