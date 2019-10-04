using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace VD1
{
    public class CarInfo
    {
        public CarInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        string maxe;
        string bienso;
        string nhanhieu;
        string soloai;
        string namsx;
        string sokhung;
        string somay;
        DateTime ngaydk;
        string loaixe;
        string maRfid;

        public string Maxe
        {
            set { maxe = value; }
            get { return maxe; }
        }
        public string Bienso
        {
            set { bienso = value; }
            get { return bienso; }
        }
        public string Nhanhieu
        {
            set { nhanhieu = value; }
            get { return nhanhieu; }
        }
        public string Soloai
        {
            set { soloai = value; }
            get { return soloai; }
        }
        public string Namsx
        {
            set { namsx = value; }
            get { return namsx; }
        }
        public string Sokhung
        {
            set { sokhung = value; }
            get { return sokhung; }
        }
        public string Somay
        {
            set { somay = value; }
            get { return somay; }
        }
        public DateTime Ngaydk
        {
            set { ngaydk = value; }
            get { return ngaydk; }
        }
        public string Loaixe
        {
            set { loaixe = value; }
            get { return loaixe; }
        }
        public string MaRfid
        {
            set { maRfid = value; }
            get { return maRfid; }
        }
    }
}
