using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace VD
{
    public partial class ucThemmoiB1 : UserControl
    {
        public ucThemmoiB1()
        {
            InitializeComponent();
        }

        //--------------------KHAI BAO BIEN-------------------------//
        ClsConnect cnB1 = new ClsConnect();
        public string maso = "";
        public int makhachhang;
        public int mataikhoan;

        //--------------------FORM LOAD-------------------------//
        private void ucThemmoiB1_Load(object sender, EventArgs e)
        {
            DataTable dt = cnB1.LoadData("ds_KhachHang_Load");
            gridControl1.DataSource = dt;
        }

        //--------------------CODE BUTTON NEXT-------------------------//
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            kiemtra();
            if (b1 == true)
            {
                if(MessageBox.Show("CONFIRM STEP 1 SUCCESSED ", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    panelControl1.Controls.Clear();
                    ucThemmoiB2 themmoi2 = new ucThemmoiB2();
                    themmoi2.txtXe_Next = txtMaxe.Text;                        // luu ma xe
                    themmoi2.txtTktam = txtMatk.Text;                          // luu ma tai khoan
                    themmoi2.Dock = System.Windows.Forms.DockStyle.Fill;
                    panelControl1.Controls.Add(themmoi2);
                }
            }
        }

        //--------------------CODE BUTTON BACK-------------------------//
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            ucThemMoi themmoi = new ucThemMoi();
            themmoi.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(themmoi);
        }

        //--------------------CODE TRO THAM SO GRIDVIEW-------------------------//
        private void gridControl1_Click(object sender, EventArgs e)
        {
            maso = gridView1.GetFocusedDisplayText();
            txtMaKh.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn1);
            txtHoten.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn2);
            txtNgaysinh.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn3);
            txtCmnd.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn4);
            txtDienthoai.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn5);
            txtMail.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn6);
            txtDiachi.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn7);
            txtMaxe.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn8);
            txtMatk.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn9);
        }

        //--------------------CODE KIEM TRA DA NHAP DU LIEU HAY CHUA-------------------------//
        private Boolean b1;
        private bool kiemtra()
        {
            b1 = true;

            if (txtMaKh.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK);
                b1 = false;
            }
            if (txtHoten.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập họ tên", "Thông báo", MessageBoxButtons.OK);
                b1 = false;
            }
            if (txtCmnd.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập chứng minh nhân dân", "Thông báo", MessageBoxButtons.OK);
                b1 = false;
            }
            if (txtMaxe.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã xe", "Thông báo", MessageBoxButtons.OK);
                b1 = false;
            } 
            if (txtMatk.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã tài khoản", "Thông báo", MessageBoxButtons.OK);
                b1 = false;
            }
            return b1;
        }

        //--------------------CODE KIEM TRA NHAP TRUNG DU LIEU-------------------------//
        public Boolean trung_du_lieu;
        public bool kiemtra_tdl()
        {
            trung_du_lieu = false;

            DataTable dt2 = new DataTable();
            dt2 = cnB1.LoadData("ds_KhachHang_Load");

            for (int i = 0; i < dt2.Rows.Count; i++ )
                {
                    if ((dt2.Rows[i][0].ToString()).Contains(txtMaKh.Text))
                    {
                        trung_du_lieu = true;
                        break;
                    }
                }
            return trung_du_lieu;
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
                if (b1 == true)
                {
                    //insert
                    int thamso = 9;
                    string[] name = new string[thamso];
                    string[] value = new string[thamso];
                    name[0] = "@KH_Id";
                    name[1] = "@KH_Name";
                    name[2] = "@KH_Dob";
                    name[3] = "@KH_Cmnd";
                    name[4] = "@KH_Phone";
                    name[5] = "@KH_Mail";
                    name[6] = "@KH_Address";
                    name[7] = "@KH_IdCar";
                    name[8] = "@KH_IdAccount";

                    value[0] = txtMaKh.Text.Trim();
                    value[1] = txtHoten.Text.Trim();
                    value[2] = txtNgaysinh.Text.Trim();
                    value[3] = txtCmnd.Text.Trim();
                    value[4] = txtDienthoai.Text.Trim();
                    value[5] = txtMail.Text.Trim();
                    value[6] = txtDiachi.Text.Trim();
                    value[7] = txtMaxe.Text.Trim();
                    value[8] = txtMatk.Text.Trim();

                    int kt = cnB1.Update("ds_ThemmoiB1_Insert", name, value, thamso);
                    if (kt > 0)
                    {
                        MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Lưu chưa thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    ucThemmoiB1_Load(sender, e);
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
                name[0] = "@KH_Id";
                value[0] = maso;

                int kt = cnB1.Update("ds_ThemmoiB1_Delete", name, value, thamso);
                if (kt > 0)
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Xóa chưa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                ucThemmoiB1_Load(sender, e);
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
                int thamso = 9;
                string[] name = new string[thamso];
                string[] value = new string[thamso];
                name[0] = "@KH_Id";
                name[1] = "@KH_Name";
                name[2] = "@KH_Dob";
                name[3] = "@KH_Cmnd";
                name[4] = "@KH_Phone";
                name[5] = "@KH_Mail";
                name[6] = "@KH_Address";
                name[7] = "@KH_IdCar";
                name[8] = "@KH_IdAccount";
                value[0] = maso;
                value[1] = txtHoten.Text.Trim();
                value[2] = txtNgaysinh.Text.Trim();
                value[3] = txtCmnd.Text.Trim();
                value[4] = txtDienthoai.Text.Trim();
                value[5] = txtMail.Text.Trim();
                value[6] = txtDiachi.Text.Trim();
                value[7] = txtMaxe.Text.Trim();
                value[8] = txtMatk.Text.Trim();

                int kt = cnB1.Update("ds_ThemmoiB1_Update", name, value, thamso);
                if (kt > 0)
                {
                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Sửa chưa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                ucThemmoiB1_Load(sender, e);
            }
        }

        //--------------------CODE BUTTON REFRESH-------------------------//
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1 = cnB1.LoadData("ds_KhachHang_Load");

            // Tao ma xe ngau nhien luc thiet lap tai khoan moi
            Random rd = new Random();
            txtMaxe.Text = rd.Next(1, 1000).ToString();

            // Lay ma khach hang tiep theo thu tu tang dan
            makhachhang = dt1.Rows.Count;
            makhachhang = makhachhang + 1;
            txtMaKh.Text = makhachhang.ToString();

            // Lay ma tai khoan tiep theo thu tu tang dan
            mataikhoan = dt1.Rows.Count;
            mataikhoan = mataikhoan + 1;
            txtMatk.Text = mataikhoan.ToString();

            // Reset cac thong tin khac
            txtHoten.Text = "";
            txtNgaysinh.Text = "";
            txtCmnd.Text = "";
            txtDienthoai.Text = "";
            txtMail.Text = "";
            txtDiachi.Text = "";
        }

        //--------------------CODE CHON TEXT-------------------------//
        private void txtMaxe_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}
