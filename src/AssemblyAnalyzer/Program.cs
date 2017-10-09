using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agebull.Common;
using Agebull.EntityModel.Designer.AssemblyAnalyzer;
using Newtonsoft.Json;

namespace AssemblyAnalyzer
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("参数不正确");
                return 1;
            }
            var arg = args[0].Split('|');
            var fileName = arg[0];
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"文件{fileName}不存在或无法访问!");
                return 2;
            }
            var rootPath = arg[1];
            if (arg[2] == "1")
            {
                var asm = new AssemblyUpgrader
                {
                    FileName = fileName
                };
                if (!asm.Prepare())
                    return 2;
                asm.Analyze();
                asm.End();
                string json = JsonConvert.SerializeObject(asm.Config,Formatting.Indented);
                File.WriteAllText(Path.Combine(rootPath, Path.GetFileNameWithoutExtension(fileName) + ".json"), json);
                return 0;
            }
            var pf = typeof(Program).Assembly.Location;
            var dir1 = Path.GetDirectoryName(pf);
            var dir2 = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "Agebull_AssemblyAnalyzer");
            IOHelper.CopyPath(dir1, dir2);
            var prog = Path.Combine(dir2, Path.GetFileName(pf));
            var process= Process.Start(prog,$"{fileName}|{rootPath}|1");
            process.WaitForExit();
            Console.ReadKey();
            return process.ExitCode;
        }
    }
}
