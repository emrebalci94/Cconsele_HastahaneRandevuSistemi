using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
namespace HastahaneRandevuSistemi
{
    class ControlImp:IControl
    {
        public bool TcKontrol(string tc,int hangiTc)
        {
            string kural = @"^\d{11}$";
            Match sonuc=Regex.Match(tc,kural);
            if (sonuc.Success)
            {
                if (hangiTc == 0)
                {
                    foreach (Uye item in DbIslemleri.UyeListesi)
                    {
                        if (item.Tc.Equals(tc))
                        {
                            return false;
                        }
                    }
                }
                else if (hangiTc == 1)
                {
                    foreach (Hemsire item in DbIslemleri.HemsireListesi)
                    {
                        if (item.Tc.Equals(tc))
                        {
                            return false;

                        }
                    }
                }
                else if (hangiTc == 2)
                {
                    foreach (Hizmetli item in DbIslemleri.HizmetliListesi)
                    {
                        if (item.Tc.Equals(tc))
                        {
                            return false;

                        }
                    }
                }
                 return true;
            }
           
            return false;
        }

        public bool SifreKural(string sifre)
        {
            string kural = @"[0-9]\W";
            Match sonuc = Regex.Match(sifre, kural);
            return sonuc.Success;
        }

        public bool SifreUyusma(string sifre1, string sifre2)
        {
            if (sifre1.Equals(sifre2))
            {
                return true;
            }
            return false;
        }

        public bool RobotKontrol(string istenilen, string alinan)
        {
            return istenilen.Equals(alinan);
         
        }



        public Uye UyeGiris(string tc, string sifre)
        {
            foreach (Uye item in DbIslemleri.UyeListesi)
            {
                if (item.Tc.Equals(tc) && item.Sifre.Equals(sifre))
                {
                    return item;
                }
            }
            return null;
        }


        public bool sicilKontrol(string sicil)
        {
            string kural = @"^\d{5}$";
            Match sonuc = Regex.Match(sicil, kural);
            if (sonuc.Success)
            {
                foreach (Doktor item in DbIslemleri.DoktorListesi)
                {
                    if (item.SicilNo.Equals(sicil))
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }

        public Doktor DoktorGiris(string sicil)
        {
            foreach (Doktor item in DbIslemleri.DoktorListesi)
            {
                if (item.SicilNo.Equals(sicil))
                {
                    return item;
                }
            }
            return null;
        }

        public bool doktorSicil(string sicil)
        {
            foreach (Doktor item in DbIslemleri.DoktorListesi)
            {
                if (item.SicilNo.Equals(sicil))
                {
                    return true;
                }
            }
            return false;
        }

        public bool BelgeKontrol(string sicil)
        {
            return   File.Exists(@".\Data\DoktorRandevu\"+sicil+".txt");
        }

        public string[] TarihKontrol(string tarih, string sicil)
        {
            using (StreamReader belge = new StreamReader(@".\Data\DoktorRandevu\"+sicil+".txt", Encoding.GetEncoding("ISO-8859-9")))
            {
                string[] veri = null;
                string oku = "";
                do
                {
                    oku = belge.ReadLine();
                    if (oku != null)
                    {
                        veri = oku.Split(';');
                        if (veri[0].Equals(tarih))
                        {
                            return veri;
                        }
                    }

                } while (oku != null);
                return null;
            }
        }

        public bool SaatUgunluk(string saat)
        {
            try
            {
                if (int.Parse(saat) > 0 && int.Parse(saat) < 18)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;

            }
            return false;
        }

        public bool RandevuTcKontrol(string[] randevu, string tc)
        {
            for (int i = 1; i < randevu.Length; i++)
            {
                if (randevu[i].Equals(tc))
                {
                    return false;//Kayıt Yapılmamasını sağla
                }
            }
            return true;// Yani Kayıt Yapılabilir
        }

        public Uye UyeBul(string tc)
        {
            string kural = @"^\d{11}$";
            Match sonuc = Regex.Match(tc, kural);
            if (sonuc.Success)
            {
                foreach (Uye item in DbIslemleri.UyeListesi)
                {
                    if (item.Tc.Equals(tc))
                    {
                        return item;
                    }
                }
            }
                return null;
            
        }
    }
}
