using System;
using System.Collections.Generic;
using System.Linq;


namespace WinFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Form ismini deðiþirsen burayý da deðiþ
            Form2 form2 = new Form2();

            form2.FormClosed += (s, args) => this.Show();  // Form2 kapanýnca Form1 tekrar göster

            form2.Show();

            this.Hide(); // Form1'i gizle
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string TC = TCtextBox.Text.Trim();
            string isim = isimTextBox.Text.Trim();
            string plaka = PlakaTextBox.Text.Trim();

            // Basit boþ kontrolü
            if (string.IsNullOrEmpty(TC) || string.IsNullOrEmpty(isim) || string.IsNullOrEmpty(plaka))
            {
                MessageBox.Show("Lütfen tüm alanlarý doldurun!", "Eksik bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Yeni satýr ekle
            int rowIndex = dataGridView1.Rows.Add();
            dataGridView1.Rows[rowIndex].Cells["Column1"].Value = TC;
            dataGridView1.Rows[rowIndex].Cells["Column2"].Value = isim;
            dataGridView1.Rows[rowIndex].Cells["Column3"].Value = plaka;

            // TextBox'larý temizle (isteðe baðlý)
            TCtextBox.Clear();
            isimTextBox.Clear();
            PlakaTextBox.Clear();


        }

        private void TCtextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }


}





// ARKA UÇ

// Arayüz (interface)

public interface IOdenecek
{
    void Ode();
}

// Ceza sýnýfý
public class Ceza : IOdenecek
{
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string TCNo { get; set; }
    public string Plaka { get; set; }
    public decimal Tutar { get; set; }
    public bool OdendiMi { get; private set; }

    public Ceza(string ad, string soyad, string tcNo, string plaka, decimal tutar)
    {
        Ad = ad;
        Soyad = soyad;
        TCNo = tcNo;
        Plaka = plaka;
        Tutar = tutar;
        OdendiMi = false;
    }

    public virtual void Ode()
    {
        OdendiMi = true;
    }

    public override string ToString()
    {
        return $"{Ad} {Soyad} | TC: {TCNo} | Plaka: {Plaka} | Tutar: {Tutar} TL | Ödendi: {OdendiMi}";
    }

    public static decimal ToplamBorc(List<Ceza> cezalar, string tcNo)
    {
        return cezalar
            .Where(c => c.TCNo == tcNo && !c.OdendiMi)
            .Sum(c => c.Tutar);
    }

    public static List<Ceza> OdenmemisCezalar(List<Ceza> cezalar, string tcNo)
    {
        return cezalar
            .Where(c => c.TCNo == tcNo && !c.OdendiMi)
            .ToList();
    }
}

// Ceza türleri
public class HizCezasi : Ceza
{
    public HizCezasi(string ad, string soyad, string tcNo, string plaka, decimal tutar)
        : base(ad, soyad, tcNo, plaka, tutar)
    {
    }

    public override string ToString()
    {
        return $"[Hýz Cezasý] {base.ToString()}";
    }
}

public class ParkCezasi : Ceza
{
    public ParkCezasi(string ad, string soyad, string tcNo, string plaka, decimal tutar)
        : base(ad, soyad, tcNo, plaka, tutar)
    {
    }

    public override string ToString()
    {
        return $"[Park Cezasý] {base.ToString()}";
    }
}

public class KirmiziIsikCezasi : Ceza
{
    public KirmiziIsikCezasi(string ad, string soyad, string tcNo, string plaka, decimal tutar)
        : base(ad, soyad, tcNo, plaka, tutar)
    {
    }

    public override string ToString()
    {
        return $"[Kýrmýzý Iþýk Cezasý] {base.ToString()}";
    }
}

// Örnek Konsol Uygulamasý
class Program
{
    static void Deneme(string[] args)
    {
        List<Ceza> cezalar = new List<Ceza>();

        // 1. Kiþiye birkaç ceza ekleyelim
        cezalar.Add(new HizCezasi("Ali", "Yýlmaz", "12345678901", "34ABC123", 500));
        cezalar.Add(new ParkCezasi("Ali", "Yýlmaz", "12345678901", "34ABC123", 200));
        cezalar.Add(new KirmiziIsikCezasi("Ayþe", "Demir", "98765432100", "06DEF456", 750));

        Console.WriteLine(">>> Ali'nin tüm cezalarý:");
        foreach (var ceza in cezalar.Where(c => c.TCNo == "12345678901"))
        {
            Console.WriteLine(ceza);
        }

        Console.WriteLine($"\n>>> Toplam borç: {Ceza.ToplamBorc(cezalar, "12345678901")} TL");

        // Ýlk cezayý ödeyelim
        Console.WriteLine("\n>>> Ali'nin ilk cezasý ödeniyor...");
        var ilkCeza = cezalar.First(c => c.TCNo == "12345678901");
        ilkCeza.Ode();

        Console.WriteLine("\n>>> Güncel ceza durumu:");
        foreach (var ceza in cezalar.Where(c => c.TCNo == "12345678901"))
        {
            Console.WriteLine(ceza);
        }

        Console.WriteLine($"\n>>> Güncel toplam borç: {Ceza.ToplamBorc(cezalar, "12345678901")} TL");

        Console.WriteLine("\n>>> Ödenmemiþ cezalar:");
        foreach (var ceza in Ceza.OdenmemisCezalar(cezalar, "12345678901"))
        {
            Console.WriteLine(ceza);
        }

        Console.WriteLine("\nProgram sona erdi. Devam etmek için bir tuþa basýn...");
        Console.ReadKey();
    }
}

