using BoVoyageFinalProject.Controllers;
using BoVoyageFinalProject.Filters;
using BoVoyageFinalProject.Models;
using System;
using System.Collections.Generic;
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
			return View();
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
}