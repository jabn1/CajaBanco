using CajaBanco.DataSetDBCajaTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CajaBanco
{
    /// <summary>
    /// Interaction logic for Transaccion.xaml
    /// </summary>
    public partial class Transaccion : Page
    {
        MainWindow mainWin;
        TipoTransaccion tipo;
        DatosTransaccion datosTransaccion;
        private int totalBM;
        private Dictionary<string, int> listaBM;
        private Dictionary<string, int> valoresBM;
        private bool started = false;
        public DatosReporteTrans datosRep = new DatosReporteTrans();
        bool transCompleted = false;
        public Transaccion()
        {
            InitializeComponent();
            totalBM = 0;
            //Focus();
            tbNumCuenta.Focus();
            listaBM = new Dictionary<string, int>();
            valoresBM = new Dictionary<string, int>();
            var denominaciones = new List<int>() { 2000, 1000, 500, 200, 100, 50, 25, 10, 5, 1 };
            var llaves = new List<string>() { "tb2000p", "tb1000p", "tb500p", "tb200p", "tb100p", "tb50p", "tb25p", "tb10p", "tb5p", "tb1p" };
            int count = 0;
            foreach (var llave in llaves)
            {
                listaBM.Add(llave, 0);
                valoresBM.Add(llave, denominaciones[count++]);
            }
            started = true;
        }
        public Transaccion(MainWindow mainWindow, TipoTransaccion tipoTrans) : this()
        {
            mainWin = mainWindow;
            tipo = tipoTrans;
        }

        private void BtCliente_Click(object sender, RoutedEventArgs e)
        {
            if (transCompleted) return;
            mainWin.Content = mainWin.resCliente;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.F10)
            {
                KeyPressFxx(tb1p);
                e.Handled = true;
                return;
            }
            switch (e.Key)
            {

                case Key.F1:
                    KeyPressFxx(tb2000p);
                    break;
                case Key.F2:
                    KeyPressFxx(tb1000p);
                    break;
                case Key.F3:
                    KeyPressFxx(tb500p);
                    break;
                case Key.F4:
                    KeyPressFxx(tb200p);
                    break;
                case Key.F5:
                    KeyPressFxx(tb100p);
                    break;
                case Key.F6:
                    KeyPressFxx(tb50p);
                    break;
                case Key.F7:
                    KeyPressFxx(tb25p);
                    break;
                case Key.F8:
                    KeyPressFxx(tb10p);
                    break;
                case Key.F9:
                    KeyPressFxx(tb5p);
                    break;
                case Key.F10:
                    KeyPressFxx(tb1p);
                    break;
                case Key.F11:
                    BtRealizarTrans_Click(null,null);
                    break;
                case Key.F12:
                    BtReporteTrans_Click(null,null);
                    break;
                case Key.Escape:
                    BtCliente_Click(null, null);
                    break;

            }
        }


        private void UpdateTotalBm()
        {
            totalBM = 0;
            foreach (var elmt in listaBM)
            {
                totalBM += elmt.Value * valoresBM[elmt.Key];
            }
            tbTotalBM.Text = totalBM.ToString();
        }

        private void KeyPressFxx(TextBox textBox)
        {
            textBox.Focus();
            textBox.CaretIndex = int.MaxValue;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                
                textBox.Text = Convert.ToString(Convert.ToInt32(textBox.Text) + 1);
            }
            else if(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (Convert.ToInt32(textBox.Text) <= 0) { return; }
                textBox.Text = Convert.ToString(Convert.ToInt32(textBox.Text) - 1);
            }
        }

        private void Efectivo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (started)
            {
                var tbSender = sender as TextBox;
                int value = 0;
                if (int.TryParse(tbSender.Text, out value) && value >= 0)
                {
                    listaBM[tbSender.Name] = value;
                    tbSender.Text = value.ToString();
                    if(tipo == TipoTransaccion.Retiro || tipo == TipoTransaccion.RetiroFueraLinea)
                    {
                        var ef = mainWin.menu.EfectivoCaja;
                        if (ef.Bm2000 < int.Parse(tb2000p.Text) ||
                            ef.Bm1000 < int.Parse(tb1000p.Text) ||
                            ef.Bm500 < int.Parse(tb500p.Text) ||
                            ef.Bm200 < int.Parse(tb200p.Text) ||
                            ef.Bm100 < int.Parse(tb100p.Text) ||
                            ef.Bm50 < int.Parse(tb50p.Text) ||
                            ef.Bm25 < int.Parse(tb25p.Text) ||
                            ef.Bm10 < int.Parse(tb10p.Text) ||
                            ef.Bm5 < int.Parse(tb5p.Text) ||
                            ef.Bm1 < int.Parse(tb1p.Text))
                        {
                            tbSender.Text = "0";
                            MessageBox.Show($"La caja no posee suficientes unidades de RD$ {valoresBM[tbSender.Name]}");
                            return;
                        }
                    }
                }
                else { tbSender.Text = "0"; }
                tbSender.CaretIndex = int.MaxValue;
                UpdateTotalBm();
            }
                        
        }


        public DatosTransaccion RealizarTransaccion(string cedula, int numeroCuenta, decimal monto, TipoTransaccion tipoTrans)
        {
            //mainWin.menu.RevisarConexion();
            var trans = new DatosTransaccion();
            if (mainWin.menu.HayConexion)
            {
                // Aqui va el codigo de realizar transaccion cuando hay conexion con la capa de integracion
                // Los datos se obtienen con el web service de la capa de integracion
            }
            else
            {
                // Aqui va el codigo para realizar transaccion fuera de linea
                // Los datos se ponen por defecto
                trans.EstadoTrans = EstadosTransaccion.PendienteFueraLinea;
                trans.NumeroTransaccion = 0;
                trans.TipoTrans = tipoTrans;
                trans.NumeroCuenta = numeroCuenta;
                trans.Monto = monto;
                trans.CedulaCliente = cedula;
                trans.ApellidoClienteCuenta = "RAMIREZ MATEO";
                trans.NombreClienteCuenta = "PEDRO MANUEL";
            }
            return trans;
        }

        private void BtReporteTrans_Click(object sender, RoutedEventArgs e)
        {
            if (!transCompleted) return;

            mainWin.reporte = new Reporte(mainWin, TiposReporte.TransaccionBancaria);
            mainWin.Content = mainWin.reporte;

        }

        private void BtRealizarTrans_Click(object sender, RoutedEventArgs e)
        {
            if (transCompleted) return;
            int numeroCuenta;
            if (!int.TryParse(tbNumCuenta.Text,out numeroCuenta))
            {
                MessageBox.Show("Numero de cuenta con formato incorrecto.");
                return;
            }
            decimal monto;
            
            if(decimal.TryParse(TbMonto.Text,out monto))
            {
                if(totalBM != Convert.ToInt32(monto))
                {
                    MessageBox.Show("El Monto RD$ de ser igual al Total(Billetes y Monedas) RD$. Verificar");
                    return;
                }
                
            }
            else
            {
                MessageBox.Show("Monto RD$ con formato incorrecto.");
                return;
            }
            monto += 0.00M;
            try
            {
                datosTransaccion = RealizarTransaccion(mainWin.resCliente.cliente.Cedula, numeroCuenta, monto, tipo);
                transCompleted = true;
                if(datosTransaccion.EstadoTrans == EstadosTransaccion.Exitosa)
                {
                    TbMensaje.Text = "Transaccion exitosa.";
                    MessageBox.Show("Transaccion exitosa.");
                }
                else if (datosTransaccion.EstadoTrans == EstadosTransaccion.PendienteFueraLinea)
                {
                    TbMensaje.Text = "Transaccion fuera de linea exitosa.";
                    MessageBox.Show("Transaccion fuera de linea exitosa.");
                }
                else
                {
                    if (datosTransaccion.EstadoTrans == EstadosTransaccion.CuentaSinFondos)
                    {
                        TbMensaje.Text = "Cuenta sin fondos suficientes.";
                    }
                    else if (datosTransaccion.EstadoTrans == EstadosTransaccion.CuentaNoExiste)
                    {
                        TbMensaje.Text = "Cuenta no existe.";
                    }
                    if (datosTransaccion.EstadoTrans == EstadosTransaccion.CedulaSinPermiso)
                    {
                        TbMensaje.Text = "Cedula no autorizada para transaccion.";
                    }
                    MessageBox.Show("Transaccion irregular. Revisar mensaje.");
                }
                
            }
            catch
            {
                MessageBox.Show("Error procesando la transaccion.");
                MainWindow.log.Error($"Error durante la operacion RealizarTransaccion. " +
                    $"Cedula: {mainWin.resCliente.cliente.Cedula}, Numero Cuenta {numeroCuenta}, Monto: {monto},Tipo: {tipo}");
                return;
            }


            var BMTrans = new EfectivoEnCaja();
            BMTrans.InsertEfectivo(listaBM);

            var ef = mainWin.menu.EfectivoCaja;
            if (tipo == TipoTransaccion.Deposito || tipo == TipoTransaccion.DepositoFueraLinea)
            {

               
                ef.TotalCaja += monto;

                ef.Bm2000 += BMTrans.Bm2000;
                ef.Bm1000 += BMTrans.Bm1000;
                ef.Bm500 += BMTrans.Bm500;
                ef.Bm200 += BMTrans.Bm200;
                ef.Bm100 += BMTrans.Bm100;
                ef.Bm50 += BMTrans.Bm50;
                ef.Bm25 += BMTrans.Bm25;
                ef.Bm10 += BMTrans.Bm10;
                ef.Bm5 += BMTrans.Bm5;
                ef.Bm1 += BMTrans.Bm1;
            }
            else if(tipo == TipoTransaccion.Retiro || tipo == TipoTransaccion.RetiroFueraLinea)
            {
                
                ef.TotalCaja -= monto;

                ef.Bm2000 -= BMTrans.Bm2000;
                ef.Bm1000 -= BMTrans.Bm1000;
                ef.Bm500 -= BMTrans.Bm500;
                ef.Bm200 -= BMTrans.Bm200;
                ef.Bm100 -= BMTrans.Bm100;
                ef.Bm50 -= BMTrans.Bm50;
                ef.Bm25 -= BMTrans.Bm25;
                ef.Bm10 -= BMTrans.Bm10;
                ef.Bm5 -= BMTrans.Bm5;
                ef.Bm1 -= BMTrans.Bm1;
            }

            if (tipo == TipoTransaccion.Deposito || tipo == TipoTransaccion.DepositoFueraLinea)
            {
                datosRep.titulo = "DEPOSITO A CUENTA";
                datosRep.tipototal = "DEPOSITADO";
            }
            else if (tipo == TipoTransaccion.Retiro || tipo == TipoTransaccion.RetiroFueraLinea)
            {
                datosRep.titulo = "RETIRO DE CUENTA";
                datosRep.tipototal = "RETIRADO";
            }
            datosRep.sucursal = mainWin.login.suc;
            datosRep.idCajero = mainWin.login.idCajeroInt;
            datosRep.fecha = DateTime.Now;
            datosRep.cuenta = datosTransaccion.NumeroCuenta;
            datosRep.nombre = datosTransaccion.ApellidoClienteCuenta + ", " + datosTransaccion.NombreClienteCuenta;
            datosRep.monto = monto.ToString();

            


            
            TransacCajaTableAdapter transacCaja = new TransacCajaTableAdapter();
            EstadoCajaTableAdapter estadoCaja = new EstadoCajaTableAdapter();
            MovimientosCajaTableAdapter movimientos = new MovimientosCajaTableAdapter();
            int idTransac = 0;
            int idDia = ef.IdDia;
            int idCajero = mainWin.login.idCajeroInt;
            decimal totalCaja = ef.TotalCaja;
            TiposAccion tipoAccion = 0;
            if (tipo == TipoTransaccion.Deposito) tipoAccion = TiposAccion.Deposito;
            else if (tipo == TipoTransaccion.Retiro) tipoAccion = TiposAccion.Retiro;
            else if (tipo == TipoTransaccion.DepositoFueraLinea) tipoAccion = TiposAccion.DepositoFueraLinea;
            else if (tipo == TipoTransaccion.RetiroFueraLinea) tipoAccion = TiposAccion.RetiroFueraLinea;

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {

                    int idEstado = (int)estadoCaja.InsertReturnIdEstado(
                        idDia, DateTime.Now, (int)tipoAccion, idCajero, totalCaja, ef.Bm2000, 
                        ef.Bm1000, ef.Bm500, ef.Bm200, ef.Bm100, ef.Bm50, ef.Bm25, ef.Bm10, ef.Bm5, ef.Bm1);


                    idTransac = (int)transacCaja.InsertTransacReturnId(
                        (int)datosTransaccion.NumeroTransaccion, monto,(int)tipo, DateTime.Now, datosTransaccion.CedulaCliente,
                        (int)datosTransaccion.EstadoTrans, (int)datosTransaccion.NumeroCuenta);

                    movimientos.Insert(
                        idDia, idCajero, DateTime.Now, (int)tipoAccion, BMTrans.Bm2000, BMTrans.Bm1000, BMTrans.Bm500,
                        BMTrans.Bm200, BMTrans.Bm100, BMTrans.Bm50, BMTrans.Bm25, BMTrans.Bm10, BMTrans.Bm5, BMTrans.Bm1,
                        idTransac, idEstado, monto, datosTransaccion.NumeroTransaccion);

                    ts.Complete();
                }
                catch
                {
                    MessageBox.Show("Error guardando registros.");
                    MainWindow.log.Error("Error de transaccion durante operaciones INSERT en EstadosCaja, MovimientosCaja y TransacCaja"+
                         $"Cedula: {mainWin.resCliente.cliente.Cedula}, Numero Cuenta {numeroCuenta}, Monto: {monto},Tipo: {tipo}");
                }
            }

            if (datosTransaccion.NumeroTransaccion == 0)
            {
                if(idTransac == 0)
                {
                    datosRep.idNoTrans = "---------";
                }
                else
                {
                    datosRep.idNoTrans = "C-" + idTransac.ToString();
                }
                
            }
            else
            {
                datosRep.idNoTrans = datosTransaccion.NumeroTransaccion.ToString();
            }
            transCompleted = true;
        }
    }
}
