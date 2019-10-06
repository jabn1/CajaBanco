using AgregarCajero.DBAppCajaDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace AgregarCajero
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private void BtAgregar_Click(object sender, RoutedEventArgs e)
        {
            try { 
            string clave = Hash(TbClave.Text);

                CajerosTableAdapter cajeros = new CajerosTableAdapter();
                cajeros.Insert(clave,TbCedula.Text,TbNombre.Text,DateTime.Now,(int)CbSucursal.SelectedValue);
                MessageBox.Show("Se creo el usuario de cajero exitosamente.");
                TbCedula.Text = "";
                TbClave.Text = "";
                TbNombre.Text = "";
            }
            catch
            {
                MessageBox.Show("Ocurrio un problema durante la durante el proceso.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            AgregarCajero.DBAppCajaDataSet dBAppCajaDataSet = ((AgregarCajero.DBAppCajaDataSet)(this.FindResource("dBAppCajaDataSet")));
            // Load data into the table Sucursales. You can modify this code as needed.
            AgregarCajero.DBAppCajaDataSetTableAdapters.SucursalesTableAdapter dBAppCajaDataSetSucursalesTableAdapter = new AgregarCajero.DBAppCajaDataSetTableAdapters.SucursalesTableAdapter();
            dBAppCajaDataSetSucursalesTableAdapter.Fill(dBAppCajaDataSet.Sucursales);
            System.Windows.Data.CollectionViewSource sucursalesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("sucursalesViewSource")));
            sucursalesViewSource.View.MoveCurrentToFirst();
        }

        static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
