using System.ComponentModel.DataAnnotations;
using KisiselHarcamaTakip.Web.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KisiselHarcamaTakip.Web.Models;

public class GiderOlusturViewModel
{
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
    public DateTime? Tarih { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
    [Display(Name = "Kategori")]
    public int? KategoriId { get; set; }

    public IReadOnlyList<SelectListItem> Kategoriler { get; set; } = [];

}
