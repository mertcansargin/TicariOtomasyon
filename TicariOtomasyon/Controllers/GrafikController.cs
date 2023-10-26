using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik

        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            var grafikciz = new Chart(600, 600);
            grafikciz.AddTitle("Kategori-Ürün Sayısı").AddLegend("Stok").AddSeries("Değerler",
                xValue: new[] { "Mobilya", "Ofis Eşyaları", "Bilgisayar" },
                yValues: new[] {85,60,90}).Write();

            return File(grafikciz.ToWebImage().GetBytes(),"images/jpeg"); 
        }
        public ActionResult Index3() { 
            ArrayList xvalue =new ArrayList();
            ArrayList yvalue =new ArrayList();
            var sonuclar =c.Uruns.ToList();
            sonuclar.ToList().ForEach(x=>xvalue.Add(x.UrunAdi));
            sonuclar.ToList().ForEach(y=>yvalue.Add(y.Stok));
            var grafik=new Chart(600, 600).AddTitle("Stoklar").AddSeries(chartType:"Pie",name:"Değerler", xValue:xvalue, yValues:yvalue).Write();

            return File(grafik.ToWebImage().GetBytes(), "images/jpeg");
        }


        public ActionResult Index4() { return View(); }
        public ActionResult VisualizeUrunResult()
        {

            return Json(UrunListesi(),JsonRequestBehavior.AllowGet);
        }
        public List<GrafikSinif> UrunListesi() {
        List<GrafikSinif>snf=new List<GrafikSinif>();
            using (var c=new Context())
            {
                snf = c.Uruns.Select(x => new GrafikSinif
                {
                    urn = x.UrunAdi,
                    stk = x.Stok
                }).ToList();
            }
            return snf;
            }
   
    public ActionResult Index6()
        {
            return View();
        }
    
    }
}