using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VD1
{
    public class ClsConnect
    {
        public ClsConnect()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        // Lay co so du lieu tra ve bang
        public static DataTable ReturnDataTable(string sp_sql)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["project"].ToString());
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = sp_sql;
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Connection = sqlConnection;
            SqlDataAdapter sqlDadap = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sqlDadap.Fill(dt);
            sqlConnection.Close();
            return dt;
        }


        // Lay co so du lieu theo Procedure tra ve theo kieu ExecuteNonQuery
        public static bool ExecuteProcdure(string ChuoiLenh, SqlParameter[] Thamso)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["project"].ToString());
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = ChuoiLenh;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddRange(Thamso);
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
