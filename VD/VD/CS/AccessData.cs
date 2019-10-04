using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace VD
{
    class AccessData
    {
        //--------------------KHAI BAO BIEN-------------------------//
        public SqlConnection conn;
        public string connectionString =
            @"Data Source= TUANANH-PC\TUANANH;Initial Catalog=ETCManager;Persist Security Info=True;User ID=sa;Password=trinhanh";
        public AccessData()
        {
            conn = new SqlConnection(connectionString);
        }

        //--------------------HAM PROCEDURED-------------------------//
        public void StorePicture(string filename)
        {
            byte[] imageData = null;
            // Read the file into a byte array
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                imageData = new Byte[fs.Length];
                fs.Read(imageData, 0, (int)fs.Length);
            }
            string shortFileName = filename.Split('\\').Last();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ds_Image_Insert", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@filename", shortFileName);
                cmd.Parameters["@filename"].Direction = ParameterDirection.Input;
                cmd.Parameters.Add("@Image", SqlDbType.Image);
                cmd.Parameters["@Image"].Direction = ParameterDirection.Input;

                // Store the byte array within the image field
                cmd.Parameters["@Image"].Value = imageData;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public byte[] RetrieveImage(int id)
        {
            byte[] imageData = null;
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Image from tblImage where ID=" + id + "", conn);
            // Assume previously established command and connection
            // The command SELECTs the IMAGE column from the table

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
            {
                reader.Read();
                // Get size of image data – pass null as the byte array parameter
                long bytesize = reader.GetBytes(0, 0, null, 0, 0);
                // Allocate byte array to hold image data
                imageData = new byte[bytesize];
                long bytesread = 0;
                int curpos = 0;
                int chunkSize = 1;
                while (bytesread < bytesize)
                {
                    // chunkSize is an arbitrary application defined value
                    bytesread += reader.GetBytes(0, curpos, imageData, curpos, chunkSize);
                    curpos += chunkSize;
                }
            }
            conn.Close();
            // byte array ‘imageData’ now contains BLOB from database
            return imageData;
        }

        //--------------------CODE PROCEDURED STT-------------------------//
        public int NumberImageInDB()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Max(ID) from tblImage", conn);
            int kq = int.Parse(cmd.ExecuteScalar().ToString());
            cmd.Dispose();
            conn.Close();
            return kq;
        }

        //--------------------CODE PROCEDURED FILENAME-------------------------//
        public string FileNameOfImage(int id)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select FileName from tblImage where ID=" + id + "", conn);
            string kq = cmd.ExecuteScalar().ToString();
            cmd.Dispose();
            conn.Close();
            return kq;
        }
    }
}
