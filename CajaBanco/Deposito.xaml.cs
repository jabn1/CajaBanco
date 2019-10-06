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
    /// Interaction logic for Deposito.xaml
    /// </summary>
    public partial class Deposito : Page
    {
        MainWindow mainWin;
        public Deposito()
        {
            InitializeComponent();
        }
        public Deposito(MainWindow mainWindow) : this()
        {
            this.mainWin = mainWindow;
        }

        private void BtMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWin.Content = mainWin.menu;
        }
    }
}
