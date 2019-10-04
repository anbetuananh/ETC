using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VD
{
    public partial class frmHuongDan : Form
    {
        public frmHuongDan()
        {
            InitializeComponent();
        }

        //--------------------FORM LOAD-------------------------//
        private void frmHuongDan_Load(object sender, EventArgs e)
        {

        }

        //--------------------CODE NARBAR BUTTON CAC BUOC CHINH-------------------------//
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            txtHuongDan.Text = "Có 3 bước chính đó là: \n                 Bước 1: Thêm thông tin khách hàng \n Bước 2: Thêm thông tin phương tiện \n Bước 3: Thêm thông tin tài khoản";
        }

        //--------------------CODE NARBAR BUTTON THEM MA RFID-------------------------//
        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        //--------------------CODE NARBAR BUTTON THEM HINH ANH-------------------------//
        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }
    }
}
