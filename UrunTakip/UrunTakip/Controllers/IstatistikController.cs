using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunTakip.Models;

namespace UrunTakip.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        Urun_TakipEntities1 db = new Urun_TakipEntities1();
        public ActionResult Index()
        {
            var satis = db.Satislar.Count();
            ViewBag.satis = satis;

            var urun = db.Urun.Count();
            ViewBag.urun = urun;

            var kategori = db.Kategori.Count();
            ViewBag.kategori = kategori;

            var sepet = db.Sepet.Count();
            ViewBag.sepet = sepet;

            var kullanici = db.Kullanici.Count();
            ViewBag.kullanici = kullanici;
            return View();
        }
    }
}