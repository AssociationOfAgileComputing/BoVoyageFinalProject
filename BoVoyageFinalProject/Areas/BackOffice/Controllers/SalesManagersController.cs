using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyageFinalProject.Controllers;
using BoVoyageFinalProject.Data;
using BoVoyageFinalProject.Filters;
using BoVoyageFinalProject.Models;
using BoVoyageFinalProject.Tools;

namespace BoVoyageFinalProject.Areas.BackOffice.Controllers
{
	[Authentication]
	public class SalesManagersController : BaseController
    {

        // GET: BackOffice/SalesManagers
        public ActionResult Index()
        {
            return View(db.SalesManagers.ToList());
        }

        // GET: BackOffice/SalesManagers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesManager salesManager = db.SalesManagers.Find(id);
            if (salesManager == null)
            {
                return HttpNotFound();
            }
            return View(salesManager);
        }

        // GET: BackOffice/SalesManagers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/SalesManagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "ID")] SalesManager salesManager)
        {
            if (ModelState.IsValid)
            {
                salesManager.Password = salesManager.Password.HashMD5();
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SalesManagers.Add(salesManager);
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
                Display("Le compte commercial a été créé avec succès.");
                return RedirectToAction("index", "Dashboard");
            }
            Display("Veuillez corriger les erreurs", MessageType.ERROR);
            return View(salesManager);
        }

        // GET: BackOffice/SalesManagers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesManager salesManager = db.SalesManagers.Find(id);
            if (salesManager == null)
            {
                return HttpNotFound();
            }
            return View(salesManager);
        }

        // POST: BackOffice/SalesManagers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, Mail, Password, Title, FirstName, LastName, ConfirmedPassword")] SalesManager salesManager)
        {
            if (ModelState.IsValid)
            {
                salesManager.Password = salesManager.Password.HashMD5();
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(salesManager).State = EntityState.Modified;
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
                Display("Les informatins ont été modifiées avec succès !");
                return RedirectToAction("Index", "dashboard");
            }
            Display("Veuillez corriger les erreurs", MessageType.ERROR);
            return View(salesManager);
        }

        // GET: BackOffice/SalesManagers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesManager salesManager = db.SalesManagers.Find(id);
            if (salesManager == null)
            {
                return HttpNotFound();
            }
            return View(salesManager);
        }

        // POST: BackOffice/SalesManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesManager salesManager = db.SalesManagers.Find(id);
            db.SalesManagers.Remove(salesManager);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
