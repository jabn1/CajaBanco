using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajaBanco
{
    public enum TipoTransaccion
    {
        Deposito = 1,
        Retiro = 2,
        DepositoFueraLinea = 3,
        RetiroFueraLinea = 4
    }
    public enum EstadosTransaccion
    {
        Exitosa = 1,
        CuentaSinFondos = 2,
        CuentaNoExiste = 3,
        TransFueraLineaInconsistente = 4,
        PendienteFueraLinea = 5,
        CedulaSinPermiso = 6


    }
    public class DatosTransaccion
    {
        
        private EstadosTransaccion estadoTrans;
        private TipoTransaccion tipoTrans;
        private int numeroTransaccion;
        private int numeroCuenta;
        private string nombreClienteCuenta, apellidoClienteCuenta;
        private decimal monto;
        private string cedulaCliente;

        public EstadosTransaccion EstadoTrans { get => estadoTrans; set => estadoTrans = value; }
        public TipoTransaccion TipoTrans { get => tipoTrans; set => tipoTrans = value; }
        public int NumeroTransaccion { get => numeroTransaccion; set => numeroTransaccion = value; }
        public string NombreClienteCuenta { get => nombreClienteCuenta; set => nombreClienteCuenta = value; }
        public string ApellidoClienteCuenta { get => apellidoClienteCuenta; set => apellidoClienteCuenta = value; }
        public decimal Monto { get => monto; set => monto = value; }
        public string CedulaCliente { get => cedulaCliente; set => cedulaCliente = value; }
        public int NumeroCuenta { get => numeroCuenta; set => numeroCuenta = value; }
    }
}
