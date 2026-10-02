# Kişisel Harcama Takip

ASP.NET Core MVC (.NET 8) ile yazılmış, Türkçe arayüzlü kişisel gelir/gider ve aylık bütçe takip uygulaması. Verileriniz yalnızca kendi bilgisayarınızdaki yerel bir SQLite dosyasında saklanır; internet bağlantısı veya harici bir servise ihtiyaç duymaz.

Arayüz tasarımı, [Ordinaryunus](https://github.com/canogluy06-byte/Ordinaryunus) projesinden ilham alınarak uyarlandı: teal vurgu rengi, yuvarlatılmış köşeler, katmanlı gölgeler ve tam açık/koyu tema desteği.

## Özellikler

- **Kategori yönetimi** — harcama kategorileri oluşturup listeleme
- **Gelir ekleme** — açıklama, tutar ve tarih ile gelir kaydı oluşturma
- **Gider ekleme** — açıklama, tutar, tarih ve kategori ile gider kaydı oluşturma
- **İşlem listesi ve düzenleme** — tüm gelir/gider kayıtlarını görüntüleme, düzenleme ve onay adımıyla silme
- **Aylık özet** — seçilen aya göre toplam gelir, toplam gider ve net bakiye
- **Türkçe doğrulama** — tüm formlar Türkçe hata mesajları verir; tutar alanına hem nokta hem virgülle ondalık girilebilir (ör. `1.250,50` veya `1250.50`)
- **Açık/koyu tema** — navbar'daki düğmeyle anında değiştirilebilir, tercih `localStorage`'da saklanır

## Teknoloji

- ASP.NET Core MVC (.NET 8), C#
- Entity Framework Core + SQLite
- Katmanlı proje yapısı:
  - `src/KisiselHarcamaTakip.Web` — MVC uygulaması (Controllers, Views, wwwroot)
  - `src/KisiselHarcamaTakip.Core` — Domain modelleri (Entities)
  - `src/KisiselHarcamaTakip.Infrastructure` — EF Core DbContext, veri erişimi

## Çalıştırma

```bash
# Paketleri indir (ilk kurulumda gerekli)
dotnet restore

# Derleme
dotnet build

# Uygulamayı çalıştırma
dotnet run --project src/KisiselHarcamaTakip.Web
```

Uygulama varsayılan olarak `https://localhost:5001` / `http://localhost:5000` (veya konsolda belirtilen port) üzerinden açılır. Windows'ta `run.bat` dosyası da aynı işi `http://localhost:5080` portunda yapar.

## Agentik ve paralel yapay zekâ kullanımı

Bu proje, iki farklı yapay zekâ ajanının paralel ve birbirini tamamlayan rollerle çalıştığı agentik bir geliştirme süreciyle inşa edildi:

- **Codex**, özellik geliştirme tarafını üstlendi: proje iskeletinin kurulması, entity'lerin ve EF Core veritabanı bağlamının yazılması, controller/view/model katmanlarının oluşturulması, Türkçe form doğrulamalarının eklenmesi ve backlog'daki görevlerin uçtan uca kodlanması.
- **Claude**, bağımsız bir inceleme ve test ajanı olarak çalıştı: her görevi gerçek bir tarayıcıdan (geçici bir veritabanı ve ayrı bir port kullanarak, gerçek veriye dokunmadan) manuel olarak çalıştırıp doğruladı, ortaya çıkan hataları (ör. Tutar alanındaki İngilizce/Türkçe ondalık ayracı çakışması) kök nedenine inip düzeltti, arayüzü referans bir tasarımdan ilham alarak yeniden tasarladı (teal renk paleti, açık/koyu tema) ve projenin GitHub'a yüklenmesini kendisi gerçekleştirdi.

Bu iş birliği modelinde Codex "yazan", Claude ise "doğrulayan ve iyileştiren" taraf olarak görev aldı; her görev yalnızca kod yazıldığında değil, tarayıcıdan bizzat test edilip onaylandığında tamamlanmış sayıldı. Sürecin ayrıntıları `AGENTS.md`, `CLAUDE.md`, `BACKLOG.md` ve `PROJE-OZETI.md` dosyalarında kayıtlıdır.

## Proje durumu

Backlog'daki tüm görevler tamamlandı ve fonksiyonel olarak test edilip onaylandı. Ayrıntılı özet için bkz. [`PROJE-OZETI.md`](PROJE-OZETI.md).
