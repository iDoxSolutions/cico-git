using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                            System.Reflection.Assembly.GetAssembly(typeof (AssemblyCache)).Location, null,
                            AssemblyCommitFlags.Default);
                        Console.WriteLine("Assembly successfully instealled");
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
