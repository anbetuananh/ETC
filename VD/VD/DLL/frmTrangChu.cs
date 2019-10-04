using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace VD
{
    public partial class frmTrangChu : DevExpress.XtraEditors.XtraForm
    {
        //--------------------KHAI BAO BIEN-------------------------//
        ClsConnect cls = new ClsConnect();

        public frmTrangChu()
        {
            InitializeComponent();
        }

        //--------------------FORM LOAD-------------------------//
        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            ucManhinhchinh manhinh = new ucManhinhchinh();
            manhinh.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(manhinh);
        }

        //--------------------CODE BUTTON DANH SACH KHACH HANG-------------------------//
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl1.Controls.Clear();
            ucDanhsachKH danhsachKH = new ucDanhsachKH();
            danhsachKH.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(danhsachKH);
        }

        //--------------------CODE BUTTON DANH SACH XE-------------------------//
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl1.Controls.Clear();
            ucDanhsachXe danhsachXe  = new ucDanhsachXe();
            danhsachXe.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(danhsachXe);
        }

        //--------------------CODE BUTTON DANH SACH TAI KHOAN-------------------------//
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl1.Controls.Clear();
            ucDanhsachTK danhsachTK = new ucDanhsachTK();
            danhsachTK.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(danhsachTK);
        }

        //--------------------CODE BUTTON THONG TIN CHINH-------------------------//
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTrangChu_Load( sender, e);
        }

        //--------------------CODE BUTTON BACK-------------------------//
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        //--------------------CODE BUTTON THEM MOI-------------------------//
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl1.Controls.Clear();
            ucThemMoi themmoi = new ucThemMoi();
            themmoi.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(themmoi);
        }

        //--------------------CODE BUTTON SUA CHUA-------------------------//
        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl1.Controls.Clear();
            ucChange change = new ucChange();
            change.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(change);
        }

        //--------------------CODE BUTTON LICH-------------------------//
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl1.Controls.Clear();
            ucCalendar calendar = new ucCalendar();
            calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(calendar);
        }

        //--------------------CODE BUTTON TIM KIEM-------------------------//
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl1.Controls.Clear();
            ucTimKiem TimKiem = new ucTimKiem();
            TimKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(TimKiem);
        }

        //--------------------CODE BUTTON NẠP TIỀN VÀO TÀI KHOẢN-------------------------//
        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl1.Controls.Clear();
            ucNapTien NapTien = new ucNapTien();
            NapTien.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(NapTien);
        }   

        //--------------------CODE BUTTON ABOUT US-------------------------//
        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl1.Controls.Clear();
            ucAboutUs aboutus = new ucAboutUs();
            aboutus.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(aboutus);
        }

        //--------------------CODE BUTTON HELP-------------------------//
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmHuongDan _frmHuongdan = new frmHuongDan();
            _frmHuongdan.Show();
        }

        //--------------------CODE BUTTON EXIT-------------------------//
        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn thực sự muốn thoát khỏi chương trình ?", "Tuan Anh", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                MessageBox.Show("Cảm ơn bạn đã sử dụng chương trình", "Tuan Anh");
                this.Close();
            }
        }

        //--------------------CODE BUTTON BAO CAO XE MOI DANG KY-------------------------//
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rptDangKyMoi dangky = new rptDangKyMoi();
            dangky.ShowPreview();
        }

        //--------------------CODE BUTTON THỐNG KÊ XE VỚI HẠN ĐĂNG KÝ-------------------------//
        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rptThoiHan thoihan = new rptThoiHan();
            thoihan.ShowPreview();
        }

        
    
    }
}