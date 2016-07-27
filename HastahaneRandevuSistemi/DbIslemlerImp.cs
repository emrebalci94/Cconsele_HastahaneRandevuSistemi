using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HastahaneRandevuSistemi
{
    class DbIslemlerImp:DbIslemleri
    {
        public override void uyeEkle(string tc, string adSoyad, string Sifre)
        {
            using (StreamWriter veriEkle = new StreamWriter(@".\Data\uyeler.txt",true))
            {
                veriEkle.WriteLine(tc+";"+adSoyad+";"+Sifre+";");
            }
            //MetinAtıcak
        }

        public override void uyeAl()
        {
            using(StreamReader belge=new StreamReader(@".\Data\uyeler.txt",Encoding.GetEncoding("ISO-8859-9")))
            {
                string oku ="";
                do
                {
                    Uye uye = null;
                    oku= belge.ReadLine();
                    if (oku != null)
                    {
                        uye = new Uye();
                        string[] veri = oku.Split(';');
                        uye.Tc = veri[0];
                        uye.AdSoyad = veri[1];
                        uye.Sifre = veri[2];
                        DbIslemleri.UyeListesi.Add(uye);
                    } 
                } while (oku !=null);
            }
            //Metinden bilgileri al
        }

     

        public override void doktorAl()
        {
            using (StreamReader belge = new StreamReader(@".\Data\doktorlar.txt", Encoding.GetEncoding("ISO-8859-9")))
            {
                string oku = "";
                do
                {
                    Doktor doktor = null;
                    oku = belge.ReadLine();
                    if (oku != null)
                    {
                        doktor = new Doktor();
                        string[] veri = oku.Split(';');
                        doktor.SicilNo = veri[0];
                        doktor.AdSoyad = veri[1];
                        doktor.Bolum =(Bolum)int.Parse(veri[2]);
                        DbIslemleri.DoktorListesi.Add(doktor);
                    }
                } while (oku != null);
            }
        }

        public override void doktorEkle(string sicil, string adSoyad, int bolumId)
        {
            using (StreamWriter veriEkle = new StreamWriter(@".\Data\doktorlar.txt", true))
            {
                veriEkle.WriteLine(sicil + ";" + adSoyad + ";" + bolumId + ";");
            }
        }

        public override string[] RandevuListesi()
        {
            using (StreamReader belge = new StreamReader(@".\Data\saatler.txt", Encoding.GetEncoding("ISO-8859-9")))
            {
                string[] veri = null;
                string oku = "";
                do
                {
                    oku = belge.ReadLine();
                    if (oku!=null)
                    {
                        veri = oku.Split(';');
                    }

                } while (oku!=null);
                return veri;
            }
        }

        public override string[] RandevuListesi(string tarih)
        {
            throw new NotImplementedException();
        }

        public override void RandevuKayit(string sicil, string saatSecim, string tarih, string tc) //Randevunun tamamen boşsa çalışır.Yani Ona uygun olarak ayarlandı.
        {
            string[] veri = null;
            using (StreamReader belge = new StreamReader(@".\Data\sifir.txt", Encoding.GetEncoding("ISO-8859-9")))
            {
                string oku = "";
                do
                {
                    oku = belge.ReadLine();
                    if (oku != null)
                    {
                        veri = oku.Split(';');
                    }
                } while (oku != null);
            }
            string topla = "";
            veri[int.Parse(saatSecim)-1] = tc;
            for (int i = 0; i < veri.Length; i++)
            {
                if (veri[i]!="")
                {
                    topla = topla + ";" + veri[i];
                }
            }

            if (!File.Exists(@".\Data\DoktorRandevu\" + sicil + ".txt"))
            {
                FileStream yarat = new FileStream(@".\Data\DoktorRandevu\" + sicil + ".txt", FileMode.Create);
                yarat.Close();
            }

            using (StreamWriter veriEkle = new StreamWriter(@".\Data\DoktorRandevu\" + sicil + ".txt", true))
            {
                veriEkle.WriteLine(tarih + topla+";");
            }
        }

        public override void UyeRandevuKayit(string sicil, string saatSecim, string tarih, string tc)
        {
           if(!File.Exists(@".\Data\UyeRandevu\" + tc + ".txt"))
            {
                FileStream yarat = new FileStream(@".\Data\UyeRandevu\" + tc + ".txt", FileMode.Create);
                yarat.Close();
            }

            FileStream fs = new FileStream(@".\Data\UyeRandevu\" + tc + ".txt", FileMode.Append, FileAccess.Write);
            using (StreamWriter veriEkle = new StreamWriter(fs))
            {
                veriEkle.WriteLine(tarih + ";"+sicil+";"+(int.Parse(saatSecim)-1));
            }
        }

        public override List<string[]> RandevuListem(string tc)
        {
            string[] veri = null;
            List<string[]> randevum = new List<string[]>();
            if (!File.Exists(@".\Data\UyeRandevu\" + tc + ".txt"))
            {
                return null;
            }
            using (StreamReader belge = new StreamReader(@".\Data\UyeRandevu\" + tc + ".txt", Encoding.GetEncoding("ISO-8859-9")))
            {
                string oku = "";
                do
                {
                    oku = belge.ReadLine();
                    if (oku != null)
                    {
                        veri = oku.Split(';');
                        randevum.Add(veri); 
                    }
                } while (oku != null);
                return randevum;
            }
        }

        public override string SecilenSaat(string saat)
        {
            string[] veri = null;
            using (StreamReader belge = new StreamReader(@".\Data\saatler.txt", Encoding.GetEncoding("ISO-8859-9")))
            {
                string oku = "";
                do
                {
                    oku = belge.ReadLine();
                    if (oku != null)
                    {
                        veri = oku.Split(';');
                    }
                } while (oku != null);
                return veri[int.Parse(saat)];
            }
        }

        public override bool RandevuKayitTarih(string sicil, string saatSecim, string tarih, string tc)
        {
            try
            {
                string[] veri = null;
                List<string[]> randevular = new List<string[]>();
                using (StreamReader belge = new StreamReader(@".\Data\DoktorRandevu\" + sicil + ".txt", Encoding.GetEncoding("ISO-8859-9")))
                {

                    string oku = "";
                    do
                    {
                        oku = belge.ReadLine();
                        if (oku != null)
                        {
                            veri = oku.Split(';');
                            randevular.Add(veri);
                        }

                    } while (oku != null);
                }

                foreach (var item in randevular)
                {
                    if (item[0].Equals(tarih))
                    {
                        item[int.Parse(saatSecim)] = tc;
                    }
                }

                 StreamWriter yaz = new StreamWriter(@".\Data\DoktorRandevu\" + sicil + ".txt");
                foreach (var item in randevular)
                {
                    if (item != null)
                    {
                        for (int i = 0; i < item.Length; i++)
                        {
                            if (item[i]!="")
                            {
                                yaz.Write(item[i] + ";");
                            }
                        }
                        yaz.WriteLine();
                    }
                }
                yaz.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public override void HizmetliEkle(string tc, string adSoyad, string gorev)
        {
            using (StreamWriter veriEkle = new StreamWriter(@".\Data\hizmetli.txt", true))
            {
                veriEkle.WriteLine(tc + ";" + adSoyad + ";" + gorev + ";");
            }
        }

        public override void HizmetliAl()
        {
            using (StreamReader belge = new StreamReader(@".\Data\hizmetli.txt", Encoding.GetEncoding("ISO-8859-9")))
            {
                string oku = "";
                do
                {
                    Hizmetli hizmetli = null;
                    oku = belge.ReadLine();
                    if (oku != null)
                    {
                        hizmetli = new Hizmetli();
                        string[] veri = oku.Split(';');
                        hizmetli.Tc = veri[0];
                        hizmetli.AdSoyad = veri[1];
                        hizmetli.Gorevi = veri[2];
                        DbIslemleri.HizmetliListesi.Add(hizmetli);
                    }
                } while (oku != null);
            }
        }

        public override void HemsireEkle(string tc, string adSoyad, string sicil)
        {
            using (StreamWriter veriEkle = new StreamWriter(@".\Data\hemsire.txt", true))
            {
                veriEkle.WriteLine(tc + ";" + adSoyad + ";" + sicil + ";");
            }
        }

        public override void HemsireAl()
        {
            
            using (StreamReader belge = new StreamReader(@".\Data\hemsire.txt", Encoding.GetEncoding("ISO-8859-9")))
            {
                string oku = "";
                do
                {
                    Hemsire hemsire = null;
                    oku = belge.ReadLine();
                    if (oku != null)
                    {
                        hemsire = new Hemsire();
                        string[] veri = oku.Split(';');
                        hemsire.Tc = veri[0];
                        hemsire.AdSoyad = veri[1];
                        hemsire.DoktorSicil = veri[2];
                        DbIslemleri.HemsireListesi.Add(hemsire);
                    }
                } while (oku != null);
            }
        }

        public override List<string[]> DoktorRandevuListesiTumu(string sicil)
        {
            string[] veri = null;
            List<string[]> randevum = new List<string[]>();
            if (!File.Exists(@".\Data\DoktorRandevu\" + sicil + ".txt"))
            {
                return null;
            }
            using (StreamReader belge = new StreamReader(@".\Data\DoktorRandevu\" + sicil + ".txt", Encoding.GetEncoding("ISO-8859-9")))
            {
                string oku = "";
                do
                {
                    oku = belge.ReadLine();
                    if (oku != null)
                    {
                        veri = oku.Split(';');
                        randevum.Add(veri);
                    }
                } while (oku != null);
                return randevum;
            }
        }
    }
}
