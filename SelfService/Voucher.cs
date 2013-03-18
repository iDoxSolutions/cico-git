using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SelfService {
    public class Voucher
    {
        public string VoucherNumber { get; set; }


        //Parse the email subject for the voucher
        public string GetVoucherNumber(string emailTitle)
        {
            string voucherNumber = null;
            var start = emailTitle.IndexOf(ConfigurationManager.AppSettings["voucherprefix"]);
            if (start == -1) return "Not Found";
            
            voucherNumber = emailTitle.Substring(start,
                            Convert.ToInt32(ConfigurationManager.AppSettings["voucherlen"]));
           
            return voucherNumber;
        }

        //Look up the voucher and return the status
        public string GetVoucherStatus(string voucherNumber)
        {
            if (voucherNumber == "Not Found") return "Invalid Voucher Format";
           
            var sql = new Sql(voucherNumber);
            return sql.GetVoucherStatusFromNumber(voucherNumber);
       }
    }
   
    
}
