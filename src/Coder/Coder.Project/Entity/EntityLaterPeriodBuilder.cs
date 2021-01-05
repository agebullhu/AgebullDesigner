using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityLaterPeriodBuilder : ModelBuilderBase
    {
        #region 基础

        /// <summary>
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => false;

        public override string BaseCode => $@"
        #region 后期处理
        {GetOnLaterPeriodBySignleModified()}
        #endregion";

        protected override string Folder => "LaterPeriod";

        #endregion

        private string GetOnLaterPeriodBySignleModified()
        {
            var code = new StringBuilder();
            code.Append(@"

        /// <summary>
        /// 单个属性修改的后期处理(保存后)
        /// </summary>
        /// <param name=""subsist"">当前实体生存状态</param>
        /// <param name=""modifieds"">修改列表</param>
        /// <remarks>
        /// 对当前对象的属性的更改,请自行保存,否则将丢失
        /// </remarks>
        protected override void OnLaterPeriodBySignleModified(EntitySubsist subsist,byte[] modifieds)
        {");
            if (!string.IsNullOrWhiteSpace(Model.ModelBase))
                code.AppendLine(@"
            base.OnLaterPeriodBySignleModified(subsist,modifieds);");

            code.Append(@"
            if (subsist == EntitySubsist.Deleting)
            {");
            foreach (var property in ReadWriteColumns)
            {
                code.Append($@"
                On{property.Name}Modified(subsist,false);");
                //if (!Entity.IsLog && property.CreateIndex && !property.IsPrimaryKey)
                //{
                //    if (DataBase.ReadOnly)
                //        code.AppendFormat(@"
                //__Index_{0}.RemoveIndex(this);", property.Name);
                //    else
                //        code.AppendFormat(@"
                //__Index_{0}.RemoveIndex(Id);", property.Name);
                //}
            }
            code.Append(@"
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {");
            foreach (var property in ReadWriteColumns)
            {
                code.AppendFormat(@"
                On{0}Modified(subsist,true);", property.Name);
                //if (Entity.IsLog || !property.CreateIndex || property.IsPrimaryKey)
                //    continue;
                //if (DataBase.ReadOnly)
                //    code.AppendFormat(@"
                //__Index_{0}.AddToIndex(this);", property.Name);
                //else if (property.UniqueIndex == 0 || property.CsType != "int")
                //    code.AppendFormat(@"
                //__Index_{0}.AddToIndex(Id,{0});", property.Name);
                //else
                //    code.AppendFormat(@"
                //__Index_{0}.AddToIndex(Id,UId , {0});", property.Name);
            }
            code.Append($@"
                return;
            }}
            else if(modifieds != null && modifieds[{Columns.Length}] > 0)
            {{");

            foreach (var property in ReadWriteColumns)
            {
                code.Append($@"
                On{property.Name}Modified(subsist,modifieds[_DataStruct_.Real_{property.Name}] == 1);");

                //code.AppendFormat(@"
                //if(modifieds[Real_{0}] == 1)
                //{{
                //    On{0}Modified(subsist,modifieds[Real_{0}] == 1);", property.Name);
                //if (!Entity.IsLog && property.CreateIndex && !property.IsPrimaryKey)
                //{
                //    if (DataBase.ReadOnly)
                //        code.AppendFormat(@"
                //    __Index_{0}.AddToIndex(this);", property.Name);
                //    else if (property.UniqueIndex == 0 || property.CsType != "int")
                //        code.AppendFormat(@"
                //    __Index_{0}.AddToIndex(Id,{0});", property.Name);
                //    else
                //        code.AppendFormat(@"
                //    __Index_{0}.AddToIndex(Id,UId , {0});", property.Name);
                //}
                //code.Append(@"
                //}");
            }
            code.Append(@"
            }
        }");
            foreach (var property in ReadWriteColumns)
            {
                code.AppendFormat(@"

        /// <summary>
        /// {1}修改的后期处理(保存前)
        /// </summary>
        /// <param name=""subsist"">当前对象状态</param>
        /// <param name=""isModified"">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void On{0}Modified(EntitySubsist subsist,bool isModified);", property.Name, property.Caption);
            }
            return code.ToString();
        }


    }
}