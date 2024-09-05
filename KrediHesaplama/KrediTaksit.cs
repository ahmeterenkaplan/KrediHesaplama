using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrediHesaplama
{
    public class KrediTaksit
    {
        public int TaksitAyi { get; set; }

        public decimal KrediTutar { get; set; }

        public KrediTaksit(int taksitAyi, decimal krediTutari)
        {
            TaksitAyi = taksitAyi;
            KrediTutar = krediTutari;
        }


    }
}
