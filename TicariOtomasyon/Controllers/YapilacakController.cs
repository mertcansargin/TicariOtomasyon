﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class YapilacakController : Controller
    {
        Context c=new Context();
        // GET: Yapilacak,
        public ActionResult Index()
        {
            var deger1 = c.Carilers.Count();
            ViewBag.d1 = deger1;
            var deger2=c.Uruns.Count();
            ViewBag.d2 = deger2;
            var deger3=c.Kategoris.Count();
            ViewBag.d3 = deger3;
            var deger4 = (from x in c.Carilers select x.CariSehir).Distinct().Count().ToString();
            ViewBag.d4 = deger4;


            var sorgu = c.Yapilacaks.ToList();
            return View(sorgu);
        }
    }
}