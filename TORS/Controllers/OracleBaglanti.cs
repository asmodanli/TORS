using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using TORS.Views.AnaSayfa;
using TORS.Views.Rezervasyon;

namespace TORS.Controllers
{
    public class OracleBaglanti
    {
        OracleConnection OracleConnection1;
        OracleCommand OracleCommand1;


        public bool baglan()
        {
            try
            {
                OracleConnection1 = new OracleConnection();
                OracleConnection1.ConnectionString = "User ID=STAJ; Password=STAJ; Data Source=lnx62; Integrated Security=NO;";
                OracleConnection1.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public int veriEkle(string T_salon, int K_Sayi, string T_Konu, string Mudurluk,string date,string T_Sahip, string Bg_Saat, string Bt_Saat, string T_Irtibat)
        {


            try {
                baglan();
                


                OracleCommand cmd;

                String secim = String.Format("select * from tors_rezervasyon where (baslama_saat " +
                                             "BETWEEN '" + Bg_Saat + "' and '" + Bt_Saat + "' and  " +
                                             "bitis_saat between '" + Bg_Saat + "' and '" + Bt_Saat + "' " +
                                             "or baslama_saat <= '" + Bg_Saat + "' and bitis_saat between '" + Bg_Saat + "' and '" + Bt_Saat + "' " +
                                             "or baslama_saat between '" + Bg_Saat  +"' and '" + Bt_Saat + "' " + " and bitis_saat >= '" + Bt_Saat +
                                              "' or baslama_saat <= '" + Bg_Saat + "' and bitis_saat >= '" + Bt_Saat +
                                             "') and t_salon= '" + T_salon + "'" +
                                             " and tarih = to_date('" + date + "', 'DD.MM.YYYY')");

               

                cmd = new OracleCommand(secim, OracleConnection1);
              OracleDataReader dr = cmd.ExecuteReader();

                if(dr.HasRows)
                {
                    return 0;
                    
                }

                else
                {
                    String sql = String.Format("INSERT INTO TORS_REZERVASYON(/*id,*/T_SALON,KISI_SAYI,TOPLANTI_KONU,MUDURLUK,TARIH,TOPLANTI_SAHIP,BASLAMA_SAAT,BITIS_SAAT,IRTIBAT)" +
                    "VALUES('{0}' ,{1},'{2}', '{3}', to_date('{4}', 'DD.MM.YYYY'),'{5}', '{6}', '{7}','{8}')", T_salon, K_Sayi, T_Konu, Mudurluk, date, T_Sahip, Bg_Saat, Bt_Saat, T_Irtibat);

                        cmd = new OracleCommand( sql, OracleConnection1);
                        cmd.ExecuteNonQuery();
                    return 1;

                }

            


                cmd.Connection.Close();
            }
            catch (Exception ex) {
                return -1;
            }
         

        }
        public void veriSil(string id)
        {


            try
            {
                baglan();


                OracleCommand cmd;

                String sql = "DELETE FROM tors_rezervasyon WHERE ID= '"+ id + "'";
               


                cmd = new OracleCommand(sql, OracleConnection1);
                cmd.ExecuteNonQuery();


                cmd.Connection.Close();
            }
            catch (Exception ex) { }


        }
        public void guncelle(int id ,string T_salon, int K_Sayi, string T_Konu, string Mudurluk, string date, string T_Sahip, string Bg_Saat, string Bt_Saat, string T_Irtibat)
        {



            try
            {
                baglan();


                OracleCommand cmd;
                String sql = String.Format("UPDATE TORS_REZERVASYON SET" +
                                     " T_SALON = '" + T_salon + "'," +
                                   " KISI_SAYI = '" + K_Sayi + "'," +
                                   " TOPLANTI_KONU = '" + T_Konu + "'," +
                                   " MUDURLUK = '" + Mudurluk + "'," +
                                   "TARIH = to_date('" + date + "','DD.MM.YYYY')," +
                                   " TOPLANTI_SAHIP = '" + T_Sahip + "'," +
                                   "BASLAMA_SAAT = '" + Bg_Saat + "'," +
                                      "BITIS_SAAT = '" + Bt_Saat + "'," +
                                   " IRTIBAT = '" + T_Irtibat + "'  where id = " + id);

               

                cmd = new OracleCommand(sql, OracleConnection1);
                cmd.ExecuteNonQuery();


                cmd.Connection.Close();
            }
            catch (Exception ex) { }
                      


        }
        public List<Rezervasyon> verGetirSahip(string id)
        {
            baglan();
            string query = "SELECT T_SALON,KISI_SAYI,TOPLANTI_KONU,MUDURLUK,TARIH,TOPLANTI_SAHIP,BASLAMA_SAAT,BITIS_SAAT,IRTIBAT,id FROM tors_rezervasyon";
            if (!string.IsNullOrEmpty(id))
                query+=" WHERE toplanti_sahip ='"+id.ToUpper()+"'";


            OracleCommand1 = new OracleCommand(query);
            OracleCommand1.Connection = this.OracleConnection1;
            OracleCommand1.CommandType = CommandType.Text;

            try
            {

                List<Rezervasyon> rezervasyonlar = new List<Rezervasyon>();


        OracleDataReader OracleDataReader1 = OracleCommand1.ExecuteReader();
    

                while (OracleDataReader1.Read())
                {
                    Rezervasyon rezervasyon = new Rezervasyon();
                    var aaa = OracleDataReader1[9].ToString();

                    rezervasyon.T_salon = OracleDataReader1[0].ToString();
                    rezervasyon.K_Sayi = Convert.ToInt32(OracleDataReader1[1]) ;
                    rezervasyon.T_Konu = OracleDataReader1[2].ToString();
                    rezervasyon.Mudurluk = OracleDataReader1[3].ToString();
                    rezervasyon.date = OracleDataReader1[4].ToString();
                    rezervasyon.T_Sahip = OracleDataReader1[5].ToString();
                    rezervasyon.Bg_Saat = OracleDataReader1[6].ToString();
                    rezervasyon.Bt_Saat = OracleDataReader1[7].ToString();
                    rezervasyon.T_Irtibat = OracleDataReader1[8].ToString();
                    rezervasyon.id = OracleDataReader1[9].ToString();             
                    rezervasyonlar.Add(rezervasyon);

                }
                return rezervasyonlar;
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<Rezervasyon> verGetirRezID(string id)
        {
            baglan();
            string query = "";
            if (!string.IsNullOrEmpty(id))
            {
                query = "SELECT T_SALON,KISI_SAYI,TOPLANTI_KONU,MUDURLUK,TARIH,TOPLANTI_SAHIP,BASLAMA_SAAT,BITIS_SAAT,IRTIBAT,id FROM tors_rezervasyon";

                query += " WHERE id ='" + id.ToUpper() + "'";
            }

            OracleCommand1 = new OracleCommand(query);
            OracleCommand1.Connection = this.OracleConnection1;
            OracleCommand1.CommandType = CommandType.Text;

            try
            {
              

                List<Rezervasyon> rezervasyonlar = new List<Rezervasyon>();


                OracleDataReader OracleDataReader1 = OracleCommand1.ExecuteReader();


                while (OracleDataReader1.Read())
                {
                    Rezervasyon rezervasyon = new Rezervasyon();
                    var aaa = OracleDataReader1[9].ToString();

                    rezervasyon.T_salon = OracleDataReader1[0].ToString();
                    rezervasyon.K_Sayi = Convert.ToInt32(OracleDataReader1[1]);
                    rezervasyon.T_Konu = OracleDataReader1[2].ToString();
                    rezervasyon.Mudurluk = OracleDataReader1[3].ToString();
                    rezervasyon.date = OracleDataReader1[4].ToString();
                    rezervasyon.T_Sahip = OracleDataReader1[5].ToString();
                    rezervasyon.Bg_Saat = OracleDataReader1[6].ToString();
                    rezervasyon.Bt_Saat = OracleDataReader1[7].ToString();
                    rezervasyon.T_Irtibat = OracleDataReader1[8].ToString();
                    rezervasyon.id = OracleDataReader1[9].ToString();
                    rezervasyonlar.Add(rezervasyon);

                }
                return rezervasyonlar;

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<OdaEkle> odalariGetir()
        {
            baglan();
            string query = "SELECT * FROM tors_oda_adi";
            

            OracleCommand1 = new OracleCommand(query);
            OracleCommand1.Connection = this.OracleConnection1;
            OracleCommand1.CommandType = CommandType.Text;

            try
            {
                List<OdaEkle> odalar = new List<OdaEkle>();
                OracleDataReader OracleDataReader1 = OracleCommand1.ExecuteReader();
                while (OracleDataReader1.Read())
                {
                    OdaEkle oda = new OdaEkle();

                    oda.oda_id = Convert.ToInt32(OracleDataReader1[0].ToString());
                    oda.oda_adi= OracleDataReader1[1].ToString();
                    oda.oda_kapasite = Convert.ToInt32(OracleDataReader1[2].ToString());
                    oda.oda_konum = OracleDataReader1[3].ToString();
                    oda.oda_bilgi = OracleDataReader1[4].ToString();


                    odalar.Add(oda);
                }
                return odalar;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
