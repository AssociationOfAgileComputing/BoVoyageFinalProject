using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
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

        public FileContentResult CSV()
        {
            List<Customer> customers = db.Customers.ToList();
            StringBuilder builder = new StringBuilder();
            builder.Append($"nom;prenom;email\n");
            foreach (Customer customer in customers)
            {
                builder.Append($"{customer.LastName};{customer.FirstName};{customer.Mail}\n");
            }
            return File(new System.Text.UTF8Encoding().GetBytes(builder.ToString()), "text/csv", string.Format("export-{0}.csv", DateTime.Now.ToString("yyyyMMdd-HHmmss")));
        }

    }
}
