using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Utils
{
    internal static class StringExtesions
    {
        public static String Numeros(this String t)
        {
            string ret = null;

            if(t == null)
            {
                return null;
            };

            foreach(var c in t)
            {
                if ("0123456789".IndexOf(c) != -1)
                {
                    ret += c;
                }
            }

            return ret;
        }
        
        public static String FormataCpf(this String t)
        {
            string ret = null;
            string cpf = t.Numeros();
            
            if(cpf.Length == 11)
            {
                ret = cpf.Substring(0, 3) + "." + cpf.Substring(3, 3) + "." + cpf.Substring(6, 3) + "-" + cpf.Substring(9, 2);
            }

            return ret;
        }
        
        public static String FormataCnpj(this String t)
        {
            string ret = null;
            string cnpj = t.Numeros();

            //03 778 130 000 148
            //03.778.130/0001-48

            if (cnpj.Length == 14)
            {
                ret = cnpj.Substring(0, 2) + "." + cnpj.Substring(2, 3) + "." + cnpj.Substring(5, 3) + "/" + cnpj.Substring(8, 4) + "-" + cnpj.Substring(12, 2);
            }

            return ret;
        }

    }
}
