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
    public partial class ucNapTien : UserControl
    {
        ClsConnect cnNT = new ClsConnect();
        public ucNapTien()
        {
            InitializeComponent();
        }

        //--------------------FORM LOAD-------------------------//
        private void ucNapTien_Load(object sender, EventArgs e)
        {

        }

        //--------------------CODE BUTTON SEARCH-------------------------//
        public string tientam = "";
        public string matk_tam = "";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = cnNT.LoadData("ds_TaiKhoan_Load");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // Tim kiem theo ma tai khoan
                if (txtMatk.Text != "")
                {
                    if ((dt.Rows[i][1].ToString()).Contains(txtMatk.Text))
                    {
                        matk_tam = dt.Rows[i][1].ToString();
                        tientam = dt.Rows[i][8].ToString();
                        break;
                    }
                }
            }

            txtSodu.Text = tientam;

            int thamso_tk = 1;
            string[] name_tk = new string[thamso_tk];
            string[] value_tk = new string[thamso_tk];
            name_tk[0] = "@MaTk";
            value_tk[0] = matk_tam;

            DataTable tk = new DataTable();
            tk = cnNT.LoadData("ds_NopTien_Tk", name_tk, value_tk, thamso_tk);
            gridControl1.DataSource = tk;
        }

        //--------------------CODE BUTTON CHARGE-------------------------//
        public int tientam_int = 0;
        public int sodu_int = 0;
        public string sodu_string = "";
        public string trangthai = "";
        private void simpleButton2_Click(object sender, EventArgs e)
        {            
           if (txtTienNop.Text != "")
              {
                if ((MessageBox.Show("Bạn có chắc chắn muốn nạp không ?", "Thông báo", MessageBoxButtons.YesNo)) == DialogResult.Yes)
                    {
                    tientam_int = Convert.ToInt32(tientam);
                    sodu_int = tientam_int + Convert.ToInt32(txtTienNop.Text);
                    sodu_string = Convert.ToString(sodu_int);

                    if (sodu_int >= 300000)
                    {
                       trangthai = "Good";
                    }
                    if ((sodu_int >= 100000) && (sodu_int < 300000))
                    {
                       trangthai = "Normal";
                    }
                    if (sodu_int <= 0)
                    {
                       trangthai = "Bad";
                    }

                    int thamso_nt = 3;
                    string[] name_nt = new string[thamso_nt];
                    string[] value_nt = new string[thamso_nt];
                    name_nt[0] = "@KH_IdAccount";
                    name_nt[1] = "@AC_Money";
                    name_nt[2] = "@AC_Status";
                    value_nt[0] = matk_tam;
                    value_nt[1] = sodu_string;
                    value_nt[2] = trangthai;

                    int abc = cnNT.Update("ds_NopTien_Update", name_nt, value_nt, thamso_nt);
                    if (abc > 0)
                    {
                        MessageBox.Show("Thành công", "Thông báo", MessageBoxButtons.OK);
                        txtSodu.Text = sodu_string ;
                        ucNapTien_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Thất bại", "Thông báo", MessageBoxButtons.OK);
                    }
                }               
            }
           else
           {
               MessageBox.Show("Bạn chưa nhập số tiền cần nạp !!!", "Thông báo", MessageBoxButtons.OK);
           }
        }

        //--------------------CODE BUTTON REFRESH-------------------------//
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            txtMatk.Text = "";
            txtSodu.Text = "";
            txtTienNop.Text = "";
            ucNapTien_Load(sender, e);
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
