﻿using BoVoyageFinalProject.Controllers;
using BoVoyageFinalProject.Data;
using BoVoyageFinalProject.Filters;
using BoVoyageFinalProject.Models;
using BoVoyageFinalProject.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageFinalProject.Areas.BackOffice.Controllers
{
	[Authentication]
	public class TravelAgenciesController : BaseController
    {
        // GET: BackOffice/TravelAgencies
        public ActionResult Index()
        {
            return View(db.TravelAgencys.ToList());
        }

        // GET: BackOffice/TravelAgencies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelAgency travelAgency = db.TravelAgencys.Find(id);
            if (travelAgency == null)
            {
                return HttpNotFound();
            }
            return View(travelAgency);
        }

        // GET: BackOffice/TravelAgencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/TravelAgencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] TravelAgency travelAgency)
        {
            if (ModelState.IsValid)
            {
                db.TravelAgencys.Add(travelAgency);
                db.SaveChanges();
                Display("Le agence a été créé avec succès.");
                return RedirectToAction("Index");
            }
            Display("Veuillez corriger les erreurs", MessageType.ERROR);
            return View(travelAgency);
        }

        // GET: BackOffice/TravelAgencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelAgency travelAgency = db.TravelAgencys.Find(id);
            if (travelAgency == null)
            {
                return HttpNotFound();
            }
            return View(travelAgency);
        }

        // POST: BackOffice/TravelAgencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] TravelAgency travelAgency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travelAgency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(travelAgency);
        }

        // GET: BackOffice/TravelAgencies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelAgency travelAgency = db.TravelAgencys.Find(id);
            if (travelAgency == null)
            {
                return HttpNotFound();
            }
            return View(travelAgency);
        }

        // POST: BackOffice/TravelAgencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TravelAgency travelAgency = db.TravelAgencys.Find(id);
            db.TravelAgencys.Remove(travelAgency);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}