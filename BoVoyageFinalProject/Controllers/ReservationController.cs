using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BoVoyageFinalProject.Controllers
{
    public class ReservationController : BaseController
    {
        public ActionResult Summary(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var bookingFile = db.BookingFiles
                .Include(x => x.Travellers)
                .Include(x => x.Insurances)
                .Include(x => x.Travel.Destination)
                .SingleOrDefault(x => x.ID == id);
            return View(bookingFile);
        }
    }
}