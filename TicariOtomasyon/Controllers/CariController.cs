using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c =new Context();
        public ActionResult Index()
        {
            var car=c.Carilers.Where(x=>x.Durum==true).ToList();
            return View(car);
        }
        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CariEkle(Cariler cari)
        {
            if (!ModelState.IsValid)
            { return View(); }
            c.Carilers.Add(cari);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariSil(int id) { 
            var d=c.Carilers.Find(id);
            d.Durum = false;
            c.SaveChanges();
            return View("Index"); 
        }
        public ActionResult CariGetir(int id) {
            var cari = c.Carilers.Find(id);
            return View("CariGetir",cari);
        }
        public ActionResult CariGuncelle(Cariler car) {
            if (!ModelState.IsValid) { return View("CariGetir"); }
            var cari = c.Carilers.Find(car.Cariid);
            cari.CariAd = car.CariAd;
            cari.CariSoyad = car.CariSoyad;
            cari.CariSehir = car.CariSehir;
            cari.CariMail = car.CariMail;
            c.SaveChanges();

           return RedirectToAction("Index");
        }
        public ActionResult MusteriSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            var cr = c.Carilers.Where(x => x.Cariid == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cr;
            return View(degerler);
        }
    }
}