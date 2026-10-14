using System.Collections.Generic;
using System.Linq;
using AtmService.ODT;
using AtmService.Repository;
using AtmService.Model;

namespace AtmService.Services.ProdutoGrupos
{
    public class ProdutoGruposService
    {
        private readonly IRepository<ProdutoGrupo> repository;

        public ProdutoGruposService(IRepository<ProdutoGrupo> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<ProdutoGrupoODT> Listar()
        {
            return this.repository.Listar()
                .Select(this.ParaODT)
                .ToList();
        }

        public ProdutoGrupoODT ObterPorId(int codProdutoGrupo)
        {
            var item = this.repository.ObterPorId(codProdutoGrupo);
            return item == null ? null : this.ParaODT(item);
        }

        public ProdutoGrupoODT Salvar(ProdutoGrupoODT request)
        {
            var item = request.CodProdutoGrupo > 0
                ? this.repository.ObterPorId(request.CodProdutoGrupo)
                : new ProdutoGrupo();

            item.Descricao = request.Descricao;

            if (request.CodProdutoGrupo <= 0)
            {
                this.repository.Inserir(item);
            }
            else
            {
                this.repository.Atualizar(item);
            }

            return this.ParaODT(item);
        }

        public void Excluir(int codProdutoGrupo)
        {
            var item = this.repository.ObterPorId(codProdutoGrupo);
            if (item != null)
            {
                this.repository.Excluir(item);
            }
        }

        private ProdutoGrupoODT ParaODT(ProdutoGrupo item)
        {
            return new ProdutoGrupoODT
            {
                CodProdutoGrupo = item.CodProdutoGrupo,
                Descricao = item.Descricao,
            };
        }
    }
}