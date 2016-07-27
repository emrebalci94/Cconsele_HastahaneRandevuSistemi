using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace HastahaneRandevuSistemi
{
    class ProfilImp : IProfil
    {
        public void UyeYeniAd(string yeniAd, string tc)
        {
            string[] veri = null;
            List<string[]> uyeler = new List<string[]>();
            using (StreamReader belge = new StreamReader(@".\Data\uyeler.txt", Encoding.GetEncoding("ISO-8859-9")))
            {

                string oku = "";
                do
                {
                    oku = belge.ReadLine();
                    if (oku != null)
                    {
                        veri = oku.Split(';');
                        uyeler.Add(veri);
                    }

                } while (oku != null);
            }

            foreach (var item in uyeler)
            {
                if (item[0].Equals(tc))
                {
                    item[1] = yeniAd;
                }
            }

            StreamWriter yaz = new StreamWriter(@".\Data\uyeler.txt");
            foreach (var item in uyeler)
            {
                if (item != null)
                {
                    for (int i = 0; i < item.Length - 1; i++)
                    {

                        yaz.Write(item[i] + ";");


                    }
                    yaz.WriteLine();
                }
            }
            yaz.Close();
        }

        public void UyeYeniSifre(string yeniSifre, string tc)
        {
            string[] veri = null;
            List<string[]> uyeler = new List<string[]>();
            using (StreamReader belge = new StreamReader(@".\Data\uyeler.txt", Encoding.GetEncoding("ISO-8859-9")))
            {

                string oku = "";
                do
                {
                    oku = belge.ReadLine();
                    if (oku != null)
                    {
                        veri = oku.Split(';');
                        uyeler.Add(veri);
                    }

                } while (oku != null);
            }

            foreach (var item in uyeler)
            {
                if (item[0].Equals(tc))
                {
                    item[2] = yeniSifre;
                }
            }

            StreamWriter yaz = new StreamWriter(@".\Data\uyeler.txt");
            foreach (var item in uyeler)
            {
                if (item != null)
                {
                    for (int i = 0; i < item.Length - 1; i++)
                    {

                        yaz.Write(item[i] + ";");


                    }
                    yaz.WriteLine();
                }
            }
            yaz.Close();
        }
    }
}
