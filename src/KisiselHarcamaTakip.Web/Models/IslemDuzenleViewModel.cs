using System.ComponentModel.DataAnnotations;
using KisiselHarcamaTakip.Core.Entities;
using KisiselHarcamaTakip.Web.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KisiselHarcamaTakip.Web.Models;

public class IslemDuzenleViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Açıklama zorunludur.")]
    [StringLength(200, ErrorMessage = "Açıklama en fazla 200 karakter olabilir.")]
    [Display(Name = "Açıklama")]
    public string Aciklama { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tutar zorunludur.")]
    [ModelBinder(BinderType = typeof(TutarModelBinder))]
    [Display(Name = "Tutar (₺)")]
    public decimal Tutar { get; set; }

    [Required(ErrorMessage = "Tarih zorunludur.")]
    [DataType(DataType.Date)]
    [Display(Name = "Tarih")]
    public DateTime? Tarih { get; set; }

    [Display(Name = "İşlem türü")]
    public IslemTuru Tur { get; set; }

    [Display(Name = "Kategori")]
    public int? KategoriId { get; set; }

    public IReadOnlyList<SelectListItem> Kategoriler { get; set; } = [];

    public IReadOnlyList<SelectListItem> IslemTurleri { get; } =
    [
        new SelectListItem("Gelir", nameof(IslemTuru.Gelir)),
        new SelectListItem("Gider", nameof(IslemTuru.Gider))
    ];

}
