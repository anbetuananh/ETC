using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

// Thư viện SerialPort
using System.IO;
using System.IO.Ports;
using System.Xml;


namespace VD
{
    public partial class ucThemmoiB2 : UserControl
    {
        //--------------------KHAI BAO BIEN-------------------------//
        ClsConnect cnB2 = new ClsConnect();
        string maso = "";
        
        public ucThemmoiB2()
        {
            InitializeComponent();
        }

        //--------------------BIEN TAM-------------------------//
        public string txtXe_Next = "";
        public string txtTktam = "";

        //--------------------FORM LOAD-------------------------//
        private void ucThemmoiB2_Load(object sender, EventArgs e)
        {
            DataTable dt = cnB2.LoadData("ds_Xe_Load");
            gridControl1.DataSource = dt;

            txtMaxe.Text = txtXe_Next;                               // hien thi ma xe
        }

        //--------------------CODE BUTTON NEXT-------------------------//
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            kiemtra();
            if (b2 == true)
            {
                if(MessageBox.Show("CONFIRM STEP 2 SUCCESSED ", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    panelControl1.Controls.Clear();
                    ucThemmoiB3 themmoi3 = new ucThemmoiB3();
                    themmoi3.txtTk = txtTktam;                                 // luu ma tai khoan
                    themmoi3.Dock = System.Windows.Forms.DockStyle.Fill;
                    panelControl1.Controls.Add(themmoi3);
                }
            }
        }

        //--------------------CODE BUTTON BACK-------------------------//
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            ucThemmoiB1 themmoi1 = new ucThemmoiB1();
            themmoi1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(themmoi1);
        }

        //--------------------CODE DISPLAY GRIDVIEW-------------------------//
        private void gridControl1_Click(object sender, EventArgs e)
        {
            maso = gridView1.GetFocusedDisplayText();
            txtMaxe.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn1);
            txtBienso.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn2);
            txtNhanhieu.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn3);
            txtSoloai.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn4);
            txtNamsx.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn5);
            txtSokhung.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn6);
            txtSomay.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn7);
            txtNgaydk.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn8);
            txtLoaixe.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn9);
            txtMaRfid.Text = gridView1.GetFocusedRowCellDisplayText(gridColumn10);
        }

        //--------------------CODE KIEM TRA DA NHAP DU LIEU HAY CHUA-------------------------//
        private Boolean b2;
        private bool kiemtra()
        {
            b2 = true;

            if (txtBienso.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập biển số", "Thông báo", MessageBoxButtons.OK);
                b2 = false;
            }
            if (txtSokhung.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số khung", "Thông báo", MessageBoxButtons.OK);
                b2 = false;
            }
            if (txtSomay.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số máy", "Thông báo", MessageBoxButtons.OK);
                b2 = false;
            }
            if (txtNgaydk.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày đăng kiểm", "Thông báo", MessageBoxButtons.OK);
                b2 = false;
            }
            if (txtLoaixe.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập loại xe", "Thông báo", MessageBoxButtons.OK);
                b2 = false;
            }
            return b2;
        }

        //--------------------CODE KIEM TRA NHAP TRUNG DU LIEU-------------------------//
        public Boolean trung_du_lieu;
        public bool kiemtra_tdl()
        {
            trung_du_lieu = false;

            DataTable dt1 = new DataTable();
            dt1 = cnB2.LoadData("ds_Xe_Load");

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if ((dt1.Rows[i][0].ToString()).Contains(txtMaxe.Text))
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
                if (b2 == true)
                {
                    //insert
                    int thamso = 10;
                    string[] name = new string[thamso];
                    string[] value = new string[thamso];
                    name[0] = "@KH_IdCar";
                    name[1] = "@CA_LicensePlate";
                    name[2] = "@CA_CarBrands";
                    name[3] = "@CA_Catelogy";
                    name[4] = "@CA_Year";
                    name[5] = "@CA_ChassisNumber";
                    name[6] = "@CA_EngineNumber";
                    name[7] = "@CA_DayRegistration";
                    name[8] = "@CA_Specy";
                    name[9] = "@CA_Rfid";

                    value[0] = txtMaxe.Text.Trim();
                    value[1] = txtBienso.Text.Trim();
                    value[2] = txtNhanhieu.Text.Trim();
                    value[3] = txtSoloai.Text.Trim();
                    value[4] = txtNamsx.Text.Trim();
                    value[5] = txtSokhung.Text.Trim();
                    value[6] = txtSomay.Text.Trim();
                    value[7] = txtNgaydk.Text.Trim();
                    value[8] = txtLoaixe.Text.Trim();
                    value[9] = txtMaRfid.Text.Trim();

                    int kt = cnB2.Update("ds_ThemmoiB2_Insert", name, value, thamso);
                    if (kt > 0)
                    {
                        MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Lưu chưa thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    ucThemmoiB2_Load(sender, e); // load lai gridview cap nhat danh sach
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
                name[0] = "@KH_IdCar";
                value[0] = maso;

                int kt = cnB2.Update("ds_ThemmoiB2_Delete", name, value, thamso);
                if (kt > 0)
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Xóa chưa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                ucThemmoiB2_Load(sender, e); // load lai gridview cap nhat danh sach
            }
        }

        //--------------------CODE BUTTON SUA-------------------------//
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn sửa không ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //update
                int thamso = 10;
                string[] name = new string[thamso];
                string[] value = new string[thamso];
                name[0] = "@KH_IdCar";
                name[1] = "@CA_LicensePlate";
                name[2] = "@CA_CarBrands";
                name[3] = "@CA_Catelogy";
                name[4] = "@CA_Year";
                name[5] = "@CA_ChassisNumber";
                name[6] = "@CA_EngineNumber";
                name[7] = "@CA_DayRegistration";
                name[8] = "@CA_Specy";
                name[9] = "@CA_Rfid";
           

                value[0] = maso;
                value[1] = txtBienso.Text.Trim();
                value[2] = txtNhanhieu.Text.Trim();
                value[3] = txtSoloai.Text.Trim();
                value[4] = txtNamsx.Text.Trim();
                value[5] = txtSokhung.Text.Trim();
                value[6] = txtSomay.Text.Trim();
                value[7] = txtNgaydk.Text.Trim();
                value[8] = txtLoaixe.Text.Trim();
                value[9] = txtMaRfid.Text.Trim();
          

                int kt = cnB2.Update("ds_ThemmoiB2_Update", name, value, thamso);
                if (kt > 0)
                {
                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Sửa chưa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                ucThemmoiB2_Load(sender, e); // load lai gridview cap nhat danh sach
            }
        }

        //--------------------CODE BUTTON IN-------------------------//
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        //--------------------CODE LOAD MA RFID-------------------------//
        private void simpleButton1_Click(object sender, EventArgs e)
        {
           frmRfid _frmRfid = new frmRfid();
            //_frmRfid.Show();
            _frmRfid.txtMaRfid = txtMaRfid.Text;
            if (_frmRfid.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtMaRfid.Text = _frmRfid.txtMaRfid;
            }
        }

        //--------------------CODE UPDATE HINH ANH CUA XE-------------------------//

        public DirectX.Capture.Filter Camera;
        public DirectX.Capture.Capture CaptureInfo;
        public DirectX.Capture.Filters CamContainer;

        Image captureImage;
        int count = 0;


        public void RefreshImage(PictureBox frame)
        {
            captureImage = frame.Image;
            try
            {
                PictureBox p1 = new PictureBox();
                p1.Size = new Size(800, 600);
                p1.Image = frame.Image;
                p1.Image.Save(@"C:\Users\TUAN ANH\Downloads\out.jpg");
            }
            catch { }
        }
        // button browser hinh anh len

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (count == 2)
            {
                this.Dispose();
            }
            count++;
            CamContainer = new DirectX.Capture.Filters();
            try
            {
                int no_of_cam = CamContainer.VideoInputDevices.Count;
                for (int i = 0; i <= no_of_cam; i++)
                {
                    try
                    {
                        // get the video input device
                        Camera = CamContainer.VideoInputDevices[i];
                        // initialize the Capture using the video input device
                        CaptureInfo = new DirectX.Capture.Capture(Camera, null);
                        // set the input video preview window
                        CaptureInfo.PreviewWindow = this.pictureBox1;
                        // Capturing complete event handler
                        CaptureInfo.FrameCaptureComplete += RefreshImage;
                        // Capture the frame from input device
                        CaptureInfo.CaptureFrame();
                        // if device found and initialize properly then exit without  
                        // checking rest of input device
                        break;
                    }
                    catch { }
                }
            }
            catch { }
        }
         
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = openfile.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openfile.FilterIndex = 1;
            openfile.RestoreDirectory = true;
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openfile.FileName;
                txtImage.Text = openfile.FileName;
            }
        }

        //--------------------CODE LUU MA RFID CHO XE-------------------------//

        private void txtMaRfid_TextChanged(object sender, EventArgs e)
        {
            
        }

        //--------------------CODE BUTTON TEST IMAGE-------------------------//
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            ImageInSQL _ImageInSQL = new ImageInSQL();
            _ImageInSQL.Show();
        }       
    }
}
