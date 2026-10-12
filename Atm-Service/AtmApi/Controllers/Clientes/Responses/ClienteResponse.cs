using AtmService.ODT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmApi.Controllers.Clientes.Responses
{
    public class ClienteResponse
    {
        public ClienteResponse(ClienteODT odt, CnaeODT cnae)
        {
            this.Cliente = odt;
            this.Cnae = cnae;
        }

        public ClienteODT Cliente { get; set; }
        public CnaeODT Cnae { get; set; }
    }
}
