namespace WinFormsApp4
{
    public class ParkCezasi : Ceza
    {
        public string Sebep;
        public ParkCezasi(string ad, string soyad, string tcNo, string plaka, decimal tutar, string tarih, string sebep)
            : base(ad, soyad, tcNo, plaka, tutar, tarih)
        {
            this.Sebep = sebep;
        }

        public override string ToString()
        {
            return $"[Park Cezasý] {base.ToString()} | Sebep: {Sebep}";
        }
    }
}