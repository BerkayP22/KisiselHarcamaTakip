using KisiselHarcamaTakip.Core.Entities;
using KisiselHarcamaTakip.Infrastructure.Data;
using KisiselHarcamaTakip.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KisiselHarcamaTakip.Web.Controllers;

public class KategoriController(ApplicationDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        var kategoriler = await dbContext.Kategoriler
            .AsNoTracking()
            .OrderBy(kategori => kategori.Ad)
            .ToListAsync();

        return View(kategoriler);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new KategoriOlusturViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(KategoriOlusturViewModel model)
    {
        var ad = model.Ad.Trim();
        if (ModelState.IsValid && await dbContext.Kategoriler
                .AnyAsync(kategori => kategori.Ad.ToLower() == ad.ToLower()))
        {
            ModelState.AddModelError(nameof(model.Ad), "Bu kategori zaten mevcut.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        dbContext.Kategoriler.Add(new Kategori { Ad = ad });
        await dbContext.SaveChangesAsync();

        TempData["SuccessMessage"] = "Kategori eklendi.";
        return RedirectToAction(nameof(Index));
    }
}
