// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-10-23
// 修改:2014-11-08
// *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

#endregion

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     表示一个代码的节点
    /// </summary>
    [DataContract, KnownType("GetKnownType")]
    public class CodeItem
    {
        static Type[] GetKnownType()
        {
            var type = typeof (CodeItem);
            var array= type.Assembly.GetTypes().Where(p=>p.IsSubclassOf(type)).ToArray();
            return array;
        }
        #region 分类

        /// <summary>
        ///     是否补全节点
        /// </summary>
        [DataMember]
        public bool IsReplenish
        {
            get;
            set;
        }
        
        /// <summary>
        ///     种族(最大分类)
        /// </summary>
        [DataMember]
        public CodeItemRace ItemRace
        {
            get;
            set;
        }

        /// <summary>
        ///     家族二级分类
        /// </summary>
        [DataMember]
        public CodeItemFamily ItemFamily
        {
            get;
            set;
        }

        /// <summary>
        ///     单元类型
        /// </summary>
        [DataMember]
        public CodeItemType ItemType
        {
            get;
            set;
        }

        /// <summary>
        ///     用途(具体方向)
        /// </summary>
        [DataMember]
        public CodeItemUse Use
        {
            get;
            set;
        }

        /// <summary>
        /// Member
        /// </summary>
        [IgnoreDataMember]
        public string  Member => GetType().Name;

        /// <summary>
        ///     是否空白
        /// </summary>
        public bool IsSpace => this.ItemFamily == CodeItemFamily.Space;


        /// <summary>
        ///     节点是否可为值
        /// </summary>
        [IgnoreDataMember]
        internal bool IsValue => this.ItemRace == CodeItemRace.Value;

        /// <summary>
        ///     是否为空内容
        /// </summary>
        [IgnoreDataMember]
        internal bool IsAssist => this.ItemRace == CodeItemRace.Assist;
        
        /// <summary>
        ///     是否为标点
        /// </summary>
        [IgnoreDataMember]
        public bool IsPunctuate => this.IsElement && this.ItemRace == CodeItemRace.Punctuate;

        /// <summary>
        ///     是否为普通单词
        /// </summary>
        [DataMember]
        internal bool IsWord
        {
            get;
            set;
        }

        /// <summary>
        ///     是否为计算符
        /// </summary>
        [IgnoreDataMember]
        internal bool IsCompute
        {
            get
            {
                if (!IsElement || this.ItemRace != CodeItemRace.Punctuate)
                    return false;
                switch (this.ItemFamily)
                {
                    case CodeItemFamily.Logical:
                    case CodeItemFamily.Compute:
                    case CodeItemFamily.BitCompute:
                        return true;
                }
                switch (this.ItemType)
                {
                    case CodeItemType.Key_As:
                    case CodeItemType.Key_Is:
                    case CodeItemType.Punctuate_CheckNull:
                        return true;
                }
                return false;
            }
        }


        /// <summary>
        ///     是否下标
        /// </summary>
        [IgnoreDataMember]
        internal bool IsInferiorRange => this.ItemType == CodeItemType.InferiorRange ||
                                         this.ItemType == CodeItemType.Var_Array;

        #endregion


        #region 文档位置

        /// <summary>
        ///     文件中的起始位置
        /// </summary>
        [DataMember]
        public int Start
        {
            get;
            set;
        }

        /// <summary>
        ///     文件中的结束位置
        /// </summary>
        [DataMember]
        public int End
        {
            get;
            set;
        }

        /// <summary>
        ///     文本长度
        /// </summary>
        [IgnoreDataMember]
        public int Lenght
        {
            get
            {
                return this.End - this.Start + 1;
            }
        }

        #endregion


        #region 分析辅助特性

        /// <summary>
        ///     是否已完成分析
        /// </summary>
        [IgnoreDataMember]
        public virtual AnalyzerState State
        {
            get;
            set;
        }
        /// <summary>
        ///     是否为单词
        /// </summary>
        [IgnoreDataMember]
        public virtual bool IsElement
        {
            get
            {
                return false;
            }
        }


        #endregion


        #region 序列化

        /// <summary>
        ///     开始反序列化时的处理
        /// </summary>
        [OnDeserializing]
        private void BeginDeserializing(StreamingContext context)
        {
            this.OnDeserializing(context);
        }

        /// <summary>
        ///     微量的逻辑检查(只修正基本逻辑)
        /// </summary>
        [OnSerializing]
        private void BeginSerializing(StreamingContext context)
        {
            this.OnSerializing(context);
        }

        /// <summary>
        ///     完成反序列化时的处理
        /// </summary>
        [OnDeserialized]
        private void EndDeserialized(StreamingContext context)
        {
            this.OnDeserialized(context);
        }

        /// <summary>
        ///     完成序列化的处理
        /// </summary>
        [OnSerialized]
        private void EndSerialized(StreamingContext context)
        {
            this.OnSerialized(context);
        }

        /// <summary>
        ///     开始反序列化时的处理
        /// </summary>
        public virtual void OnDeserializing(StreamingContext context)
        {
        }

        /// <summary>
        ///     完成反序列化时的处理
        /// </summary>
        public virtual void OnDeserialized(StreamingContext context)
        {
        }

        /// <summary>
        ///     开始序列化的处理
        /// </summary>
        public virtual void OnSerializing(StreamingContext context)
        {
        }

        /// <summary>
        ///     完成序列化的处理
        /// </summary>
        public virtual void OnSerialized(StreamingContext context)
        {
        }

        #endregion

        #region 文本

        /// <summary>
        ///     文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("〖{0}￤{1}￤{2}〗", this.ItemRace, this.ItemFamily, this.ItemType);
        }

        //public static string GetCode(IEnumerable<CodeItem> elements)
        //{
        //    if (elements == null)
        //    {
        //        return null;
        //    }
        //    StringBuilder sb = new StringBuilder();
        //    foreach (CodeItem element in elements)
        //    {
        //        sb.Append(GetCode(element));
        //    }
        //    return sb.ToString();
        //}

        //public static string GetCode(CodeItem element)
        //{
        //    return element == null 
        //        ? null 
        //        : CodeAnalyzerContext.Current.GetCode(element.Start, element.End);
        //}

        #endregion

    }
}
