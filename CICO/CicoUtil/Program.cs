using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cico.Commons.Configuration;
using Cico.Models.Utils;

namespace CicoUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].ToUpper() == "installGac".ToUpper())
                {
                    try
                    {

                        AssemblyCache.InstallAssembly(
                            System.Reflection.Assembly.GetAssembly(typeof(CicoConfiguration)).Location, null,
                            AssemblyCommitFlags.Default);
                        Console.WriteLine("Assembly successfully instealled");
                    }
                    catch (Exception exception)
                    {
                        Console.Write(exception.Message);
                    }
                }

                if (args[0].ToUpper() == "fixgac".ToUpper())
                {
                    try
                    {
                        AssemblyCacheUninstallDisposition disp = new AssemblyCacheUninstallDisposition();
                        AssemblyCache.UninstallAssembly("CICO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=54adb21d5da4ed25",null, out disp);
                        Console.WriteLine("Assembly CICO, Version=1.0.0.0 successfully uninstalled");
                        AssemblyCache.InstallAssembly(
                            System.Reflection.Assembly.GetAssembly(typeof(CicoConfiguration)).Location, null,
                            AssemblyCommitFlags.Default);
                        Console.WriteLine("Assembly cico.commmons successfully instealled");
                    }
                    catch (Exception exception)
                    {
                        Console.Write(exception.Message);
                    }
                }
            }
        }
    }
}
