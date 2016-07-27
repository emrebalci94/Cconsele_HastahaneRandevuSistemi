using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace HastahaneRandevuSistemi
{
    interface IControl
    {
       bool TcKontrol(string tc,int hangiTc);
       bool SifreKural(string sifre);
       bool SifreUyusma(string sifre1,string sifre2);
       bool RobotKontrol(string istenilen, string alinan);
       Uye UyeGiris(string tc, string sifre);
       bool sicilKontrol(string sicil);
       bool doktorSicil(string sicil);
       Doktor DoktorGiris(string sicil);
       bool BelgeKontrol(string sicil);
        bool SaatUgunluk(string saat);
        string[] TarihKontrol(string tarih,string sicil);
        bool RandevuTcKontrol(string[] randevu, string tc);
        Uye UyeBul(string tc);
    }
}
