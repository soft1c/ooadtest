using System.ComponentModel.DataAnnotations;

namespace rent.Models.ViewModels
{
    public class CreateRecenzijaViewModel
    {
        public long IdResursa { get; set; }

        [Range(1, 5)]
        public int Ocjena { get; set; }

        public string Komentar { get; set; }
    }
}
