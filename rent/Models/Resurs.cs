using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace rent.Models
{
    public class Resurs
    {
        [Key]
        public long IdResursa { get; set; }
        [ForeignKey("Korisnik")]
        public long IdVlasnika { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double Ocjena { get; set; }
    }
}
