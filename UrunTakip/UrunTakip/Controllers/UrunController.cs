using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using UrunTakip.Models;

namespace UrunTakip.Controllers
{
    public class UrunController : Controller
    {
        // GET: UrunUr
        Urun_TakipEntities1 db = new Urun_TakipEntities1();

        public ActionResult Index(string ara)
        {
            var list = db.Urun.ToList();
            if (!string.IsNullOrEmpty(ara))
            {
                list = list.Where(x => x.Ad.Contains(ara) || x.Aciklama.Contains(ara)).ToList();
            }
            return View(list);
        }

        
        public ActionResult Ekle()
        {
            List<SelectListItem> deger1 = (from x in db.Kategori.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.Ad,
                                               Value = x.Id.ToString()
                                           }).ToList();
            ViewBag.ktgr = deger1;                              
            return View();
        }

        
        [HttpPost]
        
        public ActionResult Ekle(Urun Data,HttpPostedFileBase Resim)
        {


            string path = Path.Combine("~/Content/Image" + Resim.FileName);
            Resim.SaveAs(Server.MapPath(path));
            Data.Resim = Resim.FileName.ToString();
            db.Urun.Add(Data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult Sil(int id)
        {
            var urun = db.Urun.Where(x=>x.Id == id).FirstOrDefault();
            db.Urun.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
        public ActionResult Guncelle(int id)
        {
            var guncelle = db.Urun.Where(x => x.Id == id).FirstOrDefault();
            List<SelectListItem> deger1 = (from x in db.Kategori.ToList()

                                           select new SelectListItem
                                           {
                                               Text = x.Ad,
                                               Value = x.Id.ToString()
                                           }).ToList();
            ViewBag.ktgr = deger1;
            return View(guncelle);
        }

       
        [HttpPost]
        public ActionResult Guncelle(Urun model,HttpPostedFileBase Resim)
        {

            var urun = db.Urun.Find(model.Id);
            if (Resim == null)
            {
                urun.Ad = model.Ad;
                urun.Aciklama = model.Aciklama;
                urun.Stok = model.Stok;
                urun.Fiyat = model.Fiyat;
                urun.KategoriId = model.KategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                urun.Resim = Resim.FileName.ToString();
                urun.Ad = model.Ad;
                urun.Aciklama = model.Aciklama;
                urun.Stok = model.Stok;
                urun.Fiyat = model.Fiyat;
                urun.KategoriId = model.KategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
           
        }

        
        public ActionResult KritikStok()
        {
            var kritik = db.Urun.Where(x=>x.Stok <= 50).ToList();
            return View(kritik);
        }

        public PartialViewResult StokCount()
        {
            if (User.Identity.IsAuthenticated)
            {
                var count = db.Urun.Where(x=> x.Stok < 50).Count();
                ViewBag.Count = count;
                var azalan = db.Urun.Where(x => x.Stok == 50).Count();
                ViewBag.azalan = azalan;
            }
            return PartialView();
        }

        public ActionResult StokGrafik()
        {
            ArrayList deger1 = new ArrayList();
            ArrayList deger2 = new ArrayList();
            var veriler = db.Urun.ToList();
            veriler.ToList().ForEach(x => deger1.Add(x.Ad));
            veriler.ToList().ForEach(x => deger2.Add(x.Stok));
            var grafik = new Chart(width: 500, height: 500).AddTitle("Ürün Stok Grafiği").AddSeries(chartType:"Column",name:"Ad",xValue:deger1,yValues:deger2);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }


    }
}