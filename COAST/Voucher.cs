using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace COAST {
    public class Voucher
    {
        public string VoucherNumber { get; set; }
        public string VoucherStatus { get; set; }
        public DateTime VoucherReceiveDate { get; set; }


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
        public Voucher GetVoucher(string voucherNumber)
        {
            var sql = new Sql(voucherNumber);
            return sql.GetVoucherFromNumber(voucherNumber);
       }

         //Update the voucher status to Paid and the paid date to input
        public int UpdatePaidVoucher(string voucherNumber)
        {
            if (voucherNumber == "Not Found") return 0;
           
            var sql = new Sql(voucherNumber);
            return sql.UpdateVoucherPaidDate(voucherNumber);
       }
    }
    }
   
    

