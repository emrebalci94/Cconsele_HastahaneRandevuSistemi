using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace HastahaneRandevuSistemi
{//Telefon numarasıydı e mail felan eklemedim.(Hızlı kayıt ve deneme yapmak için) Ama Bi Interface açtım onunn için GenelOzellik diye oraya ekleme yapılabilir.
    class Program
    {
     /// <summary>
     ///  Doktorlar Randevuları Hemşire Atamaları Hizmetli Kayıtları bitti. TEst gerek.
     ///  Doktorlar Menüde Tclerinin listelenmesi bilerek yapılmış birşey.Çünkü menüde Üye bul kısmı var.Üyeyi bulabilmesi için bir Tc gerekiyor...
     ///  Admin olarak giriş yapabilmeniz için: Ana Menüde direk "11111111111" yazarsanız alt satıra geçip şifreyi istiyor orayada "admin123?" yazarsak giriş gerçekleşiyor
     ///  Böyle yapmamamın nedeni Admin yani gizli kapaklı girmeli bence :)
     ///  Çalışan Listelemeyi Ana Menü ye aldım.Belki Adam bir doktor arıyordur eğer o doktor hastanede varsa randevu alsın mantığında gittiğim için
     /// </summary>

        static DbIslemlerImp dbIslem = new DbIslemlerImp();
      static ControlImp ctIslem = new ControlImp();
        static ProfilImp profil = new ProfilImp();
      static Uye girilenUye = null;
        static Doktor girilenDoktor = null;
        static void Main(string[] args)
        {
            ListeYenile();
            AnaMenu();


        }
        static void ListeYenile()
        {
            DbIslemleri.UyeListesi = new List<Uye>();
            dbIslem.uyeAl();
            DbIslemleri.DoktorListesi = new List<Doktor>();
            dbIslem.doktorAl();
            DbIslemleri.HizmetliListesi = new List<Hizmetli>();
            dbIslem.HizmetliAl();
            DbIslemleri.HemsireListesi = new List<Hemsire>();
            dbIslem.HemsireAl();
        }

        static void AnaMenu()
        {
      
            do
            {
                Console.Clear();
                Console.Write("1.Kaydol\n2.Hastahane Girişi\n3.Çalışan Listeleme\n4.(...)Çıkış\n\n>>>");
                string secim = Console.ReadLine();
                switch (secim)
                {
                    case "1": UyeEkle(); break;
                    case "2": HastahaneGirisMenu(); break;
                    case "3":CalisanListeMenu();break;
                    case "11111111111"://Admin Girişi
                        string sifre = Console.ReadLine();
                        if (sifre=="admin123?")
                        {
                            HastahaneKayitMenu();
                        }
                        break;
                    default: Environment.Exit(0); break;
                }
            } while (true);
        }

        private static void CalisanListeMenu()//Bu Menü Dahada Uzar gider aslında
        {
            do
            {
                ListeYenile();
                Console.Clear();
                Console.Write("--->Hastahane Giriş \n1.Tüm Çalışanlar\n2.Mesleğine Göre\n3.Bölümüne Göre\n4.Sicil Numarasına Göre\n5.(...)Geri\n\n>>>");
                string secim = Console.ReadLine();
                switch (secim)
                {
                    case "1":TumCalisanlar();break;
                    case "2":MeslegineGoreCalisanlar();break;
                    case "3":BolumuneGoreListele();break;//Bolumde Hemşireleride listelerdim Ama doktorlara göre hemşireyi atadığımdan Bir kaç method gerekecek.
                    case "4": SicilNumarasinaGoreListele(); break;
                    default:AnaMenu();
                        break;
                }
            } while (true);
        }

        private static void SicilNumarasinaGoreListele()
        {
            ListeYenile();
            bool yazdi = false;
            Console.Clear();
            Console.WriteLine("---> Sicil Numarasına Göre Doktor");
            Console.Write("Sicil Numarası:");
            string sicil = Console.ReadLine();
            foreach (var item in DbIslemleri.DoktorListesi)
            {
                if (item.SicilNo.Equals(sicil))
                {
                    yazdi = true;
                    Console.WriteLine(item.SicilNo+" "+item.AdSoyad+" "+item.Bolum);
                }
            }
            if (!yazdi)
            {
                Console.WriteLine("Doktor Bulunamadı...");
            }
            Console.ReadKey();
        }
        private static void BolumuneGoreListele()
        {
            ListeYenile();
            Console.Clear();
            Console.WriteLine("---> Bölümüne Göre Doktorlar");
            BolumListesi();
            Console.Write(">>>");

            try
            {
                Bolum secim = (Bolum)int.Parse(Console.ReadLine());
                if ((int)secim >3)
                {
                    throw new Exception();
                }
                Console.Clear();
                Console.WriteLine("--->" + secim + " BÖLÜMÜNE AİT DOKTORLAR");
                foreach (Doktor item in DbIslemleri.DoktorListesi)
                {
                    if (item.Bolum == secim)
                    {
                        Console.WriteLine(item.SicilNo + " " + item.AdSoyad);
                    }
                }
            }
            catch (Exception)
            {
                Console.Write("Yanlış Bölüm Seçildi...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                Console.ReadKey();
                BolumuneGoreListele();
            }

         

            Console.ReadKey();
        }

        private static void MeslegineGoreCalisanlar()
        {
            ListeYenile();
            Console.Clear();
            Console.WriteLine("---> Mesleğine Göre Çalışanlar");
            Console.Write("\n1.DOKTORLAR\n2.HEMŞİRELER\n3.HİZMETLİLER\n4.(...)Geri\n\n>>>");
            string secim = Console.ReadLine();
            switch (secim)
            {
                case "1":Doktorlar();Console.ReadKey(); MeslegineGoreCalisanlar(); break;
                case "2":Hemsireler(); Console.ReadKey(); MeslegineGoreCalisanlar(); break;
                case "3":Hizmetliler(); Console.ReadKey(); MeslegineGoreCalisanlar(); break;
                default:CalisanListeMenu();
                    break;
            }
        }

        private static void Hizmetliler()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\n--->HİZMETLİLERİMİZ<---");
            foreach (var item in DbIslemleri.HizmetliListesi)
            {
                Console.WriteLine(item.Tc + " " + item.AdSoyad + " " + item.Gorevi);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void Hemsireler()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--->HEMŞİRELERİMİZ<---");
            foreach (var item in DbIslemleri.HemsireListesi)
            {
                Console.WriteLine(item.Tc + " " + item.AdSoyad + " " + item.DoktorSicil);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void Doktorlar()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--->DOKTORLARIMIZ<---");
            foreach (var item in DbIslemleri.DoktorListesi)
            {
                Console.WriteLine(item.SicilNo + " " + item.AdSoyad + " " + item.Bolum);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void TumCalisanlar()
        {
            ListeYenile();
            Console.Clear();
            Console.WriteLine("---> Tüm Çalışanlar");
            Doktorlar();
            Hemsireler();
            Hizmetliler();
            Console.ReadKey();
        }

        private static void HastahaneGirisMenu()
        {
            do
            {
                ListeYenile();
                Console.Clear();
                Console.Write("--->Hastahane Giriş \n1.Üye Giriş\n2.Doktor Giriş\n3.(...)Geri\n\n>>>");
                string secim = Console.ReadLine();
                switch (secim)
                {
                    case "1": UyeGiris(); break;
                    case "2":DoktorGiris();break;
                    default: AnaMenu();
                        break;
                }
            } while (true);
        }

        private static void DoktorGiris()
        {
            Console.Clear();
            Console.WriteLine("--->Üye Giriş");
            Console.Write("Sicil Numarası:");
            string sicil = Console.ReadLine();
            Random r = new Random();
            string rasgele = r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString();
            Console.Write("Robot Olmadığınızı Kanıtlayın:" + rasgele + "\n>>>");
            string kanit = Console.ReadLine();
            if (ctIslem.RobotKontrol(rasgele, kanit))
            {
                girilenDoktor = ctIslem.DoktorGiris(sicil);
                if (girilenDoktor != null)//Doktor Giriş
                {
                    DoktorGirisMenu();
                }
                else
                {
                    Console.Write("Doktor Bulunamadı...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                    Console.ReadKey();
                    HastahaneGirisMenu();
                }
            }
            else
            {
                Console.Write("Robot Olup Olmadığınız Kanıtlanamadı...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                Console.ReadKey();
                HastahaneGirisMenu();
            }

        }

        private static void DoktorGirisMenu()
        {
            Console.Clear();
            Console.WriteLine("--->Doktor Giriş");
            do
            {
                Console.Write("1.Tüm Randevular\n2.Tarihe Göre Randevular\n3.Hasta Bilgisi Sorgulama\n4.(...)Çıkış Yap\n\n>>>");
                string secim = Console.ReadLine();
                switch (secim)
                {
                    case "1":DoktorTumRandevular(); break;
                    case "2":DoktorTarihRandevular();break;
                    case "3":HastaBul();break;
                    default:AnaMenu();
                        break;
                }
            } while (true);
        }

        private static void HastaBul()
        {
            Console.Clear();
            Console.WriteLine("--->Üye Bul");
            Console.Write("Tc Kimlik:");
            string tc = Console.ReadLine();
            Uye uye = ctIslem.UyeBul(tc);
            if (uye !=null)
            {
                Console.WriteLine(uye.Tc+" "+uye.AdSoyad);

                Console.Write("\nBaşka Hastaya Bakmak İstermisiniz(E-H)");
                string secim = Console.ReadLine();
                if (secim.ToUpper().ToString() == "E")
                {
                    HastaBul();
                }
                else
                {
                    DoktorGirisMenu();
                }
            }
            else
            {
                Console.Write("Üye Bulunamadı...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                Console.ReadKey();
                DoktorGirisMenu();
            }
        }

        private static void DoktorTarihRandevular()
        {
            if (ctIslem.BelgeKontrol(girilenDoktor.SicilNo))
            {
                Console.Write("Tarihi Giriniz:");
                DateTime tarih = DateTime.Parse(Console.ReadLine());
                DoktorTarihRandevularListe(tarih);
                Console.Write("\nBaşka Tarihe Bakmak İstermisiniz(E-H)");
                string secim = Console.ReadLine();
                if (secim.ToUpper().ToString()=="E")
                {
                    DoktorTarihRandevular();
                }
                else
                {
                    DoktorGirisMenu();
                }
            }
            else
            {
                Console.Write("Hiç Randevunuz Bulunmamakta...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                Console.ReadKey();
                DoktorGirisMenu();
            }
        }

        private static void DoktorTarihRandevularListe(DateTime tarih)
        {
         
            Console.Clear();
            Console.WriteLine("-->"+ tarih.ToShortDateString()+" Randevularınız");
            string[] saatler = dbIslem.RandevuListesi();
            string[] randevu= ctIslem.TarihKontrol(tarih.ToShortDateString(), girilenDoktor.SicilNo);
            if (randevu==null)
            {
                Console.WriteLine("Bu Tarihte Randevunuz Bulunmamaktadır.\nDevam Etmek için bir tuşa basınız...");
                Console.ReadKey();
                DoktorGirisMenu();    
            }
            for (int i = 1; i < randevu.Length; i++)
            {
                if (tarih<DateTime.Now)
                {               
                    if (randevu[i]!="")
                    {
                        if (randevu[i]=="0")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(saatler[i - 1] + ":BOŞ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(saatler[i - 1] + ":" + randevu[i]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        
                    }
                }
                else
                {
                    if (randevu[i] != "")
                    {
                        if (randevu[i] == "0")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(saatler[i - 1] + ":BOŞ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(saatler[i - 1] + ":" + randevu[i]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                }
            }

        }

        private static void DoktorTumRandevular()
        {
            if (ctIslem.BelgeKontrol(girilenDoktor.SicilNo))
            {
                List<string[]> randevularim = dbIslem.DoktorRandevuListesiTumu(girilenDoktor.SicilNo);
                Console.Write("Geçmiş Randevular Listelensinmi(E-H):");
                string secim = Console.ReadLine();
                if (secim.ToUpper().ToString()=="E")
                {
                    TumRandevulariListele(randevularim, true);
                }
                else
                {
                    TumRandevulariListele(randevularim, false);
                }
                Console.Write("\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                Console.ReadKey();
                DoktorGirisMenu();
            }
            else
            {
                Console.Write("Hiç Randevunuz Bulunmamakta...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                Console.ReadKey();
                DoktorGirisMenu();
            }
        }

        private static void TumRandevulariListele(List<string[]> randevularim, bool tumu)//Üşenmezsen Yanyana Yazdırmayı yap Daha ŞEkil Şukul Olur...
        {//Tclerinin listelenmesi bilerek yapılmış birşey.Çünkü menüde Üye bul kısmı var.Üyeyi bulabilmesi için bir Tc gerekiyor...
            Console.Clear();
            Console.WriteLine("-->Randevularınız");
            string[] saatler = dbIslem.RandevuListesi();//Saatleri Veriyor
            foreach (var item in randevularim)
            {
                DateTime tarih =DateTime.Parse( item[0]);
                if (tumu)
                {
                    if (tarih<DateTime.Now)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("--->" + tarih.ToShortDateString() + "<---");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Red;
                        for (int i = 1; i < item.Length; i++)
                        {
                            if (item[i]!="")
                            {
                                if (item[i]=="0")
                                {
                                    Console.WriteLine(saatler[i - 1] + ":BOŞ");
                                }
                                else
                                {
                                    Console.WriteLine(saatler[i - 1] + ":" + item[i]);
                                }
                                
                            }
                            
                        }
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("--->" + tarih.ToShortDateString() + "<---");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Green;
                        for (int i = 1; i < item.Length; i++)
                        {
                            if (item[i] != "")
                            {
                                if (item[i] == "0")
                                {
                                    Console.WriteLine(saatler[i - 1] + ":BOŞ");
                                }
                                else
                                {
                                    Console.WriteLine(saatler[i - 1] + ":" + item[i]);
                                }
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else
                {
                    if (tarih>DateTime.Now)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("--->" + tarih.ToShortDateString() + "<---");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Green;
                        for (int i = 1; i < item.Length; i++)
                        {
                            if (item[i] != "")
                            {
                                if (item[i] == "0")
                                {
                                    Console.WriteLine(saatler[i - 1] + ":BOŞ");
                                }
                                else
                                {
                                    Console.WriteLine(saatler[i - 1] + ":" + item[i]);
                                }
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                Console.WriteLine(); 
            }
        }

        private static void UyeGiris()
        {
            Console.Clear();
            Console.WriteLine("--->Üye Giriş");
            Console.Write("Tc Kimlik:");
            string tc = Console.ReadLine();
            Console.Write("Şifre:");
            string sifre=Console.ReadLine();
            Random r = new Random();
            string rasgele = r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString();
            Console.Write("Robot Olmadığınızı Kanıtlayın:"+rasgele+"\n>>>");
            string kanit = Console.ReadLine();

            if (ctIslem.RobotKontrol(rasgele,kanit))
            {
                girilenUye = ctIslem.UyeGiris(tc, sifre);
                if (girilenUye != null)//Üye Giriş
                {
                    UyeGirisMenu();
                }
                else
                {
                    Console.Write("Üye Bulunamadı...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                    Console.ReadKey();
                    HastahaneGirisMenu();
                } 
            }
            else
            {
                Console.Write("Robot Olup Olmadığınız Kanıtlanamadı...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                Console.ReadKey();
                HastahaneGirisMenu();
            }
        }

        private static void UyeGirisMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("-->Hoşgeldiniz "+girilenUye.AdSoyad);
                Console.Write("1.Randevu Al\n2.Profil Düzenle\n3.Randevularım\n4.(...)Çıkış Yap\n>>>");
                string secim = Console.ReadLine();
                switch (secim)
                {
                    case "1":RandevuAl(); break;
                    case "2":ProfilDuzenle(); break;
                    case "3":RandevuListem();break;
                    default:AnaMenu();break;
                }
            } while (true);
        }

        private static void ProfilDuzenle()
        {
            string tc = girilenUye.Tc;
            string sifre = girilenUye.Sifre;
            do
            {
                Console.Clear();
                Console.WriteLine("--->Profil Düzenle");
                Console.Write("1.Ad Soyad("+girilenUye.AdSoyad+")\n2.Sifre Değiş\n3.(...)Geri\n\n>>>");

                string secim = Console.ReadLine();
                switch (secim)
                {
                    case "1": Console.Write("Yeni Ad ve Soyadınızı Giriniz:"); string ad = Console.ReadLine(); profil.UyeYeniAd(ad, girilenUye.Tc); DbIslemleri.UyeListesi = new List<Uye>(); dbIslem.uyeAl(); girilenUye = ctIslem.UyeGiris(tc, sifre); break;
                    case "2":
                        Console.Write("Yeni Ad ve Soyadınızı Giriniz:");
                        string yeniSifre = Console.ReadLine();
                        if (ctIslem.SifreKural(yeniSifre))
                        {
                            profil.UyeYeniSifre(yeniSifre, tc);
                            DbIslemleri.UyeListesi = new List<Uye>();
                            dbIslem.uyeAl();
                            girilenUye = ctIslem.UyeGiris(tc, yeniSifre);
                            Console.WriteLine("Şifreniz Başarıyla Değişti.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Yeni Şifreniz Kurallara Uymuyor");
                            Console.ReadKey(); 
                        }
                        break;
                    default:HastahaneGirisMenu();break;
                }
            } while (true);
        }

        private static void RandevuListem()
        {
            Console.Clear();
            Console.WriteLine("--->Randevu Listem");
            List<string[]> randevularim = dbIslem.RandevuListem(girilenUye.Tc);
            if (randevularim==null)
            {
                Console.WriteLine("Daha önce hiç randevu yapmadınız...\nMenuye Devam etmek için bir tuşa basınız...");
                Console.ReadKey();
                UyeGirisMenu();
                
            }

            foreach (var item in randevularim)
            {
                Doktor dk = ctIslem.DoktorGiris(item[1]);//bana doktoru verdiği için kullandım normalde burda kullanmalık oluşmuş bir method değil.
                if (DateTime.Parse(item[0])<DateTime.Now)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(item[0] + " Tarihinde:");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(dk.Bolum + " Bölümüne Ait Doktor:'" + dk.AdSoyad + "' ile Saat:" + dbIslem.SecilenSaat(item[2])+" (BU RANDEVU GERÇEKLEŞTİ.)");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(item[0] + " Tarihinde:");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(dk.Bolum + " Bölümüne Ait Doktor:'" + dk.AdSoyad + "' ile Saat:" + dbIslem.SecilenSaat(item[2]));
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
              
            }
            Console.Write("Geri Dönmek İçin Tuşa Basınız...");
            Console.ReadKey();
            UyeGirisMenu();

        }

        private static void RandevuAl()
        {
            Console.Clear();
            Bolum secilen=(Bolum)1;
            Console.WriteLine("--->Randevu Al");
            try
            {
                BolumListesi();
                Console.Write("Bölüm Seçiniz:");
                secilen = (Bolum)int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.Write("Yanlış Bölüm Seçildi...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                Console.ReadKey();
                UyeGirisMenu();
            }
            Console.Clear();
            Console.WriteLine("--->"+secilen+" BÖLÜMÜNE AİT DOKTORLAR");
            foreach (Doktor item in DbIslemleri.DoktorListesi)
            {
                if (item.Bolum==secilen)
                {
                    Console.WriteLine(item.SicilNo+" "+item.AdSoyad);
                }
            }
            Console.Write("İstediğiniz Doktorun Sicil Numarasını Girin:");
            string sicilNo = Console.ReadLine();
            if (ctIslem.doktorSicil(sicilNo))
            {
                Console.Write("Randevu Tarihini Giriniz:");
                DateTime tarih = DateTime.Parse(Console.ReadLine());
                if (tarih<DateTime.Now)
                {
                    Console.Write("Tarih Günümüz Tarihinden Küçün girilemez...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                    Console.ReadKey();
                    UyeGirisMenu();
                }

                if (ctIslem.BelgeKontrol(sicilNo))//Randavu Belgesi Oluşturulmuş yani var hazırda tarihi ara metin belgesinde
                {
                    string[] belgeTarihRandevu = ctIslem.TarihKontrol(tarih.ToShortDateString(), sicilNo);
                    if (belgeTarihRandevu!=null)//Düzenleme Kısmı Varsa Düzenlenecek
                    {//Unutma Bu kısımda Tc kontrolü yapılacak neden ? Aynı adam 2 kere aynı doktordan aynı güne randevu alamaz :)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(tarih.ToShortDateString() + " Tarihi Uygun Saatler");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        TarihRandevuSaatleri(belgeTarihRandevu);
                        Console.Write("Saat Seçiniz:");
                        string secilenSaat = Console.ReadLine();
                        if (ctIslem.SaatUgunluk(secilenSaat))
                        {
                            if (belgeTarihRandevu[int.Parse(secilenSaat)] == "0")//Bundan sonra Tc Kontrol aşaması gelmesi gerek
                            {
                                if (ctIslem.RandevuTcKontrol(belgeTarihRandevu,girilenUye.Tc))
                                {
                                    //Burda KAyıta Doğru Devam Et
                                    //Saati,sicili,Tarihi,Tc Paslamam gerek...
                                    if(dbIslem.RandevuKayitTarih(sicilNo, secilenSaat, tarih.ToShortDateString(), girilenUye.Tc))
                                    {
                                        dbIslem.UyeRandevuKayit(sicilNo, secilenSaat, tarih.ToShortDateString(), girilenUye.Tc);
                                        Console.Write("Kayıt İşlemi Başarıyla Gerçekleşti...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                                        Console.ReadKey();
                                        UyeGirisMenu();
                                    }
                                    else
                                    {
                                        Console.Write("Kayıt İşlemi Gerçekleştirilemedi...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                                        Console.ReadKey();
                                        UyeGirisMenu();
                                    }
                                }
                                else
                                {
                                    Console.Write("Aynı Gün 2 Defa Randevu Alamazsınız..\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                                    Console.ReadKey();
                                    UyeGirisMenu();
                                }
                            }
                            else
                            {
                                Console.Write("Dolu Olan Saate Randevu Girişi Yapılamaz...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                                Console.ReadKey();
                                UyeGirisMenu();
                            }
                        }
                        else
                        {
                            Console.Write("Seçtiğiniz Saat Bulunamadı...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                            Console.ReadKey();
                            UyeGirisMenu();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(tarih.ToShortDateString()+" Tarihi Uygun Saatler");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        SaatleriListele();
                        Console.Write("Saat Seçiniz:");
                        string saat = Console.ReadLine();
                        if (ctIslem.SaatUgunluk(saat))
                        {
                            dbIslem.RandevuKayit(sicilNo, saat, tarih.ToShortDateString(), girilenUye.Tc);
                            dbIslem.UyeRandevuKayit(sicilNo, saat, tarih.ToShortDateString(), girilenUye.Tc);
                            Console.Write("Kayıt İşlemi Başarıyla Gerçekleşti...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                            Console.ReadKey();
                            UyeGirisMenu();
                        }
                        else
                        {
                            Console.Write("Seçtiğiniz Saat Bulunamadı\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                            Console.ReadKey();
                            UyeGirisMenu();
                        }
                    }
                }
                else// Randavu Oluşturulmamış Biz yapıcaz
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(tarih.ToShortDateString() + " Tarihi Uygun Saatler");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    SaatleriListele();
                    Console.Write("Saat Seçiniz:");
                    string saat = Console.ReadLine();
                    if (ctIslem.SaatUgunluk(saat))
                    {
                        dbIslem.RandevuKayit(sicilNo, saat, tarih.ToShortDateString(), girilenUye.Tc);
                        dbIslem.UyeRandevuKayit(sicilNo, saat, tarih.ToShortDateString(), girilenUye.Tc);
                        Console.Write("Kayıt İşlemi Başarıyla Gerçekleşti...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                        Console.ReadKey();
                        UyeGirisMenu();
                    }
                    else
                    {
                        Console.Write("Seçtiğiniz Saat Bulunamadı\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                        Console.ReadKey();
                        UyeGirisMenu();
                    }
                }
            }
            else
            {
                Console.Write("Sicil Numarası Yanlış...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                Console.ReadKey();
                UyeGirisMenu();
            }
            
        }

        private static void TarihRandevuSaatleri(string[] belgeTarihRandevu)//Üyenin Randevu Saatlerini Görüceği Kısım
        {
            string[] saat = dbIslem.RandevuListesi();
            for (int i = 1; i < belgeTarihRandevu.Length; i++)
            {
                if (i<10)
                {
                    if (belgeTarihRandevu[i] == "0")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("0"+i + "-)" + saat[i - 1] + "-->BOŞ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        if (belgeTarihRandevu[i]!="")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("0" + i + "-)" + saat[i - 1] + "-->DOLU");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                       
                    }
                    
                }
                else
                {

                    if (belgeTarihRandevu[i] == "0")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine( i + "-)"+ saat[i - 1] + "-->BOŞ" );
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        if (belgeTarihRandevu[i]!="")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(i + "-)" + saat[i - 1] + "-->DOLU");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                }
            }
        }

        static void SaatleriListele()
        {
           
            string[] saat = dbIslem.RandevuListesi();
            for (int i = 0; i < saat.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (i<9)
                {
                    Console.WriteLine("0"+(i + 1) + "-)" + saat[i]);   
                }
                else
                {
                    Console.WriteLine((i + 1) + "-)" + saat[i]);
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        private static void HastahaneKayitMenu()// Burası Admin Menüsü
        {
            do
            {
                Console.Clear();
                ListeYenile();
                Console.Write("--->Hastahane Kayıt \n1.Hemşire Kayıt\n2.Doktor Kayıt\n3.Hizmetli Kayıt\n4.(...)Geri\n\n>>>");
                string secim = Console.ReadLine();
                switch (secim)
                {
                    case "1":HemsireKayit(); break;
                    case "2": DoktorEkle(); break;
                    case "3":HizmetliKayit();break;
                    default: AnaMenu();
                        break;
                }
            } while (true);
        }

        private static void HemsireKayit()//Tam Kontrolünü yap
        {
            Console.Clear();
            Console.WriteLine("--->Hemşire Ekle");
            if (DbIslemleri.DoktorListesi.Count>0)
            {
                Console.Write("Tc Kimlik:");
                string tc = Console.ReadLine();
                if (ctIslem.TcKontrol(tc,1))//1=Hemsire
                {
                    Console.Write("Ad Soyad:");
                    string adSoyad = Console.ReadLine();
                    Console.WriteLine("--->Doktor Listesi<---");
                  ArrayList donenSiciller=  DoktorListele();
                    Console.Write("Sicil Numarasını Giriniz:");
                    string sicil = Console.ReadLine();
                    if (ctIslem.doktorSicil(sicil))
                    {
                        if (!donenSiciller.Contains(sicil))
                        {
                            dbIslem.HemsireEkle(tc, adSoyad, sicil);
                            Console.WriteLine("Kayıt İşlemi Başarıyla Gerçekleşti.\nMenüye Gitmek için bir tuşa basınız...");
                            Console.ReadKey();
                            HastahaneKayitMenu();
                        }
                        else
                        {
                            Console.WriteLine(sicil+" Numaralı Doktora Hemşire Atanmış.\nMenüye Gitmek için bir tuşa basınız...");
                            Console.ReadKey();
                            HastahaneKayitMenu();
                        }
                     
                    }
                    else
                    {
                        
                            Console.WriteLine("Doktor Bulunamadı.\nMenüye Gitmek için bir tuşa basınız...");
                            Console.ReadKey();
                            HastahaneKayitMenu();
                    }
                }
                else
                {
                    Console.WriteLine("Girilen Tc Kurallara uygun deil veya Aynı Tc daha önce kayıt yapılmış.\nMenüye Gitmek için bir tuşa basınız...");
                    Console.ReadKey();
                    HastahaneKayitMenu();
                }
            }
            else
            {
                Console.WriteLine("Sisteme Kayıtlı Doktor Bulunmamakta.Lütfen Önce Doktor Kaydı Yapınız.\nMenüye Gitmek için bir tuşa basınız...");
                Console.ReadKey();
                HastahaneKayitMenu();
            }
        }

        private static ArrayList DoktorListele()//Hemşireye Ait Listeleme
        {
            ListeYenile();
            ArrayList atanmisSiciller = new ArrayList();
            int sayac = 0;
            foreach (var item in DbIslemleri.DoktorListesi)
            {
                foreach (var item2 in DbIslemleri.HemsireListesi)
                {
                    if (item2.DoktorSicil.Equals(item.SicilNo)&& sayac<1)
                    {
                        sayac++;
                        atanmisSiciller.Add(item.SicilNo);
                    }
                }
                if (sayac < 1 )
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(item.SicilNo + " " + item.AdSoyad + " " + item.Bolum);
                    Console.ForegroundColor = ConsoleColor.Gray;

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(item.SicilNo + " " + item.AdSoyad + " " + item.Bolum);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    sayac = 0;
                }
            }
            return atanmisSiciller;
        }



        private static void HizmetliKayit()
        {
            Console.Clear();
            Console.WriteLine("--->Hizmetli Kayıt");
            Console.Write("Tc Kimliği Girin:");
            string tc = Console.ReadLine();
            if (ctIslem.TcKontrol(tc,2))//2=Hizmetli
            {
                Console.Write("Ad Soyad:");
                string adSoyad = Console.ReadLine();
                Console.Write("Görevi:");//Bu kısım Enumla yapabilirdik.(Üşünmezsen Enuma Çek)
                string gorevi = Console.ReadLine();
                try
                {
                    dbIslem.HizmetliEkle(tc, adSoyad, gorevi);
                    Console.Write("Hizmetli Başarıyla Kaydedildi...\n Menüye Gitmek için bir tuşa basın...");
                    Console.ReadKey();
                    HastahaneKayitMenu();
                }
                catch (Exception)
                {
                    Console.Write("Kayıt Başarısız !!!\n Menüye Gitmek için bir tuşa basın...");
                    Console.ReadKey();
                    HastahaneKayitMenu();

                }

            }
            else
            {
                Console.Write("Tc Kurallara Uygun girilmedi veya Aynı Tc Bulunmakta.\n Menüye Gitmek için bir tuşa basın...");
                Console.ReadKey();
                HastahaneKayitMenu();

            }
        }

        private static void DoktorEkle()
        {
            Console.Clear();
            Console.WriteLine("--->Doktor Kayıt");
            Console.Write("Ad Soyadınızı Giriniz:");
            string adSoyad = Console.ReadLine();
            Console.Write("Sicil Numaranızı Giriniz:");
            string sicil = Console.ReadLine();
            if (ctIslem.sicilKontrol(sicil))
            {
                BolumListesi();
                Console.Write("Bölüm Seçiniz:");
                int bolum = int.Parse(Console.ReadLine());
                if (bolum>Enum.GetValues(typeof(Bolum)).Length || bolum<1)
                {
                    Console.Write("Yanlış Bölüm Seçtiniz...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                    Console.ReadKey();
                    HastahaneKayitMenu();
                }
                else
                {
                    dbIslem.doktorEkle(sicil, adSoyad, bolum);
                    Console.Write("Kayıt İşlemi Başarıyla Gerçekleşti...\nAna Menüye Yönlendirilmek İçin Tuşa Basınız...");
                    Console.ReadKey();
                    HastahaneKayitMenu();
                }

            }
            else
            {
                Console.Write("Sicil Numaranız Düzgün Değil yada Daha Önce Girilmiş...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                Console.ReadKey();
                HastahaneKayitMenu();
            }

        }

        private static void BolumListesi()
        {
            for (int i = 1; i <= Enum.GetValues(typeof(Bolum)).Length; i++)
            {
                Console.WriteLine(i+"."+(Bolum)i);
                
            }
        }

        private static void UyeEkle()
        {
            Console.Clear();
            Console.WriteLine("--->Üye Kayıt");
            Console.Write("Ad Soyadınızı Giriniz:");
            string adSoyad = Console.ReadLine();
            Console.Write("Tc Kimlik Numaranızı Giriniz:");
            string tc = Console.ReadLine();
            if (ctIslem.TcKontrol(tc,0))//0=Üye
            {
                Console.Write("Şifrenizi Giriniz:");
                string sifre = Console.ReadLine();
                if(ctIslem.SifreKural(sifre))
                {
                    Console.Write("Yeniden Şifrenizi Giriniz:");
                    string sifre2 = Console.ReadLine();
                    if (ctIslem.SifreUyusma(sifre,sifre2))
                    {
                        dbIslem.uyeEkle(tc,adSoyad,sifre);
                        Console.Write("Kayıt İşlemi Başarıyla Gerçekleşti...\nAna Menüye Yönlendirilmek İçin Tuşa Basınız...");
                        Console.ReadKey();
                        AnaMenu();

                    }
                    else
                    {
                        Console.Write("Şifreler Uyuşmuyor...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                        Console.ReadKey();
                        AnaMenu();
                    }
                }
                else
                {
                    Console.Write("Şifreniz Kuralları İçermiyor...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                    Console.ReadKey();
                    AnaMenu();
                }
            }
            else
            {
                Console.Write("Tc Kimlik Numaranız Düzgün Değil yada Daha Önce Girilmiş...\nMenüye Yönlendirilmek İçin Tuşa Basınız...");
                Console.ReadKey();
                AnaMenu();
            }
            
        }

       
    }
}
