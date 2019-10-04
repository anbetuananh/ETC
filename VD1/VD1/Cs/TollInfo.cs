using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD1
{
    public class TollInfo
    {
        public TollInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        int TL_Stt;
        DateTime TL_Time;
        string KH_IdCar;
        string CA_LicensePlate;
        string CA_Specy;
        string Fee;
        string TL_Image;

        public int tL_Stt
        {
            set { TL_Stt = value; }
            get { return TL_Stt; }
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
        public string cA_Specy
        {
            set { CA_Specy = value; }
            get { return CA_Specy; }
        }
        public string tL_Image
        {
            set { TL_Image = value; }
            get { return TL_Image; }
        }
        public string fee
        {
            set { Fee = value; }
            get { return Fee; }
        }
      
    }
}
