using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace rent.Models
{
    public class ProfilKorisnika
    {
        [Key, ForeignKey("Korisnik")]
        public long IdKorisnika { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojTelefona { get; set; }
        public string Adresa { get; set; }
    }
}
