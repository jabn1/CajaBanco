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

namespace CajaBanco
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        public EstadoCaja estado;
        public Deposito deposito;
        public Menu menu;
        public Login login;
        public Retiro retiro;
        public ResultadosValidarCliente resCliente;
        public ValidarCliente valCliente;
        public Transaccion transaccion;
        public InicioDia inicioDia;


        

        public MainWindow()
        {
            InitializeComponent();

            login = new Login(this);
            this.Content = login;


        }
    }
}
