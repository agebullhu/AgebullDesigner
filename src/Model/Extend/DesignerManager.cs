using System;
using System.Collections.Generic;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展设计器管理类
    /// </summary>
    public static class DesignerManager
    {

        public static readonly Dictionary<Type, Dictionary<string, Func<ExtendViewModelBase>>> ExtendDictionary = new Dictionary<Type, Dictionary<string, Func<ExtendViewModelBase>>>();

        /// <summary>
        /// 注册扩展
        /// </summary>
        /// <typeparam name="TExtend">扩展类型</typeparam>
        /// <typeparam name="TConfig">对应的类型</typeparam>
        /// <param name="name">扩展名称</param>
        public static void Registe<TConfig, TExtend>(string name)
            where TExtend : ExtendViewModelBase, new()
            where TConfig : ConfigBase
        {
            Dictionary<string, Func<ExtendViewModelBase>> exts;
            if (ExtendDictionary.ContainsKey(typeof(TConfig)))
                exts=ExtendDictionary[typeof(TConfig)];
            else
                ExtendDictionary.Add(typeof(TConfig), exts = new Dictionary<string, Func<ExtendViewModelBase>>(StringComparer.OrdinalIgnoreCase));

            if (exts.ContainsKey(name))
                exts[name] = () => new TExtend();
            else
                exts.Add(name, () => new TExtend());
        }
    }
}