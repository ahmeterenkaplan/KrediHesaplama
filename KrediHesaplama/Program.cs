namespace KrediHesaplama
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal krediTutari;
            int taksitSayisi;

            KrediHesaplayici hesaplayici = new KrediHesaplayici();
            Console.WriteLine("Kredi tutarını girin :");
            krediTutari = decimal.Parse(Console.ReadLine());

            while (true)
            {
                Console.WriteLine("Taksit Sayısını girin: ");
                taksitSayisi = int.Parse(Console.ReadLine());
                if (taksitSayisi >= 1 && taksitSayisi <= 12)
                {
                    break;
                }


            }
            List<KrediTaksit> taksitler = hesaplayici.TaksitleriHesapla(krediTutari, taksitSayisi);
            Console.WriteLine(" \nödeme Planı");
            TaksitYazdir(taksitler);
            Console.ReadKey();
        }
        public static void TaksitYazdir(List<KrediTaksit> taksitler)
        {
            foreach (var taksit in taksitler)
            {
                Console.WriteLine($" {taksit.TaksitAyi}. Ödenmesi Gereken Taksit : {taksit.KrediTutar}");
            }

        }
    }
}
