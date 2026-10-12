using AtmServiceDominio.ODN;
using AtmServiceInfra.ODP;
using System.Collections.Generic;

namespace AtmServiceDominio.Utils
{
    internal static class UtilsExtension
    {
        internal static List<ClienteODN> FromListaODP(this List<ClienteODN> t, List<ClienteODP> origem)
        {
            var temp = new List<ClienteODN>();

            foreach (ClienteODP usu in origem)
            {
                var item = new ClienteODN();

                item.FromODP(usu);

                temp.Add(item);
            }

            return temp;
        }

        internal static List<CidadeODN> FromListaODP(this List<CidadeODN> t, List<CidadeODP> origem)
        {
            var temp = new List<CidadeODN>();

            foreach (CidadeODP usu in origem)
            {
                var item = new CidadeODN();

                item.FromODP(usu);

                temp.Add(item);
            }

            return temp;
        }

        internal static List<EstadoODN> FromListaODP(this List<EstadoODN> t, List<EstadoODP> origem)
        {
            var temp = new List<EstadoODN>();

            foreach (EstadoODP usu in origem)
            {
                var item = new EstadoODN();

                item.FromODP(usu);

                temp.Add(item);
            }

            return temp;
        }

        internal static List<UsuarioODN> FromListaODP(this List<UsuarioODN> t, List<UsuarioODP> origem)
        {
            var temp = new List<UsuarioODN>();

            foreach (UsuarioODP usu in origem)
            {
                var item = new UsuarioODN();

                item.FromODP(usu);

                temp.Add(item);
            }

            return temp;
        }

        internal static List<ContatoODN> FromListaODP(this List<ContatoODN> t, List<ContatoODP> origem)
        {
            var temp = new List<ContatoODN>();

            foreach (ContatoODP cnt in origem)
            {
                var item = new ContatoODN();

                item.FromODP(cnt);

                temp.Add(item);
            }

            return temp;
        }
    }
}
