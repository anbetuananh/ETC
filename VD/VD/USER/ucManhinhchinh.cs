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
    public partial class ucManhinhchinh : UserControl
    {
        ClsConnect cn = new ClsConnect();
        public ucManhinhchinh()
        {
            InitializeComponent();
        }

        private void ucManhinhchinh_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = cn.LoadData("LayThongTinGT");
            dataGridView1.DataSource = dt;
        }

    }
}
