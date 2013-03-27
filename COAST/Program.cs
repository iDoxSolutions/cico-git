using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using log4net;
using OpenPop.Mime.Header;
using DataAccess;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace COAST
{
    internal class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main()
        {
            var sourcePath = ConfigurationManager.AppSettings["sourcePath"];
            log.Info("sourcePath = " + sourcePath);
            var destPath = ConfigurationManager.AppSettings["destPath"];
            log.Info("destPath = " + destPath);
            string sourceFile;

            try
            {

                log.Info("Starting COAST Voucher Processing Program");
                var voucher = new Voucher();

                if (System.IO.Directory.Exists(sourcePath))
                {
                    string[] files = System.IO.Directory.GetFiles(sourcePath);
                   
                    // Copy the files and overwrite destination files if they already exist. 
                    foreach (string s in files)
                    {
                        sourceFile = System.IO.Path.GetFileName(s);
                        if (string.IsNullOrEmpty(sourceFile))
                        {
                            log.Debug("No files were found");
                            Console.WriteLine("No files were found");
                            Environment.Exit(0);
                        }

                        var lines = System.IO.File.ReadAllLines(System.IO.Path.Combine(sourcePath, sourceFile));


                        foreach (string line in lines)
                        {


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
                                    if (voucher.UpdatePaidVoucher(voucherNum) == 1)
                                    {
                                        var responder = new Responder();
                                        responder.Send(voucher, dateResult);


                                        log.Debug("Voucher Status: " + voucher.VoucherStatus);

                                        log.Info("Updated voucher: " + voucher.VoucherNumber + "date: " + dateResult);
                                    }
                                    else
                                    {
                                        log.Info("Could not update voucher: " + voucher.VoucherNumber + "date: " +
                                                 dateResult);
                                    }

                                }
                                else
                                {
                                    log.Info("Voucher status is not 'Payment': " + voucher.VoucherStatus);
                                }

                            }
                            else
                            {
                                log.Info("Bad Date: Extracted voucher: " + voucher.VoucherNumber + "date: " + dateResult);
                            }



                            log.Info("Extracted voucher: " + voucherNum + "date: " + dateResult);

                        }
                        if (string.IsNullOrEmpty(sourceFile)) 
                        {
                            var targetFile = System.IO.Path.Combine(sourcePath, sourceFile);
                            log.Info("targetFile: " + targetFile);
                            var destFile = System.IO.Path.Combine(destPath, sourceFile);
                            log.Info("destFile: " + destFile);

                            // To copy a folder's contents to a new location: 
                            // Create a new target folder, if necessary. 
                            if (!System.IO.Directory.Exists(destPath)) {
                                System.IO.Directory.CreateDirectory(destPath);
                            }

                            // To copy a file to another location and  
                            // overwrite the destination file if it already exists.
                            System.IO.File.Copy(targetFile, destFile, true);
                            System.IO.File.Delete(targetFile);
                        }
                    }
                  
                }
                else
                {
                    log.Info("Source path does not exist: " + sourcePath);
                }



                

                log.Info("Stopping COAST Voucher Processing Program");
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                log.Info(ex.InnerException);
                Console.WriteLine("Caught an exception");
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException);


            }
        }

    }
}

