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
using BoVoyageFinalProject.Models;

namespace BoVoyageFinalProject.Areas.BackOffice.Controllers
{
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
        public ActionResult Create([Bind(Include = "ID,Mail,Password,Title,FirstName,LastName")] SalesManager salesManager)
        {
            if (ModelState.IsValid)
            {
                db.SalesManagers.Add(salesManager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
        public ActionResult Edit([Bind(Include = "ID,Mail,Password,Title,FirstName,LastName")] SalesManager salesManager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesManager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
