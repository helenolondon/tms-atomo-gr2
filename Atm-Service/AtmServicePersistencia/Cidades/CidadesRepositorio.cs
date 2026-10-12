using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceInfra.Cidades
{
    public class CidadesRepositorio: Repositorio<CidadeODP>, ICidadesRepositorio
    {
        public CidadesRepositorio(AtmDbContext context): base(context)
        {
        }

        public List<CidadeODP> ObterCidadesPorEstado(int codEstado)
        {
            return this.context.Cidades.Where(cidade => cidade.CodigoEstado == codEstado)
                ?.OrderBy(cid => cid.NomeCidade)?.ToList();
        }

        public List<CidadeODP> ObterCidades(string nomeCidade, int? codigoEstado = null)
        {
            return this.context.Cidades
                .Where(cidade => 
                    (string.IsNullOrEmpty(nomeCidade) || cidade.NomeCidade.ToUpper().Contains(nomeCidade.ToUpper())) &&
                    (codigoEstado == null || cidade.CodigoEstado == codigoEstado))
                ?.OrderBy(cid => cid.NomeCidade)?.ToList();
        }
    }
}
