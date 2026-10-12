using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpreadsheetLight;
using System.Net.Http.Headers;
using System.Text;
using AtmService.Utils.Excel;

namespace AtmApi.Controllers.Relatórios
{
    class model
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public decimal ValorInvestido { get; set; }
        public bool MEI { get; set; }
    }


    [Route("api/download")]
    [ApiController]
    public class RelatoriosController : AtmController
    {
        [HttpGet]
        public IActionResult GetFile(int id)
        {
            var file = @"C:\Temp\atm-downloads\Pro C# 7_ With .NET and .NET Core ( PDFDrive ).pdf";
            byte[] byFile = null;

            using (var fsFile = System.IO.File.OpenRead(file)) 
            {
                using (var msFile = new MemoryStream()) 
                {
                    fsFile.CopyTo(msFile);
                    byFile = msFile.ToArray();
                };
            };

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = file,
                Inline = false
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            return File(byFile, "application/pdf");
        }
    }
}
