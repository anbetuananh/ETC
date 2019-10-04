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
    public partial class ucMainScreen : UserControl
    {
        //-------------------KHAI BAO BIEN-------------------------//
        ClsConnect cn = new ClsConnect();
        public ucMainScreen()
        {
            InitializeComponent();
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        //--------------------FORM LOAD-------------------------//
        private void ucMainScreen_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = Cls_ThaoTac.LayToll();
        }

        //--------------------CODE BUTTON IN-------------------------//
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        //--------------------CODE BUTTON XUAT FILE-------------------------//
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\" + "baocao.pdf";
            gridControl1.ExportToPdf(path);
        }
    }
}
