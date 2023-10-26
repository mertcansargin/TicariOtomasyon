using System;
using System.Collections.Generic;
using System.IO;
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
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Images/"+dosyaadi+uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                personel.PersonelGorsel = "/Images/" + dosyaadi + uzanti;
            }

            //if (!ModelState.IsValid)
            //{ return View(); }
            c.Personels.Add(personel);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {    
            
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList() select new SelectListItem { Text = x.DepartmanAd, Value = x.Departmanid.ToString() }).ToList();
            ViewBag.dgr1=deger1;
            var personel = c.Personels.Find(id);
            return View("PersonelGetir", personel);
        }
        public ActionResult PersonelGuncelle(Personel per)
        {

            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Images/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                per.PersonelGorsel = "/Images/" + dosyaadi + uzanti;
            }

            if (!ModelState.IsValid) { return View("PersonelGetir"); }
            var personel = c.Personels.Find(per.Personelid);
            personel.Personelid = per.Personelid;
            personel.PersonelAd = per.PersonelAd;
            personel.PersonelSoyad = per.PersonelSoyad;
            personel.PersonelGorsel = per.PersonelGorsel;
            personel.Departmanid = per.Departmanid;
            c.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult PersonelListesi() {
            var degerler = c.Personels.ToList();
            return View(degerler); 
        }
    }
}