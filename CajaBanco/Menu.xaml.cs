using CajaBanco.DataSetDBCajaTableAdapters;
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
using static CajaBanco.DataSetDBCaja;

namespace CajaBanco
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        MainWindow mainWin;
        bool hayDiaIniciado;
        EfectivoEnCaja efectivoCaja;

        public EfectivoEnCaja EfectivoCaja { get => efectivoCaja; set => efectivoCaja = value; }
        public bool HayDiaIniciado { get => hayDiaIniciado; set => hayDiaIniciado = value; }

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

            DiasCajaTableAdapter diasCaja = new DiasCajaTableAdapter();
            var datos = diasCaja.GetDataByLastIdCaja((int)mainWin.login.idCajeroInt).Rows;
            if (datos.Count == 0)
            {
                HayDiaIniciado = false;
            }
            else
            {
                var dia = (DiasCajaRow)datos[0];
                if ((int)dia["EstadoDia"] == (int)TiposEstadoDia.Iniciado)
                {
                    HayDiaIniciado = true;
                    int idDia = (int)dia["IdDia"];
                    EstadoCajaTableAdapter estadoCaja = new EstadoCajaTableAdapter();
                    var estado = (EstadoCajaRow)estadoCaja.GetDataByLastDia(idDia).Rows[0];
                    var ef = new EfectivoEnCaja();
                    ef.IdDia = idDia;
                    ef.TotalCaja = (decimal)estado["TotalCaja"];
                    ef.Bm2000 = (int)estado["E2000"];
                    ef.Bm1000 = (int)estado["E1000"];
                    ef.Bm500 = (int)estado["E500"];
                    ef.Bm200 = (int)estado["E200"];
                    ef.Bm100 = (int)estado["E100"];
                    ef.Bm50 = (int)estado["E50"];
                    ef.Bm25 = (int)estado["E25"];
                    ef.Bm10 = (int)estado["E10"];
                    ef.Bm5 = (int)estado["E5"];
                    ef.Bm1 = (int)estado["E1"];
                    efectivoCaja = ef;
                }
                else
                {
                    HayDiaIniciado = false;
                }
            }


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
            if (HayDiaIniciado)
            {
                MessageBox.Show("Ya existe un dia iniciado.");
                return;
            }
            mainWin.inicioDia = new InicioDia(mainWin);

            mainWin.Content = mainWin.inicioDia;
        }

        private void BtCierreDia_Click(object sender, RoutedEventArgs e)
        {
            if (!HayDiaIniciado)
            {
                MessageBox.Show("No existe un dia iniciado.");
                return;
            }
            mainWin.cierreDia = new CierreDia(mainWin);
            mainWin.Content = mainWin.cierreDia;
        }

        private void BtEstadoCaja_Click(object sender, RoutedEventArgs e)
        {
            if (!HayDiaIniciado)
            {
                MessageBox.Show("No existe un dia iniciado.");
                return;
            }
            mainWin.estado = new EstadoCaja(mainWin);
            mainWin.Content = mainWin.estado;
        }
    }
}
