using System.Collections.Generic;

namespace rent.Models
{
    public class AdminViewModel
    {
        public List<Resurs> Resources { get; set; }
        public List<Recenzija> Reviews { get; set; }
        public List<Rezervacija> Reservations { get; set; }
    }
}
