using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AtmServiceDominio.ODN.Tipos.Pessoas
{
    public enum EstadoCivil: int
    {
        None = -1,
        Solteiro = 1,
        Casado = 2,
        Divorciado = 3,
        Viuvo = 4,
        Outros = 5       
    }
}
