// Nguyễn Tuấn Anh - 41100116
// Ho Chi Minh University of Technology 
// Automation & Control Engineerings


// Thư viện chung
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
using System.Collections.Generic;
using System.Configuration;
using System.Collections;

// Thư viện SerialPort
using System.IO;
using System.IO.Ports;
using System.Xml;

// Thư viên Xtra Report
using DevExpress.XtraReports.UI;


namespace VD1
{
    public partial class frmTrangChu : Form
    {
        //--------------------KHAI BAO BIEN-------------------------//
        ClsConnect cn = new ClsConnect();           // Khai bao ket noi voi SQL

        SerialPort P = new SerialPort();            // Khai báo 1 Object SerialPort mới.
        string InputData = String.Empty;            // Khai báo string buff dùng cho hiển thị dữ liệu sau này.
        delegate void SetTextCallback(string text); // Khai bao delegate SetTextCallBack voi tham so string

        public frmTrangChu()
        {
            InitializeComponent();

            // Cấu hình Serial Port
            string[] ports = SerialPort.GetPortNames();
            cbCom.Items.AddRange(ports); // Sử dụng AddRange thay vì dùng foreach
            P.ReadTimeout = 1000;
            P.DataReceived += new SerialDataReceivedEventHandler(DataReceive); 
        }
        //--------------------FORM LOAD-------------------------//
        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            timer1.Interval = 5000;
            timer2.Interval = 1000;

            timer2.Start();

            // Cấu hình thông số cho Port lúc load form
            cbCom.SelectedIndex = 0;
            P.BaudRate = Convert.ToInt32("9600");
            P.DataBits = Convert.ToInt32("8");
            P.StopBits = StopBits.One;
            P.Parity = Parity.None;

            // Tro den button KetNou luc load form ra
            this.AcceptButton = btKetNoi;

            // Load User Control màn hình chính lên
            panelControl4.Controls.Clear();
            ucMainScreen manhinh = new ucMainScreen();
            manhinh.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(manhinh);
        }
        
        //--------------------CODE BUTTON DANH SACH KHACH HANG-------------------------//
        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            timer1.Stop();

            panelControl4.Controls.Clear();
            ucDanhsachKH danhsachKH = new ucDanhsachKH();
            danhsachKH.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(danhsachKH);
        }

        //--------------------CODE BUTTON DANH SACH XE-------------------------//
        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            timer1.Stop();

            panelControl4.Controls.Clear();
            ucDanhsachXe danhsachXe = new ucDanhsachXe();
            danhsachXe.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(danhsachXe);
        }

        //--------------------CODE BUTTON DANH SACH TAI KHOAN-------------------------//
        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            timer1.Stop();

            panelControl4.Controls.Clear();
            ucDanhsachTK danhsachTK = new ucDanhsachTK();
            danhsachTK.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(danhsachTK);
        }

        //--------------------CODE BUTTON TEST RFID-------------------------//
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            P.Close();
            frmConnectRfid _frmConnectRfid = new frmConnectRfid();
            _frmConnectRfid.Show();
        }

        //--------------------CODE BUTTON HOME-------------------------//
        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTrangChu_Load(sender, e);
            timer1.Start();
        }

        //--------------------CODE BUTTON CHECK-------------------------//
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (var sr = new StreamReader(@"D:\STUDYING\THESIS GRADUATION\Code\ANPR_ETC\ANPR\FileLicensePlate.txt"))
            {
                txtBienSo.Text = "";
                txtBienSo.Text = sr.ReadToEnd();   //Đọc toàn bộ nội dung của file
                sr.Close();
            }
            timer2.Stop();
        }

        public string bienso_ANPR = "";
        public string bienso_ANPR_tam = "";

        //--------------------CODE HAM CAP NHAT THEO TIMER 1-------------------------//
        private void timer1_Tick(object sender, EventArgs e)
        {
            frmTrangChu_Load(sender, e);         
        }

        //--------------------CODE HAM CAP NHAT THEO TIMER 2-------------------------//
        private void timer2_Tick(object sender, EventArgs e)
        {
            using (var sr = new StreamReader(@"D:\STUDYING\THESIS GRADUATION\Code\ANPR_ETC\ANPR\FileLicensePlate.txt"))
            {
                bienso_ANPR = sr.ReadToEnd();   //Đọc toàn bộ nội dung của file
                sr.Close();
            }
            // Truong hop co xe qua
            if (bienso_ANPR_tam != "")
            { 
                if (bienso_ANPR != bienso_ANPR_tam)
                {
                    kt_yesno = true;
                    bienso_ANPR_tam = bienso_ANPR;
                    txtBienSo.Text = bienso_ANPR_tam;
                    bienso_ANPR = "";
                }
                // Truong hop khong co xe qua
                else
                {
                    bienso_ANPR_tam = bienso_ANPR;
                    bienso_ANPR = "";
                }
            }
            else
            {
                bienso_ANPR_tam = bienso_ANPR;
                bienso_ANPR = "";
            }
            RFID_YesNo();
        }

        //--------------------HAM XU PHAT XE QUA TRAM KHONG CO THE-------------------------//
        public void RFID_YesNo()
        {
            if (kt_yesno == true)
            {
                if (flag == false)
                {
                    kt_yesno = false;
                    kieuvipham = 1;
                    Bienso_tam = bienso_ANPR_tam;
                    xuphat();
                }
            }
        }


//..............................................................................................................................//
        //--------------------KHAI BAO BIEN KIEM TRA RFID-------------------------//
        
        public bool flag = false;               // Co bao co the RFID hay khong ?
        public bool kt_yesno = false;           // Co kiem tra xe co the RFID hay khong?
        public bool kt = false;                 // Co kiem tra maRFID co dung hay khong?
        public string Rfid_Read;                // Bien luu ma RFID duoc quet tu tram ETC
        public int dem = 0;                     // Bien dem 2 lan in/out cua the RFID
        public string ma;                       // Bien tam 1 ma RFID 
        public string ma1;                      // Bien tam 2 ma RFID
        public string Rfid_tam1;                // Bien ma RFID tam dung de kiem tra dung sai
        public string Rfid_tam2;                // Bien ma RFID tam dung de kiem tra cap nhat du lieu lên ETC
        public string Tien_du_string = "";      // Bien tien sau khi da tru khi qua tram bang chu
        public UInt32 Tien_du_int;              // Bien tien sau khi da tru khi qua tram bang so
        public DateTime now;                    // Bien lay thoi gian ngay hom hay
        public DateTime thoihanthe;             // Bien lay thoi han cua the ra de so sanh xem con han hay khong
        public int solanvipham = 0;             // Bien dem so lan da vi pham cu khach hang
        public int kieuvipham = 0;              // Bien su dung ham CASE de xu ly tung loai vi pham
        public string Maxe_tam = "";            // Bien ma xe qua tram
        public string Bienso_tam = "";          // Bien bien so xe tu RFID
        public string Loaixe_tam = "";          // Bien loai xe
        public string Bienso_tam1 = "";
            
        //--------------------HAM CHON CONG COM-------------------------//
        private void cbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();                              // Nếu đang mở Port thì phải đóng lại
            }
            P.PortName = cbCom.SelectedItem.ToString(); // Gán PortName bằng COM đã chọn 
        }

        //--------------------CODE BUTTON KET NOI-------------------------//
        private void btKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                P.Open();
                btNgat.Enabled = true;
                btKetNoi.Enabled = false;
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
        }

        //--------------------CODE BUTTON XOA-------------------------//
        private void btXoa_Click(object sender, EventArgs e)
        {
            txtkp.Text = "";
            txtBienSo.Text = "";
        }

        //--------------------HAM NHAN DU LIEU-------------------------//
        private void DataReceive(object obj, SerialDataReceivedEventArgs e)
        {
            // chu y co bao co the RFID 2 lan la : luc in va luc out
            flag = true;             
            if (flag == true)
            {
                dem = dem + 1;
                InputData = P.ReadExisting();

                if (InputData != String.Empty)
                {
                    ma = "";
                    SetText(InputData); //Gọi delegate đã khai báo trước đó.
                    ma = InputData;
                    ma1 = ma1 + ma;     // ma1 la ma RFID duoc quet tu BOT_RFID
                }
                Thread.Sleep(100);
            }
            if (dem == 2)
            {
                execute();
                dem = 0;
                flag = false;
            }
            flag = false;
        }

        //--------------------HAM NHAN SETTEXT-------------------------//
        private void SetText(string text)
        {
            if (this.txtkp.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText); // khởi tạo 1 delegate mới gọi đến SetText
                this.Invoke(d, new object[] { text });
            }
            else this.txtkp.Text += text;
        }
        
        //--------------------HAM KIEM THUC HIEN-------------------------//
        public void execute()
        {
            RFID_check();
            capnhat();
            flag = false; 
        }

        //--------------------HAM KIEM TRA MA RFID-------------------------//
        public void RFID_check()
        {
            if (flag == true)
            {
                Rfid_Read = ma1.Trim();
                ma1 = "";

                // Lay bang RFID tu SQL ra bang DataTable
                DataTable dt = new DataTable();
                dt = Cls_ThaoTac.LayMaRfid();

                // Kiem tra co dung voi ma RFID da quet khong
                Rfid_tam1 = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Rfid_tam1 = dt.Rows[i][0].ToString().Trim();
                    Bienso_tam1 = dt.Rows[i][1].ToString().Trim();

                    // Truong hop dung
                    if ((Rfid_Read.Contains(Rfid_tam1)) && (bienso_ANPR_tam.Contains(Bienso_tam1)))
                    {
                        kt = true;
                        Rfid_Read = "";
                        break;
                    }
                }
            }
        }
       
        //--------------------HAM CAP NHAT DU LIEU LEN PHAN MEM BANG TOLL TAI TRAM ETC-------------------------//
        public void capnhat()
        {
            if (kt == true)
            {
                // Tra lai gia tri false cho kiem tra ma RFID
                kt = false;

                // Khai bao bien trong bang Toll
                TollInfo tl = new TollInfo();

                // Bien so thu tu xe qua tram trong ngay
                int sothutu;                     

                // Tao bang lay stt
                DataTable dt1 = new DataTable();
                dt1 = Cls_ThaoTac.LayStt();

                // Lay Stt tu SQL ra bang DataTable
                sothutu = dt1.Rows.Count;
                sothutu = sothutu + 1;

                // Lay thong tin CAR tu SQL ra bang DataTable
                DataTable dt2 = new DataTable();
                dt2 = Cls_ThaoTac.LayThongTin();

                // Bien tam thong tin update len Toll
                UInt32 Phi_tam_int = 0;
                string Phi_tam_string = "";

                // Kiem tra ma RFID de lay cac thong tin cung hang voi ma do ra bang cua TOLL
                Rfid_tam2 = "";
                for (int i = 0; i < dt2.Rows.Count; i++ )
                {
                    Rfid_tam2 = dt2.Rows[i][0].ToString().Trim();
                    if(Rfid_tam1 == Rfid_tam2)
                    {
                        Maxe_tam = dt2.Rows[i][1].ToString().Trim();
                        Bienso_tam = dt2.Rows[i][2].ToString().Trim();
                        Loaixe_tam =  dt2.Rows[i][3].ToString().Trim();
                        break;
                    }
                }

                // Phi cho tung loai xe luc qua tram
                if (Loaixe_tam == "Xe tải")
                {
                    Phi_tam_int = 40000;
                    Phi_tam_string = Convert.ToString(Phi_tam_int) + "Đ";
                }
                if (Loaixe_tam == "Xe khách")
                {
                    Phi_tam_int = 34000;
                    Phi_tam_string = Convert.ToString(Phi_tam_int) + "Đ";
                }
                if (Loaixe_tam == "Xe con")
                {
                    Phi_tam_int = 23000;
                    Phi_tam_string = Convert.ToString(Phi_tam_int) + "Đ";
                }

                // Lay tien trong tblAccount tu SQL ra bang DataTable
                // Tru tien trong tai khoan

                DataTable dt3 = new DataTable();
                dt3 = Cls_ThaoTac.LayTienRaTru();

                // Bien tam de tien hanh tru tien trong tai khoan
                string Maxe1_tam = "";          // Bien ma xe trong bang tai khoan va bang xe so sanh voi nha
                string Tien_tam = "";           // Bien tien con trong tai khoan lay ra tu SQL (chua tien hanh tru)

                // Tien hanh tru tien trong tai khoan
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    Maxe1_tam = dt3.Rows[i][0].ToString().Trim();
                    if (Maxe_tam == Maxe1_tam)
                    {
                        // Kiem tra thoi han cua the
                        kt_thoihan();
                        if (kt_thoihanthe ==  true)
                        {
                            kt_thoihanthe = false;
                            kieuvipham = 4;
                            MessageBox.Show("Thẻ đã hết hạn sử dụng -> Nhắc nhỡ gia hạn lại !!!", "Thông báo", MessageBoxButtons.OK);
                            solanvipham = solanvipham + 1;

                            kt_viphamquasolan();
                            if(kt_vp == true)
                            {
                                kt_vp = false;
                                xuphat();
                                break;
                            }
                        }
                        else
                        {
                            Tien_tam = dt3.Rows[i][2].ToString().Trim();

                            if (Convert.ToInt32(Tien_tam) < Phi_tam_int )
                            {
                                kieuvipham = 5;
                                MessageBox.Show("Số tiền trong tài khoản không đủ thanh toán !", "Thông báo", MessageBoxButtons.OK);
                                solanvipham = solanvipham + 1;

                                kt_viphamquasolan();
                                if(kt_vp == true)
                                {
                                    kt_vp = false;
                                    xuphat();
                                    break;
                                }
                            }
                            else
                            {                               
                                Tien_du_int = Convert.ToUInt32(Tien_tam) - Phi_tam_int;
                                Tien_du_string = Convert.ToString(Tien_du_int);
                                trangthai = "";
                                if (Tien_du_int >= 300000)
                                {
                                    trangthai = "Good";
                                }
                                if ((Tien_du_int >= 100000) && (Tien_du_int < 300000))
                                {
                                    trangthai = "Normal";
                                }
                                if (Tien_du_int <= 0)
                                {
                                    trangthai = "Bad";
                                }
                                break;                              
                            }
                        }
                    }
                }

                // Dua so tien da duoc tru vao lai SQL
                Tien();

                // Tra lai gia tri tu bien so luu vao SQL
                tl.tL_Stt = Convert.ToInt32(sothutu);
                tl.cA_LicensePlate = Convert.ToString(Bienso_tam);
                tl.cA_Specy = Convert.ToString(Loaixe_tam);
                tl.kH_IdCar = Convert.ToString(Maxe_tam);
                tl.fee = Convert.ToString(Phi_tam_string);
                //tl.tL_Image = Convert.ToString("123456"); 

                UpdateCSDL q = new UpdateCSDL();
                int abc = q.updateToll(tl);

                if (abc == 1)
                {
                    MessageBox.Show("Thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Thất bại", "Thông báo", MessageBoxButtons.OK);
                }
            }
            else
            {                   
                Maxe_tam = "";
                Bienso_tam = "";
                    
                // Sai ma the va tien hanh xu phat
                MessageBox.Show("Dùng sai thẻ", "Thông báo", MessageBoxButtons.OK);
                kieuvipham = 2;
                xuphat();
                
                // Tien hanh kiem tra bien so xe da chup
                // Xu ly vi pham bien so xe
            }
        }
        //--------------------HAM DUA SO TIEN SAU KHI DA DUOC TRU VAO LAI TAI KHOAN-------------------------//
        public string trangthai = "";
        public void Tien()
        {
             AccountInfo ac = new AccountInfo();
             UpdateCSDL p = new UpdateCSDL();
            
             p.updateMoney(Maxe_tam, trangthai,Tien_du_string);
        }

        //--------------------HAM KIEM TRA THOI HAN CUA THE RFID-------------------------//
        public bool kt_thoihanthe;
        public Boolean kt_thoihan()
        {
            kt_thoihanthe = false;

            // Gia su thoi han cua the la 1 nam
            DataTable dt4 = new DataTable();
            dt4 = Cls_ThaoTac.LayTk();

            for (int i = 0; i < dt4.Rows.Count ; i++)
            {
                now = DateTime.Now;

                // Bien ma xe 2 de lam dieu kien so sanh thoi han the
                string Maxe2_tam = "";
                Maxe2_tam = dt4.Rows[i][0].ToString();

                if (Maxe_tam == Maxe2_tam)
                {
                    thoihanthe = Convert.ToDateTime(dt4.Rows[i][6].ToString());
                    
                    // Bien tam thoi han cua the sau 1 nam
                    DateTime hethan = thoihanthe.AddYears(1);
           
                    int sosanh = DateTime.Compare(now, hethan);
                    if (sosanh < 0)
                    {
                        // The da het han su dung
                        kt_thoihanthe = true;
                        break;
                    }
                }
            }
            return kt_thoihanthe;
        }

        //--------------------HAM KIEM TRA VI PHAM CUA KHACH HANG-------------------------//
        public bool kt_vp;
        public Boolean kt_viphamquasolan()
        {
            kt_vp = false;
            if (solanvipham > 5 )
            {
                kieuvipham = 3;
                // Tien hanh xu phat
                kt_vp = true;
            }
            return kt_vp;
        }

        //--------------------HAM XU PHAM VI PHAM CUA KHACH HANG-------------------------//
        public void xuphat()
        {
            ViolationInfo vl = new ViolationInfo();

            int sothutu_vipham ;
            string Phi_vipham_string = "";
            int Phi_vipham_int = 0;

            DataTable dt5 = new DataTable();
            dt5 = Cls_ThaoTac.LayViolation();

            // Lay Stt tu SQL ra bang DataTable
            sothutu_vipham = dt5.Rows.Count;
            sothutu_vipham = sothutu_vipham + 1;

            // Bien kieu vi pham
            string kieuvipham_string = "";

            switch(kieuvipham)
                {
                    // Xe khong co the RFID -> Luu vao bang vi pham va xuat hoa don ve nha 
                    // Muc phi xu phat la: 500.000Đ
                    case 1:
                        Phi_vipham_int = 500000;
                        Phi_vipham_string = Convert.ToString(Phi_vipham_int) + "Đ";
                        kieuvipham_string = "Xe không có thẻ";
                        break;

                    // Ma the RFID khong co trong SQL -> dung sai the
                    // Muc phi xu phat la: 200.000Đ
                    case 2:
                        Phi_vipham_int = 200000;
                        Phi_vipham_string = Convert.ToString(Phi_vipham_int) + "Đ";
                        kieuvipham_string = "Dùng sai thẻ";
                        break;

                    // Xe vi pham qua so lan cho phep
                    // Muc phu xu phat la: 300.000Đ
                    case 3:
                        Phi_vipham_int = 300000;
                        Phi_vipham_string = Convert.ToString(Phi_vipham_int) + "Đ";
                        kieuvipham_string = "Quá số lần vi phạm cho phép";
                        break;
                    // The het han su dung
                    // Muc phu xu phat la: 150.000Đ
                    case 4:
                        Phi_vipham_int = 150000;
                        Phi_vipham_string = Convert.ToString(Phi_vipham_int) + "Đ";
                        kieuvipham_string = "Thẻ hết hạn sử dụng";
                        break;
                    // The khong du so tien thanh toan
                    // Muc phu xu phat la: 150.000Đ
                    case 5:
                        Phi_vipham_int = 100000;
                        Phi_vipham_string = Convert.ToString(Phi_vipham_int) + "Đ";
                        kieuvipham_string = "Thẻ không đủ số tiền thanh toán";
                        break;
                }
            
            vl.vL_Stt = Convert.ToInt32(sothutu_vipham);
            vl.kH_IdCar = Convert.ToString(Maxe_tam);
            vl.cA_LicensePlate = Convert.ToString(Bienso_tam);
            // time update automaticlly
            vl.vL_Specy = Convert.ToString(kieuvipham_string);
            vl.vL_Fee = Convert.ToString(Phi_vipham_string);
            vl.vL_Number = Convert.ToInt32(solanvipham);

            UpdateCSDL x = new UpdateCSDL();
            int ghi = x.updateViolation(vl);

            Bienso_tam = "";
            solanvipham = 0;

            if (ghi == 1)
            {
                MessageBox.Show("Xữ phạt thành công", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Xữ phạt thất bại", "Thông báo", MessageBoxButtons.OK);
            }
        }

//..............................................................................................................................//
        //--------------------CODE NARBARCONTROL LOAD XE-------------------------//
        private void Load_Xe_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer1.Stop();

            panelControl4.Controls.Clear();
            ucDanhsachXe danhsachXe = new ucDanhsachXe();
            danhsachXe.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(danhsachXe);
        }

        //--------------------CODE NARBARCONTROL LOAD KHACH HANG-------------------------//
        private void Load_KH_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer1.Stop();

            panelControl4.Controls.Clear();
            ucDanhsachKH danhsachKH = new ucDanhsachKH();
            danhsachKH.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(danhsachKH);
        }

        //--------------------CODE NARBARCONTROL LOAD TAI KHOAN-------------------------//
        private void Load_TK_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer1.Stop();

            panelControl4.Controls.Clear();
            ucDanhsachTK danhsachTK = new ucDanhsachTK();
            danhsachTK.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(danhsachTK);
        }

        //--------------------CODE NARBARCONTROL HOA DON XE VI PHAM-------------------------//
        private void XeViPham_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer1.Stop();

            panelControl4.Controls.Clear();
            ucViPham vipham = new ucViPham();
            vipham.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(vipham);
        }

        //--------------------CODE NARBARCONTROL HOA DON THANH TOAN-------------------------//
        private void HoaDon_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer1.Stop();

            panelControl4.Controls.Clear();
            ucMainScreen manhinh = new ucMainScreen();
            manhinh.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(manhinh);
        }

        //--------------------CODE NARBARCONTROL BAO CAO DOANH THU-------------------------//
        private void DoanhThu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer1.Stop();

            rptDoanhThu doanhthu = new rptDoanhThu();
            doanhthu.ShowPreview();   
        }

        //--------------------CODE NARBARCONTROL BAO CAO XE VI PHAM-------------------------//
        private void XeVP_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer1.Stop();

            rptViPham vipham = new rptViPham();
            vipham.ShowPreview();  
        }

        //--------------------CODE BUTTON EXIT-------------------------//
        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn thực sự muốn thoát khỏi chương trình ?", "Tuan Anh", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                MessageBox.Show("Cảm ơn bạn đã sử dụng chương trình", "Tuan Anh");
                this.Close();
            }
        }

        //--------------------CODE BUTTON ABOUT US-------------------------//
        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl4.Controls.Clear();
            ucAboutUs aboutus = new ucAboutUs();
            aboutus.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(aboutus);
        }

        //--------------------CODE BUTTON HELP-------------------------//
        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl4.Controls.Clear();
            ucHelp help = new ucHelp();
            help.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(help);
        }
    

        //--------------------CODE BUTTON HOA DON VI PHAM-------------------------//
        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            timer1.Stop();

            panelControl4.Controls.Clear();
            ucViPham vipham = new ucViPham();
            vipham.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(vipham);
        }
        //--------------------CODE BUTTON HOA DON THANH TOAN-------------------------//
        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            timer1.Stop();

            panelControl4.Controls.Clear();
            ucMainScreen manhinh = new ucMainScreen();
            manhinh.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl4.Controls.Add(manhinh);
        }
        //--------------------OTHER-------------------------//
        private void navBarControl4_Click(object sender, EventArgs e)
        {

        }

        private void Load_Xe_ItemChanged(object sender, EventArgs e)
        {

        }
        private void txtkp_TextChanged(object sender, EventArgs e)
        {

        }

        private void XeViPham_ItemChanged(object sender, EventArgs e)
        {

        }

    }
}
