using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TORS.Views.AnaSayfa;
using Newtonsoft.Json;
namespace TORS.Controllers
{
    public class AnaSayfaController : Controller
    {
        // GET: AnaSayfa
        public ActionResult AnaSayfa(string id)
        {

            ViewBag.RezervasyonListesi = JsonConvert.SerializeObject(RezervasyonGetir(id));

          // a.veriEkle("KASIMPAŞA ZMİN A","","","","","staj hk",1,"","","");

            return View();
        }


        public JsonResult RezervasyonGetir(string id) {

            OracleBaglanti a = new OracleBaglanti();
            List<Rezervasyon> rezervasyonListesi = a.verGetirSahip(id);
            string JsonReservation = new JavaScriptSerializer().Serialize(rezervasyonListesi);

            return Json(rezervasyonListesi, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult RezSil(string id)
        {


            OracleBaglanti connectDb = new OracleBaglanti();
            connectDb.veriSil(id);

            return Json("OK");
        }
    }
}