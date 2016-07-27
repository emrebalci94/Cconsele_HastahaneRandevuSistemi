using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastahaneRandevuSistemi
{
    class Uye:IGenelOzellik
    {
        public string AdSoyad
        {
            get;
            set;
        }
        private string tc;

        public string Tc
        {
            get { return tc; }
            set { tc = value; }
        }
        string sifre;

        public string Sifre
        {
            get { return sifre; }
            set { sifre = value; }
        }

        public override string ToString()
        {
            return Tc + " " + AdSoyad + " " + Sifre;
        }

    }
}
