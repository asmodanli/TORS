using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TORS.Views.AnaSayfa;

namespace TORS.Controllers
{
    public class RezervasyonController : Controller
    {
        // GET: Rezervasyon
        public ActionResult Rezervasyon()
        {

            return View();
        }

        public ActionResult Rezervasyon2(string id)
        {
            OracleBaglanti connectDb = new OracleBaglanti();
            var odalar = connectDb.odalariGetir();
            var rez =  new JavaScriptSerializer().Serialize(connectDb.verGetirRezID(id));

            ViewBag.Odalar = odalar;
            ViewBag.Rez = rez;
            return View();
        }


     
        [HttpPost]
        public ActionResult RezEkle(Rezervasyon rez)
        {

           
            OracleBaglanti connectDb = new OracleBaglanti();
      int i=connectDb.veriEkle(rez.T_salon,Convert.ToInt32( rez.K_Sayi),rez.T_Konu,rez.Mudurluk,rez.date, rez.T_Sahip,rez.Bg_Saat,rez.Bt_Saat,rez.T_Irtibat);

            return Json(i);
        }


    }

}

