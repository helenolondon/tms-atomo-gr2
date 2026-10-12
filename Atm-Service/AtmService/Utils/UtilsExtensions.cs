using AtmService.ODT;
using AtmServiceDominio.ODN;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Utils
{
    public static class UtilsExtensions
    {
        public static List<BairroODT> FromListaODN(this List<BairroODT> t, List<BairroODN> origem)
        {
            var temp = new List<BairroODT>();

            foreach (BairroODN usu in origem)
            {
                temp.Add(BairroODT.NewFromODN(usu));
            }

            return temp;
        }
        
        public static void FromListaODN(this List<ClienteODT> t, List<ClienteODN> origem)
        {
            t.Clear();

            foreach (ClienteODN cli in origem)
            {
                var odt = new ClienteODT();
                odt.FromODN(cli);

                t.Add(odt);
            }
        }

        public static void FromListaODN(this List<CnaeODT> t, List<CnaeODN> origem)
        {
            t.Clear();

            foreach (CnaeODN cn in origem)
            {
                t.Add(CnaeODT.NewFromODN(cn));
            }
        }

        public static List<CidadeODT> FromListaODN(this List<CidadeODT> t, List<CidadeODN> origem)
        {
            var temp = new List<CidadeODT>();

            foreach (CidadeODN usu in origem)
            {
                temp.Add(CidadeODT.NewFromODN(usu));
            }

            return temp;
        }

        public static List<EstadoODT> FromListaODN(this List<EstadoODT> t, List<EstadoODN> origem)
        {
            var temp = new List<EstadoODT>();

            foreach (EstadoODN usu in origem)
            {
                temp.Add(EstadoODT.NewFromODN(usu));
            }

            return temp;
        }

        public static List<UsuarioODT> FromListaODN(this List<UsuarioODT> t, List<UsuarioODN> origem)
        {
            var temp = new List<UsuarioODT>();

            foreach (UsuarioODN usu in origem)
            {
                temp.Add(UsuarioODT.NewFromODN(usu));
            }

            return temp;
        }

        public static List<ContatoODT> FromListaODN(this List<ContatoODT> t, List<ContatoODN> origem)
        {
            var temp = new List<ContatoODT>();

            foreach (ContatoODN cnt in origem)
            {
                temp.Add(ContatoODT.NewFromODN(cnt));
            }

            return temp;
        }
    }
}
