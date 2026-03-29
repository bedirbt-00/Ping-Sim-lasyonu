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