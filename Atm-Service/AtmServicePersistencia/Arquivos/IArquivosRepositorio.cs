using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.IO;

namespace AtmServiceInfra.Arquivos
{
    public interface IArquivosRepositorio : IRepositorio<ArquivoODP>
    {
        List<ArquivoODP> ObterArquivos(string tag, string reference);
        Stream ObterArquivoStream(int codArquivo);
    }
}
