using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajaBanco
{
    public enum TiposCliente
    {
        ClienteNormal = 1,
        CedulaNoExiste = 2
    }
    public class Cliente
    {
        private string cedula, nombres, apellidos;
        private int numeroCuenta;
        private TiposCliente tipo;

        public string Cedula { get => cedula; set => cedula = value; }
        public string Nombres { get => nombres; set => nombres = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public int NumeroCuenta { get => numeroCuenta; set => numeroCuenta = value; }
        public TiposCliente Tipo { get => tipo; set => tipo = value; }
    }
}
