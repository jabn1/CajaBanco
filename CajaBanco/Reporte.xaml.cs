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
        public ReportViewer reportViewer;
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
            MovimientosCajaTableAdapter movimientos = new MovimientosCajaTableAdapter();

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
                ReportDataSource Smov = new ReportDataSource("DSMov", (DataTable)movimientos.GetDataByIdDia(mainWin.menu.EfectivoCaja.IdDia));


                this.ReportViewer1.LocalReport.DataSources.Clear();

                this.ReportViewer1.LocalReport.DataSources.Add(Scajeros);
                this.ReportViewer1.LocalReport.DataSources.Add(Ssucursal);
                this.ReportViewer1.LocalReport.DataSources.Add(SdiasCaja);
                this.ReportViewer1.LocalReport.DataSources.Add(SestadoInicio);
                this.ReportViewer1.LocalReport.DataSources.Add(SestadoCierre);
                this.ReportViewer1.LocalReport.DataSources.Add(Smov);

                this.ReportViewer1.RefreshReport();

            }
            else if(tipo == TiposReporte.TransaccionBancaria)
            {
                DatosReporteTrans datos = mainWin.transaccion.datosRep;


                this.ReportViewer1.LocalReport.ReportPath = "ReportTransaccion.rdlc";
                this.ReportViewer1.ProcessingMode = ProcessingMode.Local;


                ReportParameter rp1 = new ReportParameter("TituloRecibo", datos.titulo);
                ReportParameter rp2 = new ReportParameter("NombreCliente", datos.nombre);
                ReportParameter rp3 = new ReportParameter("Sucursal", datos.sucursal);
                ReportParameter rp4 = new ReportParameter("idCajero", datos.idCajero.ToString());
                ReportParameter rp5 = new ReportParameter("Cuenta", datos.cuenta.ToString());
                ReportParameter rp6 = new ReportParameter("Monto", datos.monto);
                ReportParameter rp7 = new ReportParameter("TipoTotal", datos.tipototal);
                ReportParameter rp8 = new ReportParameter("Fecha", datos.fecha.ToString());
                ReportParameter rp9 = new ReportParameter("IdNoTrans", datos.idNoTrans);

                ReportViewer1.LocalReport.DataSources.Clear();
                
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3,rp4,rp5,rp6,rp7,rp8,rp9 });

                this.ReportViewer1.RefreshReport();
            }



        }

        private void BtMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWin.Content = mainWin.menu;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {

                case Key.Escape:
                    BtMenu_Click(null, null);
                    break;

            }
        }
    }
}
