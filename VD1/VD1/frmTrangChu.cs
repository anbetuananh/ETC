using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VD1
{
    public partial class frmTrangChu : Form
    {
        public frmTrangChu()
        {
            InitializeComponent();
        }

        //--------------------KHAI BAO BIEN-------------------------//
        ClsConnect cn = new ClsConnect();

        //--------------------FORM LOAD-------------------------//
        private void frmTrangChu_Load(object sender, EventArgs e)
        {          
            panelControl4.Controls.Clear();
            ucMainScreen manhinh = new ucMainScreen();
            manhinh.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(manhinh);
        }

        //--------------------CODE BUTTON DANH SACH KHACH HANG-------------------------//
        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl4.Controls.Clear();
            ucDanhsachKH danhsachKH = new ucDanhsachKH();
            danhsachKH.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(danhsachKH);
        }

        //--------------------CODE BUTTON DANH SACH XE-------------------------//
        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl4.Controls.Clear();
            ucDanhsachXe danhsachXe = new ucDanhsachXe();
            danhsachXe.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(danhsachXe);
        }

        //--------------------CODE BUTTON DANH SACH TAI KHOAN-------------------------//
        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl4.Controls.Clear();
            ucDanhsachTK danhsachTK = new ucDanhsachTK();
            danhsachTK.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(danhsachTK);
        }

        //--------------------CODE BUTTON TEST RFID-------------------------//
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            frmConnectRfid _frmConnectRfid = new frmConnectRfid();
            _frmConnectRfid.Show();
        }

        //--------------------CODE BUTTON HOME-------------------------//
        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTrangChu_Load(sender, e);
        }
    }
}
