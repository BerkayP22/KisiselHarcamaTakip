using System.Globalization;
using KisiselHarcamaTakip.Core.Entities;
using KisiselHarcamaTakip.Infrastructure.Data;
using KisiselHarcamaTakip.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KisiselHarcamaTakip.Web.Controllers;

public class AylikOzetController(ApplicationDbContext dbContext) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string? ay)
    {
        DateTime secilenTarih;
        if (string.IsNullOrWhiteSpace(ay))
        {
            var bugun = DateTime.Today;
            secilenTarih = new DateTime(bugun.Year, bugun.Month, 1);
        }
        else if (!DateTime.TryParseExact(
                     $"{ay}-01",
                     "yyyy-MM-dd",
                     CultureInfo.InvariantCulture,
                     DateTimeStyles.None,
                     out secilenTarih))
        {
            return BadRequest("Ay yyyy-AA biçiminde olmalıdır.");
        }

        var baslangic = new DateTime(secilenTarih.Year, secilenTarih.Month, 1);
        var sonrakiAy = baslangic.AddMonths(1);
        var islemler = await dbContext.Islemler
            .AsNoTracking()
            .Where(islem => islem.Tarih >= baslangic && islem.Tarih < sonrakiAy)
            .Select(islem => new { islem.Tutar, islem.Tur })
            .ToListAsync();

        var kultur = CultureInfo.GetCultureInfo("tr-TR");
        var model = new AylikOzetViewModel
        {
            SecilenAy = baslangic.ToString("yyyy-MM", CultureInfo.InvariantCulture),
            AyBasligi = baslangic.ToString("MMMM yyyy", kultur),
            ToplamGelir = islemler.Where(islem => islem.Tur == IslemTuru.Gelir).Sum(islem => islem.Tutar),
            ToplamGider = islemler.Where(islem => islem.Tur == IslemTuru.Gider).Sum(islem => islem.Tutar)
        };

        return View(model);
    }
}
