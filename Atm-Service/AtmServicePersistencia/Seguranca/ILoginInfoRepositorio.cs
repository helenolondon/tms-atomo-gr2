using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra.Seguranca
{
    public interface ILoginInfoRepositorio: IRepositorio<LoginInfoODP>
    {
        LoginInfoODP ObterLoginInfoPorLogin(string login);
    }
}
