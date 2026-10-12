using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra.Erros
{
    public class PersistenciaExcecao: Exception
    {
        public PersistenciaExcecao() : base() { }
        public PersistenciaExcecao(string mensagem) : base(mensagem) { }
    }
}
