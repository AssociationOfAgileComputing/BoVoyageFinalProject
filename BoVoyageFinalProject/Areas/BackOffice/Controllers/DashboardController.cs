using BoVoyageFinalProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageFinalProject.Areas.BackOffice.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: BackOffice/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}