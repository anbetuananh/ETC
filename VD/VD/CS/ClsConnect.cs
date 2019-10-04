using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VD
{   
    class ClsConnect
    {
    SqlConnection con = new SqlConnection();
    string ketnoi = ConfigurationManager.ConnectionStrings["project"].ConnectionString;

    public ClsConnect()
    {
        con.ConnectionString = ketnoi;
        if (con.State == ConnectionState.Closed)
        con.Open();
    }
    public DataTable LoadData(string sql)
    {
        SqlCommand command = new SqlCommand(sql,con);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        return dt;
    }
    public DataTable LoadData(string sql, string[] name, object[] value, int Nparameter)
    {
        SqlCommand command = new SqlCommand(sql,con);
        for (int i =0 ; i < Nparameter; i++)
        {
            command.Parameters.AddWithValue(name[i], value[i]);
        }
        command.CommandType = CommandType.StoredProcedure; 
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        return dt;
    }
    public int Update(string sql)
    {
        SqlCommand command = new SqlCommand(sql,con);
        command.CommandType = CommandType.StoredProcedure; 
        int kq = command.ExecuteNonQuery();
        return kq;
    }
    public int Update(string sql, string [] name,object[] value, int Nparameter)
    {
        SqlCommand command = new SqlCommand(sql,con);
        for (int i = 0; i < Nparameter; i++)
        {
            command.Parameters.AddWithValue(name[i], value[i]);
        }
        command.CommandType = CommandType.StoredProcedure;
        int kq = command.ExecuteNonQuery();
        return kq;
    }
    }
}
