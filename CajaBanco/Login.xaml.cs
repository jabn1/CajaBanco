using CajaBanco.DataSetDBCajaTableAdapters;
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
using static CajaBanco.DataSetDBCaja;

namespace CajaBanco
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public MainWindow mainWin;
        public string idCajero;
        public string nomSucursal;
        public int idCajeroInt;
        public int idSucursal;
        public Login()
        {
            InitializeComponent();
            //Focus();
            usuario.Focus();
        }
        public Login(MainWindow mainWindow) : this()
        {
            this.mainWin = mainWindow;
        }

        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            CajerosTableAdapter cajeros = new CajerosTableAdapter();

            int id;
            
            if(int.TryParse(usuario.Text, out id))
            {
                var datos = cajeros.GetDataByIdCajero(id).Rows;
                if(datos.Count == 1)
                {
                    var user = (CajerosRow)datos[0];
                    if (user["Clave"].ToString() == Hash(clave.Password))
                    {
                        var sucs = new SucursalesTableAdapter();
                        idCajeroInt = id;
                        idCajero = "Cajero No.: " + id.ToString();
                        idSucursal = (int)user["IdSuc"];
                        nomSucursal = "Sucursal: " + idSucursal.ToString()+ " - " +  sucs.GetNomSucById(idSucursal).ToString();

                        MainWindow.log.Info($"Inicio de sesion exitoso. Id: {usuario.Text}");

                        mainWin.menu = new Menu(mainWin);
                        mainWin.Content = mainWin.menu;
                    }
                    else
                    {
                        MainWindow.log.Info($"Inicio de sesion fallido. Clave incorrecta. Id: {usuario.Text}");
                        MessageBox.Show("Credenciales invalidos.");
                    }
                }
                else
                {
                    MainWindow.log.Info($"Inicio de sesion fallido. Id invalido: {usuario.Text}");
                    MessageBox.Show("Credenciales invalidos.");
                }
            }
            else
            {
                MainWindow.log.Info($"Inicio de sesion fallido. Id no numerico: {usuario.Text}");
                MessageBox.Show("Credenciales invalidos.");
            }           
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    BtLogin_Click(null, null);
                    break;
                case Key.Enter:
                    BtLogin_Click(null, null);
                    break;
            }
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
