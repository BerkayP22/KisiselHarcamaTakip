# Kişisel Harcama ve Bütçe Takip — Proje Özeti

Son güncelleme: 2 Ekim 2026 — **Tüm görevler tamamlandı ve onaylandı.**

## Ne yapıldı

Bu uygulama, gelir ve giderlerinizi kaydedip aylık bazda takip edebileceğiniz,
Türkçe arayüzlü, yerel bir web uygulaması. ASP.NET Core MVC (.NET 8) ile
yazıldı, verileri bilgisayarınızda yerel bir SQLite dosyasında (`harcama-takip.db`)
saklıyor; internet bağlantısı veya harici bir servise ihtiyaç duymuyor.

Katmanlar: `KisiselHarcamaTakip.Web` (arayüz ve controller'lar), `KisiselHarcamaTakip.Core`
(veri modelleri), `KisiselHarcamaTakip.Infrastructure` (veritabanı erişimi).

## Özellikler

1. **Kategori yönetimi** — harcama kategorileri (ör. "Market") oluşturup listeleyebilirsiniz.
2. **Gelir ekleme** — açıklama, tutar ve tarih ile gelir kaydı oluşturulur.
3. **Gider ekleme** — açıklama, tutar, tarih ve kategori ile gider kaydı oluşturulur.
4. **İşlem listesi ve düzenleme** — tüm gelir/gider kayıtları tek listede görüntülenir,
   düzenlenebilir ve onay adımıyla silinebilir.
5. **Aylık özet** — seçilen aya göre toplam gelir, toplam gider ve net bakiye gösterilir.
6. **Türkçe doğrulama** — tüm formlar Türkçe hata mesajları verir; tutar alanına hem
   nokta hem virgülle ondalık girilebilir (ör. `1.250,50` veya `1250.50`).
7. **Modern arayüz** — kart tabanlı, mor-indigo renk paletli, Gelir/Gider için
   yeşil/kırmızı rozetler ve tutar renklendirmesi içeren sade bir tasarım.

## Bu oturumda yapılan son düzeltme ve iyileştirmeler

Önceki incelemede (Görev 7), Tutar alanına Türkçe virgüllü bir değer girildiğinde
(`1.250,50` gibi) formun İngilizce bir hata mesajıyla ("The field Tutar (₺) must be
a number.") isteğin sunucuya ulaşmasını engellediği tespit edilmişti. Kök neden:
ASP.NET Core'un `asp-for` etiketi, alan `decimal` tipinde olduğu için otomatik
olarak jQuery'nin yalnızca İngilizce/ABD ondalık biçimini kabul eden "number"
kuralını ekliyordu — bu kural Türkçe virgüllü biçimi reddediyordu.

Bu oturumda düzeltme bizzat uygulandı ve tarayıcıdan tekrar test edildi:

- Gelir, Gider ve İşlem düzenleme formlarındaki Tutar alanları `asp-for` yerine
  elle yazılmış `name="Tutar"` alanına çevrildi; bu sayede otomatik İngilizce
  doğrulama kuralı hiç üretilmiyor ve doğrulama tamamen sunucudaki
  `TutarModelBinder`'a bırakılıyor (zaten Türkçe mesajlar üreten, doğru çalışan
  bir bileşendi).
- Ek bir güvenlik önlemi olarak `_ValidationScriptsPartial.cshtml`'e, ileride bir
  decimal alan yine `asp-for` ile kullanılırsa aynı sorunun tekrarlanmaması için
  hem nokta hem virgül ondalık ayıracını kabul eden özel bir jQuery doğrulama
  kuralı eklendi.
- Gerçek tarayıcıdan (geçici SQLite veritabanı + ayrı port ile, gerçek veritabanı
  etkilenmeden) şu senaryolar doğrulandı: `1.250,50` ve `350,75` gibi virgüllü
  tutarlar artık sorunsuz kaydediliyor (SQL loglarında `INSERT`/`UPDATE` ile
  teyit edildi); `abc` girişinde Türkçe "Geçerli bir tutar girin." mesajı;
  boş girişte "Tutar zorunludur." mesajı; İşlem düzenleme formunda da aynı
  şekilde virgüllü tutar güncelleme başarıyla çalışıyor.

Aynı oturumda arayüz modernize edildi: mor-indigo gradyanlı navbar, kart tabanlı
ana sayfa (hızlı erişim kartları), İşlemler listesinde Gelir/Gider rozetleri ve
yeşil/kırmızı tutar renklendirmesi, Aylık özet sayfasında renkli özet kartları,
tüm formlarda tutarlı kart düzeni. Harici bir servise veya yazı tipine bağımlılık
eklenmedi; tasarım tamamen mevcut yerel Bootstrap üzerine kuruldu.

## Nasıl çalıştırılır

- Gerçek kullanım için: `run.bat` dosyasını çalıştırın (gerçek `harcama-takip.db`
  dosyasını kullanır).
- `run-test*.bat` dosyaları yalnızca geliştirme/inceleme sırasında geçici veritabanı
  ve ayrı portla test amaçlı kullanıldı; gerçek veriyi etkilemezler, silinebilirler.

## Küçük, engelleyici olmayan notlar

- Uygulama kapanırken Windows olay günlüğü izin hatası veriyor (exit code 1);
  bu, kısıtlı ortamda EventLog sağlayıcısının olay kaynağı oluşturamamasından
  kaynaklanıyor ve uygulamanın çalışmasını etkilemiyor.
- `~/KisiselHarcamaTakip.Web.styles.css` isteği 404 dönüyor (Razor'ın otomatik
  paketlediği, görünüme özel küçük bir CSS dosyası); asıl tasarım dosyası olan
  `~/css/site.css` sorunsuz yükleniyor, bu yüzden görsel bir eksiklik yaratmıyor.
- Proje klasöründe `run-test*.bat` dosyaları ve bazı geçici `.db` test dosyaları
  birikti; hiçbiri gerçek veriyi etkilemiyor, istenirse elle silinebilir.

## Sonuç

Backlog'daki 7 görevin tamamı tamamlandı ve fonksiyonel olarak test edilip
onaylandı. Yeni bir görev kalmadı; proje mevcut haliyle kullanıma hazır.
