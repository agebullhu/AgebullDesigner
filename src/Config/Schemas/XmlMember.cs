using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// 从程序集文档读取的注释信息
    /// </summary>
    public class XmlMember
    {
        /// <summary>
        /// 载入
        /// </summary>
        /// <returns></returns>
        public static List<XmlMember> Load(string path)
        {
            if (!File.Exists(path))
                return null;
            XElement xRoot = XElement.Load(path);
            var xElement = xRoot.Element("members");
            if (xElement == null)
            {
                return null;
            }
            List<XmlMember> members = (from p in xElement.Elements("member")
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
                }).ToList();
            return members;
        }

        private string _remark;

        private string _summary;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 上级
        /// </summary>
        public string Parent
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName
        {
            get;
            set;
        }
        /// <summary>
        /// 摘要
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
        /// 参见
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