using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace COAST{
    class Sql {
        private string _voucherNumber;

        readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["McVIPConnectionString"].ConnectionString);
        readonly SqlCommand _findStatusCmd;
        private  SqlCommand _findReceiveDateCmd;
        readonly SqlCommand _updateCmd;

        public Sql(string voucherNumber)
        {
            _voucherNumber = voucherNumber;
            _findStatusCmd = new SqlCommand("select status from PsuAction where vouchernumber = 'myvouchernumber'", conn);
            _findReceiveDateCmd = new SqlCommand("select datereceived from PsuAction where vouchernumber = 'myvouchernumber'", conn);
            _updateCmd = new SqlCommand("Update  PsuAction set status = 'Paid' where vouchernumber = 'myvouchernumber'",conn);
        }

        public Voucher GetVoucherFromNumber(string voucherNumber)
        {
            conn.Open();
            _findStatusCmd.CommandText = _findStatusCmd.CommandText.Replace("myvouchernumber", voucherNumber);
            _findReceiveDateCmd.CommandText = _findReceiveDateCmd.CommandText.Replace("myvouchernumber", voucherNumber);

            var voucher = new Voucher
                {
                    VoucherStatus = _findStatusCmd.ExecuteScalar().ToString(),
                    VoucherReceiveDate = (DateTime) _findReceiveDateCmd.ExecuteScalar()
                };

            return voucher;
        }

        public int UpdateVoucherPaidDate(string voucherNumber) {
            if (voucherNumber == "Not Found") return 0;

            conn.Open();
            _updateCmd.CommandText = _updateCmd.CommandText.Replace("myvouchernumber", voucherNumber);

            var rows = _updateCmd.ExecuteNonQuery();
            if (rows == 0) {
                return 0;
            }

            return rows;
        }
     
    }
 
}
