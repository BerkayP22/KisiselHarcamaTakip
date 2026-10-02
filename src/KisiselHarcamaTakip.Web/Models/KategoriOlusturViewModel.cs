using System.ComponentModel.DataAnnotations;

namespace KisiselHarcamaTakip.Web.Models;

public class KategoriOlusturViewModel
{
    [Required(ErrorMessage = "Kategori adı zorunludur.")]
    [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir.")]
    [Display(Name = "Kategori adı")]
    public string Ad { get; set; } = string.Empty;
}
