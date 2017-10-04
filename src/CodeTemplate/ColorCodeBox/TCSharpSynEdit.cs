namespace SQLDialog.SynEdit
{
    using System.Drawing;

    /// <summary>
    /// 作者：徐侠君
    /// 描述：C#代码着色编辑器
    /// 创建时间：2004-08-15
    /// </summary>
    public class TCSharpSynEdit:LanguageEditor
    {
        readonly string[] _KeyWords = new[]{"using","namespace","public","private","protected","internal","null",
                                             "class","interface","new","int","double","char","string",
                                             "this","byte","float","if","else","get","set",
                                             "while","return","true","false","readonly",
                                             "override","static","void","partial","abstract",
                                             "var","try","catch","finally",
                                             "bool","sealed ","long","ulong","uint","ushort","short",
                                             "default","switch","case","struct","for","region","#","endregion","",
                                             "foreach","break","continue","object","base"};

        readonly string[] _Members = new[] { "yet", "from", "select", "let", "Guid", "DateTime", "value",
            "String","Int32","Int64",
            "DataMember", "IgnoreDataMember", "DataContract", "Serializable", "DataMember", "DataMember" };
        public TCSharpSynEdit()
        {
            this.KeyWordHighlighter = new Coloration
            {
                ForeColor = Color.Blue,
                IsIgnoreCase = false
            };
            this.KeyWordHighlighter.AddMultiWords(_KeyWords);

            this.UserTypesHighlighter = new Coloration
            {
                IsIgnoreCase = false,
                ForeColor = Color.FromArgb(255, 5, 51, 65)
            };

            this.SystemTypesHighlighter = new Coloration
            {
                IsIgnoreCase = false,
                ForeColor = Color.FromArgb(255, 5, 51, 65)
            };
            this.SystemTypesHighlighter.AddMultiWords(_Members);

            this.MemberHighlighter = new Coloration
            {
                IsIgnoreCase = false,
                ForeColor = Color.FromArgb(255, 43, 145, 175)
            };
            this.StringHighlighter = new Coloration
            {
                ForeColor = Color.Red
            };

            this.CommentHighlighter = new Coloration
            {
                IsIgnoreCase = false,
                ForeColor = Color.Green
            };

            this.DefaultHighlighter = new Coloration
            {
                IsIgnoreCase = false,
                ForeColor = Color.Black
            };

            this.SingleRowRemark = "//";
            this.MultiRowRemHead = "/*";
            this.MultiRowRemTail = "*/";
            this.Quota = new TSynQuotaCollection();
            this.Quota.Add(new ColorBlock('"', '"', "\\"));
        }
    }
}