using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoVoyageFinalProject.Models;
using System.Data.Entity;
using System.Net;
using BoVoyageFinalProject.Tools;

namespace BoVoyageFinalProject.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            HomeTopViewModel model = new HomeTopViewModel();
            var query = db.Travels.Include(t => t.Destination).Include(t => t.Destination.Pictures).Include("TravelAgency");
            model.Top5Cheaper = query.OrderBy(x => x.Price).Take(5).ToList();
            model.Top5CloserDeparture = query.Where(x => x.DateGo >= DateTime.Now).OrderBy(x => x.DateGo).Take(5).ToList();

            // Get the area with most travel occurencies
            model.Top5MaxAreaOccurencies = query.OrderBy(x => x.Destination.Area).Take(5).ToList();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Bienvenue sur l'application BoVoyage";
            var modelInfo = new Info
            {
                DevName = "Association of Agile Computing",
                Address = "rue de la bonne Entente - Paris",
                ContactMail = "a2c@gmail.com",
                CreatedDate = "Octobre 2008"
            };

            return View(modelInfo);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacter le service commercial";

            return View();
        }

        public ActionResult SendMail()
        {
            Display("Merci de nous avoir contactés nous vous recontacterons dans les meilleurs délais");
            return RedirectToAction("Index");
        }

        [Route("voyage-{region}-{pays}/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var travels = db.Travels.Include(t => t.Destination)
                .Include(t => t.Destination.Pictures)
                .Include("TravelAgency").SingleOrDefault(x => x.ID == id);
            if (travels == null)
            {
                return HttpNotFound();
            }
            return View(travels);
        }

        public ActionResult Search(string pays, decimal? prixMin, decimal? prixMax, DateTime? dateAller, DateTime? dateRetour)
        {
            var query = db.Travels.Include(t => t.Destination).Include(t => t.Destination.Pictures).Include("TravelAgency");

            // Search by pays
            if (!string.IsNullOrWhiteSpace(pays))
            {
                query = query.Where(x => x.Destination.Country == pays);
            }

            // Search by priceMin
            if (prixMin != null)
            {
                query = query.Where(x => x.Price >= prixMin);
            }

            // Search by priceMax
            if (prixMax != null)
            {
                query = query.Where(x => x.Price <= prixMax);
            }

            // Search by dateAller
            if (dateAller != null)
            {
                query = query.Where(x => x.DateGo >= dateAller);
            }

            // Search by dateRetour
            if (dateRetour != null)
            {
                query = query.Where(x => x.DateBack <= dateRetour);
            }

            var travels = query.ToList();


            if (travels == null)
            {
                return HttpNotFound();
            }

            return View(travels);
        }

        //GET
        public ActionResult Reservation(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.travel = db.Travels.SingleOrDefault(x => x.ID == id);

            // Création de la liste des assurances a afficher sur la page de réservation
            ViewBag.InsuranceList = db.Insurances.Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.InsuranceType
            }).ToList();

            if (Session["CUSTOMER"] == null)
            {
                Display("Veuillez vous connecter à votre espace pour effectuer une réservation");
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reservation(int? id, [Bind(Include = "ID,CustomerId,TravelId,CreditCardNumber,TotalPrice,TravellersNumber,IsCustomerTraveller,BookingFileState,BookingFileCancellationReason,Insurances")] BookingFile bookingFile, List<int> InsuranceList)
        {
            Travel travel = db.Travels.SingleOrDefault(x => x.ID == id);
            if (ModelState.IsValid)
            {
                // Controle si le nombre de place maximum n'est pas dépassé
                if (!bookingFile.CheckPlaceMaxCapacity())
                {
                    ReservationActionBeforeRedirect(id);
                    Display("Nombre de participant maximum autorisé : 9", MessageType.ERROR);
                    return View();
                }

                // Controle si le nombre de place est suffisant par rapport au nombre total de place à réserver
                if (!bookingFile.CheckPlaceNumber(travel.SpaceAvailable))
                {
                    ReservationActionBeforeRedirect(id);
                    Display("Nombre de place insuffisant", MessageType.ERROR);
                    return View();
                }

                // On récupère les assurances par rapport à la liste des assurances selectionnées et on ajoute à la réservation
                foreach (int insuranceId in InsuranceList)
                {
                    bookingFile.Insurances.Add(db.Insurances.Find(insuranceId));
                }

                db.BookingFiles.Add(bookingFile);
                db.SaveChanges();
                Session["Travellers"] = bookingFile.TravellersNumber;
                Display("Le voyage a été créé. Ajoutez maintenant les participants.");
                return RedirectToAction("AddTravellers", new { id = bookingFile.ID }); //Rediriger vers ajout participant
            }
            ReservationActionBeforeRedirect(id);
            return View();
        }

        public ActionResult AddTravellers(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Titles = getTitles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTravellers(int? id, List<Traveller> travellers)
        {
            BookingFile bookingFile = db.BookingFiles
                .Include(x => x.Travel)
                .Include(x => x.Insurances)
                .Include(x => x.Travellers).SingleOrDefault(x => x.ID == id);

            if (ModelState.IsValid)
            {
                if (bookingFile.IsCustomerTraveller)
                {
                    Customer user = (Customer)Session["CUSTOMER"];
                    Traveller traveller = new Traveller
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        BirthDate = user.BirthDate,
                        Mail = user.Mail,
                        Title = user.Title,
                        PhoneNumber = user.PhoneNumber
                    };
                    travellers.Add(traveller);
                }
                foreach (Traveller t in travellers)
                {
                    var traveller = db.Travellers.SingleOrDefault(x => x.Mail == t.Mail);
                    if (traveller == null)
                    {
                        traveller = t;
                        db.Travellers.Add(traveller);
                        db.SaveChanges();
                    }

                    traveller.BookingFiles.Add(bookingFile);

                    bookingFile.Travellers.Add(traveller);
                    bookingFile.Travel.SpaceAvailable--;
                    db.Entry(bookingFile).State = EntityState.Modified;
                }
                bookingFile.GetTotalPrice();
                db.SaveChanges();
                Display($"Ajout des participants pour le dossier de réservation {bookingFile.ID} effectué avec succès.");
                return RedirectToAction("Summary", "Reservation", new { id = bookingFile.ID });
            }
            else
            {
                ViewBag.Titles = getTitles();
                return View(travellers);
            }
        }

        private List<SelectListItem> getTitles()
        {
            return new SelectListItem[] {
                new SelectListItem {Value = "Monsieur", Text= "Monsieur"},
                new SelectListItem {Value = "Madame", Text= "Madame"}
            }.ToList();
        }

        private void ReservationActionBeforeRedirect(int? id)
        {
            ViewBag.travel = db.Travels.SingleOrDefault(x => x.ID == id);
            ViewBag.InsuranceList = db.Insurances.Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.InsuranceType
            }).ToList();
        }
    }

    public class HomeTopViewModel
    {
        public List<Travel> Top5Cheaper { get; set; }
        public List<Travel> Top5CloserDeparture { get; set; }
        public List<Travel> Top5MaxAreaOccurencies { get; set; }
    }
}