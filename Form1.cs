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
            // Seçili satýr kontrolü
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir satýr seçin!", "Seçim Hatasý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçili satýrdan verileri al
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string tc = selectedRow.Cells["Column1"].Value?.ToString() ?? "";
            string isim = selectedRow.Cells["Column2"].Value?.ToString() ?? "";
            string plaka = selectedRow.Cells["Column3"].Value?.ToString() ?? "";

            // Boþ satýr kontrolü
            if (string.IsNullOrEmpty(tc) || string.IsNullOrEmpty(isim) || string.IsNullOrEmpty(plaka))
            {
                MessageBox.Show("Seçilen satýrda eksik bilgiler var! Lütfen dolu bir satýr seçin.", "Boþ Satýr", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Form ismini deðiþirsen burayý da deðiþ
            Form2 form2 = new Form2(tc, isim, plaka); // Seçili verileri Form2'ye gönder

            form2.FormClosed += (s, args) => this.Show();  // Form2 kapanýnca Form1 tekrar göster

            form2.Show();

            this.Hide(); // Form1'i gizle
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string TC = TCtextBox.Text.Trim();
            string isim = isimTextBox.Text.Trim();
            string plaka = PlakaTextBox.Text.Trim().ToUpper(); // Küçük harfleri büyük yap

            // TC kimlik numarasý kontrolü
            if (TC.Length != 11 || !TC.All(char.IsDigit))
            {
                MessageBox.Show("TC kimlik numarasý 11 haneli ve sadece rakamlardan oluþmalýdýr!", "Hatalý TC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Plaka boþ mu?
            if (string.IsNullOrEmpty(plaka))
            {
                MessageBox.Show("Plaka alaný boþ býrakýlamaz!", "Eksik bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Plaka formatý kontrolü (örn: 34ABC123)
            Regex plakaRegex = new Regex(@"^\d{2}[A-Z]{1,3}\d{2,4}$");
            if (!plakaRegex.IsMatch(plaka))
            {
                MessageBox.Show("Plaka formatý hatalý! Örnek: 34ABC123", "Hatalý Plaka Formatý", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ýl kodu kontrolü (01-81 arasý)
            int ilKodu = int.Parse(plaka.Substring(0, 2));
            if (ilKodu < 1 || ilKodu > 81)
            {
                MessageBox.Show("Plakanýn baþýndaki il kodu 01 ile 81 arasýnda olmalýdýr!", "Geçersiz Ýl Kodu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Diðer boþ alan kontrolü
            if (string.IsNullOrEmpty(isim))
            {
                MessageBox.Show("Lütfen isim alanýný doldurun!", "Eksik bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Ayný TC veya plaka kontrolü
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string existingTC = row.Cells["Column1"].Value?.ToString();
                    string existingPlaka = row.Cells["Column3"].Value?.ToString();

                    if (existingTC == TC)
                    {
                        MessageBox.Show("Bu TC numarasý zaten eklenmiþ!", "Tekrar Eden TC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (existingPlaka == plaka)
                    {
                        MessageBox.Show("Bu plaka zaten eklenmiþ!", "Tekrar Eden Plaka", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // Satýrý ekle
            int rowIndex = dataGridView1.Rows.Add();
            dataGridView1.Rows[rowIndex].Cells["Column1"].Value = TC;
            dataGridView1.Rows[rowIndex].Cells["Column2"].Value = isim;
            dataGridView1.Rows[rowIndex].Cells["Column3"].Value = plaka;

            // TextBox'larý temizle
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





// ARKA UÇ




