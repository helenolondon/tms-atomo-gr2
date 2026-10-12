using AtmService.ODT;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Contatos
{
    public interface IContatosService
    {
        ContatoODT SalvarContato(ContatoODT temp);
        void Excluir(int codContato);

        List<ContatoODT> ListarTodos();
        ContatoODT ObterContatoPorCodigo(int codContato);
    }
}
