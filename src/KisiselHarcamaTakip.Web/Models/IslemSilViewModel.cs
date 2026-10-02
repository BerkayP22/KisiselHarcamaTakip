using KisiselHarcamaTakip.Core.Entities;

namespace KisiselHarcamaTakip.Web.Models;

public class IslemSilViewModel
{
    public int Id { get; set; }

    public string Aciklama { get; set; } = string.Empty;

    public decimal Tutar { get; set; }

    public DateTime Tarih { get; set; }

    public IslemTuru Tur { get; set; }

    public string? KategoriAdi { get; set; }
}
