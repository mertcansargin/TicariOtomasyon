using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman

        Context c = new Context();
        public ActionResult Index()
        {
            var dep=c.Departmans.Where(x=>x.Durum).ToList();
            return View(dep);
        }

        public ActionResult DepartmanSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles ="1")]
        [HttpGet]
        public ActionResult DepartmanEkle() { return View(); }

        [HttpPost]
        public ActionResult DepartmanEkle(Departman d) {

            c.Departmans.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanGetir(int id) {
            var dep = c.Uruns.Find(id);
            return View("DepartmanGetir",dep);
        }
        public ActionResult DepartmanGuncelle(Departman d) {

            var dep = c.Departmans.Find(d.Departmanid);
            dep.Departmanid= d.Departmanid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id) {
            var dep = c.Personels.Where(x=>x.Departmanid==id).ToList();
            var bg=c.Departmans.Where(x=>x.Departmanid==id).Select(y=>y.DepartmanAd).FirstOrDefault();
            ViewBag.bag=bg;
            return View(dep);
        }
        public ActionResult DepartmanPersonelSatis(int id) {
            var dep = c.SatisHarekets.Where(x => x.Personelid == id).ToList();
            var bg=c.Personels.Where(x=>x.Personelid==id).Select(y=>y.PersonelAd+" "+ y.PersonelSoyad).FirstOrDefault();
            ViewBag.bagPers = bg;

            return View(dep); }
    }
}