using System.ComponentModel.DataAnnotations;

namespace rent.Models
{
    public class Korisnik
    {
        [Key]
        public string IdKorisnika { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [EnumDataType(typeof(TipKorisnika))]   public TipKorisnika TipKorisnika { get; set; }


    }

    public enum TipKorisnika
    {
        [Display(Name="Admin")]
        Admin,
        [Display(Name = "Uposlenik")]
        UposlenikSistema,
        [Display(Name = "Ponuditelj")]
        Ponuditelj,
        [Display(Name = "Potrezitelj")]
        Potrazitelj
    }
}
