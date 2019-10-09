using CajaBanco.DataSetDBCajaTableAdapters;
using Microsoft.Reporting.WinForms;
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
    /// Interaction logic for Reporte.xaml
    /// </summary>
    public partial class Reporte : Page
    {
        MainWindow mainWin;
        TiposReporte tipo;
        public Reporte()
        {
            InitializeComponent();
        }
        public Reporte(MainWindow mainWindow, TiposReporte tipoReporte) : this()
        {
            mainWin = mainWindow;
            tipo = tipoReporte;
            
            
        }

        private void ReportViewer1_Load(object sender, EventArgs e)
        {
            //this.ReportViewer1.LocalReport.ReportPath = "ReportInicioDia.rdlc";
            //this.ReportViewer1.ProcessingMode = ProcessingMode.Local;


            ////tblCuentasTableAdapter cuentas = new tblCuentasTableAdapter();

            

            //ReportDataSource source = new ReportDataSource("DataSet1", (DataTable)diasCaja.GetDataByLastIdCaja(1000));
            //this.ReportViewer1.LocalReport.DataSources.Clear();
            //this.ReportViewer1.LocalReport.DataSources.Add(source);
            
            //this.ReportViewer1.RefreshReport();

            DiasCajaTableAdapter diasCaja = new DiasCajaTableAdapter();
            EstadoCajaTableAdapter estadoCaja = new EstadoCajaTableAdapter();
            CajerosTableAdapter cajeros = new CajerosTableAdapter();
            SucursalesTableAdapter sucursales = new SucursalesTableAdapter();

            int idCajero = mainWin.login.idCajeroInt;
            int idSuc = mainWin.login.idSucursal;
            int idDia = mainWin.menu.EfectivoCaja.IdDia;

            if (tipo == TiposReporte.CierreDia)
            {
                this.ReportViewer1.LocalReport.ReportPath = "ReportCierreDia.rdlc";
                this.ReportViewer1.ProcessingMode = ProcessingMode.Local;

                ReportDataSource Scajeros = new ReportDataSource("DScajeros", (DataTable)cajeros.GetDataByIdCajero(idCajero));
                ReportDataSource Ssucursal = new ReportDataSource("DSsucursal", (DataTable)sucursales.GetDataById(idSuc));
                ReportDataSource SdiasCaja = new ReportDataSource("DSdiasCaja", (DataTable)diasCaja.GetDataByIdDia(idDia));
                ReportDataSource SestadoInicio = new ReportDataSource("DSestadocajainicio", (DataTable)estadoCaja.GetDataByIdDiaAndAccion(idDia,(int)TiposAccion.InicioDelDia));
                ReportDataSource SestadoCierre = new ReportDataSource("DSestadocajacierre", (DataTable)estadoCaja.GetDataByIdDiaAndAccion(idDia, (int)TiposAccion.CierreDelDia));

                this.ReportViewer1.LocalReport.DataSources.Clear();

                this.ReportViewer1.LocalReport.DataSources.Add(Scajeros);
                this.ReportViewer1.LocalReport.DataSources.Add(Ssucursal);
                this.ReportViewer1.LocalReport.DataSources.Add(SdiasCaja);
                this.ReportViewer1.LocalReport.DataSources.Add(SestadoInicio);
                this.ReportViewer1.LocalReport.DataSources.Add(SestadoCierre);

                this.ReportViewer1.RefreshReport();

            }



        }

        private void BtMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWin.Content = mainWin.menu;
        }
    }
}
