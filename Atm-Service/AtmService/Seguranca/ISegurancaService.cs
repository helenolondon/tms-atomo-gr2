using AtmService.ODT;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Seguranca
{
    public interface ISegurancaService
    {
        void Login(string login, string senha, out string token);
        bool VerificarToken(string token);
        void AlterarSenha(string login, string novaSenha, string senhaAntiga, string confirmaSenha);
        string GerarSenhaSegura();
    }
}
