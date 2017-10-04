using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Agebull.Common.SimpleDesign.Upgrade
{
    public class XmlMember
    {
        private string _remark;

        private string _summary;

        public string Name
        {
            get;
            set;
        }

        public string Parent
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public string Summary
        {
            get
            {
                return _summary;
            }
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

        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _remark = null;
                    return;
                }
                _remark = value.Trim().ConverToAscii();//Strings.StrConv(value.Trim().ConverToAscii(), VbStrConv.Narrow | VbStrConv.SimplifiedChinese);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<XmlMember> LoadHelpXml(string path)
        {
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
    }
}