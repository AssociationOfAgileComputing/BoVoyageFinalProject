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
			var travels = db.Travels.Include(t => t.Destination).Include(t => t.Destination.Pictures).Include("TravelAgency").ToList();
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
        [Route("DetailVoyageRoute")]
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
                Display("Malheureusement, nous n'avons pas voyagé à ces dates");
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
                Display("Malheureusement, nous n'avons pas voyagé sur ces valeurs");
                return RedirectToAction("Index");
            }
            return View(travels);
        }

    }
}