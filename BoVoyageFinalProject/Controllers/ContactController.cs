using BoVoyageFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageFinalProject.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //POST:
        [HttpPost]
        public ActionResult Index(ContactViewModel contactViewModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(contactViewModel.Email);//Email which you are getting 
                                                                        //from contact us page 
                    mail.To.Add("mail@mail.com");//Where mail will be sent 
                    mail.Subject = contactViewModel.Subject;
                    mail.Body = contactViewModel.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.mail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("mail@mail.com", "Password");

                    smtp.EnableSsl = true;

                    smtp.Send(mail);

                    ModelState.Clear();
                    ViewBag.Message = "Merci de nous avoir contactés";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Désolé nous avons rencontré un problème: {ex.Message}";
                }
            }
            return View();
        }
    }
}