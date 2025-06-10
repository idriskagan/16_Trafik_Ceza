using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Form2 : Form
    {
        private string selectedTC;
        private string selectedIsim;
        private string selectedPlaka;
        public static List<Ceza> tumCezalar = new List<Ceza>
        {
            new HizCezasi("Ali", "Yılmaz", "12345678901", "34ABC123", 500, "15.03.2024", "30"),
            new ParkCezasi("Ali", "Yılmaz", "12345678901", "34ABC123", 200, "20.04.2024", "Otobüs Durağı"),
            new KirmiziIsikCezasi("Ali", "Yılmaz", "12345678901", "34ABC123", 750, "25.05.2024", "Otomatik"),
            new HizCezasi("Ayşe", "Demir", "98765432100", "06DEF456", 300, "10.06.2024", "40"),
            new ParkCezasi("Mehmet", "Özkan", "11111111111", "35GHI789", 150, "05.07.2024", "Engelli Yeri")
        };

        // Parametreli constructor
        public Form2(string tc, string isim, string plaka)
        {
            InitializeComponent();
            selectedTC = tc;
            selectedIsim = isim;
            selectedPlaka = plaka;




            LoadCezaData();
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void LoadCezaData()
        {
            dataGridView1.Rows.Clear();

            var kisiCezalari = tumCezalar
                .Where(c => c.TCNo == selectedTC)
                .ToList();

            foreach (var ceza in kisiCezalari)
            {
                int rowIndex = dataGridView1.Rows.Add();

                string cezaTuru = "";
                string detay = "";

                if (ceza is HizCezasi hizCeza)
                {
                    cezaTuru = "Hız Cezası";
                    detay = hizCeza.Hiz;
                }
                else if (ceza is ParkCezasi parkCeza)
                {
                    cezaTuru = "Park Cezası";
                    detay = parkCeza.Sebep;
                }
                else if (ceza is KirmiziIsikCezasi isikCeza)
                {
                    cezaTuru = "Kırmızı Işık Cezası";
                    detay = isikCeza.TespitSekli;
                }

                dataGridView1.Rows[rowIndex].Cells[0].Value = cezaTuru;
                dataGridView1.Rows[rowIndex].Cells[1].Value = ceza.Tarih;
                dataGridView1.Rows[rowIndex].Cells[2].Value = ceza.Tutar.ToString() + " TL";
                dataGridView1.Rows[rowIndex].Cells[3].Value = ceza.OdendiMi ? "ÖDENDİ" : "ÖDENMEDİ";
                dataGridView1.Rows[rowIndex].Cells[4].Value = detay; // 👈 Detay eklendi



                label8.Text = $"Toplam Ödenmemiş Borç: {Ceza.ToplamBorc(tumCezalar, selectedTC)} TL";

            }
        }
        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ComboBox ve TextBox'lardan verileri al
            string cezaTuru = comboBox1.SelectedItem?.ToString() ?? "";
            string tarih = textBox2.Text.Trim();
            string ucret = textBox3.Text.Trim();
            string Detay = textBox4.Text.Trim();

            // Basit boş kontrolü
            if (string.IsNullOrEmpty(cezaTuru) || string.IsNullOrEmpty(ucret))
            {
                MessageBox.Show("Lütfen ceza türü ve ücret bilgisini doldurun!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ücret kontrolü (sayısal olmalı)
            if (!decimal.TryParse(ucret, out decimal ucretDeger))
            {
                MessageBox.Show("Lütfen geçerli bir ücret tutarı girin!", "Geçersiz Ücret", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tarih kontrolü - boşsa bugünün tarihi kullan
            if (string.IsNullOrEmpty(tarih))
            {
                tarih = DateTime.Now.ToString("dd.MM.yyyy");
            }

            // DataGridView'e yeni satır ekle
            int rowIndex = dataGridView1.Rows.Add();
            dataGridView1.Rows[rowIndex].Cells[0].Value = cezaTuru; // Ceza
            dataGridView1.Rows[rowIndex].Cells[1].Value = tarih; // Tarih
            dataGridView1.Rows[rowIndex].Cells[2].Value = ucret + " TL"; // Ücret
            dataGridView1.Rows[rowIndex].Cells[3].Value = "ÖDENMEDİ"; // Durum (varsayılan)
            dataGridView1.Rows[rowIndex].Cells[4].Value = Detay; // Detay

            // Yeni cezayı tumCezalar listesine de ekle
            Ceza yeniCeza = null;
            switch (cezaTuru)
            {
                case "Hız Cezası":
                    yeniCeza = new HizCezasi(selectedIsim.Split(' ')[0],
                                           selectedIsim.Contains(' ') ? selectedIsim.Split(' ')[1] : "",
                                           selectedTC, selectedPlaka, ucretDeger, tarih, Detay);
                    break;
                case "Park Cezası":
                    yeniCeza = new ParkCezasi(selectedIsim.Split(' ')[0],
                                            selectedIsim.Contains(' ') ? selectedIsim.Split(' ')[1] : "",
                                            selectedTC, selectedPlaka, ucretDeger, tarih, Detay);
                    break;
                case "Kırmızı Işık Cezası":
                    yeniCeza = new KirmiziIsikCezasi(selectedIsim.Split(' ')[0],
                                                   selectedIsim.Contains(' ') ? selectedIsim.Split(' ')[1] : "",
                                                   selectedTC, selectedPlaka, ucretDeger, tarih, Detay);
                    break;
            }

            if (yeniCeza != null)
            {
                tumCezalar.Add(yeniCeza);
            }

            // TextBox'ları temizle
            comboBox1.SelectedIndex = -1;
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            MessageBox.Show("Ceza başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            label8.Text = $"Toplam Ödenmemiş Borç: {Ceza.ToplamBorc(tumCezalar, selectedTC)} TL";



        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Seçili satırı kontrol et
            if (dataGridView1.SelectedCells.Count > 0)
            {
                // Seçilen hücreyi al (ilk hücreyi alıyoruz, durum sütunundaki hücre)
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                var durumCell = dataGridView1.Rows[rowIndex].Cells[3];

                // Eğer durum "ÖDENDİ" değilse, durumu "ÖDENDİ" olarak değiştir
                if (durumCell.Value.ToString() != "ÖDENDİ")
                {
                    // Seçilen satırdaki ceza bilgilerini al
                    string cezaTuru = dataGridView1.Rows[rowIndex].Cells[0].Value?.ToString() ?? "";
                    string cezaTarihi = dataGridView1.Rows[rowIndex].Cells[1].Value?.ToString() ?? "";
                    string cezaTutarStr = dataGridView1.Rows[rowIndex].Cells[2].Value?.ToString().Replace(" TL", "") ?? "";

                    decimal.TryParse(cezaTutarStr, out decimal cezaTutar);

                    // İlgili cezayı bul ve öde
                    var ceza = tumCezalar.FirstOrDefault(c =>
                        c.TCNo == selectedTC &&
                        c.Tarih == cezaTarihi &&
                        c.Tutar == cezaTutar &&
                        !c.OdendiMi);

                    if (ceza != null)
                    {
                        ceza.Ode(); // Durumu "ÖDENDİ" olarak işaretle
                        durumCell.Value = "ÖDENDİ";
                        MessageBox.Show("Ceza durumu başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("İlgili ceza bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Bu ceza zaten ödendi olarak işaretlenmiş.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir ceza seçin!", "Seçim Yapılmadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            label8.Text = $"Toplam Ödenmemiş Borç: {Ceza.ToplamBorc(tumCezalar, selectedTC)} TL";

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

   

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}