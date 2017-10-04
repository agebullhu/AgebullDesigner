using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using Agebull.Common;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 全局对象注册器MEF自动处理类
    /// </summary>
    public class RegisterImporter : IAutoRegister
    {
        [ImportMany(typeof(IAutoRegister))]
        public IEnumerable<IAutoRegister> Registers;

        /// <summary>
        /// 执行自动注册
        /// </summary>
        public void AutoRegist()
        {
            foreach (var reg in Registers)
                reg.AutoRegist();
        }
        /// <summary>  
        ///构造 
        /// </summary>  
        public RegisterImporter()
        {
            var root = Path.GetDirectoryName(Path.GetDirectoryName(GetType().Assembly.Location));
            var path = Path.Combine(root, "AddIn");
            var bin = Path.Combine(path, "Bin");
            var runtime = Path.Combine(path, "Runtime");
            IOHelper.DeleteDirectory(runtime);
            IOHelper.CheckPath(runtime);
            var files = File.ReadAllText(Path.Combine(path, "config.txt")).Split(new[] { '\r', '\n' },StringSplitOptions.RemoveEmptyEntries);

            int index = 1;
            foreach (var file in files)
            {
                if (file[0] == '*')
                    continue;
                if (File.Exists(Path.Combine(bin, file)))
                    File.Copy(Path.Combine(bin, file), Path.Combine(runtime, $"{index:D3}.{file}"), true);
                var pdb = file.ToLower().Replace(".dll", ".pdb");
                if (File.Exists(Path.Combine(bin, pdb)))
                    File.Copy(Path.Combine(bin, pdb), Path.Combine(runtime, $"{index:D3}.{pdb}"), true);
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