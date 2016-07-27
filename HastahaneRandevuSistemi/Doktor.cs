using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace HastahaneRandevuSistemi
{
    class Doktor:IGenelOzellik
    {
        public Doktor()
        {
            randevuListesi = new List<Randevu>();
        }

        public string AdSoyad
        {
            get;
            set;
        }
        string sicilNo;

        public string SicilNo
        {
            get { return sicilNo; }
            set { sicilNo = value; }
        }

        Bolum bolum;

        internal Bolum Bolum
        {
            get { return bolum; }
            set { bolum = value; }
        }
        List<Randevu> randevuListesi;

        internal List<Randevu> RandevuListesi
        {
            get { return randevuListesi; }
            set { randevuListesi = value; }
        }

    }
}
