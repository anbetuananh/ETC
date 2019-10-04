using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace VD
{
    public partial class frmLogin : Form
    {
        private SqlConnection con;
        private DataTable dtManager = new DataTable("tblManager");
        private SqlDataAdapter da = new SqlDataAdapter();

        // Hàm kết nối với SQL
        private void connect()
        {
            String cn = @"Data Source=TUANANH-PC\TUANANH;Initial Catalog=ETCManager;Persist Security Info=True;User ID=sa;Password=trinhanh";
            try
            {
                con = new SqlConnection(cn);
                con.Open(); // mo ket noi
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối cơ sở dữ liệu!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm ngắt kết nối với SQL
        private void disconnect()
        {
            con.Close();    // dong ket noi
            con.Dispose();  // giai phong tai nguyen
            con = null;     // huy  doi tuong
        }


        public frmLogin()
        {
            InitializeComponent();
        }

        //--------------------FORM LOAD-------------------------//
        private void frmLogin_load(object sender, EventArgs e)
        {
            connect();
            // Mặc định lúc đăng nhập là manager
            txtManv.Text = "1";
            txtTennv.Text = "Nguyễn Tuấn Anh";
            this.AcceptButton = simpleButton1;
        }

        //--------------------CODE BUTTON LOGIN-------------------------//
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = @"SELECT *FROM tblManager 
                                  WHERE (NV_Id = @NV_Id) and (NV_Name = @NV_Name) and (NV_PassWord = @NV_PassWord)";
            command.Parameters.Add("@NV_Id", SqlDbType.NChar, 10).Value = txtManv.Text;
            command.Parameters.Add("@NV_Name", SqlDbType.NVarChar, 50).Value = txtTennv.Text;
            command.Parameters.Add("@NV_PassWord", SqlDbType.NVarChar, 50).Value = txtMknv.Text;

            da.SelectCommand = command;
            da.Fill(dtManager);

            if (dtManager.Rows.Count > 0)
            {
                MessageBox.Show("LOGIN SUCCESSED", "Thông báo", MessageBoxButtons.OK);
                frmTrangChu _frmTrangChu = new frmTrangChu();
                _frmTrangChu.Show();
                Hide();
            }
            else
            {
                if (MessageBox.Show("Đăng nhập thất bại !!! Bạn có muốn đăng nhập lại hay không ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    txtTennv.Focus();
                }
                else
                {
                    Close();
                    System.Windows.Forms.Application.Exit();
                }
            }
        }

        //--------------------CODE BUTTON EXIT-------------------------//
        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
                System.Windows.Forms.Application.Exit();
            }
        }

        //--------------------CODE TEXT EXCHANGED-------------------------//
        private void txtManv_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTennv_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMknv_TextChanged(object sender, EventArgs e)
        {

        }

         
    }
}
