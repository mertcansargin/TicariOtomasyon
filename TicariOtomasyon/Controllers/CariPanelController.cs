using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c=new Context();
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler=c.Carilers.FirstOrDefault(x=>x.CariMail==mail);
            ViewBag.mail = mail;

            return View(degerler);
        }
        public ActionResult Siparislerim() {
            var mail = (string)Session["CariMail"];
            var id=c.Carilers.Where(x=>x.CariMail==mail.ToString()).Select(y=>y.Cariid).FirstOrDefault();
            var bilgiler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(bilgiler);
        }


        public ActionResult GelenMesaj() {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x=>x.Alıcı==mail).OrderByDescending(x=>x.MesajID).ToList();
            var gelenm = c.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            var gidenm=c.Mesajlars.Count(x=>x.Gönderici==mail).ToString();
            ViewBag.gelensayisi = gelenm;
            ViewBag.gidensayisi= gidenm;
            return View(mesajlar);}

        public ActionResult MesajDetay(int id)
        {
            var mail = (string)Session["CariMail"];
            var degerler=c.Mesajlars.Where(x=>x.MesajID==id).ToList();

            var gelenm = c.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            var gidenm = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.gelensayisi = gelenm;
            ViewBag.gidensayisi = gidenm;
            return View(degerler);
        }
        public ActionResult GidenMesaj()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gönderici == mail).OrderByDescending(x=>x.MesajID).ToList();
            var gelenm = c.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            var gidenm = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.gelensayisi = gelenm;
            ViewBag.gidensayisi = gidenm;
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult YeniMesaj() {
            var mail = (string)Session["CariMail"];
            var gelenm = c.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            var gidenm = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.gelensayisi = gelenm;
            ViewBag.gidensayisi = gidenm;
            return View(); }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar mesaj) {
            var mail = (string)Session["CariMail"];

            mesaj.Gönderici = mail;
            mesaj.Tarih=DateTime.Parse(DateTime.Now.ToShortDateString());
            c.Mesajlars.Add(mesaj);
            c.SaveChanges();
            return View(); }
    }
}