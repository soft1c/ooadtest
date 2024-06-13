using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace rent.Models
{
    public class Recenzija
    {
        [Key]
        public long IdRecenzije { get; set; }
        [ForeignKey("Korisnik")]
        public long IdAutora { get; set; }
        [ForeignKey("Resurs")]
        public long IdResursa { get; set; }

        [Range(1, 5)]
        public int Ocjena { get; set; }
        public string Komentar { get; set; }
    }
}
