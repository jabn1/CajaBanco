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
    /// Interaction logic for AutenticarCliente.xaml
    /// </summary>
    public partial class ValidarCliente : Page
    {
        MainWindow mainWin;
        public ValidarCliente()
        {
            InitializeComponent();
            tbCedula.Focus();
        }
        public ValidarCliente(MainWindow mainWindow):this()
        {
            mainWin = mainWindow;
        }

        private void BtProcesar_Click(object sender, RoutedEventArgs e)
        {
            mainWin.resCliente = new ResultadosValidarCliente(mainWin);
            mainWin.Content = mainWin.resCliente;
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
                    BtProcesar_Click(null,null);
                    break;
                case Key.Escape:
                    BtMenu_Click(null,null);
                    break;
                

            }
        }
    }
}
