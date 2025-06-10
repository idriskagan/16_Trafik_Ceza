# 🚔 Trafik Cezası Yönetim Sistemi

Bu proje, [Proje Adı] dersi kapsamında geliştirilen **Basit Trafik Cezası Yönetimi** sistemidir. Windows Forms ile geliştirilen bu sistem sayesinde sürücülere ait trafik cezaları kolayca işlenebilir, ödemeler takip edilebilir ve raporlamalar yapılabilir.

---

## 🎯 Proje Amacı

- Trafik cezası türlerinin (Hız, Park, Kırmızı Işık) işlenmesi
- Ceza ödeme işlemlerinin gerçekleştirilmesi
- Toplam borçların izlenmesi
- LINQ ile raporlama

---

## 🧱 Nesne Yönelimli Yapı (OOP)

- **Kalıtım (Inheritance):**
  - `Ceza` sınıfı temel sınıftır.
  - `Hiz`, `Park`, `KirmiziIsik` sınıfları `Ceza` sınıfından türetilir.

- **Arayüz (Interface):**
  - `IOdenecek` arayüzü ödeme işlemleri için kullanılır.

---

## 🖥️ Arayüz Özellikleri

- Ceza ekleme: plaka, tür, tutar, tarih
- Ceza ödeme: ödeme durumu güncellenir
- Raporlama: LINQ ile ödenmemiş cezalar filtrelenir
- Toplam borç hesaplanır ve kullanıcıya sunulur

---

## ✅ Kontroller

- Boş veya hatalı girişler kontrol edilir
- Aynı plakaya birden fazla ceza eklenebilir
- Ödeme işlemi yalnızca bir kez yapılabilir
- Toplam borç sadece ödenmemiş cezalardan hesaplanır

---

## 🧪 Test Edilen Senaryolar

| Test Durumu | Açıklama | Sonuç |
|-------------|----------|--------|
|Boş plaka	|Uyarı veriliyor	|✅
|Negatif tutar	|Uyarı veriliyor	|✅
|Tekrar ödeme	|Engelleniyor	|✅
|Raporlama	|LINQ ile çalışıyor	|✅
|TC tekrar girişi	|Aynı TC tekrar eklenemiyor	|✅
|TC 11 haneli değil	|Uyarı veriliyor	|✅
|Plaka küçük harfli	|Otomatik olarak büyük harfe çevriliyor	|✅
|Geçersiz il kodu	|Uyarı veriliyor	|✅
|Plaka tekrar girişi	|Aynı plaka farklı tcye eklenmiyor	|✅
---

## 📊 Kullanılan Teknolojiler

- C# (.NET Framework)
- Windows Forms
- LINQ
- OOP prensipleri

---

## 🧭 UML Diyagramı

Aşağıda projeye ait sınıf ilişkilerini gösteren UML diyagramı yer almaktadır:

[ceza2 drawio](https://github.com/user-attachments/assets/5545545b-3e37-4ab2-a477-7272c5ed2149)

g)

> `images/uml_diyagrami.png` yoluna kendi UML diyagramı resmini yerleştir. Örneğin draw.io ya da Lucidchart üzerinden çizip kaydedebilirsin.

---

## 🎥 Proje Tanıtım Videosu


---

## 📎 Notlar

- Veritabanı kullanılmamıştır, veriler bellekte tutulur.
- Arayüz basit kullanıcı deneyimi ön planda tutularak tasarlanmıştır.

---

## 📬 İletişim

Her türlü öneri ve geri bildirim için GitHub üzerinden iletişime geçebilirsiniz.
