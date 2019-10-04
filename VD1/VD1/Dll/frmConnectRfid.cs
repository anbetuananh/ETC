using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

// Thư viện Ilist trong SQL
using System.Configuration;
using System.Collections;


// Thư viện SerialPort
using System.IO;
using System.IO.Ports;
using System.Xml;

// Bắt đầu code
namespace VD1
{
    public partial class frmConnectRfid : Form
    {
        //--------------------KHAI BAO BIEN-------------------------//
        SerialPort P = new SerialPort();            
        string InputData = String.Empty;            
        delegate void SetTextCallback(string text); // Khai bao delegate SetTextCallBack voi tham so string
        public frmConnectRfid()
        {
            InitializeComponent();
            // Cài đặt các thông số cho COM
            string[] ports = SerialPort.GetPortNames();
            cbCom.Items.AddRange(ports); // Sử dụng AddRange thay vì dùng foreach
            P.ReadTimeout = 1000;
            P.DataReceived += new SerialDataReceivedEventHandler(DataReceive);

            // Cài đặt cho BaudRate
            string[] BaudRate = { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" };
            cbRate.Items.AddRange(BaudRate);

            // Cài đặt cho DataBits
            string[] Databits = { "6", "7", "8" };
            cbBits.Items.AddRange(Databits);

            //Cài đặt cho Parity
            string[] Parity = { "None", "Odd", "Even" };
            cbParity.Items.AddRange(Parity);

            //Cài đặt cho Stop bit
            string[] stopbit = { "1", "1.5", "2" };
            cbBit.Items.AddRange(stopbit);
        }

        //--------------------CODE CHON PORTNAME-------------------------//
        private void cbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();                              
            }
            P.PortName = cbCom.SelectedItem.ToString(); // Gán PortName bằng COM đã chọn 
        }

        //--------------------CODE CHON BAUDRATE-------------------------//
        private void cbRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();
            }
            P.BaudRate = Convert.ToInt32(cbRate.Text);
        }

        private void cbBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();
            }
            P.DataBits = Convert.ToInt32(cbBits.Text);
        }

        //--------------------CODE BUTTON CHON PARITY-------------------------//
        private void cbParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();
            }
            switch (cbParity.SelectedItem.ToString())
            {
                case "Odd":
                    P.Parity = Parity.Odd;
                    break;
                case "None":
                    P.Parity = Parity.None;
                    break;
                case "Even":
                    P.Parity = Parity.Even;
                    break;
            }
        }

        //--------------------CODE CHON STOP BIT-------------------------//
        private void cbBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();
            }
            switch (cbBit.SelectedItem.ToString())
            {
                case "1":
                    P.StopBits = StopBits.One;
                    break;
                case "1.5":
                    P.StopBits = StopBits.OnePointFive;
                    break;
                case "2":
                    P.StopBits = StopBits.Two;
                    break;
            }
        }
   
        //--------------------HAM NHAN DU LIEU-------------------------//
        // Hàm này được sự kiện nhận dữ liệu gọi đến -> để hiển thị
        private void DataReceive(object obj, SerialDataReceivedEventArgs e)
        {
            InputData = P.ReadExisting();
            Thread.Sleep(500);
            
            if (InputData != String.Empty)
            {
                SetText(InputData);
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

        //--------------------FORM LOAD-------------------------//
        public void frmConnectRfid_Load(object sender, EventArgs e) // Mặc định được gọi khi mở chương trình.
        {
            cbCom.SelectedIndex = 0;
            cbRate.SelectedIndex = 3;       // 9600
            cbBits.SelectedIndex = 2;       // 8
            cbParity.SelectedIndex = 0;     // None
            cbBit.SelectedIndex = 0;        // None

            this.AcceptButton = btKetNoi;

            // Hiện thị Status
            status.Text = "Hãy chọn 1 cổng COM để kết nối.";
        }
 
        //--------------------CODE BUTTON SEND-------------------------//
        private void btSend_Click(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                if (txtSend.Text == "") MessageBox.Show("Chưa có dữ liệu!", "Thông Báo");
                else P.Write(txtSend.Text);
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
        private void txtSend_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtkq_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
