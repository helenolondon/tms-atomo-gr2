using AtmServiceDominio.Erros;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio
{
    public class AtmObjetoDominio : IAtmObjetoDominio
    {
        /// <summary>
        /// Nome do Objeto de negócio representado por este domínio no singular.
        /// </summary>
        public string NomeSingular { get; set; }

        /// <summary>
        /// Nome do Objeto de negócio representado por este domínio no plural.
        /// </summary>
        public string NomePlural { get; set; }

        internal AtmObjetoDominio() : base() { }

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

    public class AtmObjetoDominio<T> : AtmObjetoDominio, IAtmObjetoDominio where T: ObjetoDePersistencia
    {
        protected readonly IRepositorio<T> repositorio;

        internal AtmObjetoDominio() : base() { }

        internal AtmObjetoDominio(IRepositorio<T> repositorio)
        {
            this.repositorio = repositorio ?? throw new DominioExcecao("Objeto de domínio precisa de repositório para ser instanciado");
        }

        /// <summary>
        /// Procedimento executado antes de colocar os novos valores no repositório
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="inserindo"></param>
        internal protected virtual void validar(ObjetoDeNegocio Obj, bool inserindo)
        {

        }

        /// <summary>
        /// Procedimento que será executado antes de salvar um objeto de negócio.
        /// Se uma exceção for lançada aqui, o processo de salvar é abortado.
        /// </summary>
        /// <param name="obj"></param>
        internal protected virtual void AntesDeSalvar(T obj, bool inserindo)
        {   
        }

        /// <summary>
        /// Procedimento que será executado antes de excluir um objeto de negócio.
        /// Se uma exceção for lançada aqui, o processo de excluir é abortado.
        /// </summary>
        /// <param name="obj"></param>
        internal protected virtual void AntesDeExcluir(T obj)
        {            
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            return new object[1] { obj };
        }

        /// <summary>
        /// Salva um objeto de negócio
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ObjetoDeNegocio Salvar<J>(ObjetoDeNegocio obj, IRepositorio<J> repositorio, 
            Func<ObjetoDeNegocio, object[]>  retIdFunc = null, 
            Action<ObjetoDePersistencia, bool> antesSalvarFunc = null,
            string nomeSingular = null) where J : ObjetoDePersistencia
        {
            base.Salvar(obj);

            var id = retIdFunc == null ? ObterID(obj as ObjetoDeNegocio) : retIdFunc(obj);

            if (id != null)
            {
                validar(obj, false);

                var temp = repositorio.Obter(id);

                if (temp == null)
                {
                    throw new RegistroJaExcluidoPorOutroUsuarioDE($"{nomeSingular ?? this.NomeSingular} não existe");
                }

                obj.ToODP(temp);

                if (antesSalvarFunc == null)
                {
                    AntesDeSalvar(temp as T, false);
                }
                else
                {
                    antesSalvarFunc(temp, false);
                };

                temp.DataAlt = DateTime.Now;
                temp.UsuarioAlt = "SISTEMA";

                temp.DataCad = temp.DataCad;
                temp.UsuarioCad = temp.UsuarioCad;

                repositorio.Salvar(temp);

                obj.FromODP(temp);

                return obj;
            }
            else
            {
                validar(obj, true);

                var odp = obj.ToODP();

                odp.DataCad = DateTime.Now;
                odp.UsuarioCad = "SISTEMA";

                if (antesSalvarFunc == null)
                {
                    AntesDeSalvar(odp as T, true);
                }
                else
                {
                    antesSalvarFunc(odp, true);
                };

                repositorio.Inserir(odp);

                obj.FromODP(odp);

                return obj;
            }
        }

        /// <summary>
        /// Salva um objeto de negócio
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override ObjetoDeNegocio Salvar(ObjetoDeNegocio obj)
        {
            return this.Salvar<T>(obj, this.repositorio, null, null, this.NomeSingular);
        }

        /// <summary>
        /// Exclui um objeto de negócio
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override void Excluir(params object[] id)
        {
            base.Excluir();

            var odp = this.repositorio.Obter(id) as T;

            if (odp == null)
            {
                throw new RegistroNaoEncontradoDE("Objeto não existe");
            }

            AntesDeExcluir(odp);

            this.repositorio.Excluir(odp);
        }

        /// <summary>
        /// Trata o retorno de 1 objeto de negócio vindo do repositório
        /// </summary>
        /// <typeparam name="TODN"></typeparam>
        /// <param name="odns"></param>
        /// <returns>Objeto de negócio especializado</returns>
        protected TODN Retorna1<TODN>(List<TODN> odns) where TODN: ObjetoDeNegocio
        {
            if (odns != null)
            {
                if (odns.Count > 0)
                {
                    return odns[0];
                }
                else if (odns.Count > 1)
                {
                    throw new DominioExcecao($"Foram encontrados mais de um {this.NomePlural}");
                }
            }

            return null;
        }
    }
}
