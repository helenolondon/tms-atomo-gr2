using AtmServiceDominio.ObjetosDeDominio.Validacao;
using AtmServiceDominio.ODN.Attributes;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeNegocio
{
    /// <summary>
    /// Interface básica para Objeto de negócio
    /// </summary>
    public class ObjetoDeNegocio
    {
        internal protected List<IRegraDeValidacao> regrasDeValidacao;

        public ObjetoDeNegocio()
        {
            this.regrasDeValidacao = new List<IRegraDeValidacao>();
            this.ConfiguraRegrasDeValidacao();
        }
        /// <summary>
        /// Erros de validação atualizadas após chamada a Validar()
        /// </summary>
        public List<string> ErrosDeValidacao { get; private set; }
        
        /// <summary>
        /// Método responsável pala configuração das regras de valiação nas classes descendentes
        /// </summary>
        protected internal virtual void ConfiguraRegrasDeValidacao()
        {
            AddRegrasDasAnotacoes();
        }

        private void AddRegrasDasAnotacoes()
        {
            var props = this.GetType().GetProperties();

            foreach (var prop in props)
            {
                foreach (var attr in prop.GetCustomAttributes(false))
                {
                    var obrigatorioAttr = (attr as ObrigatorioAttribute);

                    if (obrigatorioAttr != null)
                    {
                        this.regrasDeValidacao.Add(new ObrigatorioRV(this, prop.Name, obrigatorioAttr.MensagemDeErro));
                    };

                    var minimoAttr = (attr as TamanhoMinimoAttribute);

                    if (minimoAttr != null)
                    {
                        this.regrasDeValidacao.Add(new TamanhoMinimoRV(this, prop.Name, minimoAttr.Tamanho, minimoAttr.MensagemDeErro));
                    };
                    
                    var valoresPermitidosAttr = (attr as ValoresPermitidosAttribute);

                    if (valoresPermitidosAttr != null)
                    {
                        this.regrasDeValidacao.Add(new ValoresPermitidosRV(this, prop.Name, valoresPermitidosAttr.Valores, valoresPermitidosAttr.MensagemDeErro));
                    };
                };
            };
        }

        internal virtual void FromODP(ObjetoDePersistencia obj)
        {
        }

        internal virtual ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            return destino;
        }

        internal virtual T ToODP<T>(ObjetoDePersistencia destino = null) where T: ObjetoDePersistencia, new()
        {
            T ret = null; 

            if (destino == null)
            {
                ret = new T();
            }

            return ret;
        }

        /// <summary>
        /// Executa regras de validação
        /// </summary>
        /// <returns>Retorna true se todas as regras passarem</returns>
        public bool Validar()
        {
            if (regrasDeValidacao == null || regrasDeValidacao.Count() == 0)
            {
                return true;
            }

            var regrasFalhadas = this.regrasDeValidacao.Where(r => !r.Validar()).ToList();
            this.ErrosDeValidacao = regrasFalhadas.Select(m => m.MensagemDeErro).ToList<string>();

            return regrasFalhadas == null || regrasFalhadas.Count() == 0; 
        }
    }
}
