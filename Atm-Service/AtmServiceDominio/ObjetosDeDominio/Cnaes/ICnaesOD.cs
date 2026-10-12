using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Cnaes
{
    public interface ICnaesOD: IAtmObjetoDominio
    {
        CnaeODN ObterCnaePorId(int codCnae);
        List<CnaeODN> ObterCnaesPorId(int codCnaeInicial, int? codCnaeFinal);
        List<CnaeODN> ObterCnaes(string filtro);
    }
}
