using AtmServiceDominio.Erros;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra;
using AtmServiceInfra.ODP;
using System;

namespace AtmServiceDominio.ObjetosDeDominio
{
    public class AtmObjetoDominioLab: IAtmObjetoDominio
    {
        /// <summary>
        /// Nome do Objeto de negócio representado por este domínio no singular.
        /// </summary>
        public string NomeSingular { get; set; }

        /// <summary>
        /// Nome do Objeto de negócio representado por este domínio no plural.
        /// </summary>
        public string NomePlural { get; set; }

        internal AtmObjetoDominioLab() : base() { }

        protected virtual object[] ObterID(ObjetoDeNegocio obj)
        {
            return new object[1] { obj };
        }

        /// <summary>
        /// Salva um objeto de negócio
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual ObjetoDeNegocio Salvar(ObjetoDeNegocio obj)
        {
            if (!obj.Validar())
            {
                throw new EstadoDoObjetoDeNegcioEInvalidoDE("Objeto não pode ser validado", obj);
            }

            return null;            
        }

        /// <summary>
        /// Exclui um objeto de negócio
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual void Excluir(params object[] id)
        {
        }
    }
}
