﻿using System;
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
using CajaBanco.IntegracionService1;
namespace CajaBanco
{
    /// <summary>
    /// Interaction logic for AutenticarCliente.xaml
    /// </summary>
    public partial class ValidarCliente : Page
    {
        MainWindow mainWin;

        public ValidarCliente()
        {
            InitializeComponent();
            tbCedula.Focus();
        }
        public ValidarCliente(MainWindow mainWindow):this()
        {
            mainWin = mainWindow;
            if (mainWin.menu.HayConexion)
            {
                estadoConexion.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
            else
            {
                estadoConexion.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }

        }

        private void BtProcesar_Click(object sender, RoutedEventArgs e)
        {
            string cedula = tbCedula.Text;
            var cedulaRegex = new System.Text.RegularExpressions.Regex("^[0-9]{11}$");
            if (!cedulaRegex.IsMatch(cedula))
            {
                MessageBox.Show("Formato de cedula invalido.");
                return;
            }

            mainWin.resCliente = new ResultadosValidarCliente(mainWin, Validar(cedula));
            mainWin.Content = mainWin.resCliente;
        }

        private void BtMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWin.Content = mainWin.menu;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    BtProcesar_Click(null,null);
                    break;
                case Key.Escape:
                    BtMenu_Click(null,null);
                    break;
                

            }
        }

        public Cliente Validar(string cedula)
        {
            mainWin.menu.RevisarConexion();
            var cliente = new Cliente();
            if (mainWin.menu.HayConexion)
            {
                // Aqui va el codigo de validar cliente cuando hay conexion con la capa de integracion
                // Los datos se obtienen con el web service de la capa de integracion

                CajaServiceSoapClient cajaService = new CajaServiceSoapClient();
                try
                {
                   var cli = cajaService.ValidarCliente(cedula, mainWin.login.idCajeroInt.ToString());

                    cliente.Cedula = cedula;
                    cliente.Nombres = cli.Nombres;
                    cliente.Apellidos = cli.Apellidos;
                    cliente.NumeroCuenta = cli.Cuenta.NumeroCuenta;
                    cliente.Tipo = TiposCliente.ClienteNormal;
                }
                catch
                {

                }
                

            }
            else
            {
                // Aqui va el codigo para validar cliente fuera de linea
                // Los datos se ponen por defecto
                cliente.Cedula = cedula;
                cliente.Nombres = "JUAN PABLO";
                cliente.Apellidos = "PEREZ SOTO";
                cliente.NumeroCuenta = 0;
                cliente.Tipo = TiposCliente.ClienteNormal;
            }
            return cliente;
        }

    }
}
