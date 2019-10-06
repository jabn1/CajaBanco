using CajaBanco.DataSetDBCajaTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for EstadoCaja.xaml
    /// </summary>
    public partial class EstadoCaja : Page
    {
        MainWindow mainWin;
        public EstadoCaja()
        {
            InitializeComponent();

            EstadoCajaTableAdapter estadoCajaTableAdapter = new EstadoCajaTableAdapter();
            //var datos = estadoCajaTableAdapter.GetData().Rows;
            var datos = new DataSetDBCaja.EstadoCajaDataTable();
            DgEstado.ItemsSource = datos;
        }
        public EstadoCaja(MainWindow mainWindow) : this()
        {
            mainWin = mainWindow;
        }

        private void BtCliente_Click(object sender, RoutedEventArgs e)
        {
            mainWin.Content = mainWin.menu;
        }
    }
}
