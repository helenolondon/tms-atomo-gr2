using AtmServiceDominio.ODN;

namespace AtmService.ODT
{
    public class MarcaODT
    {
        public int CodMarca { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }

        public MarcaODT()
        {
        }

        public MarcaODT(MarcaODN marca)
        {
            FromODN(marca);
        }

        public void FromODN(MarcaODN marca)
        {
            if (marca == null)
            {
                return;
            }

            this.CodMarca = marca.CodMarca;
            this.Nome = marca.Nome;
            this.Descricao = marca.Descricao;
            this.Observacao = marca.Observacao;
        }

        public MarcaODN ToODN()
        {
            return new MarcaODN
            {
                CodMarca = this.CodMarca,
                Nome = this.Nome,
                Descricao = this.Descricao,
                Observacao = this.Observacao
            };
        }
    }
}
