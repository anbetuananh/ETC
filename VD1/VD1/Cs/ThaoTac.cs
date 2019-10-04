using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VD1
{
    public class Cls_ThaoTac
    {
        public Cls_ThaoTac()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //--------------------LAY DANH SACH KHACH HANG-------------------------//
        public static DataTable LayKh()
        {
            try
            {
                DataTable dt = ClsConnect.ReturnDataTable("ds_KhachHang_Load");
                return dt;
            }
            catch
            {
                return null;
            }
        }

        //--------------------LAY DANH SACH XE-------------------------//
        public static DataTable LayXe()
        {
            try
            {
                DataTable dt = ClsConnect.ReturnDataTable("ds_Xe_Load");
                return dt;
            }
            catch
            {
                return null;
            }
        }

        //--------------------LAY DANH SACH TAI KHOAN-------------------------//
        public static DataTable LayTk()
        {
            try
            {
                DataTable dt = ClsConnect.ReturnDataTable("ds_TaiKhoan_Load");
                return dt;
            }
            catch
            {
                return null;
            }
        }

        //--------------------LAY DANH SACH TOLL-------------------------//
        public static DataTable LayToll()
        {
            try
            {
                DataTable dt = ClsConnect.ReturnDataTable("ds_Toll_Load");
                return dt;
            }
            catch
            {
                return null;
            }
        }

        //--------------------LAY MA THE RFID TU BANG tblCar-------------------------//
        public static DataTable LayMaRfid()
        {
            try
            {
                DataTable dt = ClsConnect.ReturnDataTable("ds_LayMaRfid");
                return dt;
            }
            catch
            {
                return null;
            }
        }

        //--------------------LAY THONG TIN XE SAU KHI KIEM TRA MA RFID-------------------------//
        public static DataTable LayThongTin()
        {
            try
            {
                DataTable dt = ClsConnect.ReturnDataTable("ds_LayThongTin");
                return dt;
            }
            catch
            {
                return null;
            }
        }

        //--------------------LAY SO THU TU BANG TOLL-------------------------//
        public static DataTable LayStt()
        {
            try
            {
                DataTable dt = ClsConnect.ReturnDataTable("ds_LayStt");
                return dt;
            }
            catch
            {
                return null;
            }
        }

        //--------------------LAY SO TIEN TRONG TAI KHOAN-------------------------//
        public static DataTable LayTienRaTru()
        {
            try
            {
                DataTable dt = ClsConnect.ReturnDataTable("ds_LayTien_RaTru");
                return dt;
            }
            catch
            {
                return null;
            }
        }

        //--------------------LAY DANH SACH VI PHAM-------------------------//
        public static DataTable LayViolation()
        {
            try
            {
                DataTable dt = ClsConnect.ReturnDataTable("ds_Violation_Load");
                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}
