using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VD
{
    public partial class ImageInSQL : Form
    {
        ClsConnect cnImage = new ClsConnect();
        public ImageInSQL()
        {
            InitializeComponent();
        }

        //--------------------FORM LOAD-------------------------//
        private void ImageInSQL_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = cnImage.LoadData("ds_Image_Load");
            gridControl1.DataSource = dt;
        }

        //--------------------KHAI BAO BIEN-------------------------//
        AccessData ac=new AccessData();
        private string filename;

        //--------------------CODE BUTTON SELECT IMAGE-------------------------//
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter ="All File (*.*)|*.*|JPG Files(*.JPG)|*.JPG | GIF Files(*.GIF)|*.GIF";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dlg.FileName);
                filename = dlg.FileName;
                blFileName.Text = filename.Split('\\').Last();
            }

        }

        //--------------------CODE BUTTON SAVE TO DATABASE-------------------------//
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ac.StorePicture(filename);
                MessageBox.Show("Successful!", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        //--------------------CODE BUTTON PICTURE FIRST-------------------------//
        private void button4_Click(object sender, EventArgs e)
        {
            byte[] img = ac.RetrieveImage(1);
            MemoryStream str = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(str);
            i = 1;
            button5.Enabled = false;
            button3.Enabled = true;
            blFileName.Text = ac.FileNameOfImage(1);
        }

        //--------------------CODE BUTTON PICTURE LAST-------------------------//
        private void button6_Click(object sender, EventArgs e)
        {
            byte[] img = ac.RetrieveImage(ac.NumberImageInDB());
            MemoryStream str = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(str);
            i = ac.NumberImageInDB();
            button3.Enabled = false;
            button5.Enabled = true;
            blFileName.Text = ac.FileNameOfImage(ac.NumberImageInDB());
        }

        private int i = 1;

        //--------------------CODE BUTTON NEXT-------------------------//
        private void button3_Click(object sender, EventArgs e)
        {
            button5.Enabled = true;
            if (i < ac.NumberImageInDB()) i++;
            else button3.Enabled = false;
            byte[] img = ac.RetrieveImage(i);
            MemoryStream str = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(str);
            blFileName.Text = ac.FileNameOfImage(i);
        }

        //--------------------CODE BUTTON PREVIEW-------------------------//
        private void button5_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            if (i >1) i--;
            else button5.Enabled = false;
            byte[] img = ac.RetrieveImage(i);
            MemoryStream str = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(str);
            blFileName.Text = ac.FileNameOfImage(i);
        }

        //--------------------CODE BUTTON EXIT-------------------------//
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }
    }
}
