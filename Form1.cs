using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


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
            // Se�ili sat�r kontrol�
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("L�tfen bir sat�r se�in!", "Se�im Hatas�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Se�ili sat�rdan verileri al
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string tc = selectedRow.Cells["Column1"].Value?.ToString() ?? "";
            string isim = selectedRow.Cells["Column2"].Value?.ToString() ?? "";
            string plaka = selectedRow.Cells["Column3"].Value?.ToString() ?? "";

            // Bo� sat�r kontrol�
            if (string.IsNullOrEmpty(tc) || string.IsNullOrEmpty(isim) || string.IsNullOrEmpty(plaka))
            {
                MessageBox.Show("Se�ilen sat�rda eksik bilgiler var! L�tfen dolu bir sat�r se�in.", "Bo� Sat�r", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Form ismini de�i�irsen buray� da de�i�
            Form2 form2 = new Form2(tc, isim, plaka); // Se�ili verileri Form2'ye g�nder

            form2.FormClosed += (s, args) => this.Show();  // Form2 kapan�nca Form1 tekrar g�ster

            form2.Show();

            this.Hide(); // Form1'i gizle
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string TC = TCtextBox.Text.Trim();
            string isim = isimTextBox.Text.Trim();
            string plaka = PlakaTextBox.Text.Trim().ToUpper(); // K���k harfleri b�y�k yap

            // TC kimlik numaras� kontrol�
            if (TC.Length != 11 || !TC.All(char.IsDigit))
            {
                MessageBox.Show("TC kimlik numaras� 11 haneli ve sadece rakamlardan olu�mal�d�r!", "Hatal� TC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Plaka bo� mu?
            if (string.IsNullOrEmpty(plaka))
            {
                MessageBox.Show("Plaka alan� bo� b�rak�lamaz!", "Eksik bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Plaka format� kontrol� (�rn: 34ABC123)
            Regex plakaRegex = new Regex(@"^\d{2}[A-Z]{1,3}\d{2,4}$");
            if (!plakaRegex.IsMatch(plaka))
            {
                MessageBox.Show("Plaka format� hatal�! �rnek: 34ABC123", "Hatal� Plaka Format�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // �l kodu kontrol� (01-81 aras�)
            int ilKodu = int.Parse(plaka.Substring(0, 2));
            if (ilKodu < 1 || ilKodu > 81)
            {
                MessageBox.Show("Plakan�n ba��ndaki il kodu 01 ile 81 aras�nda olmal�d�r!", "Ge�ersiz �l Kodu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Di�er bo� alan kontrol�
            if (string.IsNullOrEmpty(isim))
            {
                MessageBox.Show("L�tfen isim alan�n� doldurun!", "Eksik bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Ayn� TC veya plaka kontrol�
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string existingTC = row.Cells["Column1"].Value?.ToString();
                    string existingPlaka = row.Cells["Column3"].Value?.ToString();

                    if (existingTC == TC)
                    {
                        MessageBox.Show("Bu TC numaras� zaten eklenmi�!", "Tekrar Eden TC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (existingPlaka == plaka)
                    {
                        MessageBox.Show("Bu plaka zaten eklenmi�!", "Tekrar Eden Plaka", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // Sat�r� ekle
            int rowIndex = dataGridView1.Rows.Add();
            dataGridView1.Rows[rowIndex].Cells["Column1"].Value = TC;
            dataGridView1.Rows[rowIndex].Cells["Column2"].Value = isim;
            dataGridView1.Rows[rowIndex].Cells["Column3"].Value = plaka;

            // TextBox'lar� temizle
            TCtextBox.Clear();
            isimTextBox.Clear();
            PlakaTextBox.Clear();
        }


        private void TCtextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }


}





// ARKA U�

// Aray�z (interface)

public interface IOdenecek
{
    void Ode();
    decimal ToplamBorc(List<Ceza> cezalar, string tcNo);
    string ToString();
    List<Ceza> OdenmemisCezalar(List<Ceza> cezalar, string tcNo);
}

// Ceza s�n�f�
public class Ceza : IOdenecek
{
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string TCNo { get; set; }
    public string Plaka { get; set; }
    public decimal Tutar { get; set; }
    public string Tarih { get; set; }
    public bool OdendiMi { get; private set; }

    // Tarih belirtilmedi�inde bug�n�n tarihini kullanan constructor
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
        return $"{Ad} {Soyad} | TC: {TCNo} | Plaka: {Plaka} | Tutar: {Tutar} TL | Tarih: {Tarih} | �dendi: {OdendiMi}";
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

// Ceza t�rleri
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
        return $"[Park Cezas�] {base.ToString()} | Sebep: {Sebep}";
    }
}


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
        return $"[K�rm�z� I��k Cezas�] {base.ToString()} | Tespit: {TespitSekli}";
    }
}

// �rnek Konsol Uygulamas�
