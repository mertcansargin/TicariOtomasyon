using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{ [Authorize]
    public class KategoriController : Controller
    {
        // GET: Kategori
        
        Context c= new Context();
       
        public ActionResult Index(int sayfa=1)
        {
            var degerler=c.Kategoris.ToList().ToPagedList(sayfa,8);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult KategoriEkle() { return View(); }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k) {
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index"); 
        }
        public ActionResult KategoriSil(int id ) {
            var ktg=c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var kategori=c.Kategoris.Find(id);
            return View("KategoriGetir",kategori);
        }
        public ActionResult KategoriGuncelle(Kategori k)
        {
            var ktgr = c.Kategoris.Find(k.KategoriID);
            ktgr.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriAlt()
        {
            AltDegerSinif dgr= new AltDegerSinif();
            dgr.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            dgr.Urunler = new SelectList(c.Uruns, "UrunID", "UrunAd");
            return View(dgr);
        }
        public JsonResult UrunGetir(int p)
        {
            var urunListesi=(from x in c.Uruns
                             join y in c.Kategoris
                             on x.Kategori.KategoriID equals y.KategoriID
                             where x.Kategori.KategoriID== p
                             select new
                             {
                                 Text=x.UrunAdi,
                                 Value=x.Urunid.ToString()
                             }).ToList();
            return Json(urunListesi,JsonRequestBehavior.AllowGet);
        }
    }
}