using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TORS.Views.Rezervasyon
{
    public class OdaEkle
    {
        public int oda_id { get; set; }
        public int oda_kapasite { get; set; }
        public string oda_konum { get; set; }
        public string oda_bilgi { get; set; }
        public string oda_adi { get; set; }
    }
}