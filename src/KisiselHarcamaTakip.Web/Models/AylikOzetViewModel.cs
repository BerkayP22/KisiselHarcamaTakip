namespace KisiselHarcamaTakip.Web.Models;

public class AylikOzetViewModel
{
    public string SecilenAy { get; set; } = string.Empty;

    public string AyBasligi { get; set; } = string.Empty;

    public decimal ToplamGelir { get; set; }

    public decimal ToplamGider { get; set; }

    public decimal NetBakiye => ToplamGelir - ToplamGider;
}
