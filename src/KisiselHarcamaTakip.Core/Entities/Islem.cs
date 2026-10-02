namespace KisiselHarcamaTakip.Core.Entities;

public class Islem
{
    public int Id { get; set; }

    public string Aciklama { get; set; } = string.Empty;

    public decimal Tutar { get; set; }

    public DateTime Tarih { get; set; }

    public IslemTuru Tur { get; set; }

    public int? KategoriId { get; set; }

    public Kategori? Kategori { get; set; }
}
