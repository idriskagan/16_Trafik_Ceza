namespace WinFormsApp4
{
    public class KirmiziIsikCezasi : Ceza
    {
        public string TespitSekli;
        public KirmiziIsikCezasi(string ad, string soyad, string tcNo, string plaka, decimal tutar, string tarih, string tespitSekli)
            : base(ad, soyad, tcNo, plaka, tutar, tarih)
        {
            this.TespitSekli = tespitSekli;
        }

        public override string ToString()
        {
            return $"[Kýrmýzý Iþýk Cezasý] {base.ToString()} | Tespit: {TespitSekli}";
        }
    }
}