namespace KisiselHarcamaTakip.Core.Entities;

public class Kategori
{
    public int Id { get; set; }

    public string Ad { get; set; } = string.Empty;

    public ICollection<Islem> Islemler { get; set; } = new List<Islem>();
}
