namespace WinFormsApp4
{

    public interface IOdenecek
    {
        void Ode();
        decimal ToplamBorc(List<Ceza> cezalar, string tcNo);
        string ToString();
        List<Ceza> OdenmemisCezalar(List<Ceza> cezalar, string tcNo);
    }
    public class Ceza : IOdenecek
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TCNo { get; set; }
        public string Plaka { get; set; }
        public decimal Tutar { get; set; }
        public string Tarih { get; set; }
        public bool OdendiMi { get; private set; }

        // Tarih belirtilmediðinde bugünün tarihini kullanan constructor
        public Ceza(string ad, string soyad, string tcNo, string plaka, decimal tutar)
            : this(ad, soyad, tcNo, plaka, tutar, DateTime.Now.ToString("dd.MM.yyyy"))
        {
        }

        // Tarih belirtilen constructor
        public Ceza(string ad, string soyad, string tcNo, string plaka, decimal tutar, string tarih)
        {
            Ad = ad;
            Soyad = soyad;
            TCNo = tcNo;
            Plaka = plaka;
            Tutar = tutar;
            Tarih = tarih;
            OdendiMi = false;
        }

        public virtual void Ode()
        {
            OdendiMi = true;
        }

        public override string ToString()
        {
            return $"{Ad} {Soyad} | TC: {TCNo} | Plaka: {Plaka} | Tutar: {Tutar} TL | Tarih: {Tarih} | Ödendi: {OdendiMi}";
        }

        public decimal ToplamBorc(List<Ceza> cezalar, string tcNo)
        {
            return cezalar
                .Where(c => c.TCNo == tcNo && !c.OdendiMi)
                .Sum(c => c.Tutar);

        }

        public List<Ceza> OdenmemisCezalar(List<Ceza> cezalar, string tcNo)
        {
            return cezalar
                .Where(c => c.TCNo == tcNo && !c.OdendiMi)
                .ToList();
        }

    }
}