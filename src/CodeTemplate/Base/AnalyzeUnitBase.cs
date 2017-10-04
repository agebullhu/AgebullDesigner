using System.Runtime.Serialization;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.CodeTemplate
{
    public class AnalyzeUnitBase
    {
        #region 特性


        /// <summary>
        ///     种族(最大分类)
        /// </summary>
        [DataMember]
        public CodeItemRace ItemRace { get; set; }

        /// <summary>
        ///     家族二级分类
        /// </summary>
        [DataMember]
        public CodeItemFamily ItemFamily { get; set; }

        /// <summary>
        ///     单元类型
        /// </summary>
        [DataMember]
        public CodeItemType ItemType { get; set; }

        public void SetRace(CodeItemRace race = CodeItemRace.None, CodeItemFamily family = CodeItemFamily.None, CodeItemType type = CodeItemType.None)
        {
            ItemRace = race;
            ItemFamily = family;
            ItemType = type;
        }
        #endregion

        #region 文档位置

        public virtual string Word { get; set; }

        public int Level { get; set; } = -1;

        /// <summary>
        ///     文件中的行号
        /// </summary>
        [DataMember]
        public int Line { get; set; } = -1;

        /// <summary>
        ///     行中的列号
        /// </summary>
        [DataMember]
        public int Column { get; set; } = -1;
        
        /// <summary>
        ///     文件中的起始位置
        /// </summary>
        [DataMember]
        public int Start { get; set; } = -1;

        /// <summary>
        ///     文件中的结束位置
        /// </summary>
        [DataMember]
        public int End { get; set; } = -1;

        /// <summary>
        ///     文本长度
        /// </summary>
        [IgnoreDataMember]
        public int Lenght => Start < 0 || End < 0 ? 0 : End - Start + 1;

        #endregion

        #region 特性

        /// <summary>
        /// 上级节点
        /// </summary>
        public AnalyzeBlock Parent { get; set; }

        /// <summary>
        ///     是否为空
        /// </summary>
        public virtual bool IsEmpty { get; } = false;

        /// <summary>
        ///     是否错误
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        ///     是否补全节点
        /// </summary>
        [DataMember]
        public bool IsReplenish { get; set; }

        /// <summary>
        ///     是否块
        /// </summary>
        public bool IsBlock { get; set; }

        /// <summary>
        ///     是否锁定
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        ///     是否内容
        /// </summary>
        public bool IsContent { get; set; }

        /// <summary>
        ///     代码类型
        /// </summary>
        public TemplateUnitType UnitType { get; set; }
        #endregion

        #region 类型推展

        /// <summary>
        ///     值类型连接
        /// </summary>
        public AnalyzeUnitBase ValueLink { get; set; }


        /// <summary>
        ///     值类型
        /// </summary>
        public LuaDataTypeItem ValueType
        {
            get; set;
        }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// 到文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Word;
        }
        /// <summary>
        /// 是否基础单元
        /// </summary>
        public virtual bool IsUnit { get; set; }
        #endregion

        #region 扩展

        /// <summary>
        ///     是否需要语句
        /// </summary>
        [DataMember]
        public virtual bool NeedSentence { get; set; }

        /// <summary>
        ///     是否可以当成单词
        /// </summary>
        [DataMember]
        public virtual bool IsWord { get; set; }

        /// <summary>
        ///     组合等级
        /// </summary>
        [DataMember]
        public int JoinLevel { get; set; } = -1;

        /// <summary>
        ///     组合特性
        /// </summary>
        [DataMember]
        public JoinFeature JoinFeature { get; set; }

        /// <summary>
        ///     组合后的类型
        /// </summary>
        [DataMember]
        public JoinFeature JoinType { get; set; }

        /// <summary>
        /// 到文本
        /// </summary>
        /// <returns></returns>
        public virtual string ToCode()
        {
            return Word;
        }

        /// <summary>
        /// 到文本
        /// </summary>
        /// <returns></returns>
        public virtual void ToContent(StringBuilder builder)
        {
            builder.Append(Word);
        }
        
        #endregion
    }
}