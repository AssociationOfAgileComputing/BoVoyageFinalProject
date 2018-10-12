using BoVoyageFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BoVoyageFinalProject.Data
{
    public class BoVoyageDbContext : DbContext
    {
        public BoVoyageDbContext() : base("GROUPE1")
        {
            this.Database.Log = s => Debug.Write(s);
        }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<DestinationPicture> DestinationPictures { get; set; }

        public DbSet<Travel> Travels { get; set; }

        public DbSet<TravelAgency> TravelAgencys { get; set; }

        public DbSet<BookingFile> BookingFiles { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Insurance> Insurances { get; set; }

        public DbSet<SalesManager> SalesManagers { get; set; }

        public DbSet<Traveller> Travellers { get; set; }
        public object Travel { get; internal set; }
    }
}