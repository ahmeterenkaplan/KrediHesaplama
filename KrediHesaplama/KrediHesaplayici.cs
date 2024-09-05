using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrediHesaplama
{
    public class KrediHesaplayici
    {
        public List<KrediTaksit> TaksitleriHesapla(decimal krediTutari, int taksitSayisi)
        {
            if (krediTutari < taksitSayisi / 100m)
                throw new Exception("Kredi tutarı, taksit sayısı kadar kuruştan az olamaz.");

            decimal kalanTutar = krediTutari;
            List<KrediTaksit> taksitler = new List<KrediTaksit>();

            decimal taksitTutari = krediTutari / taksitSayisi;
            decimal odencekTutar = KusuratiAsagiYuvarla(taksitTutari, 2);

            for (int i = 0; i < taksitSayisi; i++)
            {
                kalanTutar -= odencekTutar;
                if (i == taksitSayisi - 1)
                {
                    odencekTutar += kalanTutar;
                }

                KrediTaksit taksit = new KrediTaksit(i + 1, odencekTutar);
                taksitler.Add(taksit);

            }
            return taksitler;
        }

        public decimal KusuratiAsagiYuvarla(decimal tutar, int kusuratSayisi)
        {
            return Math.Floor(tutar * (int)Math.Pow(10, kusuratSayisi)) / (int)Math.Pow(10, 2);
        }
    }
}