﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun

        Context c = new Context();
        public ActionResult Index()
        {
            var urunler=c.Uruns.Where(X=>X.Durum==true).ToList();
            return View(urunler);
        }

        [HttpGet]
        public ActionResult UrunEkle() { 
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList() select new SelectListItem {Text=x.KategoriAd,Value=x.KategoriID.ToString()}).ToList();
            ViewBag.dgr1 = deger1;
            return View(); }
                
        [HttpPost]
        public ActionResult UrunEkle(Urun u) { 
        c.Uruns.Add(u);
        c.SaveChanges();
        return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            var deger=c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id) 
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList() select new SelectListItem { Text = x.KategoriAd, Value = x.KategoriID.ToString() }).ToList();
            ViewBag.dgr1 = deger1;

            var urundeger=c.Uruns.Find(id);
            return View("UrunGetir", urundeger);
        }

        public ActionResult UrunGuncelle(Urun p) {
            var urn = c.Uruns.Find(p.Urunid);
            urn.UrunAdi= p.UrunAdi;
            urn.AlisFiyat = p.AlisFiyat;
            urn.SatisFiyat= p.SatisFiyat;
            urn.Durum = p.Durum;
            urn.Stok=p.Stok;
            urn.Marka=p.Marka;
            urn.UrunGorsel=p.UrunGorsel;
            urn.Kategoriid=p.Kategoriid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunListesi(Urun p) { return View(p); } 

    }
}