using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD1
{
    public class AccountInfo
    {
        public AccountInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        string KH_IdCar;
        string KH_IdAccount;
        string AC_Name;
        string AC_PassWord;
        string AC_Type;
        string AC_Status;
        DateTime AC_Day;
        string AC_CardNumber;
        string AC_Money;

        public string kH_IdCar
        {
            set { KH_IdCar = value; }
            get { return KH_IdCar; }
        }
        public string kH_IdAccount
        {
            set { KH_IdAccount = value; }
            get { return KH_IdAccount; }
        }
        public string aC_Name
        {
            set { AC_Name = value; }
            get { return AC_Name; }
        }
        public string aC_PassWord
        {
            set { AC_PassWord = value; }
            get { return AC_PassWord; }
        }

        public string aC_Type
        {
            set { AC_Type = value; }
            get { return AC_Type; }
        }
        public string aC_Status
        {
            set { AC_Status = value; }
            get { return AC_Status; }
        }
        public DateTime aC_Day
        {
            set { AC_Day = value; }
            get { return AC_Day; }
        }
        public string aC_CardNumber
        {
            set { AC_CardNumber = value; }
            get { return AC_CardNumber; }
        }
        public string aC_Money
        {
            set { AC_Money = value; }
            get { return AC_Money; }
        }
    }
}
