using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for ResultadosValidarCliente.xaml
    /// </summary>
    public partial class ResultadosValidarCliente : Page
    {
        MainWindow mainWin;
        private Dictionary<string,string> datosCliente;
        public Cliente cliente;
        public ResultadosValidarCliente()
        {
            InitializeComponent();
            Focus();

            


            var fotoCliente = new BitmapImage();
            fotoCliente.BeginInit();
            fotoCliente.UriSource = new Uri("pack://siteoforigin:,,,/fotoCliente.bmp");
            fotoCliente.EndInit();
            imFotoCliente.Source = fotoCliente;

            var firmaCliente = new BitmapImage();
            firmaCliente.BeginInit();
            firmaCliente.UriSource = new Uri("pack://siteoforigin:,,,/firmaCliente.bmp");
            firmaCliente.EndInit();
            imFirma.Source = firmaCliente;


        }
        public ResultadosValidarCliente(MainWindow mainWindow, Cliente clienteR):this()
        {
            mainWin = mainWindow;
            this.cliente = clienteR;

            datosCliente = new Dictionary<string, string>() { { "Cliente:", $"{cliente.Apellidos}, {cliente.Nombres}" },
                { "Cedula:", $"{cliente.Cedula}" }, { "Estado:", "Normal" } };
            dgDatosCliente.ItemsSource = datosCliente;
            var cuentasCliente = new List<string>() { cliente.NumeroCuenta.ToString() };
            dgCuentas.ItemsSource = cuentasCliente;

            if (mainWin.menu.HayConexion)
            {
                estadoConexion.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
            else
            {
                estadoConexion.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }

        private void BtDepositar_Click(object sender, RoutedEventArgs e)
        {
            if(cliente.NumeroCuenta == 0)
            {
                mainWin.transaccion = new Transaccion(mainWin, TipoTransaccion.DepositoFueraLinea);
            }
            else
            {
                mainWin.transaccion = new Transaccion(mainWin, TipoTransaccion.Deposito);
            }
            
            mainWin.transaccion.TipoTrans.Text = "Deposito";
            mainWin.transaccion.tbCliente.Visibility = Visibility.Hidden;
            mainWin.transaccion.TbCedula.Visibility = Visibility.Hidden;
            mainWin.transaccion.tblockCed.Visibility = Visibility.Hidden;
            mainWin.transaccion.tblockCli.Visibility = Visibility.Hidden;
            mainWin.Content = mainWin.transaccion;
        }

        private void BtMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWin.Content = mainWin.menu;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    BtDepositar_Click(null,null);
                    break;
                case Key.F2:
                    BtRetirar_Click(null,null);
                    break;
                case Key.Escape:
                    BtMenu_Click(null, null);
                    break;
                

            }
        }

        private void BtRetirar_Click(object sender, RoutedEventArgs e)
        {

            if (cliente.NumeroCuenta == 0)
            {
                mainWin.transaccion = new Transaccion(mainWin, TipoTransaccion.RetiroFueraLinea);
            }
            else
            {
                mainWin.transaccion = new Transaccion(mainWin, TipoTransaccion.Retiro);
            }

            mainWin.transaccion.TipoTrans.Text = "Retiro";

            mainWin.transaccion.TbCedula.Text = cliente.Cedula;

            mainWin.transaccion.tbCliente.Text = $"{cliente.Apellidos}, {cliente.Nombres}";

            if(cliente.NumeroCuenta != 0)
            {
                mainWin.transaccion.tbNumCuenta.Text = cliente.NumeroCuenta.ToString();
                mainWin.transaccion.tbNumCuenta.IsReadOnly = true;
            }

            mainWin.Content = mainWin.transaccion;
        }
    }
}
