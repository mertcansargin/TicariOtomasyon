using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c = new Context();
        public ActionResult Index(string a)
        {
            var kar = from x in c.KargoDetays select x;
            if (!string.IsNullOrEmpty(a))
            {
                kar = kar.Where(x => x.TakipKodu.Contains(a));
            }
            return View(kar.ToList());
            
        }
        [HttpGet]
        public ActionResult YeniKargo() {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D" };
            int k1, k2, k3;
            k1=rnd.Next(0,4);
            k2=rnd.Next(0,4);
            k3=rnd.Next(0,4);
            int s1, s2, s3;
            s1=rnd.Next(100,1000);
            s2=rnd.Next(10,100);
            s3=rnd.Next(10,100);
            string kod = s1.ToString() + karakterler[k1] + s2.ToString() + karakterler[k2] + s3.ToString() + karakterler[k3];
            ViewBag.takipkodu = kod;
            return View(); 
        }
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay kd)
        {
            c.KargoDetays.Add(kd);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KargoTakip(string id)
        {
            var ktakip = c.KargoTakips.Where(x=>x.TakipKodu==id).ToList();
            return View(ktakip);
        }
    }
}