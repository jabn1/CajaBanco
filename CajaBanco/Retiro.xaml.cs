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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Retiro : Page
    {
        MainWindow mainWin;
        public Retiro()
        {
            InitializeComponent();
        }
        public Retiro(MainWindow mainWindow): this()
        {
            mainWin = mainWindow;
        }

        private void BtMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWin.Content = mainWin.menu;
        }
    }
}
