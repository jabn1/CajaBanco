using CajaBanco.DataSetDBCajaTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
    /// Interaction logic for InicioDia.xaml
    /// </summary>
    /// 

    
    public partial class InicioDia : Page
    {

        MainWindow mainWin;
        private int totalBM;
        private Dictionary<string, int> listaBM;
        private Dictionary<string, int> valoresBM;
        private bool started = false;

        public InicioDia()
        {
            InitializeComponent();
            Focus();
            listaBM = new Dictionary<string, int>();
            valoresBM = new Dictionary<string, int>();
            var denominaciones = new List<int>() { 2000, 1000, 500, 200, 100, 50, 25, 10, 5, 1 };
            var llaves = new List<string>() { "tb2000p", "tb1000p", "tb500p", "tb200p", "tb100p", "tb50p", "tb25p", "tb10p", "tb5p", "tb1p" };
            int count = 0;
            foreach (var llave in llaves)
            {
                listaBM.Add(llave, 0);
                valoresBM.Add(llave, denominaciones[count++]);
            }
            started = true;
        }
        public InicioDia(MainWindow mainWindow):this()
        {
            mainWin = mainWindow;
            tbCajero.Text = mainWin.login.idCajero;
            tbSucursal.Text = mainWin.login.nomSucursal;
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.F10)
            {
                KeyPressFxx(tb1p);
                e.Handled = true;
                return;
            }
            switch (e.Key)
            {

                case Key.F1:
                    KeyPressFxx(tb2000p);
                    break;
                case Key.F2:
                    KeyPressFxx(tb1000p);
                    break;
                case Key.F3:
                    KeyPressFxx(tb500p);
                    break;
                case Key.F4:
                    KeyPressFxx(tb200p);
                    break;
                case Key.F5:
                    KeyPressFxx(tb100p);
                    break;
                case Key.F6:
                    KeyPressFxx(tb50p);
                    break;
                case Key.F7:
                    KeyPressFxx(tb25p);
                    break;
                case Key.F8:
                    KeyPressFxx(tb10p);
                    break;
                case Key.F9:
                    KeyPressFxx(tb5p);
                    break;
                case Key.F10:
                    KeyPressFxx(tb1p);
                    break;
                case Key.F11:
                    BtIniciar_Click(null, null);
                    break;
                case Key.Escape:
                    BtCliente_Click(null, null);
                    break;

            }
        }
        private void UpdateTotalBm()
        {
            totalBM = 0;
            foreach (var elmt in listaBM)
            {
                totalBM += elmt.Value * valoresBM[elmt.Key];
            }
            tbTotalBM.Text = totalBM.ToString();
        }
        private void KeyPressFxx(TextBox textBox)
        {
            textBox.Focus();
            textBox.CaretIndex = int.MaxValue;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {

                textBox.Text = Convert.ToString(Convert.ToInt32(textBox.Text) + 1);
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (Convert.ToInt32(textBox.Text) <= 0) { return; }
                textBox.Text = Convert.ToString(Convert.ToInt32(textBox.Text) - 1);
            }
        }
        private void BtCliente_Click(object sender, RoutedEventArgs e)
        {
            mainWin.Content = mainWin.menu;
        }
        private void Efectivo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (started)
            {
                var tbSender = sender as TextBox;
                int value = 0;
                if (int.TryParse(tbSender.Text, out value) && value >= 0)
                {
                    listaBM[tbSender.Name] = value;
                    tbSender.Text = value.ToString();

                }
                else { tbSender.Text = "0"; }
                tbSender.CaretIndex = int.MaxValue;
                UpdateTotalBm();
            }

        }

        private void BtIniciar_Click(object sender, RoutedEventArgs e)
        {
            
            DiasCajaTableAdapter diasCaja = new DiasCajaTableAdapter();
            EstadoCajaTableAdapter estadoCaja = new EstadoCajaTableAdapter();
            decimal totalCaja;
            int idCajero;
            using (TransactionScope ts = new TransactionScope())
            {
                                       
                try
                {
                    mainWin.menu.EfectivoCaja = new EfectivoEnCaja();
                    mainWin.menu.EfectivoCaja.InsertEfectivo(listaBM);
                    var ef = mainWin.menu.EfectivoCaja;
                    idCajero = mainWin.login.idCajeroInt;
                    totalCaja = Convert.ToDecimal(totalBM);
                    int idDia = (int)diasCaja.InsertDiasCajaGetId(DateTime.Now, DateTime.Now, Convert.ToDecimal(totalBM), totalCaja,idCajero, 1);
                    estadoCaja.Insert(idDia,DateTime.Now,(int)TiposAccion.InicioDelDia,idCajero,totalCaja,ef.Bm2000,ef.Bm1000,ef.Bm500,ef.Bm200,ef.Bm100,ef.Bm50,ef.Bm25,ef.Bm10,ef.Bm5,ef.Bm1);
                    mainWin.menu.HayDiaIniciado = true;
                    mainWin.menu.EfectivoCaja.IdDia = idDia;
                    mainWin.menu.EfectivoCaja.TotalCaja = totalCaja;
                    MessageBox.Show("La operacion se realizo exitosamente.");
                    mainWin.Content = mainWin.menu;
                    ts.Complete();
                }
                catch
                {
                    MessageBox.Show("La operacion no pudo ser realizada.");
                    MainWindow.log.Info("Error en transaccion incio de dia");
                }                                     
            }
            



        }
    }
}
