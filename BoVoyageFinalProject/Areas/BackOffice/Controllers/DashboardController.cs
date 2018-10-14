using BoVoyageFinalProject.Controllers;
using BoVoyageFinalProject.Filters;
using BoVoyageFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageFinalProject.Areas.BackOffice.Controllers
{
    [Authentication]
    public class DashboardController : BaseController
    {
        // GET: BackOffice/Home
        public ActionResult Index()
        {
            var model = new DashboardIndexViewModel();
            // Recherche des 10 prochains voyages
            model.NextTravels = db.Travels
                .Include(x => x.Destination)
                .Include(x => x.TravelAgency)
                .Where(x => x.DateGo >= DateTime.Now)
                .OrderBy(x => x.DateGo).Take(10).ToList();

            // Recherche des dossiers de réservations en attente pour les prochains voyages
            model.BookingFilesWaitingStatus = db.BookingFiles
                .Include(x => x.Customer)
                .Include(x => x.Insurances)
                .Include(x => x.Travellers)
                .Include(x => x.Travel.Destination)
                .Where(x => x.Travel.DateGo >= DateTime.Now && x.BookingFileState == "EN ATTENTE")
                .OrderBy(x => x.Travel.DateGo).Take(10).ToList();
            return View(model);
        }

        public ActionResult SearchCustomer(string LastName)
        {
            var customer = db.Customers.Where(x => x.LastName == LastName).ToList();
            if (customer == null)
            {
                return HttpNotFound();
            }
            if (customer.Count == 0)
            {
                Display("Il n'existe pas de client correspondant à votre recherche");
                return RedirectToAction("Index");
            }
            return View(customer);
        }
    }

    public class DashboardIndexViewModel
    {
        public IEnumerable<Travel> NextTravels { get; set; }
        public IEnumerable<BookingFile> BookingFilesWaitingStatus { get; set; }
    }
}