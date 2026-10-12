using AtmServiceDominio.ObjetosDeNegocio;

namespace AtmServiceDominio.ObjetosDeDominio
{
    public interface IAtmObjetoDominio
    {
        string NomePlural { get; set; }
        string NomeSingular { get; set; }

        void Excluir(params object[] id);
        ObjetoDeNegocio Salvar(ObjetoDeNegocio obj);
    }
}