using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastahaneRandevuSistemi
{
    class Hizmetli:IGenelOzellik
    {
        string tc;

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

        public string Gorevi
        {
            get
            {
                return gorevi;
            }

            set
            {
                gorevi = value;
            }
        }

        string gorevi;

    }
}
