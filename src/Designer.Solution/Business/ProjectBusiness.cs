// // /*****************************************************
// // (c)2016-2016 Copy right Agebull.hu
// // 作者:
// // 工程:CodeRefactor
// // 建立:2016-09-18
// // 修改:2016-09-18
// // *****************************************************/


using System;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Designer;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Agebull.EntityModel.Config
{

    /// <summary>
    /// 实体配置相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class EnumModel : DesignCommondBase<EntityConfig>
    {

        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Catalog = "升级",
                Action = ClearOldInterface,
                Caption = "清理接口强绑定",
                IconName = "tree_sum"
            });
        }


        public void ClearOldInterface(EntityConfig entity)
        {
            foreach (var field in entity.Properties.ToArray())
            {
                if (field.IsSystemField)
                    entity.Remove(field);
            }
        }
    }
}