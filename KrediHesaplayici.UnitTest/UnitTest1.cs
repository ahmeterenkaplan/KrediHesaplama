namespace KrediHesaplayici.UnitTest
{
    [TestClass]
    public class KrediHesaplayiciTests
    {
        [TestMethod]
        public void TaksitlerHesaplanirkenTutaraTamBolunenTaksitSayisiBasariylaHesaplanir()

        {
            //arrange

            decimal krediTutari = 120.12m;
            int taksitSayisi = 12;
            KrediHesaplama.KrediHesaplayici sut = new KrediHesaplama.KrediHesaplayici(); //systemundertest 


            //action

            var taksitler = sut.TaksitleriHesapla(krediTutari, taksitSayisi);
            // assertion

            Assert.AreEqual(12, taksitler.Count);

            Assert.AreEqual(1, taksitler[0].TaksitAyi);
            Assert.AreEqual(10.01m, taksitler[0].KrediTutar);

            Assert.AreEqual(2, taksitler[1].TaksitAyi);
            Assert.AreEqual(10.01m, taksitler[1].KrediTutar);

            Assert.AreEqual(3, taksitler[2].TaksitAyi);
            Assert.AreEqual(10.01m, taksitler[2].KrediTutar);

            Assert.AreEqual(4, taksitler[3].TaksitAyi);
            Assert.AreEqual(10.01m, taksitler[3].KrediTutar);

            Assert.AreEqual(5, taksitler[4].TaksitAyi);
            Assert.AreEqual(10.01m, taksitler[4].KrediTutar);

            Assert.AreEqual(6, taksitler[5].TaksitAyi);
            Assert.AreEqual(10.01m, taksitler[5].KrediTutar);

            Assert.AreEqual(7, taksitler[6].TaksitAyi);
            Assert.AreEqual(10.01m, taksitler[6].KrediTutar);

            Assert.AreEqual(8, taksitler[7].TaksitAyi);
            Assert.AreEqual(10.01m, taksitler[7].KrediTutar);

            Assert.AreEqual(9, taksitler[8].TaksitAyi);
            Assert.AreEqual(10.01m, taksitler[8].KrediTutar);

            Assert.AreEqual(10, taksitler[9].TaksitAyi);
            Assert.AreEqual(10.01m, taksitler[9].KrediTutar);

            Assert.AreEqual(11, taksitler[10].TaksitAyi);
            Assert.AreEqual(10.01m, taksitler[10].KrediTutar);

            Assert.AreEqual(12, taksitler[11].TaksitAyi);
            Assert.AreEqual(10.01m, taksitler[11].KrediTutar);

        }
        [TestMethod]

        public void TaksitlerHesaplanirkenTutaraTamBolunmeyenTaksitSayisiBasariylaHesaplanir()

        { //arrange
            decimal krediTutari = 999.99m;

            int taksitSayisi = 7;

            KrediHesaplama.KrediHesaplayici sut = new KrediHesaplama.KrediHesaplayici();//systemundertest 

            //action
            var taksitler = sut.TaksitleriHesapla(krediTutari, taksitSayisi);

            //assert
            Assert.AreEqual(7, taksitler.Count);

            Assert.AreEqual(1, taksitler[0].TaksitAyi);
            Assert.AreEqual(142.85m, taksitler[0].KrediTutar);

            Assert.AreEqual(2, taksitler[1].TaksitAyi);
            Assert.AreEqual(142.85m, taksitler[1].KrediTutar);

            Assert.AreEqual(3, taksitler[2].TaksitAyi);
            Assert.AreEqual(142.85m, taksitler[2].KrediTutar);

            Assert.AreEqual(4, taksitler[3].TaksitAyi);
            Assert.AreEqual(142.85m, taksitler[3].KrediTutar);

            Assert.AreEqual(5, taksitler[4].TaksitAyi);
            Assert.AreEqual(142.85m, taksitler[4].KrediTutar);

            Assert.AreEqual(6, taksitler[5].TaksitAyi);
            Assert.AreEqual(142.85m, taksitler[5].KrediTutar);

            Assert.AreEqual(7, taksitler[6].TaksitAyi);
            Assert.AreEqual(142.89m, taksitler[6].KrediTutar);
        }
        [TestMethod]
        public void TaksitlerHesaplanirkenTutarveTaksitSayisiEsitOlanBasariylaHesaplanir()
        {
            //arrange
            decimal krediTutari = 0.12m;
            int taksitSayisi = 12;
            KrediHesaplama.KrediHesaplayici sut = new KrediHesaplama.KrediHesaplayici();//systemundertest 
                                                                                        //action
            var taksitler = sut.TaksitleriHesapla(krediTutari, taksitSayisi);


            // assertion
            Assert.AreEqual(12, taksitler.Count);
            Assert.AreEqual(1, taksitler[0].TaksitAyi);
            Assert.AreEqual(0.01m, taksitler[0].KrediTutar);
            Assert.AreEqual(2, taksitler[1].TaksitAyi);
            Assert.AreEqual(0.01m, taksitler[1].KrediTutar);
            Assert.AreEqual(3, taksitler[2].TaksitAyi);
            Assert.AreEqual(0.01m, taksitler[2].KrediTutar);
            Assert.AreEqual(4, taksitler[3].TaksitAyi);
            Assert.AreEqual(0.01m, taksitler[3].KrediTutar);
            Assert.AreEqual(5, taksitler[4].TaksitAyi);
            Assert.AreEqual(0.01m, taksitler[4].KrediTutar);
            Assert.AreEqual(6, taksitler[5].TaksitAyi);
            Assert.AreEqual(0.01m, taksitler[5].KrediTutar);
            Assert.AreEqual(7, taksitler[6].TaksitAyi);
            Assert.AreEqual(0.01m, taksitler[6].KrediTutar);
            Assert.AreEqual(8, taksitler[7].TaksitAyi);
            Assert.AreEqual(0.01m, taksitler[7].KrediTutar);
            Assert.AreEqual(9, taksitler[8].TaksitAyi);
            Assert.AreEqual(0.01m, taksitler[8].KrediTutar);
            Assert.AreEqual(10, taksitler[9].TaksitAyi);
            Assert.AreEqual(0.01m, taksitler[9].KrediTutar);
            Assert.AreEqual(11, taksitler[10].TaksitAyi);
            Assert.AreEqual(0.01m, taksitler[10].KrediTutar);
            Assert.AreEqual(12, taksitler[11].TaksitAyi);
            Assert.AreEqual(0.01m, taksitler[11].KrediTutar);


        }
        [TestMethod]

        //[ExpectedException (typeof (Exception))]

        public void KrediTutariTaksitSayisiKadarKurustanAzIseBasarisiz()
        {
            //arrange
            decimal krediTutari = 0.11m;
            int taksitSayisi = 12;
            KrediHesaplama.KrediHesaplayici sut = new KrediHesaplama.KrediHesaplayici();//systemundertest
            try
            {
                //action
                var taksitler = sut.TaksitleriHesapla(krediTutari, taksitSayisi);
            }
            catch (Exception ex)
            {

                // assert
                Assert.AreEqual("Kredi tutar�, taksit say�s� kadar kuru�tan az olamaz.", ex.Message);
            }
        }

    }

}
