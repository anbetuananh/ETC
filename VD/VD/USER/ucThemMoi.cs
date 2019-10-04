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
    public partial class ucThemMoi : UserControl
    {
        public ucThemMoi()
        {
            InitializeComponent();
        }

        //--------------------FORM LOAD-------------------------//
        private void ucThemMoi_Load(object sender, EventArgs e)
        {

        }

        //--------------------CODE BUTTON XAC NHAN-------------------------//
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            ucThemmoiB1 themmoi1 = new ucThemmoiB1();
            themmoi1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(themmoi1);
        }

        //--------------------CODE BUTTON EXIT-------------------------//
        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

       
    }
}
