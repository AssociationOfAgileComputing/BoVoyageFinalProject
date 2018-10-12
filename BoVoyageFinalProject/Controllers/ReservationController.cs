using BoVoyageFinalProject.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageFinalProject.Controllers
{
    public class ReservationController : BaseController
    {
        // GET: Reservation
        public ActionResult Reservation(int id)
        {
            //TempData["Travel"]=db.Travels.Include(x=> x.TravelAgency).Include(x=>x.Destination).SingleOrDefault(x=>x.ID==Id));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reservation([Bind(Include = "CustomerId,TravelId,CreditCardNumber,TotalPrice,TravellersNumber,IsCustomerTraveller,BookingFileState,BookingFileCancellationReason")] BookingFile bookingFile)
        {
            if (ModelState.IsValid)
            {
                db.BookingFiles.Add(bookingFile);
                db.SaveChanges();
                TempData["BookingFileId"] = bookingFile.ID;
                //TempData["Travellers"] = travellersNumber;
                return RedirectToAction("Ajout", "Travellers");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "Email", bookingFile.CustomerId);
            ViewBag.TravelId = new SelectList(db.Travels, "ID", "ID", bookingFile.TravelId);
            return View(bookingFile);
        }
    }
}