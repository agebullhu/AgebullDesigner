using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
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
        /// <typeparam name="TEditor">��չ����</typeparam>
        /// <typeparam name="TConfig">��Ӧ������</typeparam>
        /// <param name="name">��չ����</param>
        /// <param name="filter">�����Ĺ���ģʽ</param>
        public static void Registe<TConfig, TEditor>(string name, params string[] filter)
            where TEditor : UserControl, new()
            where TConfig : ConfigBase
        {
            Registe<TConfig, TEditor>(name, -1, filter);
        }

        /// <summary>
        /// ע����չ
        /// </summary>
        /// <typeparam name="TEditor">��չ����</typeparam>
        /// <typeparam name="TConfig">��Ӧ������</typeparam>
        /// <param name="name">��չ����</param>
        /// <param name="index">��ʾ˳��</param>
        /// <param name="filter">�����Ĺ���ģʽ</param>
        public static void Registe<TConfig, TEditor>(string name, int index, params string[] filter)
            where TEditor : UserControl, new()
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
                    Create = () => new TEditor()
                };
            else
                exts.Add(name, new ExtendViewOption
                {
                    Index = index,
                    Name = name,
                    Caption = name,
                    Create = () => new TEditor()
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
        public Func<UserControl> Create { get; set; }
    }
}