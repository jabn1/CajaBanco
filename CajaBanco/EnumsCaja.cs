using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajaBanco
{
    public enum TiposAccion
    {
        InicioDelDia = 1,
        CierreDelDia = 2
    }
    public enum TiposEstadoDia
    {
        Iniciado = 1,
        Finalizado = 2
    }
    public enum TiposReporte
    {
        InicioDia = 1,
        CierreDia = 2,
        TransaccionBancaria = 3
    }
}
