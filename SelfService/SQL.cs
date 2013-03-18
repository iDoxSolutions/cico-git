using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SelfService{
    class Sql {
        private string _voucherNumber;

        readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["McVIPConnectionString"].ConnectionString);
        readonly SqlCommand _cmd;

        public Sql(string voucherNumber)
        {
            _voucherNumber = voucherNumber;
            _cmd = new SqlCommand("Select status from PsuAction where vouchernumber = 'myvouchernumber'",conn);
        }

        public string GetVoucherStatusFromNumber(string voucherNumber)
        {
            if (voucherNumber == "Not Found") return "Invalid Voucher Format";

            conn.Open();
            _cmd.CommandText = _cmd.CommandText.Replace("myvouchernumber", voucherNumber);

            var voucherStatus = _cmd.ExecuteScalar();
            if (voucherStatus == null)
            {
                return "Not Found";
            }

           return voucherStatus.ToString();
        }
     
    }
 
}
