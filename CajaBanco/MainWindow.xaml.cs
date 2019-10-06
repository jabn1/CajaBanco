using log4net;
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

        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public MainWindow()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Inicio la aplicacion");


            login = new Login(this);
            this.Content = login;


        }
    }
}
