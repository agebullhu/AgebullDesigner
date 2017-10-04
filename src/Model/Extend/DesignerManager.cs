using System;
using System.Collections.Generic;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��չ�����������
    /// </summary>
    public static class DesignerManager
    {

        public static readonly Dictionary<Type, Dictionary<string, Func<ExtendViewModelBase>>> ExtendDictionary = new Dictionary<Type, Dictionary<string, Func<ExtendViewModelBase>>>();

        /// <summary>
        /// ע����չ
        /// </summary>
        /// <typeparam name="TExtend">��չ����</typeparam>
        /// <typeparam name="TConfig">��Ӧ������</typeparam>
        /// <param name="name">��չ����</param>
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