using CajaBanco.DataSetDBCajaTableAdapters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static CajaBanco.DataSetDBCaja;

namespace CajaBanco
{
    /// <summary>
    /// Interaction logic for EstadoCaja.xaml
    /// </summary>
    public partial class EstadoCaja : Page
    {
        MainWindow mainWin;
        public ObservableCollection<EfectivoEnCaja> datos;
        public EstadoCaja()
        {
            InitializeComponent();

            //EstadoCajaTableAdapter estadoCajaTableAdapter = new EstadoCajaTableAdapter();
            //var datos = estadoCajaTableAdapter.GetData().Rows;
            //var datos = new DataSetDBCaja.EstadoCajaDataTable();

            //var datos = (EstadoCajaDataTable)estadoCajaTableAdapter.GetDataByLastDia(mainWin.menu.EfectivoCaja.IdDia);
        }
        public EstadoCaja(MainWindow mainWindow) : this()
        {
            mainWin = mainWindow;

            tbCajero.Text = mainWin.login.idCajero;
            tbSucursal.Text = mainWin.login.nomSucursal;

            datos = new ObservableCollection<EfectivoEnCaja>() { mainWin.menu.EfectivoCaja };

            DgEstado.ItemsSource = datos;
        }

        private void BtCliente_Click(object sender, RoutedEventArgs e)
        {
            mainWin.Content = mainWin.menu;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {

                case Key.Escape:
                    BtCliente_Click(null, null);
                    break;

            }
        }
    }
}
