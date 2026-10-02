using KisiselHarcamaTakip.Core.Entities;
using KisiselHarcamaTakip.Infrastructure.Data;
using KisiselHarcamaTakip.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KisiselHarcamaTakip.Web.Controllers;

public class IslemController(ApplicationDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        var islemler = await dbContext.Islemler
            .AsNoTracking()
            .Include(islem => islem.Kategori)
            .OrderByDescending(islem => islem.Tarih)
            .ThenByDescending(islem => islem.Id)
            .ToListAsync();

        return View(islemler);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var islem = await dbContext.Islemler.FindAsync(id);
        if (islem is null)
        {
            return NotFound();
        }

        var model = new IslemDuzenleViewModel
        {
            Id = islem.Id,
            Aciklama = islem.Aciklama,
            Tutar = islem.Tutar,
            Tarih = islem.Tarih,
            Tur = islem.Tur,
            KategoriId = islem.KategoriId
        };
        await KategorileriYukleAsync(model);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(IslemDuzenleViewModel model)
    {
        await KategorileriYukleAsync(model);

        if (!Enum.IsDefined(model.Tur))
        {
            ModelState.AddModelError(nameof(model.Tur), "Geçerli bir işlem türü seçin.");
        }
        else if (model.Tur == IslemTuru.Gider &&
                 (model.KategoriId is null || !await dbContext.Kategoriler
                     .AnyAsync(kategori => kategori.Id == model.KategoriId)))
        {
            ModelState.AddModelError(nameof(model.KategoriId), "Gider için geçerli bir kategori seçin.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var islem = await dbContext.Islemler.FindAsync(model.Id);
        if (islem is null)
        {
            return NotFound();
        }

        islem.Aciklama = model.Aciklama.Trim();
        islem.Tutar = model.Tutar;
        islem.Tarih = model.Tarih!.Value;
        islem.Tur = model.Tur;
        islem.KategoriId = model.Tur == IslemTuru.Gider ? model.KategoriId : null;

        await dbContext.SaveChangesAsync();
        TempData["SuccessMessage"] = "İşlem güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var islem = await dbContext.Islemler
            .AsNoTracking()
            .Include(item => item.Kategori)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (islem is null)
        {
            return NotFound();
        }

        return View(new IslemSilViewModel
        {
            Id = islem.Id,
            Aciklama = islem.Aciklama,
            Tutar = islem.Tutar,
            Tarih = islem.Tarih,
            Tur = islem.Tur,
            KategoriAdi = islem.Kategori?.Ad
        });
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var islem = await dbContext.Islemler.FindAsync(id);
        if (islem is null)
        {
            return NotFound();
        }

        dbContext.Islemler.Remove(islem);
        await dbContext.SaveChangesAsync();

        TempData["SuccessMessage"] = "İşlem silindi.";
        return RedirectToAction(nameof(Index));
    }

    private async Task KategorileriYukleAsync(IslemDuzenleViewModel model)
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
