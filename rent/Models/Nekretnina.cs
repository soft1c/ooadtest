using System.ComponentModel.DataAnnotations;

namespace rent.Models
{
    public class Nekretnina : Resurs
    {
        [EnumDataType(typeof(TipNekretnine))]   public TipNekretnine Tip { get; set; }
        public double Povrsina { get; set; }
        public int BrojSoba {  get; set; }

        public bool Parking { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; } // Dodano svojstvo za putanju slike

    }


    public enum TipNekretnine
    {
        [Display(Name ="Stan")]
        Stan,
        [Display(Name = "Kuca")]
        Kuca,
        [Display(Name = "Vikendica")]
        Vikendica
    }
}
