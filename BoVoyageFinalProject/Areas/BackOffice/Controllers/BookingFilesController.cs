using BoVoyageFinalProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageFinalProject.Areas.BackOffice.Controllers
{
    public class BookingFilesController : BaseController
    {
        // GET: BackOffice/BookingFiles
        public ActionResult Registration()
        {
            var insurances = db.Insurances.ToList();
            ViewBag.Insurances = insurances.Select(x => new SelectListItem
            {
                Text = x.InsuranceType.ToString(),
                Value = x.InsuranceCost.ToString()
            });
            return View();
        }
    }
}