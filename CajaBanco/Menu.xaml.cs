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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        MainWindow mainWin;

        public Menu()
        {
            InitializeComponent();
            Focus();
            
        }
        
        public Menu(MainWindow mainWindow) : this()
        {
            this.mainWin = mainWindow;
            
            tbCajero.Text = mainWin.login.idCajero;
            tbSucursal.Text = mainWin.login.nomSucursal;
        }

        private void BtValidarCliente_Click(object sender, RoutedEventArgs e)
        {
            mainWin.valCliente = new ValidarCliente(mainWin);
            mainWin.Content = mainWin.valCliente;
        }

        private void BtCerrar_Click(object sender, RoutedEventArgs e)
        {
            mainWin.login = new Login(mainWin);
            mainWin.Content = mainWin.login;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    BtInicioDia_Click(null,null);
                    break;
                case Key.F2:
                    BtCierreDia_Click(null,null);
                    break;
                case Key.F3:
                    BtValidarCliente_Click(null,null);
                    break;
                case Key.F4:

                    break;
                case Key.F5:

                    break;
                
                case Key.Escape:
                    BtCerrar_Click(null,null);
                    break;

            }
        }

        private void BtInicioDia_Click(object sender, RoutedEventArgs e)
        {
            mainWin.inicioDia = new InicioDia(mainWin, TiposAccionDia.IncioDia);

            mainWin.Content = mainWin.inicioDia;
        }

        private void BtCierreDia_Click(object sender, RoutedEventArgs e)
        {
            mainWin.inicioDia = new InicioDia(mainWin, TiposAccionDia.CierreDia);

            mainWin.Content = mainWin.inicioDia;
        }

        private void BtEstadoCaja_Click(object sender, RoutedEventArgs e)
        {
            mainWin.estado = new EstadoCaja(mainWin);
            mainWin.Content = mainWin.estado;
        }
    }
}
