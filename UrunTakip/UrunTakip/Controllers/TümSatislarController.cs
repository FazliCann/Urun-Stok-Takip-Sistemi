using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunTakip.Models;

namespace UrunTakip.Controllers
{
    public class TümSatislarController : Controller
    {
        // GET: TümSatislar
        Urun_TakipEntities1 db = new Urun_TakipEntities1();
        public ActionResult Index(int sayfa=1)
        {
            return View(db.Satislar.ToList().ToPagedList(sayfa,5));
        }
    }
}