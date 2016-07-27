using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastahaneRandevuSistemi
{
    class Randevu
    {
        string tc;

        public string Tc
        {
            get
            {
                return tc;
            }

            set
            {
                tc = value;
            }
        }

        public string Sicil
        {
            get
            {
                return sicil;
            }

            set
            {
                sicil = value;
            }
        }

        public DateTime Tarih
        {
            get
            {
                return tarih;
            }

            set
            {
                tarih = value;
            }
        }

        string sicil;

        DateTime tarih;
    }
}
