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
    public class BookingFilesController : BaseController
    {
        

        // GET: BackOffice/BookingFiles
        public ActionResult Index()
        {
            var bookingFiles = db.BookingFiles.Include(b => b.Customer).Include(b => b.Travel);
            return View(bookingFiles.ToList());
        }

        // GET: BackOffice/BookingFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingFile bookingFile = db.BookingFiles.Find(id);
            if (bookingFile == null)
            {
                return HttpNotFound();
            }
            return View(bookingFile);
        }

        // GET: BackOffice/BookingFiles/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "PhoneNumber");
            ViewBag.TravelId = new SelectList(db.Travels, "ID", "ID");
            return View();
        }

        // POST: BackOffice/BookingFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerId,TravelId,CreditCardNumber,TotalPrice,TravellersNumber,BookingFileState,IsCustomerTraveller")] BookingFile bookingFile)
        {
            if (ModelState.IsValid)
            {
                db.BookingFiles.Add(bookingFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "PhoneNumber", bookingFile.CustomerId);
            ViewBag.TravelId = new SelectList(db.Travels, "ID", "ID", bookingFile.TravelId);
            return View(bookingFile);
        }

        // GET: BackOffice/BookingFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingFile bookingFile = db.BookingFiles.Find(id);
            if (bookingFile == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "PhoneNumber", bookingFile.CustomerId);
            ViewBag.TravelId = new SelectList(db.Travels, "ID", "ID", bookingFile.TravelId);
            return View(bookingFile);
        }

        // POST: BackOffice/BookingFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustomerId,TravelId,CreditCardNumber,TotalPrice,TravellersNumber,BookingFileState,IsCustomerTraveller")] BookingFile bookingFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "PhoneNumber", bookingFile.CustomerId);
            ViewBag.TravelId = new SelectList(db.Travels, "ID", "ID", bookingFile.TravelId);
            return View(bookingFile);
        }

        // GET: BackOffice/BookingFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingFile bookingFile = db.BookingFiles.Find(id);
            if (bookingFile == null)
            {
                return HttpNotFound();
            }
            return View(bookingFile);
        }

        // POST: BackOffice/BookingFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingFile bookingFile = db.BookingFiles.Find(id);
            db.BookingFiles.Remove(bookingFile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       //[HttpGet]
	   //[Authentification ]
    }
}
