using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

// Thư viện SerialPort
using System.IO;
using System.IO.Ports;
using System.Xml;

// Bắt đầu code
namespace VD
{
    public partial class frmRfid : Form
    {

        //--------------------KHAI BAO BIEN-------------------------//
        SerialPort P = new SerialPort();            // Khai báo 1 Object SerialPort mới.
        string InputData = String.Empty;            // Khai báo string buff dùng cho hiển thị dữ liệu sau này.
        delegate void SetTextCallback(string text); // Khai bao delegate SetTextCallBack voi tham so string


        public frmRfid()
        {
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
            cbCom.Items.AddRange(ports); // Sử dụng AddRange thay vì dùng foreach
            P.ReadTimeout = 1000;
            P.DataReceived += new SerialDataReceivedEventHandler(DataReceive);   
        }

        //--------------------FORM LOAD-------------------------//
        private void frmRfid_Load(object sender, EventArgs e) // Mặc định được gọi khi mở chương trình.
        {
            cbCom.SelectedIndex = 0;
            P.BaudRate = Convert.ToInt32("9600");
            P.DataBits = Convert.ToInt32("8");
            P.StopBits = StopBits.One;
            P.Parity = Parity.None;

            this.AcceptButton = btKetNoi;

            txtSend.Text = txtMaRfid;

            // Hiện thị Status
            status.Text = "Hãy chọn 1 cổng COM để kết nối.";
        }

        //--------------------HAM CHON CONG COM-------------------------//
        private void cbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();                              // Nếu đang mở Port thì phải đóng lại
            }
            P.PortName = cbCom.SelectedItem.ToString(); // Gán PortName bằng COM đã chọn 
        }

        //--------------------HAM NHAN DU LIEU DEN-------------------------//
        private void DataReceive(object obj, SerialDataReceivedEventArgs e)
        {
            InputData = P.ReadExisting();
            if (InputData != String.Empty)
            {
                SetText(InputData); // Chính vì vậy phải sử dụng ủy quyền tại đây. Gọi delegate đã khai báo trước đó.
            }
        }

        private void SetText(string text)
        {
            if (this.txtkq.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText); // khởi tạo 1 delegate mới gọi đến SetText
                this.Invoke(d, new object[] { text });
            }
            else this.txtkq.Text += text;
        }

        //--------------------BIEN TAM-------------------------//
        public string txtMaRfid = "";

        //--------------------CODE BUTTON SEND-------------------------//
        private void btSend_Click(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                if (txtSend.Text == "") MessageBox.Show("Chưa có dữ liệu!", "Thông Báo");
                else
                {
                    P.Close();
                    txtMaRfid = txtSend.Text;
                }
            }
            else MessageBox.Show("COM chưa mở.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtSend.Clear();
        }
        
        //--------------------CODE BUTTON KET NOI-------------------------//
        private void btKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                P.Open();
                btNgat.Enabled = true;
                btKetNoi.Enabled = false;

                // Hiện thị Status
                status.Text = "Đang kết nối với cổng " + cbCom.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được.", "Thử lại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------CODE BUTTON NGAT-------------------------//
        private void btNgat_Click(object sender, EventArgs e)
        {
            P.Close();
            btKetNoi.Enabled = true;
            btNgat.Enabled = false;

            // Hiện thị Status
            status.Text = "Đã Ngắt Kết Nối";
        }

        //--------------------CODE BUTTON THOAT-------------------------//
        private void btThoat_Click(object sender, EventArgs e)
        {
            P.Close();
            this.Close();
        }

        //--------------------CODE BUTTON XOA-------------------------//
        private void btXoa_Click(object sender, EventArgs e)
        {
            txtkq.Text = "";
            txtSend.Text = "";
        }

        //--------------------OTHER-------------------------//
        private void txtkq_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSend_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
