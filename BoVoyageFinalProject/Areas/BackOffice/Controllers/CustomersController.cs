using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyageFinalProject.Controllers;
using BoVoyageFinalProject.Data;
using BoVoyageFinalProject.Filters;
using BoVoyageFinalProject.Models;

namespace BoVoyageFinalProject.Areas.BackOffice.Controllers
{
    [Authentication]
    public class CustomersController : BaseController
    {

        // GET: BackOffice/Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: BackOffice/Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Include("BookingFiles").SingleOrDefault(x => x.ID == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

    }
}
