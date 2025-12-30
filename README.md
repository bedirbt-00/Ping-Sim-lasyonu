# Ping Simülasyonu - Grup 1

Bu proje, Afyon Kocatepe Üniversitesi Sinanpaşa MYO "Araştırma Yöntemleri" dersi için geliştirilmiş, C# tabanlı bir ağ erişim testi (Ping) uygulamasıdır.

## Grup Üyeleri
 Bedirhan Tanrıkulu

## Projenin Amacı
Kullanıcının belirlediği web adreslerine veya IP adreslerine ICMP (Internet Control Message Protocol) paketleri göndererek, hedef cihazın ulaşılabilirliğini test etmek ve gidiş-dönüş gecikme süresini (RoundtripTime) ölçmektir.

## Çalışma Mantığı
1. Program açıldığında kullanıcıdan bir hedef adres (örn: google.com) ister.
2. Girilen adrese 4 adet ping paketi gönderilir.
3. Her paketin durumu (Başarılı/Başarısız), gecikme süresi (ms) ve TTL değeri ekrana yazdırılır.
4. İşlem bittikten sonra program kapanmaz, yeni bir adres sormak için döngüye girer.
5. Kullanıcı `q` veya `exit` yazana kadar program çalışmaya devam eder.

## Kullanılan Teknolojiler
- **Programlama Dili:** C# (.NET Core / .NET Framework)
- **Kütüphane:** `System.Net.NetworkInformation` (Ping sınıfı için)
- **IDE:** Visual Studio 2022

## Sistem Gereksinimleri
- **İşletim Sistemi:** Windows 10 veya Windows 11
- **Yazılım:** .NET Runtime yüklü olmalıdır.

## Kurulum ve Çalıştırma
1. `Grup1_PingSimulasyonu` klasörünü bilgisayarınıza indirin.
2. `Program.cs` dosyasını Visual Studio ile açın.
3. Klavyeden `F5` tuşuna basarak veya "Başlat" butonuna tıklayarak konsolu çalıştırın.

## Lisans
Bu proje **MIT Lisansı** ile lisanslanmıştır.

**Seçilme Gerekçesi:** MIT lisansı, yazılımın özgürce kullanılmasına, değiştirilmesine ve dağıtılmasına izin veren açık kaynaklı bir lisanstır. Akademik ve eğitim amaçlı projelerin paylaşımı için en uygun ve kısıtlaması en az olan lisans türü olduğu için tercih edilmiştir.



Kod Bloğu:

using System;
using System.Net.NetworkInformation;
using System.Threading;

namespace PingSimulasyonu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("     GRUP 1 - PING SİMÜLASYONU ARACI      ");
            Console.WriteLine("     (Çıkmak için 'q' yazıp Enter'a basın)");
            Console.WriteLine("------------------------------------------");

            while (true)
            {
                Console.WriteLine(); 
                Console.Write("Ping atmak istediğiniz adresi girin: ");

                string girilenDeger = Console.ReadLine() ?? "";

                if (girilenDeger.ToLower() == "q" || girilenDeger.ToLower() == "exit")
                {
                    Console.WriteLine("Programdan çıkılıyor...");
                    break; 
                }


                string adres = girilenDeger;
                if (string.IsNullOrWhiteSpace(adres))
                {
                    Console.WriteLine("Hata: Boş adres girdiniz! Varsayılan (google.com) deneniyor...");
                    adres = "google.com";
                }

                Ping pingGonderici = new Ping();
                int pingSayisi = 4;
                int timeout = 1000;

                Console.WriteLine($"--- {adres} için ping işlemi başlatılıyor ---");

                for (int i = 0; i < pingSayisi; i++)
                {
                    try
                    {
                        PingReply cevap = pingGonderici.Send(adres, timeout);

                        if (cevap.Status == IPStatus.Success)
                        {
                            Console.WriteLine(
                                $"Ping {i + 1}: " +
                                $"Adres={cevap.Address} " +
                                $"Süre={cevap.RoundtripTime}ms " +
                                $"TTL={cevap.Options?.Ttl}"
                            );
                        }
                        else
                        {
                            Console.WriteLine($"Ping {i + 1}: Hata - {cevap.Status}");
                        }
                    }
                    catch (PingException)
                    {
                        Console.WriteLine($"Ping {i + 1}: Bağlantı başarısız (Host bulunamadı).");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Genel Hata: {ex.Message}");
                    }
                    Thread.Sleep(1000);
                }
                Console.WriteLine("--- İşlem bitti, yeni adres bekleniyor ---");
            }
        }
    }
}
