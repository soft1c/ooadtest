using System.ComponentModel.DataAnnotations;

namespace rent.Models
{
    public class Vozilo : Resurs
    {
        [EnumDataType(typeof(TipVozila))]  public TipVozila Tip { get; set; }
        [EnumDataType(typeof(MarkaVozila))]  public MarkaVozila Marka { get; set; }
        public string Model { get; set; }
        public int Godiste { get; set; }
        public int BrojSjedista { get; set; }
        [EnumDataType(typeof(TipGoriva))]  public TipGoriva TipGoriva { get; set; }

        [Display(Name ="Slika")]
        public string ImagePath { get; set; } // Dodano svojstvo za putanju slike
    }


    public enum TipVozila
    {
        [Display(Name = "Limuzina")]
        Limuzina,
        [Display(Name = "Karavan")]
        Karavan,
        [Display(Name = "Malo auto")]
        MaloAuto,
        [Display(Name = "SUV")]
        SUV,
        [Display(Name = "Kombi")]
        Kombi,
        [Display(Name = "Motor")]
        Motor
    }

    public enum MarkaVozila
    {
        [Display(Name = "Volkswagen")]
        Volkswagen,
        [Display(Name = "Audi")]
        Audi,
        [Display(Name = "Mercedes")]
        Mercedes
    }



    public enum TipGoriva
    {
        [Display(Name = "Benzin")]
        Benzin,
        [Display(Name = "Dizel")]
        Dizel,
        [Display(Name = "Plin")]
        Plin,
        [Display(Name = "Hybrid")]
        Hibrid,
        [Display(Name = "Struja")]
        Struja
    }
}
