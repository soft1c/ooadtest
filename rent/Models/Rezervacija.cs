using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace rent.Models
{
    public class Rezervacija
    {
        [Key]
        public long IdRezervacije { get; set; }
        [ForeignKey("Korisnik")]
        public long IdOsobe { get; set; }
        [ForeignKey("Resurs")]
        public long IdResursa { get; set; }
        public DateTime Pocetak { get; set; }
        public DateTime Kraj { get; set; }
        [EnumDataType(typeof(Status))]  public Status Status { get; set; }

         public virtual Resurs Resurs { get; set; }
        
    }

    public enum Status
    {
        [Display(Name ="Završeno")]
        Zavrseno,
        [Display(Name = "Odbijeno")]
        Odbijeno,
        [Display(Name = "Potvrdjeno")]
        Potvrdjeno,
        [Display(Name = "U obradi...")]
        UObradi
    }
}
