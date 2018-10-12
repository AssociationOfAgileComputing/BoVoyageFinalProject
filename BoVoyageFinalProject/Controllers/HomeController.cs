﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoVoyageFinalProject.Models;
using System.Data.Entity;
using System.Net;

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
      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var travels = db.Travels.Include(t => t.Destination).Include(t => t.Destination.Pictures).Include("TravelAgency").ToList();
            if (travels == null)
            {
                return HttpNotFound();
            }
            return View(travels);
        }
    }
}