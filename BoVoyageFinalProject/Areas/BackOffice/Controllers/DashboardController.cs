using BoVoyageFinalProject.Controllers;
using BoVoyageFinalProject.Filters;
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
    }
}