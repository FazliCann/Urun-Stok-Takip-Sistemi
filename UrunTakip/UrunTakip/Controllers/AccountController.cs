using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UrunTakip.Models;

namespace UrunTakip.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        Urun_TakipEntities1 db = new Urun_TakipEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult Login(Kullanici p)
        {
            var bilgiler = db.Kullanici.FirstOrDefault(x => x.Email == p.Email && x.Sifre == p.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Email, false);
                Session["mail"] = bilgiler.Email.ToString();
                Session["ad"] = bilgiler.Ad.ToString();
                Session["soyad"] = bilgiler.Soyad.ToString();
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.hata = "Email veya sifre hatalı";
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Kullanici data)
        {
            db.Kullanici.Add(data);
            data.Rol = "U";
            db.SaveChanges();
            return RedirectToAction("Login","Account");
        }

        public ActionResult Guncelle()
        {
            var kullanicilar = (string)Session["Mail"];
            var degerler = db.Kullanici.FirstOrDefault(x=>x.Email == kullanicilar);
            return View(degerler);
        }


        [HttpPost]
        public ActionResult Guncelle(Kullanici data)
        {
            var kullanicilar = (string)Session["Mail"];
            var user = db.Kullanici.Where(x => x.Email == kullanicilar).FirstOrDefault();
            user.Ad = data.Ad;
            user.Soyad = data.Soyad;
            user.Email = data.Email;
            user.KullaniciAd = data.KullaniciAd;
            user.Sifre = data.Sifre;
            user.SifreTekrar = data.SifreTekrar;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}