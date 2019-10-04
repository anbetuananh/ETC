using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VD1
{
    public partial class ucDanhsachKH : UserControl
    {
        ClsConnect cnKH = new ClsConnect();
        public ucDanhsachKH()
        {
            InitializeComponent();
        }

        //--------------------FORM LOAD-------------------------//
        private void ucDanhsachKH_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = Cls_ThaoTac.LayKh();
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
