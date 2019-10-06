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
        public ResultadosValidarCliente()
        {
            InitializeComponent();
            Focus();

            datosCliente = new Dictionary<string, string>() { {"Cliente:","Juan Perez" },{"Cedula:","0019999992" },{ "Estado:","Normal"} };
            dgDatosCliente.ItemsSource = datosCliente;
            var cuentasCliente = new List<string>() { "000100001", "000100002", "000100003" };
            dgCuentas.ItemsSource = cuentasCliente;


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
        public ResultadosValidarCliente(MainWindow mainWindow):this()
        {
            mainWin = mainWindow;
        }

        private void BtDepositar_Click(object sender, RoutedEventArgs e)
        {
            mainWin.transaccion = new Transaccion(mainWin);
            mainWin.transaccion.TipoTrans.Text = "Deposito";
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
            mainWin.transaccion = new Transaccion(mainWin);
            mainWin.transaccion.TipoTrans.Text = "Retiro";
            mainWin.Content = mainWin.transaccion;
        }
    }
}
