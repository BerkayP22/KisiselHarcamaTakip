using KisiselHarcamaTakip.Core.Entities;
using KisiselHarcamaTakip.Infrastructure.Data;
using KisiselHarcamaTakip.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace KisiselHarcamaTakip.Web.Controllers;

public class GelirController(ApplicationDbContext dbContext) : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        return View(new GelirOlusturViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GelirOlusturViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var gelir = new Islem
        {
            Aciklama = model.Aciklama.Trim(),
            Tutar = model.Tutar,
            Tarih = model.Tarih!.Value,
            Tur = IslemTuru.Gelir
        };

        dbContext.Islemler.Add(gelir);
        await dbContext.SaveChangesAsync();

        TempData["SuccessMessage"] = "Gelir kaydı eklendi.";
        return RedirectToAction(nameof(Create));
    }
}
