using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD1
{
    public class ViolationInfo
    {
        public ViolationInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        int VL_Stt;
        string KH_IdCar;
        string CA_LicensePlate;
        DateTime TL_Time;
        string VL_Specy;
        string VL_Fee;
        int VL_Number;

        public int vL_Stt
        {
            set { VL_Stt = value; }
            get { return VL_Stt; }
        }
        public DateTime tL_Time
        {
            set { TL_Time = value; }
            get { return TL_Time; }
        }
        public string kH_IdCar
        {
            set { KH_IdCar = value; }
            get { return KH_IdCar; }
        }
        public string cA_LicensePlate
        {
            set { CA_LicensePlate = value; }
            get { return CA_LicensePlate; }
        }
        public string vL_Specy
        {
            set { VL_Specy = value; }
            get { return VL_Specy; }
        }
        public string vL_Fee
        {
            set { VL_Fee = value; }
            get { return VL_Fee; }
        }
        public int vL_Number
        {
            set { VL_Number = value; }
            get { return VL_Number; }
        }

    }
}
