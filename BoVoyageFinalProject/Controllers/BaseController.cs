using BoVoyageFinalProject.Data;
using BoVoyageFinalProject.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageFinalProject.Controllers
{
    public class BaseController : Controller
    {
        protected BoVoyageDbContext db = new BoVoyageDbContext();

        /// <summary>
        /// Affiche un message dans le layout success ou erreur avec ou sans redirection
        /// </summary>
        /// <param name="text">le text a afficher</param>
        /// <param name="type">le type de message</param>
        protected void Display(string text, MessageType type = MessageType.SUCCESS)
        {
            var m = new Message(type, text);
            TempData["MESSAGE"] = m;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing)
                this.db.Dispose();
        }
    }
}