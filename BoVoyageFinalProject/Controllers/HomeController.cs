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
			var travels = db.Travels.Include(t => t.Destination).Include(t => t.Destination.Pictures).Include("TravelAgency").OrderBy(x => x.DateGo).ToList();
			return View(travels);
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
            var travels = db.Travels.Include(t => t.Destination).Include(t => t.Destination.Pictures).Include("TravelAgency").SingleOrDefault(x => x.ID == id);
            if (travels == null)
            {
                return HttpNotFound();
            }
            return View(travels);
        }
        
        public ActionResult Search(string pays)
        {
            var travels = db.Travels.Include(t => t.Destination)
                .Include(t => t.Destination.Pictures)
                .Include("TravelAgency").Where(x=>x.Destination.Country == pays).ToList();
            if (travels == null)
            {
                return HttpNotFound();
            }
            if (travels.Count==0)
            {
                Display("Malheureusement, nous n'avons pas de voyages à cette destination");
                return RedirectToAction("Index");
            }
            return View(travels);
        }
        public ActionResult SearchPrice(decimal? prixMin, decimal? prixMax)
        {
            var travels = db.Travels.Include(t => t.Destination)
                .Include(t => t.Destination.Pictures)
                .Include("TravelAgency").Where(x => x.Price >= prixMin && x.Price <= prixMax).ToList();
            if (travels == null)
            {
                return HttpNotFound();
            }
            if (travels.Count == 0)
            {
                Display("Malheureusement, nous n'avons pas de voyages dans votre gamme de prix");
                return RedirectToAction("Index");
            }
            return View(travels);
        }
        public ActionResult SearchDate(DateTime? dateAller, DateTime? dateRetour)
        {
            var travels = db.Travels.Include(t => t.Destination)
                .Include(t => t.Destination.Pictures)
                .Include("TravelAgency").Where(x => x.DateGo >= dateAller && x.DateBack <= dateRetour).ToList();
            if (travels == null)
            {
                return HttpNotFound();
            }
            if (travels.Count == 0)
            {
                Display("Malheureusement, nous n'avons pas de voyages à ces dates");
                return RedirectToAction("Index");
            }
            return View(travels);
        }

        //GET
        public ActionResult Reservation(int? id)
        {
            ViewBag.travel = db.Travels.SingleOrDefault(x => x.ID == id);
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
        public ActionResult Reservation(int? id, [Bind(Include = "ID,CustomerId,TravelId,CreditCardNumber,TotalPrice,TravellersNumber,IsCustomerTraveller,BookingFileState,BookingFileCancellationReason")] BookingFile bookingFile)
        {
            Travel travel = db.Travels.SingleOrDefault(x => x.ID == id);
            if (ModelState.IsValid)
            {
                if (!bookingFile.CheckPlaceNumber(travel.SpaceAvailable))
                {
                    ViewBag.travel = db.Travels.SingleOrDefault(x => x.ID == id);
                    Display("Nombre de place insuffisant", MessageType.ERROR);
                    return View();
                }

                db.BookingFiles.Add(bookingFile);
                db.SaveChanges();
                Session["Travellers"] = bookingFile.TravellersNumber;
                Display("Le voyage a été créé. Ajoutez maintenant les participants.");
                return RedirectToAction("AddTravellers", new { id = bookingFile.ID});//Rediriger vers ajout participant
            }
            ViewBag.travel = db.Travels.SingleOrDefault(x => x.ID == id);
            return View();
        }

        public ActionResult AddTravellers(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTravellers(int? id, List<Traveller> travellers)
        {
                BookingFile bookingFile = db.BookingFiles.Include(x => x.Travel).SingleOrDefault(x => x.ID == id);

                if (ModelState.IsValid)
                {
                    if (bookingFile.IsCustomerTraveller)
                {
                    Customer user = (Customer) Session["CUSTOMER"];
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    db.BookingFiles.Remove(bookingFile);
                    db.SaveChanges();
                    return View(travellers);
                }
            }
        }
    }