namespace rent.Models
{
    public class Filtriranje
    {
        public string TipResursa { get; set; }
        public List<string> TipoviResursa { get; set; } = new List<string> { "Vozilo", "Nekretnina" };

        // Filteri za vozila
        public List<string> TipoviGoriva { get; set; } = Enum.GetNames(typeof(TipGoriva)).ToList();
        public List<string> OdabraniTipoviGoriva { get; set; } = new List<string>();
        public int? BrojSjedistaOd { get; set; }
        public int? BrojSjedistaDo { get; set; }
        public int? GodisteOd { get; set; }
        public int? GodisteDo { get; set; }
        public List<string> TipoviVozila { get; set; } = Enum.GetNames(typeof(TipVozila)).ToList();
        public List<string> OdabraniTipoviVozila { get; set; } = new List<string>();

        // Filteri za nekretnine
        public double? PovrsinaOd { get; set; }
        public double? PovrsinaDo { get; set; }
        public int? BrojSobaOd { get; set; }
        public int? BrojSobaDo { get; set; }
        public List<string> TipoviNekretnina { get; set; } = Enum.GetNames(typeof(TipNekretnine)).ToList();
        public List<string> OdabraniTipoviNekretnina { get; set; } = new List<string>();

        public string SortirajPo { get; set; }
        public List<string> OpcijeSortiranja { get; set; } = new List<string> { "Najjeftinije", "Najskuplje", "Najnovije", "Najstarije" };


        public IEnumerable<Resurs> Rezultati { get; set; }


    }
}