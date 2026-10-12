using AtmServiceDominio.ObjetosDeDominio.Validacao;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;

namespace AtmServiceDominio.ODN
{
    public class ContatoODN: ObjetoDeNegocio
    {
        public int CodContato { get; set; }
        public string Nome { get; set; }
        public string Url { get; set; }
        public string Telefone { get; set; }
        public string Observacoes { get; set; }

        protected internal override void ConfiguraRegrasDeValidacao()
        {            
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Nome", 100, "Nome deve ter no máximo 100 letras"));
            this.regrasDeValidacao.Add(new TamanhoMinimoRV(this, "Nome", 2, "Nome deve ter no mínimo 2 letras"));

            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Telefone", 40, "Telefone deve ter no máximo 40 letras/números"));

            this.regrasDeValidacao.Add(new ObrigatorioRV(this, "Nome", "Nome é de preenchimento obrigatório"));
        }

        internal override void FromODP(ObjetoDePersistencia origem)
        {
            var odp = origem as ContatoODP;

            this.Nome = odp.Nome;
            this.CodContato = odp.CodContato;
            this.Telefone = odp.Telefone;
            this.Observacoes = odp.Observacoes;
            this.Url = odp.Url;
        }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino)
        {
            var odp = destino as ContatoODP;

            if (odp == null)
            {
                odp = new ContatoODP();
            }

            odp.Observacoes = this.Observacoes;
            odp.CodContato = this.CodContato;
            odp.Url = this.Url;
            odp.Nome = this.Nome;
            odp.Telefone = this.Telefone;

            return odp;
        }
    }
}
