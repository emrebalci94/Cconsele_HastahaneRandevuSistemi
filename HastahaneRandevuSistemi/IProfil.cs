using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastahaneRandevuSistemi
{
    interface IProfil
    {
        void UyeYeniAd(string yeniAd, string tc);
        void UyeYeniSifre(string yeniSifre, string tc);
    }
}
