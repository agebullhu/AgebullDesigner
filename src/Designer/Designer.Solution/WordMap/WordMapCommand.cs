using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.ComponentModel.Composition;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     百度翻译的命令
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class WordMapCommand : IAutoRegister
    {
        #region 命令注入

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand(new CommandItemBuilder<ConfigBase>
            {
                Action = WordMapModel.Name2CaptionChiness,
                Caption = "名称标题互译",
                Catalog = "翻译",
                IconName = "imgBaidu"
            });
            CommandCoefficient.RegisterCommand(new CommandItemBuilder<ConfigBase>
            {
                Action = WordMapModel.ToChiness,
                Caption = "空标题从名称翻译到中文",
                Catalog = "翻译",
                Description = "通过百度翻译接口,将字段的标题与说明从英文翻译成中文(需要网络连接)",
                IconName = "imgBaidu"
            });
            CommandCoefficient.RegisterCommand(new CommandItemBuilder<ConfigBase>
            {
                Catalog = "翻译",
                Caption = "翻译标题(英译中)",
                Action = WordMapModel.CaptionToChiness,
                Description = "通过百度翻译接口,将标题从中文翻译成英文(需要网络连接)",
                IconName = "imgBaidu"
            });
            CommandCoefficient.RegisterCommand(new CommandItemBuilder<ConfigBase>
            {
                Catalog = "翻译",
                Caption = "名称翻译成标题(英译中)",
                Action = WordMapModel.NameToCaption,
                Description = "通过百度翻译接口,将标题从中文翻译成英文(需要网络连接)",
                IconName = "imgBaidu"
            }); 
            CommandCoefficient.RegisterCommand(new CommandItemBuilder<ConfigBase>
            {
                Catalog = "翻译",
                Caption = "翻译名称(中译英)",
                Action = WordMapModel.NameToEnglish,
                Description = "通过百度翻译接口,将名称从中文翻译成英文(需要网络连接)",
                IconName = "imgBaidu"
            });

            CommandCoefficient.RegisterCommand(new CommandItemBuilder<ConfigBase>
            {
                Catalog = "翻译",
                Caption = "清除标题",
                Action = WordMapModel.ClearCaption
            });

            CommandCoefficient.RegisterCommand(new CommandItemBuilder
            {
                Catalog = "翻译",
                Caption = "当前内容加入字典",
                Action = arg => WordMapModel.AddMap(DesignContext.Instance.SelectConfig)
            });
            CommandCoefficient.RegisterCommand(new CommandItemBuilder
            {
                Catalog = "翻译",
                Caption = "新增字典",
                Action = arg => WordMapModel.NewWord()
            });
        }

        #endregion


    }
}