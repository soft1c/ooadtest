using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rent.Models;

namespace rent.Data
{
    public class ApplicationDbContext : IdentityDbContext



    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<ProfilKorisnika> ProfiliKorisnika { get; set; }
        public DbSet<Resurs> Resurs { get; set; }
        public DbSet<Vozilo> Vozilo { get; set; }
        public DbSet<Nekretnina> Nekretnina { get; set; }
        public DbSet<Recenzija> Recenzija { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
            modelBuilder.Entity<ProfilKorisnika>().ToTable("ProfilKorisnika");
            modelBuilder.Entity<Resurs>().ToTable("Resurs");
            modelBuilder.Entity<Vozilo>().ToTable("Vozilo");
            modelBuilder.Entity<Nekretnina>().ToTable("Nekretnina");
            modelBuilder.Entity<Recenzija>().ToTable("Recenzija");
            modelBuilder.Entity<Rezervacija>().ToTable("Rezervacija");


            base.OnModelCreating(modelBuilder);




        }



    }
}
