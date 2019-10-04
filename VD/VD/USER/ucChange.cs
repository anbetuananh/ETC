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
    public partial class ucChange : UserControl
    {
        public ucChange()
        {
            InitializeComponent();
        }

        private void ucChange_Load(object sender, EventArgs e)
        {

        }

        //--------------------CODE BUTTON BUOC 1-------------------------//
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            ucThemmoiB1 themmoi1 = new ucThemmoiB1();
            themmoi1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(themmoi1);
        }

        //--------------------CODE BUTTON BUOC 2-------------------------//
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            ucThemmoiB2 themmoi2 = new ucThemmoiB2();
            themmoi2.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(themmoi2);
        }

        //--------------------CODE BUTTON BUOC 3-------------------------//
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            ucThemmoiB3 themmoi3 = new ucThemmoiB3();
            themmoi3.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(themmoi3);
        }

    }
}
