# Kişisel Harcama ve Bütçe Takip Uygulaması

## Projenin Amacı
Kullanıcının gelirlerini, giderlerini ve harcama kategorilerini takip edebildiği basit bir web uygulaması oluşturmak.

## İlk Sürüm Kapsamı
- Gelir ekleme
- Gider ekleme
- Kategori seçme
- Aylık toplamları görüntüleme
- İşlem silme ve düzenleme

## Teknoloji Seçimi
- ASP.NET Core MVC (.NET 8)
- C#
- Entity Framework Core
- SQLite
- Katmanlı proje yapısı:
  - `src/KisiselHarcamaTakip.Web` — MVC uygulaması (Controllers, Views, wwwroot)
  - `src/KisiselHarcamaTakip.Core` — Domain modelleri ve arayüzler (Entities, Interfaces)
  - `src/KisiselHarcamaTakip.Infrastructure` — EF Core DbContext, veri erişimi (Data, Repositories)

## Çalıştırma Komutları
```
# Paketleri indir (ilk kurulumda gerekli)
dotnet restore

# Derleme
dotnet build

# Uygulamayı çalıştırma
dotnet run --project src/KisiselHarcamaTakip.Web
```
Uygulama varsayılan olarak `https://localhost:5001` / `http://localhost:5000` (veya konsolda belirtilen port) üzerinden açılır.

> Not: Proje iskeleti, NuGet.org'a erişimi olmayan bir bulut ortamında oluşturuldu. `Microsoft.EntityFrameworkCore.Sqlite` ve `Microsoft.EntityFrameworkCore.Design` paket referansları `.csproj` dosyalarına eklendi ancak henüz indirilemedi. Projeyi kendi bilgisayarınızda ilk açtığınızda `dotnet restore` çalıştırmanız gerekiyor.

## Çalışma Kuralları
- Büyük değişiklik yapmadan önce planı açıkla.
- Görevleri küçük parçalara böl.
- Kullanıcı onayı olmadan yeni teknoloji ekleme.
- Mevcut dosyaları silmeden veya değiştirmeden önce açıkça belirt.
- Her görevden sonra uygulamanın nasıl kontrol edileceğini yaz.
- Çalışan her aşamada Git commit öner.
- Henüz özellik kodlamaya başlama.
