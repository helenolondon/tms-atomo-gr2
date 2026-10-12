using AtmServiceInfra.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra
{
    public class InfraResources : IInfraResources
    {
        public InfraResources()
        {
            this.Comunicacao = new ComunicacaoResource();
        }

        public ComunicacaoResource Comunicacao { get; private set; }
    }
}
