using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Logradouros
{
    public interface ILogradourosOD: IAtmObjetoDominio
    {
        LogradouroODN ObterPorCep(string cep);
    }
}
