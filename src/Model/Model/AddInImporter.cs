using Agebull.Common;
using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// MEF插件导入器
    /// </summary>
    public class AddInImporter
    {
        [ImportMany(typeof(IAutoRegister))] public IEnumerable<IAutoRegister> Registers;

        /// <summary>
        /// 执行自动注册
        /// </summary>
        private void AutoRegist()
        {
            GlobalTrigger.RegistTrigger<CodeGeneratorTrigger>();
            foreach (var reg in Registers)
                reg.AutoRegist();
        }

        /// <summary>
        /// 保证只执行一次的变量
        /// </summary>
        private static readonly bool _isDoit = false;
        /// <summary>
        /// 导入
        /// </summary>
        public static void Importe()
        {
            if (_isDoit)
                return;
            AddInImporter importer = new AddInImporter();
            importer.Prepare();
            importer.AutoRegist();
        }

        /// <summary>  
        ///构造 
        /// </summary>  
        private void Prepare()
        {
            var path = Path.Combine(GlobalConfig.RootPath, "AddIn");
            if (!Directory.Exists(path))
            {
                Registers = new List<IAutoRegister>();
                return;
            }
            var bin = Path.Combine(path, "Bin");
            var runtime = IOHelper.CheckPath(path, "Runtime");
            IOHelper.DeleteDirectory(runtime);
            GlobalConfig.CheckPath(runtime);
            var files = File.ReadAllText(Path.Combine(path, "config.txt")).Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int index = 1;
            foreach (var file in files)
            {
                if (file[0] == '*')
                    continue;
                try
                {
                    if (File.Exists(Path.Combine(bin, file)))
                        File.Copy(Path.Combine(bin, file), Path.Combine(runtime, $"{index:D3}.{file}"), true);
                    var pdb = file.ToLower().Replace(".dll", ".pdb");
                    if (File.Exists(Path.Combine(bin, pdb)))
                        File.Copy(Path.Combine(bin, pdb), Path.Combine(runtime, $"{index:D3}.{pdb}"), true);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e, "AddInImporter");
                }
                index++;
            }
            if (index <= 0)
                return;
            // 通过容器对象将宿主和部件组装到一起。 
            DirectoryCatalog directoryCatalog = new DirectoryCatalog(runtime);
            var container = new CompositionContainer(directoryCatalog);
            container.ComposeParts(this);
        }
    }
}