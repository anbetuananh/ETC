using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace VD1
{
    public class UpdateCSDL
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["project"].ToString());
	
        public UpdateCSDL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        
        public int updateToll(TollInfo tl)
        {
            Conn.Open();
            SqlCommand cmd = new SqlCommand("ds_Toll_Update", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] par = new SqlParameter[]
            {
            new SqlParameter("@TL_Stt",SqlDbType.Int),
            new SqlParameter("@TL_Time",SqlDbType.DateTime),
            new SqlParameter("@KH_IdCar",SqlDbType.NChar),
            new SqlParameter("@CA_LicensePlate",SqlDbType.NChar),
            new SqlParameter("@CA_Specy",SqlDbType.NVarChar),
            new SqlParameter("@Fee",SqlDbType.NChar),
            //new SqlParameter("@TL_Image",SqlDbType.NVarChar)
            };


            par[0].Value = tl.tL_Stt;
            par[1].Value =  DateTime.Now.Hour.ToString()    + ":" +
                            DateTime.Now.Minute.ToString()  + ":" +
                            DateTime.Now.Second.ToString()  + " " + 
                            DateTime.Now.Month.ToString()   + "/" + 
                            DateTime.Now.Day.ToString()     + "/" + 
                            DateTime.Now.Year.ToString();
            par[2].Value = tl.kH_IdCar;
            par[3].Value = tl.cA_LicensePlate;
            par[4].Value = tl.cA_Specy;
            par[5].Value = tl.fee;
            //par[6].Value = tl.tL_Image;
           
            cmd.Parameters.AddRange(par);
            cmd.ExecuteNonQuery();
            Conn.Close();
            return 1;
        }


        public int updateMoney(string KH_IdCar,string AC_Status, string AC_Money)
        {
            Conn.Open();
            SqlCommand cmd = new SqlCommand("update tblAccount set AC_Status = @AC_Status,AC_Money = @AC_Money where KH_IdCar = @KH_IdCar", Conn);
            SqlParameter[] par = new SqlParameter[]{
            new SqlParameter("@KH_IdCar",SqlDbType.NChar),
            new SqlParameter("@AC_Status",SqlDbType.NVarChar),
            new SqlParameter("@AC_Money",SqlDbType.NChar)};

            par[0].Value = KH_IdCar;
            par[1].Value = AC_Status;
            par[2].Value = AC_Money;
            
            cmd.Parameters.AddRange(par);
            int i = cmd.ExecuteNonQuery();
            Conn.Close();
            return i;
        }

        public int updateViolation(ViolationInfo vl)
        {
            Conn.Open();
            SqlCommand cmd = new SqlCommand("ds_Violation_Update", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] par = new SqlParameter[]
            {
            new SqlParameter("@VL_Stt",SqlDbType.Int),
            new SqlParameter("@TL_Time",SqlDbType.DateTime),
            new SqlParameter("@KH_IdCar",SqlDbType.NChar),
            new SqlParameter("@CA_LicensePlate",SqlDbType.NChar),
            new SqlParameter("@VL_Specy",SqlDbType.NVarChar),
            new SqlParameter("@VL_Fee",SqlDbType.NChar),
            new SqlParameter("@VL_Number",SqlDbType.Int),
            };

            par[0].Value = vl.vL_Stt;
            par[1].Value = DateTime.Now.Hour.ToString() + ":" +
                            DateTime.Now.Minute.ToString() + ":" +
                            DateTime.Now.Second.ToString() + " " +
                            DateTime.Now.Month.ToString() + "/" +
                            DateTime.Now.Day.ToString() + "/" +
                            DateTime.Now.Year.ToString();
            par[2].Value = vl.kH_IdCar;
            par[3].Value = vl.cA_LicensePlate;
            par[4].Value = vl.vL_Specy;
            par[5].Value = vl.vL_Fee;
            par[6].Value = vl.vL_Number;

            cmd.Parameters.AddRange(par);
            cmd.ExecuteNonQuery();
            Conn.Close();
            return 1;
        }
    }
}
