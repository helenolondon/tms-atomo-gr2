using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra
{
    public interface IRepositorio<TEntity> where TEntity : ObjetoDePersistencia
    { 
        void Salvar(TEntity obj);
        void Excluir(TEntity obj);
        TEntity Obter(params object[] chave);
        void Inserir(ObjetoDePersistencia entity);

        int ObterProximoValorDeSequencia(string sequencia);
    }
}
