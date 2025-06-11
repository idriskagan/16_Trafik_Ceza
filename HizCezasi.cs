namespace WinFormsApp4
{
    public class HizCezasi : Ceza
    {
        public string Hiz;

        public HizCezasi(string ad, string soyad, string tcNo, string plaka, decimal tutar, string tarih, string hiz)
            : base(ad, soyad, tcNo, plaka, tutar, tarih)
        {
            this.Hiz = hiz;
        }

        public override string ToString()
        {
            return $"[H�z Cezas�] {base.ToString()} | Anl�k H�z: {Hiz}";
        }

    }
}