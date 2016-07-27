using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace HastahaneRandevuSistemi
{
    abstract  class DbIslemleri
    {
        static DbIslemleri()
        {
            UyeListesi = new List<Uye>();
            DoktorListesi = new List<Doktor>();
            HizmetliListesi = new List<Hizmetli>();
            HemsireListesi = new List<Hemsire>();
        }

        static List<Uye> uyeListesi;

        public static List<Uye> UyeListesi
        {
            get { return DbIslemleri.uyeListesi; }
            set { DbIslemleri.uyeListesi = value; }
        }

        static List<Doktor> doktorListesi;

        public static List<Doktor> DoktorListesi
        {
            get { return DbIslemleri.doktorListesi; }
            set { DbIslemleri.doktorListesi = value; }
        }

        public static List<Hizmetli> HizmetliListesi
        {
            get
            {
                return hizmetliListesi;
            }

            set
            {
                hizmetliListesi = value;
            }
        }

        internal static List<Hemsire> HemsireListesi
        {
            get
            {
                return hemsireListesi;
            }

            set
            {
                hemsireListesi = value;
            }
        }

        static List<Hizmetli> hizmetliListesi;

        static List<Hemsire> hemsireListesi;

        abstract public void doktorEkle(string sicil, string adSoyad, int bolumId);
        abstract public void uyeEkle(string tc, string adSoyad,string Sifre);
        abstract public void HizmetliEkle(string tc, string adSoyad, string gorev);
        abstract public void HemsireEkle(string tc, string adSoyad, string sicil);
        abstract public void uyeAl();
        abstract public void doktorAl();
        abstract public void HizmetliAl();
        abstract public void HemsireAl();
        abstract public string[] RandevuListesi();
        abstract public string[] RandevuListesi(string tarih);//.ToShortDate diye atıcaz
        abstract public void RandevuKayit(string sicil, string saatSecim,string tarih,string tc);
        abstract public void UyeRandevuKayit(string sicil, string saatSecim, string tarih,string tc);
        abstract public List<string[]> RandevuListem(string tc);
        abstract public List<string[]> DoktorRandevuListesiTumu(string sicil);
        abstract public string SecilenSaat(string saat);
        abstract public bool RandevuKayitTarih(string sicil, string saatSecim, string tarih, string tc);
    }
}
