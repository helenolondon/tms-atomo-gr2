using AtmService.ODT;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AtmService.Arquivos
{
    public interface IArquivosService
    {
        void SalvarArquivos(IList<IFormFile> files, string tag, string reference);
        void SalvarArquivo(IFormFile file, string tag, string reference);
        ArquivoODT ObterArquivo(int codArquivo, bool somenteMetadados);
        void Excluir(int codArquivo);
    }
}
