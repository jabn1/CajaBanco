using CajaBanco.DataSetDBCajaTableAdapters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TransFuera.xaml
    /// </summary>
    public partial class TransFuera : Page
    {
        MainWindow mainWin;
        ObservableCollection<DatosTransaccion> datos;
        public TransFuera()
        {
            InitializeComponent();
            BtEnviar.Focus();
        }
        public TransFuera(MainWindow mainWindow):this()
        {
            mainWin = mainWindow;


            datos = new ObservableCollection<DatosTransaccion>();
            TransCajaViewTableAdapter transCajaView = new TransCajaViewTableAdapter();
            var rows = transCajaView.GetDataByIdCajero(mainWin.login.idCajeroInt).Rows;
            foreach (TransCajaViewRow record in rows)
            {
                if((int)record["EstadoTransac"] == (int)EstadosTransaccion.PendienteFueraLinea)
                {
                    var t = new DatosTransaccion();
                    t.IdTrans = (int)record["IdTransac"];
                    t.Fecha = (DateTime)record["FechaCreacion"];
                    t.NumeroCuenta = (int)record["NumCuenta"];
                    t.CedulaCliente = record["CedulaCliente"].ToString();
                    t.NombreTipo = record["NombreTipoT"].ToString();
                    t.NombreEstadoTrans = record["NombreEstadoT"].ToString();
                    t.Monto = (decimal)record["Monto"];

                    t.IdCajero = (int)record["IdCajero"];
                    t.TipoTrans = (TipoTransaccion)record["Tipo"];

                    datos.Add(t);
                }
            }




            DgTFL.ItemsSource = datos;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWin.Content = mainWin.menu;
        }




        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    BtEnviar_Click(null,null);
                    break;
               

                case Key.Escape:
                    Button_Click(null, null);
                    break;

            }
        }

        private void BtEnviar_Click(object sender, RoutedEventArgs e)
        {
            mainWin.menu.RevisarConexion();
            if (mainWin.menu.HayConexion)
            {
                TransacCajaTableAdapter transacCaja = new TransacCajaTableAdapter();
                foreach(var trans in datos)
                {
                    EstadosTransaccion newEstado;
                    //Aqui va el codigo para enviar todas las transacciones fuera de linea mediante el web service de capa integracion
                    transacCaja.UpdateQueryTransacCaja((int)newEstado,trans.IdTrans);

                }
            }
            else
            {
                MessageBox.Show("El servicio no esta disponible acualmente. Intentar mas tarde.");
            }
        }
    }
}
