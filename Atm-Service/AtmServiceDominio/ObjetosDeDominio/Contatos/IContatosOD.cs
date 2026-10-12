using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Contatos
{
    public interface IContatosOD: IAtmObjetoDominio
    {
        List<ContatoODN> ObterContatos();
    }
}
