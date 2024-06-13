using System.Collections.Generic;

namespace rent.Models.ViewModels 
{
    public class VoziloDetailsViewModel
    {
        public Vozilo Vozilo { get; set; }
        public List<Recenzija> Recenzije { get; set; }
    }
}