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
    public partial class ucThemmoiB3 : UserControl
    {
        public ucThemmoiB3()
        {
            InitializeComponent();
        }

        //--------------------KHAI BAO BIEN-------------------------//
        ClsConnect cnB3 = new ClsConnect();
        string maso = "";

        //--------------------BIEN TAM-------------------------//
        public string txtTk = "";

        //--------------------FORM LOAD-------------------------//
        private void ucThemmoiB3_Load(object sender, EventArgs e)
        {
            DataTable dt = cnB3.LoadData("ds_TaiKhoan_Load");
            gridControl1.DataSource = dt;
            txtMatk.Text = txtTk;
        }

        //--------------------CODE BUTTON BACK-------------------------//
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            ucThemmoiB2 themmoi2 = new ucThemmoiB2();
            themmoi2.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(themmoi2);
        }

        //--------------------CODE BUTTON COMPLETE-------------------------//
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SUCCESSED", "Thông báo", MessageBoxButtons.OK);
            panelControl1.Controls.Clear();
            ucManhinhchinh manhinh = new ucManhinhchinh();
            manhinh.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(manhinh);
        }

        //--------------------CODE DISPLAY GRIDVIEW-------------------------//
        private void gridControl1_Click(object sender, EventArgs e)
        {
            maso = gridView1.GetFocusedDisplayText();
            txtMatk.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn1);
            txtTien.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn4);
            txtLoaitk.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn2);
            txtNgaydk.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn3);
            txtTinhTrangTk.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn5);
        }

        //--------------------CODE BUTTON LUU-------------------------//
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            kiemtra_tdl();
            if(trung_du_lieu == true)
            {
                MessageBox.Show("Bạn đã nhập trùng dữ liệu -> Mời nhập lại !!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                kiemtra();
                if (b3 == true)
                {
                //insert
                int thamso = 5;
                string[] name = new string[thamso];
                string[] value = new string[thamso];
                name[0] = "@KH_IdAccount";
                name[1] = "@AC_Type";
                name[2] = "@AC_Day";
                name[3] = "@AC_Money";
                name[4] = "@AC_Status";
           
                value[0] = txtMatk.Text.Trim();
                value[1] = txtLoaitk.Text.Trim();
                value[2] = txtNgaydk.Text.Trim();
                value[3] = txtTien.Text.Trim();
                
                if( Convert.ToInt32(txtTien.Text) <= 0)
                {
                    value[4] = "Bad";
                }
                else
                { 
                    if (Convert.ToInt32(txtTien.Text) > 50000)
                    {
                        value[4] = "Good";
                    }
                    else
                    {
                        value[4] = "Normal";
                    }
                }      

                int kt = cnB3.Update("ds_ThemmoiB3_Insert", name, value, thamso);
                if (kt > 0)
                {
                    MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Lưu chưa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                ucThemmoiB3_Load(sender, e);
             }
            }
        }

        //--------------------CODE BUTTON XOA-------------------------//
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {             
                //delete
                int thamso = 1;
                string[] name = new string[thamso];
                string[] value = new string[thamso];
                name[0] = "@KH_IdAccount";
                value[0] = maso;

                int kt = cnB3.Update("ds_ThemmoiB3_Delete", name, value, thamso);
                if (kt > 0)
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Xóa chưa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                ucThemmoiB3_Load(sender, e);
            }
        }

        //--------------------CODE BUTTON IN-------------------------//
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        //--------------------CODE BUTTON SUA-------------------------//
        private void simpleButton6_Click(object sender, EventArgs e)
        {
           if (MessageBox.Show("Bạn có chắc chắn muốn sửa không ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {             
                //update
                int thamso = 5;
                string[] name = new string[thamso];
                string[] value = new string[thamso];
                name[0] = "@KH_IdAccount";
                name[1] = "@AC_Type";
                name[2] = "@AC_Day";
                name[3] = "@AC_Money";
                name[4] = "@AC_Status";

                value[0] = maso;
                value[1] = txtLoaitk.Text.Trim();
                value[2] = txtNgaydk.Text.Trim();
                value[3] = txtTien.Text.Trim();
                value[4] = txtTinhTrangTk.Text.Trim();

                int kt = cnB3.Update("ds_ThemmoiB3_Update", name, value, thamso);
                if (kt > 0)
                {
                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Sửa chưa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                ucThemmoiB3_Load(sender, e);
            }
        }

        //--------------------CODE KIEM TRA DA NHAP DU LIEU HAY CHUA-------------------------//
        private Boolean b3;
        private bool kiemtra()
        {
            b3 = true;

            if (txtNgaydk.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày đăng ký", "Thông báo", MessageBoxButtons.OK);
                b3 = false;
            }
            if (txtLoaitk.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập loại tài khoản", "Thông báo", MessageBoxButtons.OK);
                b3 = false;
            }
            return b3;
        }

        //--------------------CODE KIEM TRA NHAP TRUNG DU LIEU-------------------------//
        public Boolean trung_du_lieu;
        public bool kiemtra_tdl()
        {
            trung_du_lieu = false;

            DataTable dt1 = new DataTable();
            dt1 = cnB3.LoadData("ds_TaiKhoan_Load");

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if ((dt1.Rows[i][0].ToString()).Contains(txtMatk.Text))
                {
                    trung_du_lieu = true;
                    break;
                }
            }
            return trung_du_lieu;
        }
    }
}
