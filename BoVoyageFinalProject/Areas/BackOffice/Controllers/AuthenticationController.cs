using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoVoyageFinalProject.Areas.BackOffice.Models;
using BoVoyageFinalProject.Controllers;
using BoVoyageFinalProject.Tools;

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
        public ActionResult Login(AuthenticationLoginViewModels model)
        {
            if (ModelState.IsValid)
            {
                var hash = model.Password.HashMD5();
                var manager = db.SalesManagers.SingleOrDefault(x => x.Mail == model.Mail && x.Password == hash);
                if (manager == null)
                {
                    Display("Login/Mot de passe incorrect", MessageType.ERROR);
                    return View();
                }
                else
                {
                    Session["SALESMANAGER"] = manager;
                    if (TempData["REDIRECT"] != null)
                        return Redirect(TempData["REDIRECT"].ToString());
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