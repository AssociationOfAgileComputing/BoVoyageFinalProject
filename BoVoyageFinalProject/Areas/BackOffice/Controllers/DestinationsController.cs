﻿using BoVoyageFinalProject.Controllers;
using BoVoyageFinalProject.Data;
using BoVoyageFinalProject.Filters;
using BoVoyageFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageFinalProject.Areas.BackOffice.Controllers
{
	[Authentication]
	public class DestinationsController : BaseController
    {
        // GET: BackOffice/Destinations
        public ActionResult Index()
        {
            return View(db.Destinations.OrderBy(x=>x.Area).OrderBy(r=>r.Country).OrderBy(z=>z.Continent).ToList());
        }

        // GET: BackOffice/Destinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // GET: BackOffice/Destinations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Destinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Continent,Country,Area,Description")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Destinations.Add(destination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(destination);
        }

        // GET: BackOffice/Destinations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Include("Pictures").SingleOrDefault(x => x.ID == id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: BackOffice/Destinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Continent,Country,Area,Description")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(destination);
        }

        // GET: BackOffice/Destinations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: BackOffice/Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Destination destination = db.Destinations.Find(id);
            db.Destinations.Remove(destination);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddPicture(HttpPostedFileBase picture, int id)
        {
            if (picture?.ContentLength > 0)
            {
                var destination = new DestinationPicture();
                destination.ContentType = picture.ContentType;
                destination.Name = picture.FileName;
                destination.DestinationID = id;

                using (var reader = new BinaryReader(picture.InputStream))
                {
                    destination.Content = reader.ReadBytes(picture.ContentLength);
                }

                db.DestinationPictures.Add(destination);
                db.SaveChanges();

                return RedirectToAction("Edit", "Destinations", new { id = id });
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult DeletePicture(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var picture = db.DestinationPictures.Find(id);

            if (picture == null)
                return HttpNotFound();

            db.DestinationPictures.Remove(picture);
            db.SaveChanges();
            return Json(picture);
        }
    }
}