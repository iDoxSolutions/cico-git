using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using log4net;
using OpenPop.Mime.Header;
using DataAccess;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace COAST
{

    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main(string[] args)
        {
            var sourcePath = ConfigurationManager.AppSettings["sourcePath"];
            var destPath = ConfigurationManager.AppSettings["destPath"];
            var sourceFile = "";
            
               

            log.Info("Starting COAST Voucher Processing Program");
            var voucher = new Voucher();

            if (System.IO.Directory.Exists(sourcePath)) {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                // Copy the files and overwrite destination files if they already exist. 
                foreach (string s in files) {
                    // Use static Path methods to extract only the file name from the path.
                    sourceFile = System.IO.Path.GetFileName(s);

                    
                    var lines = System.IO.File.ReadAllLines(System.IO.Path.Combine(sourcePath, sourceFile));


                    foreach (string line in lines) {


                        var voucherNum = line.Substring(61, 11);
                        var invoiceDate = line.Substring(42, 6);

                        var invYear = "20" + invoiceDate.Substring(4, 2);
                        var invMonth = invoiceDate.Substring(2, 2);
                        var invDay = invoiceDate.Substring(0, 2);
                        var sDate = invYear + "/" + invMonth + "/" + invDay;
                        DateTime dateResult;

                        var culture = CultureInfo.CreateSpecificCulture("en-US");
                        var styles = DateTimeStyles.None;
                        if (DateTime.TryParse(sDate, culture, styles, out dateResult))
                        {
                            

                            voucher = voucher.GetVoucher(voucherNum);
                            if (voucher.VoucherStatus == "Payment")  
                            {
                                if (voucher.UpdatePaidVoucher(voucherNum) == 1) {
                                var responder = new Responder();
                                responder.Send(voucher, dateResult);


                                log.Debug("Voucher Status: " + voucher.VoucherStatus);

                                log.Info("Updated voucher: " + voucher.VoucherNumber + "date: " + dateResult);
                                }
                                else
                                {
                                    log.Info("Could not update voucher: " + voucher.VoucherNumber + "date: " + dateResult);
                                }
                                
                            }
                            else
                            {
                                log.Info("Voucher status is not 'Payment': " + voucher.VoucherStatus);
                            }
                           
                        }
                        else {
                            log.Info("Bad Date: Extracted voucher: " + voucher.VoucherNumber + "date: " + dateResult);
                        }



                        log.Info("Extracted voucher: " + voucherNum + "date: " + dateResult);

                    }
                }
            }
            else {
                log.Info("Source path does not exist: " + sourcePath);
            }


            var targetFile = System.IO.Path.Combine(sourcePath, sourceFile);
            var destFile = System.IO.Path.Combine(destPath, sourceFile);

            // To copy a folder's contents to a new location: 
            // Create a new target folder, if necessary. 
            if (!System.IO.Directory.Exists(destPath)) {
                System.IO.Directory.CreateDirectory(destPath);
            }

            // To copy a file to another location and  
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(targetFile, destFile, true);
            System.IO.File.Delete(targetFile);

            log.Info("Stopping COAST Voucher Processing Program");
        }
        
    }
   
  }
 

