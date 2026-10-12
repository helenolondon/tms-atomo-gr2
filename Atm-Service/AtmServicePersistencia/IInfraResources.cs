using AtmServiceInfra.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra
{
    public interface IInfraResources
    {
        public ComunicacaoResource Comunicacao { get; }
    }
}
