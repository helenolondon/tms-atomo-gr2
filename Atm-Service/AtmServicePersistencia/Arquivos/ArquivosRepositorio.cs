using AtmServiceInfra.Arquivos.Files;
using AtmServiceInfra.EF;
using AtmServiceInfra.Erros;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AtmServiceInfra.Arquivos
{
    public class ArquivosRepositorio : Repositorio<ArquivoODP>, IArquivosRepositorio
    {
        private readonly string uploadedFilesFolder = null;

        public override void Inserir(ObjetoDePersistencia entity)
        {
            var fileSaver = new FileSaver();
            var odp = entity as ArquivoODP;

            fileSaver.Folder = Path.Combine(this.uploadedFilesFolder, odp.CodArquivo.ToString());

            if (!Directory.Exists(fileSaver.Folder))
            {
                try
                {
                    Directory.CreateDirectory(fileSaver.Folder);
                    odp.Path = fileSaver.SaveFile(odp.fileStream, odp.NomeArquivo);
                    
                    base.Inserir(entity);
                }
                catch (Exception e)
                {
                    while (e.InnerException != null)
                    {
                        e = e.InnerException;
                    };

                    throw new PersistenciaExcecao(e.Message);
                };
            };
        }

        public override void AntesDeExcluir(ArquivoODP obj)
        {
            var fileSaver = new FileSaver();
            fileSaver.Folder = Path.Combine(this.uploadedFilesFolder, obj.CodArquivo.ToString());

            fileSaver.Excluir(obj.NomeArquivo);

            try
            {
                if (Directory.Exists(fileSaver.Folder))
                {
                    Directory.Delete(fileSaver.Folder);
                };
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                };

                throw new PersistenciaExcecao(e.Message);
            };
        }

        public ArquivosRepositorio(AtmDbContext context, string uploadedFilesFolder): base(context)
        {
            this.uploadedFilesFolder = uploadedFilesFolder ?? throw new Erros.PersistenciaExcecao("Caminho para salvar uploads não pode estar vazio");
        }

        public List<ArquivoODP> ObterArquivos(string tag, string reference)
        {
            return this.context.Arquivos
                .Where(a => (a.Tag.ToUpper() == tag.ToUpper() || string.IsNullOrEmpty(tag)) && 
                (a.Reference.ToUpper() == reference.ToUpper() || string.IsNullOrEmpty(reference)))
                .ToList();
        }

        public Stream ObterArquivoStream(int codArquivo)
        {
            var arquivoODP = this.context.Arquivos
                .Where(arquivo => arquivo.CodArquivo == codArquivo)
                .SingleOrDefault();

            if(arquivoODP != null)
            {
                if (!File.Exists(arquivoODP.Path))
                {
                    throw new Erros.PersistenciaExcecao("Arquivo não existe no caminho especificado");
                };

                return System.IO.File.OpenRead(arquivoODP.Path);
            };

            return null;
        }
    }
}
