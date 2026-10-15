using AtmServiceDominio.ODN;

namespace AtmService.ODT
{
    public class ServicoCategoriaODT
    {
        public int CodServicoCategoria { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public ServicoCategoriaODT()
        {
        }

        public ServicoCategoriaODT(ServicoCategoriaODN item)
        {
            FromODN(item);
        }

        public void FromODN(ServicoCategoriaODN item)
        {
            if (item == null)
            {
                return;
            }

            this.CodServicoCategoria = item.CodServicoCategoria;
            this.Nome = item.Nome;
            this.Descricao = item.Descricao;
        }

        public ServicoCategoriaODN ToODN()
        {
            return new ServicoCategoriaODN
            {
                CodServicoCategoria = this.CodServicoCategoria,
                Nome = this.Nome,
                Descricao = this.Descricao
            };
        }
    }
}
