using KisiselHarcamaTakip.Core.Entities;
using KisiselHarcamaTakip.Infrastructure.Data;
using KisiselHarcamaTakip.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KisiselHarcamaTakip.Web.Controllers;

public class GiderController(ApplicationDbContext dbContext) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new GiderOlusturViewModel();
        await KategorileriYukleAsync(model);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GiderOlusturViewModel model)
    {
        await KategorileriYukleAsync(model);

        if (ModelState.IsValid && !await dbContext.Kategoriler
                .AnyAsync(kategori => kategori.Id == model.KategoriId))
        {
            ModelState.AddModelError(nameof(model.KategoriId), "Seçilen kategori bulunamadı.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var gider = new Islem
        {
            Aciklama = model.Aciklama.Trim(),
            Tutar = model.Tutar,
            Tarih = model.Tarih!.Value,
            Tur = IslemTuru.Gider,
            KategoriId = model.KategoriId!.Value
        };

        dbContext.Islemler.Add(gider);
        await dbContext.SaveChangesAsync();

        TempData["SuccessMessage"] = "Gider kaydı eklendi.";
        return RedirectToAction(nameof(Create));
    }

    private async Task KategorileriYukleAsync(GiderOlusturViewModel model)
    {
        model.Kategoriler = await dbContext.Kategoriler
            .AsNoTracking()
            .OrderBy(kategori => kategori.Ad)
            .Select(kategori => new SelectListItem
            {
                Value = kategori.Id.ToString(),
                Text = kategori.Ad
            })
            .ToListAsync();
    }
}
