using AtmService.ODT;
using AtmService.Utils;
using AtmServiceDominio.ObjetosDeDominio.Contatos;
using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Contatos
{
    public class ContatosService : AtmService, IContatosService
    {
        private IContatosOD contatosOD = null;

        public ContatosService(IContatosOD contatosOD)
        {
            this.contatosOD = contatosOD;
        }
        public void Excluir(int codContato)
        {
            this.contatosOD.Excluir(codContato);
        }

        public List<ContatoODT> ListarTodos()
        {
            var odns = this.contatosOD.ObterContatos();

            if(odns == null)
            {
                return null;
            }

            return new List<ContatoODT>().FromListaODN(odns);
        }

        public ContatoODT ObterContatoPorCodigo(int codContato)
        {
            throw new NotImplementedException();
        }
        public ContatoODT SalvarContato(ContatoODT temp)
        {
            var odn = temp.ToContatoODN();

            this.ExecutarAcaoDeDominio(() =>
            {
                odn = this.contatosOD.Salvar(odn) as ContatoODN;
            });

            return ContatoODT.NewFromODN(odn);
        }
    }
}
