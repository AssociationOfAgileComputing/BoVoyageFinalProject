using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoVoyageFinalProject.Areas.BackOffice.Models;
using BoVoyageFinalProject.Controllers;

namespace BoVoyageFinalProject.Areas.BackOffice.Controllers
{
    public class AuthenticationController : BaseController
    {
        // GET: BackOffice/Authentication
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AuthenticationLoginViewModels model)
        {
            if (ModelState.IsValid)
            {
                var manager = db.SalesManagers.SingleOrDefault(x => x.Mail == model.Mail && x.Password == model.Password);
                if (manager == null)
                {
                    ModelState.AddModelError("mail", "Login / mot de passe invalide.");
                    return View();
                }
                else
                {
                    Session["SALESMANAGER"] = manager;
                    return RedirectToAction("Index", "Dashboard", new { area = "BackOffice" });
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("SALESMANAGER");
            return RedirectToAction("Index", "Dashboard", new { area = "BackOffice" });
        }
    }
}