using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityLaterPeriodBuilder : ModelBuilderBase
    {
        #region ����

        /// <summary>
        /// �Ƿ�ͻ��˴���
        /// </summary>
        protected override bool IsClient => false;

        public override string BaseCode => $@"
        #region ���ڴ���
        {GetOnLaterPeriodBySignleModified()}
        #endregion";

        protected override string Folder => "LaterPeriod";

        #endregion

        private string GetOnLaterPeriodBySignleModified()
        {
            var code = new StringBuilder();
            code.Append(@"

        /// <summary>
        /// ���������޸ĵĺ��ڴ���(�����)
        /// </summary>
        /// <param name=""subsist"">��ǰʵ������״̬</param>
        /// <param name=""modifieds"">�޸��б�</param>
        /// <remarks>
        /// �Ե�ǰ��������Եĸ���,�����б���,���򽫶�ʧ
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
        /// {1}�޸ĵĺ��ڴ���(����ǰ)
        /// </summary>
        /// <param name=""subsist"">��ǰ����״̬</param>
        /// <param name=""isModified"">�Ƿ��޸�</param>
        /// <remarks>
        /// �Թ��������Եĸ���,�����б���,������ܶ�ʧ
        /// </remarks>
        partial void On{0}Modified(EntitySubsist subsist,bool isModified);", property.Name, property.Caption);
            }
            return code.ToString();
        }


    }
}