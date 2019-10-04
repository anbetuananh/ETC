using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;


namespace VD1
{
    public class Ex_Car
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["project"].ToString());
        public Ex_Car()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string[] maTrue;

        public int showCar(string Sql,string bien)
        {
            Conn.Open();
            string sql = Sql;
            SqlCommand cmd = new SqlCommand(sql, Conn);
            ArrayList lst = new ArrayList();
            CarInfo hh;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                hh = new CarInfo();
                if (!dr.IsDBNull(0)) hh.Maxe = dr.GetString(0);
                if (!dr.IsDBNull(1)) hh.Bienso = dr.GetString(1);
                if (!dr.IsDBNull(2)) hh.Nhanhieu = dr.GetString(2);
                if (!dr.IsDBNull(3)) hh.Soloai = dr.GetString(3);
                if (!dr.IsDBNull(4)) hh.Namsx = dr.GetString(4);
                if (!dr.IsDBNull(5)) hh.Sokhung = dr.GetString(5);
                if (!dr.IsDBNull(6)) hh.Somay = dr.GetString(6);
                if (!dr.IsDBNull(7)) hh.Ngaydk = dr.GetDateTime(7);
                if (!dr.IsDBNull(8)) hh.Loaixe = dr.GetString(8);
                if (!dr.IsDBNull(9)) hh.MaRfid = dr.GetString(9);
                lst.Add(hh);

                if (bien.Trim() == hh.MaRfid.Trim())
                {
                    return 1;
                    break;
                }
            }

            dr.Close();
            Conn.Close();
           return 0;
        }
        public string[] giatri()
        {
            maTrue.ToArray();
            return maTrue;
        }
    }
}
