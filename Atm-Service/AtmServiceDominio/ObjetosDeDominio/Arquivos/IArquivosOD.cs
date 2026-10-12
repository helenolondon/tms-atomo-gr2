using AtmServiceDominio.ODN;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.Arquivos
{
    public interface IArquivosOD : IAtmObjetoDominio
    {
        ArquivoODN Obter(int codArquivo, bool somenteMetadados = true);
        List<ArquivoODN> ObterArquivos(string reference, string tag);
    }
}
