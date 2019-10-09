using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajaBanco
{
    public class EfectivoEnCaja
    {
        private int bm2000, bm1000, bm500, bm200, bm100, bm50, bm25, bm10, bm5, bm1;

        private int idDia;
        private decimal totalCaja;

        public int Bm2000 { get => bm2000; set => bm2000 = value; }
        public int Bm1000 { get => bm1000; set => bm1000 = value; }
        public int Bm500 { get => bm500; set => bm500 = value; }
        public int Bm200 { get => bm200; set => bm200 = value; }
        public int Bm100 { get => bm100; set => bm100 = value; }
        public int Bm50 { get => bm50; set => bm50 = value; }
        public int Bm25 { get => bm25; set => bm25 = value; }
        public int Bm10 { get => bm10; set => bm10 = value; }
        public int Bm5 { get => bm5; set => bm5 = value; }
        public int Bm1 { get => bm1; set => bm1 = value; }
        public int IdDia { get => idDia; set => idDia = value; }
        public decimal TotalCaja { get => totalCaja; set => totalCaja = value; }

        public void InsertEfectivo(Dictionary<string, int> listBm)
        {
            bm2000 = listBm["tb2000p"];
            bm1000 = listBm["tb1000p"];
            bm500 = listBm["tb500p"];
            bm200 = listBm["tb200p"];
            bm100 = listBm["tb100p"];
            bm50 = listBm["tb50p"];
            bm25 = listBm["tb25p"];
            bm10 = listBm["tb10p"];
            bm5 = listBm["tb5p"];
            bm1 = listBm["tb1p"];
        }
    }
}
