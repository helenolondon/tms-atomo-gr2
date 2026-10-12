using AtmService.Erros;
using AtmServiceDominio.Erros;
using AtmServiceDominio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;

namespace AtmService
{
    public class AtmService
    {
        /// <summary>
        /// Executa uma ação qualquer no domínio e traduz os erros
        /// </summary>
        /// <param name="action"></param>
        protected void ExecutarAcaoDeDominio(Action action)
        {
            try
            {
                action();
            }
            catch (OperacaoNaoPermitidaDE e)
            {
                throw new AtmServiceOperacaoInvalidaExcecao(e.Message);
            }
            catch (RegistroNaoEncontradoDE e)
            {
                throw new AtmServiceNaoEncontradoSE(e.Message);
            }
            catch (EstadoDoObjetoDeNegcioEInvalidoDE e)
            {
                throw new AtmServiceValidacaoExcecao(e.Message, e.ODN[0].ErrosDeValidacao);
            }
            catch (DominioExcecao e)
            {
                throw new AtmServiceValidacaoExcecao(e.Message);
            }
        }
    }
}
