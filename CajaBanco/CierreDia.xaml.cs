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
    /// Interaction logic for CierreDia.xaml
    /// </summary>
    public partial class CierreDia : Page
    {
        MainWindow mainWin;

        public CierreDia()
        {
            InitializeComponent();
        }
        public CierreDia(MainWindow mainWindow):this()
        {
            mainWin = mainWindow;
        }

        private void BtConfirmarCierre_Click(object sender, RoutedEventArgs e)
        {
            DiasCajaTableAdapter diasCaja = new DiasCajaTableAdapter();
            EstadoCajaTableAdapter estadoCaja = new EstadoCajaTableAdapter();
            var ef = mainWin.menu.EfectivoCaja;
            int idDia = ef.IdDia;
            int idCajero = mainWin.login.idCajeroInt;
            decimal totalCaja = ef.TotalCaja;

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    diasCaja.UpdateCierreDia(DateTime.Now,ef.TotalCaja,(int)TiposEstadoDia.Finalizado,ef.IdDia);

                    estadoCaja.Insert(idDia, DateTime.Now, (int)TiposAccion.CierreDelDia, idCajero, totalCaja, ef.Bm2000, ef.Bm1000, ef.Bm500, ef.Bm200, ef.Bm100, ef.Bm50, ef.Bm25, ef.Bm10, ef.Bm5, ef.Bm1);


                    mainWin.reporte = new Reporte(mainWin, TiposReporte.CierreDia);
                    mainWin.menu.HayDiaIniciado = false;
                    mainWin.Content = mainWin.reporte;
                    ts.Complete();
                }
                catch
                {
                    MessageBox.Show("El procedimiento no pudo ser completado.");
                }
            }
        }
    }
}
