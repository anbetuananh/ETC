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
    public partial class ucDanhsachTK : UserControl
    {
        public ucDanhsachTK()
        {
            InitializeComponent();
        }
        ClsConnect cnTK = new ClsConnect();

        //--------------------FORM LOAD-------------------------//
        private void ucDanhsachTK_Load(object sender, EventArgs e)
        {
            DataTable dt = cnTK.LoadData("ds_TaiKhoan_Load");
            gridControl1.DataSource = dt;
        }

        //--------------------CODE BUTTON IN-------------------------//
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        //--------------------CODE BUTTON XUAT FILE-------------------------//
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\" + "baocao.pdf";
            gridControl1.ExportToPdf(path);
        }
    }
}
