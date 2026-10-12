using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AtmServiceInfra.Arquivos.Files
{
    public class FileSaver
    {
        public string Folder { get; set; }

        public void Excluir(string fileName)
        {
            var fullFileName = Path.Combine(Folder, fileName);

            if (File.Exists(fullFileName))
            {
                File.Delete(fullFileName);
            };
        }

        public string SaveFile(MemoryStream file, string fileName)
        {
            if (!Directory.Exists(this.Folder))
            {
                Directory.CreateDirectory(Folder);
            };

            var fullFileName = Path.Combine(Folder, fileName);
            File.Delete(fullFileName);

            using (var fileStream = new FileStream(fullFileName, FileMode.CreateNew))
            {
                file.Position = 0;
                file.CopyTo(fileStream);
            }

            return fullFileName;
        }
    }
}
