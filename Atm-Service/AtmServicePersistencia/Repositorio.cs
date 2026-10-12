
using AtmServiceInfra.EF;
using AtmServiceInfra.Erros;
using AtmServiceInfra.ODP;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace AtmServiceInfra
{
    public class Repositorio<TEntity> where TEntity: ObjetoDePersistencia
    {
        protected AtmDbContext context;
        protected DbSet<TEntity> dbSet;

        public Repositorio(AtmDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual void AntesDeExcluir(TEntity obj)
        {

        }

        public virtual List<TEntity> Query(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity ObterPorID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity Obter(params object[] chave)
        {
            return dbSet.Find(chave);
        }

        public virtual void Inserir(ObjetoDePersistencia entity)
        {
            dbSet.Add(entity as TEntity);
        }

        public virtual void Excluir(params object[] chave)
        {
            ObjetoDePersistencia entityToDelete = dbSet.Find(chave);

            AntesDeExcluir(entityToDelete as TEntity);

            Excluir(entityToDelete);
        }

        public void Excluir(TEntity obj)
        {
            AntesDeExcluir(obj);

            if (context.Entry(obj).State == EntityState.Detached)
            {
                
                dbSet.Attach(obj as TEntity);
            };

            dbSet.Remove(obj as TEntity);
        }

        public virtual void Salvar(TEntity entityToUpdate)
        {
            if (context.Entry(entityToUpdate).State == EntityState.Added)
            {
                return;
            }

            dbSet.Attach(entityToUpdate as TEntity);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual int ObterProximoValorDeSequencia(string sequencia)
        {
            int proximoValor = 0;
            sequencia = sequencia.ToUpper().Trim();

            try
            {
                var conn = this.context.Database.GetDbConnection();
                conn.Open();

                var trans = conn.BeginTransaction();

                try
                {

                    var cmd = this.context.Database.GetDbConnection().CreateCommand();
                    cmd.Transaction = trans;

                    cmd.CommandText = "UPDATE TS_SEQUENCIAS SET ULTIMASEQUENCIA = ULTIMASEQUENCIA + 1 " +
                                       $"WHERE UPPER(NOMESEQUENCIA) = @SEQUENCIA";
                    cmd.Parameters.Add(new MySqlParameter("SEQUENCIA", sequencia));

                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new PersistenciaExcecao($"{sequencia} não está configurada");
                    };

                    cmd.CommandText = "SELECT ULTIMASEQUENCIA FROM TS_SEQUENCIAS WHERE UPPER(NOMESEQUENCIA) = @SEQUENCIA";
                    cmd.CommandType = System.Data.CommandType.Text;

                    proximoValor = (int)cmd.ExecuteScalar();

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
                finally
                {                    
                    conn.Close();
                }

                return proximoValor;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                throw new PersistenciaExcecao(ex.Message);
            }
        }
    }
}