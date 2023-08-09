using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        Context c=new Context();
        // GET: Personel
        public ActionResult Index()
        {
            var degerler=c.Personels.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle() {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList() select new SelectListItem { Text = x.DepartmanAd, Value = x.Departmanid.ToString() }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel personel)
        {
            if (!ModelState.IsValid)
            { return View(); }
            c.Personels.Add(personel);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {            List<SelectListItem> deger1 = (from x in c.Departmans.ToList() select new SelectListItem { Text = x.DepartmanAd, Value = x.Departmanid.ToString() }).ToList();
            ViewBag.dgr1=deger1;
            var personel = c.Personels.Find(id);
            return View("PersonelGetir", personel);
        }
        public ActionResult PersonelGuncelle(Personel per)
        {
            if (!ModelState.IsValid) { return View("PersonelGetir"); }
            var personel = c.Personels.Find(per.Personelid);
            per.Personelid = per.Personelid;
            per.PersonelAd = per.PersonelAd;
            per.PersonelSoyad = per.PersonelSoyad;
            per.PersonelGorsel = per.PersonelGorsel;
            per.Departmanid = per.Departmanid;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}