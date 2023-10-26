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
    }
}