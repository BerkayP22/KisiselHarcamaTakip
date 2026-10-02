# Backlog — İlk Geliştirme Görevleri

## Durum
- [x] Proje iskeleti oluşturuldu (Web / Core / Infrastructure, çözüm dosyası, referanslar)
- [x] EF Core + SQLite paket referansları eklendi (indirme için `dotnet restore` gerekiyor)
- [x] Başlangıç sayfasının açıldığı doğrulandı
- [x] Veritabanı ve temel modeller (Claude: APPROVED)
- [x] Kategori yönetimi (Claude: APPROVED)
- [x] Gelir ekleme (Claude: APPROVED)
- [x] Gider ekleme (Claude: APPROVED)
- [x] İşlem listesi ve düzenleme (Claude: APPROVED)
- [x] Aylık toplamlar (Claude: APPROVED)
- [x] Doğrulama ve kullanıcı deneyimi (Claude: APPROVED)

## Görevler

1. **Veritabanı ve temel modeller** — Tamamlandı (Claude: APPROVED)
   - `Core/Entities` içine `Islem` (Gelir/Gider ortak), `Kategori` entity'lerini tanımla
   - `Infrastructure/Data` içine `ApplicationDbContext` ekle, SQLite bağlantısını yapılandır
   - İlk migration'ı oluştur ve veritabanını oluştur

2. **Kategori yönetimi** — Tamamlandı (Claude: APPROVED)
   - Kategori ekleme / listeleme

3. **Gelir ekleme** — Tamamlandı (Claude: APPROVED)
   - Gelir kaydı oluşturma formu ve kaydetme

4. **Gider ekleme** — Tamamlandı (Claude: APPROVED)
   - Gider kaydı oluşturma formu, kategori seçimi

5. **İşlem listesi ve düzenleme** — Tamamlandı (Claude: APPROVED)
   - Tüm işlemleri listeleme
   - İşlem düzenleme
   - İşlem silme (onay adımıyla)

6. **Aylık toplamlar** — Tamamlandı (Claude: APPROVED)
   - Seçilen aya göre toplam gelir / toplam gider / net bakiye görüntüleme

7. **Doğrulama ve kullanıcı deneyimi** — Tamamlandı (Claude: APPROVED)
   - Form doğrulamaları (zorunlu alanlar, pozitif tutar vb.)
   - Basit ve anlaşılır arayüz
   - Tutar alanındaki İngilizce/istemci tarafı doğrulama sorunu çözüldü ve gerçek
     tarayıcıdan test edilip doğrulandı (bkz. status.md, final Claude incelemesi).
   - Arayüz modernize edildi (kart tabanlı tasarım, renk paleti, rozetler).

> Backlog tamamlandı — 7 görevin tamamı fonksiyonel olarak test edilip onaylandı.
> Projenin genel, sadeleştirilmiş özeti için bkz. **PROJE-OZETI.md**.
