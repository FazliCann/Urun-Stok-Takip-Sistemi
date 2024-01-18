using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using UrunTakip.Models;

namespace UrunTakip.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        Urun_TakipEntities1 db = new Urun_TakipEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SifreReset()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SifreReset(string eposta)
        {
            var mail = db.Kullanici.Where(x=>x.Email == eposta).SingleOrDefault();
            if (mail != null) 
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();
                Kullanici sifre = new Kullanici();
                mail.Sifre = Crypto.Hash(Convert.ToString(yenisifre), "MD5");
                db.SaveChanges();
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "kurumsalweb85@gmail.com";
                WebMail.Password = "123";
                WebMail.SmtpPort = 587;
                WebMail.Send(eposta, "Giriş Şifreniz", "Şifreniz:" + yenisifre);
                ViewBag.uyari = "Şifreniz Başarıyla Gönderildi";
            }
            else 
            {
                ViewBag.uyari = "Hata Oluştu";
                
            }
            return View();
        }


    }
}