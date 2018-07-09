using System;
using System.Collections.Generic;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��չ�����������
    /// </summary>
    public static class DesignerManager
    {
        internal static readonly Dictionary<Type, Dictionary<string, ExtendViewOption>> ExtendDictionary = new Dictionary<Type, Dictionary<string, ExtendViewOption>>();

        /// <summary>
        /// ע����չ
        /// </summary>
        /// <typeparam name="TExtend">��չ����</typeparam>
        /// <typeparam name="TConfig">��Ӧ������</typeparam>
        /// <param name="name">��չ����</param>
        /// <param name="filter">�����Ĺ���ģʽ</param>
        public static void Registe<TConfig, TExtend>(string name, params string[] filter)
            where TExtend : ExtendViewModelBase, new()
            where TConfig : ConfigBase
        {
            Registe<TConfig, TExtend>(name, -1, filter);
        }

        /// <summary>
        /// ע����չ
        /// </summary>
        /// <typeparam name="TExtend">��չ����</typeparam>
        /// <typeparam name="TConfig">��Ӧ������</typeparam>
        /// <param name="name">��չ����</param>
        /// <param name="index">��ʾ˳��</param>
        /// <param name="filter">�����Ĺ���ģʽ</param>
        public static void Registe<TConfig, TExtend>(string name, int index, params string[] filter)
            where TExtend : ExtendViewModelBase, new()
            where TConfig : ConfigBase
        {
            if (!ExtendDictionary.TryGetValue(typeof(TConfig), out var exts))
                ExtendDictionary.Add(typeof(TConfig), exts = new Dictionary<string, ExtendViewOption>(StringComparer.OrdinalIgnoreCase));

            if (exts.ContainsKey(name))
                exts[name] = new ExtendViewOption
                {
                    Index = index,
                    Name = name,
                    Caption = name,
                    Create = () => new TExtend()
                };
            else
                exts.Add(name, new ExtendViewOption
                {
                    Index = index,
                    Name = name,
                    Caption = name,
                    Create = () => new TExtend()
                });
            if (filter.Length > 0)
                exts[name].Filter.AddRange(filter.Select(p => p.ToLower()));
        }
    }

    public class ExtendViewOption : SimpleConfig
    {
        /// <summary>
        /// ��ʾ˳��
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// �����Ĺ���ģʽ
        /// </summary>
        public List<string> Filter { get; } = new List<string>();
        /// <summary>
        /// ������
        /// </summary>
        public Func<ExtendViewModelBase> Create { get; set; }
    }
}