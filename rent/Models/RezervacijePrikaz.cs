using System.Collections.Generic;

namespace rent.Models.ViewModels
{
    public class ReservationsViewModel
    {
        public List<Rezervacija> MojeRezervacije { get; set; }
        public List<Rezervacija> RezervacijeOdMene { get; set; }
    }
}
