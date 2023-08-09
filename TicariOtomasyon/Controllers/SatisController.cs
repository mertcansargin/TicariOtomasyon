using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        Context c = new Context();
        // GET: Satis
        public ActionResult Index()
        {
            var degerler=c.SatisHarekets.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniSatis() {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList() select new SelectListItem { Text = x.UrunAdi, Value = x.Urunid.ToString() }).ToList();
            List<SelectListItem> deger2 = (from x in c.Carilers.ToList() select new SelectListItem { Text = x.CariAd+" "+x.CariSoyad, Value = x.Cariid.ToString() }).ToList();
            List<SelectListItem> deger3 = (from x in c.Personels.ToList() select new SelectListItem { Text = x.PersonelAd, Value = x.Personelid.ToString() }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;

            return View(); }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket satis) { 
            satis.Tarih=DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(satis);
            c.SaveChanges();
            return RedirectToAction("Index"); 
        }
        public ActionResult SatisGetir(int id) {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList() select new SelectListItem { Text = x.UrunAdi, Value = x.Urunid.ToString() }).ToList();
            List<SelectListItem> deger2 = (from x in c.Carilers.ToList() select new SelectListItem { Text = x.CariAd + " " + x.CariSoyad, Value = x.Cariid.ToString() }).ToList();
            List<SelectListItem> deger3 = (from x in c.Personels.ToList() select new SelectListItem { Text = x.PersonelAd, Value = x.Personelid.ToString() }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;

            var satisdeger = c.SatisHarekets.Find(id);
            return View("SatisGetir", satisdeger);
        }

        public ActionResult SatisGuncelle(SatisHareket s) {
            var deger = c.SatisHarekets.Find(s.Satisid);
            deger.Cariid = s.Cariid; 
            deger.Personelid = s.Personelid;
            deger.Adet = s.Adet;
            deger.Fiyat = s.Fiyat;
            deger.Urunid= s.Urunid;
            deger.ToplamTutar = s.ToplamTutar;
            deger.Tarih= s.Tarih;
            c.SaveChanges();
            return RedirectToAction("Index"); 
        }
        public ActionResult SatisDetay(int id)
        {
            var deger = c.SatisHarekets.Where(x => x.Satisid == id).ToList();

            return View(deger);
        }
    }
}