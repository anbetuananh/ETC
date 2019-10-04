using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VD
{
    public partial class ucTimKiem : UserControl
    {
        ClsConnect cnTK = new ClsConnect();
        public ucTimKiem()
        {
            InitializeComponent();
        }

        //--------------------FORM LOAD-------------------------//
        private void ucTimKiem_Load(object sender, EventArgs e)
        {

        }

        //--------------------CODE BUTTON NGAT-------------------------//
        private void btNgat_Click(object sender, EventArgs e)
        {

        }

        //--------------------CODE BUTTON XOA-------------------------//
        private void btXoa_Click(object sender, EventArgs e)
        {

        }

        //--------------------CODE BUTTON KET NOI-------------------------//
        private void btKetNoi_Click(object sender, EventArgs e)
        {

        }

        private void cbCom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //--------------------CODE BUTTON SEARCH-------------------------//
        public string hoten_tam = "";
        public string makh_tam = "";
        public string maxe_tam = "";
        public int    kieutimkiem_int;
        public string kieutimkiem_string = "";

        public string bang_xe = "";
        public string bang_tk = "";

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dt_kh = new DataTable();
            dt_kh = cnTK.LoadData("ds_KhachHang_Load");

            
            for (int i = 0; i < dt_kh.Rows.Count; i++)
            {
                // Tim kiem theo ho ten
                if (txtHoTen.Text != "")
                {
                    if ((dt_kh.Rows[i][1].ToString()).Contains(txtHoTen.Text))
                    {
                        hoten_tam = dt_kh.Rows[i][1].ToString();
                        bang_xe = dt_kh.Rows[i][7].ToString();
                        bang_tk = dt_kh.Rows[i][8].ToString();
                        kieutimkiem_int = 1;
                        break;
                    }
                }
                
                // Tim kiem theo ma khach hang
                if (txtMaKH.Text != "")
                {
                     if ((dt_kh.Rows[i][0].ToString()) == txtMaKH.Text)
                    {
                         makh_tam = txtMaKH.Text.Trim();
                         bang_xe = dt_kh.Rows[i][7].ToString();
                         bang_tk = dt_kh.Rows[i][8].ToString();
                         kieutimkiem_int = 2;
                         break;
                    }
                }
                if (txtMaxe.Text != "")
                {
                 // Tim kiem theo ma xe
                     if ((dt_kh.Rows[i][7].ToString()).Contains(txtMaxe.Text))
                     {
                         maxe_tam = dt_kh.Rows[i][7].ToString();
                         bang_xe = dt_kh.Rows[i][7].ToString();
                         bang_tk = dt_kh.Rows[i][8].ToString();
                         kieutimkiem_int = 3;
                         break;
                     }
                }
                kieutimkiem_int = 0;
            }
            
            if (kieutimkiem_int != 0 )
            {
                int thamso = 1;
                string[] name = new string[thamso];
                string[] value = new string[thamso];

                switch(kieutimkiem_int)
                {
                    case 1:
                        name[0] = "@HoTen";
                        value[0] = hoten_tam;
                        kieutimkiem_string = "ds_TimKiem_HoTen";
                        break;
                    case 2:
                        name[0] = "@MaKH";
                        value[0] = makh_tam;
                        kieutimkiem_string = "ds_TimKiem_MaKH";
                        break;
                    case 3:
                        name[0] = "@MaXe";
                        value[0] = maxe_tam;
                        kieutimkiem_string = "ds_TimKiem_MaXe";
                        break;
                }
            
                // Load ra bang cac thong tin
                DataTable tk1 = new DataTable();
                tk1 = cnTK.LoadData(kieutimkiem_string, name, value, thamso);
                gridControl1.DataSource = tk1;

                int thamso_xe = 1;
                string[] name_xe = new string[thamso_xe];
                string[] value_xe = new string[thamso_xe];
                name_xe[0] = "@BangXe";
                value_xe[0] = bang_xe;
                DataTable tk2 = new DataTable();
                tk2 = cnTK.LoadData("ds_TimKiem_BangXe", name_xe, value_xe, thamso_xe);
                gridControl2.DataSource = tk2;

                int thamso_tk = 1;
                string[] name_tk = new string[thamso_tk];
                string[] value_tk = new string[thamso_tk];
                name_tk[0] = "@BangTk";
                value_tk[0] = bang_tk;
                DataTable tk3 = new DataTable();
                tk3 = cnTK.LoadData("ds_TimKiem_BangTk", name_tk, value_tk, thamso_tk);
                gridControl3.DataSource = tk3;


                // Xoa cac bien tam cho buoc tiep theo
                makh_tam = "";
                maxe_tam = "";
                hoten_tam = "";
                bang_xe = "";
                bang_tk = "";
            }
            else
            {
                MessageBox.Show("KHÔNG CÓ DỮ LIỆU CẦN TÌM", "Thông báo", MessageBoxButtons.OK);
            }
        }

        //--------------------CODE BUTTON REFRESH-------------------------//
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtHoTen.Text = "";
            txtMaKH.Text = "";
            txtMaxe.Text = "";
            ucTimKiem_Load(sender,e);
        }
    }
}
