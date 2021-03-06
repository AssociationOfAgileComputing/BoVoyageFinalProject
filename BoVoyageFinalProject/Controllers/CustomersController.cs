﻿using BoVoyageFinalProject.Areas.BackOffice.Models;
using BoVoyageFinalProject.Models;
using BoVoyageFinalProject.Tools;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BoVoyageFinalProject.Filters;
using System.Web.Services.Description;

namespace BoVoyageFinalProject.Controllers
{
    public class CustomersController : BaseController
    {

        // GET: Customers
        [Authentication(Type = "BO")]
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        [Authentication(Type = "CUSTOMER")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Include("BookingFiles").Include("BookingFiles.Travellers").Include("BookingFiles.Insurances").SingleOrDefault(x=>x.ID==id);
            foreach(var customerBookingFiles in customer.BookingFiles)
            {
                customerBookingFiles.Travel = db.Travels.Include(t => t.Destination).SingleOrDefault(x => x.ID == customerBookingFiles.TravelId);
            }
            
            
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "ID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Password = customer.Password.HashMD5();
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Customers.Add(customer);
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
                Display("Votre compte client a été créé avec succès.");
                return RedirectToAction("index", "Home");
            }
            Display("Veuillez corriger les erreurs", MessageType.ERROR);
            return View();
        }

        // GET: Customers/Edit/5
        [Authentication(Type = "CUSTOMER")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication(Type = "CUSTOMER")]
        public ActionResult Edit([Bind(Include = "ID,BirthDate,PhoneNumber,AddressLine1,AddressLine2,ZIPCode,City,Country,Mail,Password,Title,FirstName,LastName, ConfirmedPassword")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Password = customer.Password.HashMD5();
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
                Display("Les informations ont été modifiées avec succès !");
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            Display("Veuillez corriger les erreurs", MessageType.ERROR);
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authentication(Type = "BO")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authentication(Type = "BO")]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
        // Customer Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthenticationLoginViewModels model)
        {
            var hash = model.Password.HashMD5();
            var customer = db.Customers.SingleOrDefault(x => x.Mail == model.Mail && x.Password == hash);

            if (customer == null)
            {
                Display("Login/Mot de passe incorrect", MessageType.ERROR);
                return View();
            }
            else
            {
                Session["CUSTOMER"] = customer;
                if (TempData["REDIRECT"] != null)
                    return Redirect(TempData["REDIRECT"].ToString());

                else
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("CUSTOMER");
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
