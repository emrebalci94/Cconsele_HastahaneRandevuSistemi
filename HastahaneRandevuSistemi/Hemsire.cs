using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastahaneRandevuSistemi
{
    class Hemsire : IGenelOzellik
    {
        public string AdSoyad
        {
            get;set;
        }

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

        public string DoktorSicil
        {
            get
            {
                return doktorSicil;
            }

            set
            {
                doktorSicil = value;
            }
        }

        string tc;

        string doktorSicil;
    }
}
