using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using TORS.Views.AnaSayfa;

namespace TORS.Controllers
{
    public class ToplantiTarihController : Controller
    {
        // GET: ToplantiTarih
        public ActionResult ToplantiTarih(string id)
        {
            ViewBag.RezervasyonListesi = JsonConvert.SerializeObject(RezervasyonGetir(id));
            return View();
        }

        public JsonResult RezervasyonGetir(string id)
        {

            OracleBaglanti a = new OracleBaglanti();
            List<Rezervasyon> rezervasyonListesi = a.verGetirSahip(id);
            string JsonReservation = new JavaScriptSerializer().Serialize(rezervasyonListesi);

            return Json(rezervasyonListesi, JsonRequestBehavior.AllowGet);
        }
    }
}