using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// �ӳ����ĵ���ȡ��ע����Ϣ
    /// </summary>
    public class XmlMember
    {
        /// <summary>
        /// ��ȡ�İ���XML
        /// </summary>
        private static List<XmlMember> HelpXml = new List<XmlMember>();

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XmlMember Find(string name)
        {
            return HelpXml.Find(p => p.Name == name);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static XmlMember Find(Type type)
        {
            if (!Assemblys.ContainsKey(type.Assembly))
            {
                Load(type.Assembly);
            }
            return HelpXml.Find(p => p.Name == type.FullName);
        }

        private static Dictionary<Assembly, string> Assemblys = new Dictionary<Assembly, string>();
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private static void Load(Assembly assembly)
        {
            var file = Path.Combine(Path.GetDirectoryName(assembly.Location),Path.GetFileNameWithoutExtension(assembly.Location) + ".xml");
            Assemblys.Add(assembly, "");
            if (!File.Exists(file))
                return ;
            XElement xRoot = XElement.Load(file);
            var xElement = xRoot.Element("members");
            if (xElement == null)
            {
                return ;
            }
            var members = from p in xElement.Elements("member")
                let name = p.Attribute("name")
                where !string.IsNullOrEmpty(name?.Value) && name.Value[0] != 'M'
                let summary = p.Element("summary")
                let remarks = p.Element("remarks")
                let np = name.Value.Split(':', '(')
                select new XmlMember
                {
                    Type = np[0],
                    Name = np[1],
                    Remark = remarks?.Value,
                    Summary = summary?.Value.Trim()
                };
            HelpXml.AddRange(members);
        }

        private string _remark;

        private string _summary;

        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// �ϼ�
        /// </summary>
        public string Parent
        {
            get;
            set;
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// ��ʾ����
        /// </summary>
        public string DisplayName
        {
            get;
            set;
        }
        /// <summary>
        /// ժҪ
        /// </summary>
        public string Summary
        {
            get => _summary;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _summary = null;
                    return;
                }
                _summary = value.Trim().ConverToAscii();// Strings.StrConv(value.Trim().ConverToAscii(), VbStrConv.Narrow | VbStrConv.SimplifiedChinese);
                DisplayName = _summary.Split(',', ':', '-', '\r', '\n', '(')[0];
            }
        }
        /// <summary>
        /// �μ�
        /// </summary>
        public string Remark
        {
            get => _remark;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _remark = null;
                    return;
                }
                _remark = value.Trim().ConverToAscii();
            }
        }
    }
}