using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{  
    [Authorize]
    public class CariPanelController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alıcı == mail).ToList();
            ViewBag.mail = mail;
            var mailid=c.Carilers.Where(x=>x.CariMail == mail).Select(y=>y.Cariid).FirstOrDefault();
            ViewBag.mid = mailid;
            var toplamsatis=c.SatisHarekets.Where(x=>x.Cariid==mailid).Count();
            ViewBag.toplamsatis=toplamsatis;
            var toplamtutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y=>y.ToplamTutar);
            ViewBag.toplamtutar=toplamtutar;
            var toplamadet = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            ViewBag.toplamadet=toplamadet;
            var adsoyad=c.Carilers.Where(x=>x.Cariid==mailid).Select(y=>y.CariAd+" "+y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad=adsoyad;
            var carimail = c.Carilers.Where(x => x.Cariid == mailid).Select(y => y.CariMail).FirstOrDefault();
            ViewBag.carimail=carimail;
            var carisehir = c.Carilers.Where(x => x.Cariid == mailid).Select(y => y.CariSehir).FirstOrDefault();
            ViewBag.carisehir = carisehir;
            return View(degerler);
        }
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.Cariid).FirstOrDefault();
            var bilgiler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(bilgiler);
        }
        public ActionResult GelenMesaj()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Alıcı == mail).OrderByDescending(x => x.MesajID).ToList();
            var gelenm = c.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            var gidenm = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.gelensayisi = gelenm;
            ViewBag.gidensayisi = gidenm;
            return View(mesajlar);
        }
        public ActionResult MesajDetay(int id)
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();
            var gelenm = c.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            var gidenm = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.gelensayisi = gelenm;
            ViewBag.gidensayisi = gidenm;
            return View(degerler);
        }
        public ActionResult GidenMesaj()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gönderici == mail).OrderByDescending(x => x.MesajID).ToList();
            var gelenm = c.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            var gidenm = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.gelensayisi = gelenm;
            ViewBag.gidensayisi = gidenm;
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gelenm = c.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            var gidenm = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.gelensayisi = gelenm;
            ViewBag.gidensayisi = gidenm;
            return View();
        }
        
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar mesaj)
        {
            var mail = (string)Session["CariMail"];
            mesaj.Gönderici = mail;
            mesaj.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.Mesajlars.Add(mesaj);
            c.SaveChanges();
            return View();
        }

        public ActionResult KargoTakip(string a)
        {
            var kar = from x in c.KargoDetays select x;
            kar = kar.Where(x => x.TakipKodu.Contains(a));
            return View(kar.ToList());

        }

        public ActionResult CariKargoTakip(string id)
        {
            var ktakip = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(ktakip);

        }
        public ActionResult LogOut() {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login"); }
        public PartialViewResult PartialAyarlar()
        {
            var mail = (string)Session["CariMail"];
            var id=c.Carilers.Where(x=>x.CariMail==mail).Select(y=>y.Cariid).FirstOrDefault();
            var caribul = c.Carilers.Find(id);
            return PartialView("PartialAyarlar", caribul);
        }
        public ActionResult PartialDuyurular()
        {
            var duyurular=c.Mesajlars.Where(x=>x.Gönderici=="admin").ToList();
            return PartialView(duyurular);
        }
        public ActionResult CariBilgiGuncelle(Cariler cr)
        {
            var cari = c.Carilers.Find(cr.Cariid);
            cari.CariAd=cr.CariAd;
            cari.CariSoyad=cr.CariSoyad;
            cari.CariMail=cr.CariMail;
            cari.CariSehir = cr.CariSehir;
            cari.CariSifre=cr.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}