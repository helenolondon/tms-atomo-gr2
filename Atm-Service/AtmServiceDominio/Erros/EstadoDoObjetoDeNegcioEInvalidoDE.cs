using AtmServiceDominio.ObjetosDeNegocio;
using System.Collections.Generic;

namespace AtmServiceDominio.Erros
{
    public class EstadoDoObjetoDeNegcioEInvalidoDE : DominioExcecao
    {
        /// <summary>
        /// Objetos de domínio que causaram o erro;
        /// </summary>
        public List<ObjetoDeNegocio> ODN { get; }

        public EstadoDoObjetoDeNegcioEInvalidoDE(string message, ObjetoDeNegocio odn) : base(message)
        {
            this.ODN = new List<ObjetoDeNegocio>() { odn };
        }
    }
}
