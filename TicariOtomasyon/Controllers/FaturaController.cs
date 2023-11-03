using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;


namespace TicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        Context c=new Context();
        // GET: Fatura
        public ActionResult Index()
        {
            var faturalar = c.Faturalars.ToList();
            return View(faturalar);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id) {
            var fatura=c.Faturalars.Find(id);
            return View(fatura);
        }
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var fatura = c.Faturalars.Find(f.Faturaid);
            fatura.FaturaSıraNo = f.FaturaSıraNo;
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.Tarih=f.Tarih;
            fatura.Saat = f.Saat;
            fatura.TeslimAlan=f.TeslimAlan;
            fatura.TeslimEden = f.TeslimEden;
            fatura.Toplam=f.Toplam;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var faturak=c.FaturaKalems.Where(f=>f.Faturaid==id).ToList();
            return View(faturak);
        }

        [HttpGet]
        public ActionResult YeniKalem() { return View(); }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem fk) { 
            c.FaturaKalems.Add(fk);
            c.SaveChanges();
            return RedirectToAction("Index"); }

        public ActionResult DinamikFatura() { 
            DinamikFaturaClass dmk=new DinamikFaturaClass();
            dmk.deger1 = c.Faturalars.ToList();
            dmk.deger2=c.FaturaKalems.ToList();
            return View(dmk); }

        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo, DateTime Tarih , string VergiDairesi,
            string Saat, string TeslimEden, string TeslimAlan, string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar f=new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.Tarih = Tarih;
            f.VergiDairesi= VergiDairesi;
            f.Saat = Saat;
            f.TeslimEden= TeslimEden;
            f.TeslimAlan = TeslimAlan;
            f.Toplam = decimal.Parse(Toplam);
            c.Faturalars.Add(f);
            foreach(var x in kalemler)
            {
                FaturaKalem fk=new FaturaKalem();
                fk.Aciklama = x.Aciklama;
                fk.BirimFiyat= x.BirimFiyat;
                fk.Faturaid= x.Faturaid;
                fk.Miktar= x.Miktar;
                fk.Tutar= x.Tutar;
                c.FaturaKalems.Add(fk);
            }
            c.SaveChanges();
            return Json("İşlem Başarılı",JsonRequestBehavior.AllowGet);
        }

    }
} 