#region 引用

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;
using ZeroTeam.MessageMVC;
using ZeroTeam.MessageMVC.Context;
using ZeroTeam.MessageMVC.Messages;
using static Agebull.EntityModel.Interfaces.GlobalDataInterfaces;
using IAuthorData = Agebull.EntityModel.Interfaces.GlobalDataInterfaces.IAuthorData;
using IHistoryData = Agebull.EntityModel.Interfaces.GlobalDataInterfaces.IHistoryData;
using IStateData = Agebull.EntityModel.Interfaces.GlobalDataInterfaces.IStateData;
using IVersionData = Agebull.EntityModel.Interfaces.GlobalDataInterfaces.IVersionData;

#endregion

namespace Agebull.EntityModel.Events
{
    /// <summary>
    /// 操作注入
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class OperatorInjection<TEntity> : IOperatorInjection<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// 依赖对象
        /// </summary>
        public IDataAccessProvider<TEntity> Provider { get; set; }

        #region 构造

        void AddValue<T>(StringBuilder fields, StringBuilder values, PropertyDefault field, T value)
        {
            fields.Append($",`{field.FieldName}`");
            values.Append($",'{value}'");
        }
        void AddValue<T>(StringBuilder fields, StringBuilder values, PropertyDefault field, string value)
        {
            fields.Append($",`{field.FieldName}`");
            values.Append($",'{value.Replace("'", "\\'")}'");
        }

        void AddValue(StringBuilder fields, StringBuilder values, PropertyDefault field, DateTime value)
        {
            fields.Append($",`{field.FieldName}`");
            values.Append($",'{value:yyyy-MM-dd HH:mm:ss}'");
        }

        void SetValue(StringBuilder valueExpression, PropertyDefault field, DateTime value)
        {
            valueExpression.Append($",`{field.FieldName}` = '{value:yyyy-MM-dd HH:mm:ss}'");
        }

        void SetValue<T>(StringBuilder valueExpression, PropertyDefault field, string value)
        {
            valueExpression.Append($",`{field.FieldName}` = '{value.Replace("'", "\\'")}'");
        }

        void SetValue<T>(StringBuilder valueExpression, PropertyDefault field, T value)
        {
            valueExpression.Append($",`{field.FieldName}` = '{value}'");
        }
        #endregion

        #region 注入

        /// <summary>
        ///     得到可正确拼接的SQL条件语句（可能是没有）
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public void InjectionQueryCondition(List<string> conditions)
        {
            if (Provider.Option.DataStruct.InterfaceFeature == null ||
                Provider.Option.DataStruct.InterfaceFeature.Count == 0 ||
                !Provider.Option.InjectionLevel.HasFlag(InjectionLevel.QueryCondition))
                return;
            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(IStateData)))
            {
                conditions.Add($"{IStateData.DataState.FieldName} < 255");
            }
            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(GlobalDataInterfaces.ILogicDeleteData)))
            {
                conditions.Add($"{GlobalDataInterfaces.ILogicDeleteData.IsDeleted.FieldName} = 0");
            }
        }

        /// <summary>
        ///     注入数据插入代码
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public void InjectionInsertCode(StringBuilder fields, StringBuilder values)
        {
            if (Provider.Option.DataStruct.InterfaceFeature == null ||
                Provider.Option.DataStruct.InterfaceFeature.Count == 0 ||
                !Provider.Option.InjectionLevel.HasFlag(InjectionLevel.InsertField))
                return;
            var user = GlobalContext.Get<IUser>();
            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(IAuthorData)))
            {
                AddValue(fields, values, IAuthorData.AddDate, DateTime.Now);
                //AddValue(fields, values, IAuthorData.Author, user?.NickName);
                AddValue(fields, values, IAuthorData.AuthorId, user?.UserId);
            }
            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(IHistoryData)))
            {
                AddValue(fields, values, IHistoryData.LastModifyDate, DateTime.Now);
                //AddValue(fields, values, IHistoryData.LastReviser, user?.NickName);
                AddValue(fields, values, IHistoryData.LastReviserId, user?.UserId);
            }
            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(IStateData)))
            {
                AddValue(fields, values, IStateData.IsFreeze, 0);
                AddValue(fields, values, IStateData.DataState, 0);
            }
            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(GlobalDataInterfaces.ILogicDeleteData)))
            {
                AddValue(fields, values, GlobalDataInterfaces.ILogicDeleteData.IsDeleted, 0);
            }
            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(IDepartmentData)))
            {
                AddValue(fields, values, IDepartmentData.DepartmentId, GlobalContext.Get<IUser>().OrganizationId);
                //AddValue(fields, values, IDepartmentData.DepartmentId, user?.OrganizationId);
            }
            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(IVersionData)))
            {
                AddValue(fields, values, IVersionData.DataVersion, DateTime.Now.Ticks);
            }
        }

        /// <summary>
        ///     注入数据更新代码
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="valueExpression"></param>
        /// <returns></returns>
        public void InjectionUpdateCode(ref string valueExpression, ref string condition)
        {
            if (Provider.Option.DataStruct.InterfaceFeature == null ||
                Provider.Option.DataStruct.InterfaceFeature.Count == 0 ||
                (!Provider.Option.InjectionLevel.HasFlag(InjectionLevel.UpdateField) &&
                !Provider.Option.InjectionLevel.HasFlag(InjectionLevel.UpdateCondition)))
            {
                return;
            }
            var val = new StringBuilder();
            var conditions = new List<string>();

            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(IHistoryData)))
            {
                SetValue(val, IHistoryData.LastModifyDate, DateTime.Now);
                //SetValue(val, IHistoryData.LastReviser, user?.NickName);
                var user = GlobalContext.Get<IUser>();
                SetValue(val, IHistoryData.LastReviserId, user?.UserId);
            }
            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(IVersionData)))
            {
                SetValue(val, IVersionData.DataVersion, DateTime.Now.Ticks);
            }
            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(IStateData)))
            {
                conditions.Add($"{IStateData.IsFreeze.FieldName} = 0");
                conditions.Add($"{IStateData.DataState.FieldName} < 255");
            }
            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(GlobalDataInterfaces.ILogicDeleteData)))
            {
                conditions.Add($"{GlobalDataInterfaces.ILogicDeleteData.IsDeleted.FieldName} = 0");
            }
            if (Provider.Option.InjectionLevel.HasFlag(InjectionLevel.UpdateField) && val.Length > 0)
                valueExpression += val.ToString();
            if (Provider.Option.InjectionLevel.HasFlag(InjectionLevel.UpdateCondition) && conditions.Count > 0)
            {
                condition = string.IsNullOrEmpty(condition)
                    ? string.Join(" AND ", conditions)
                    : $"({condition}) AND {string.Join(" AND ", conditions)}";
            }
        }

        /// <summary>
        ///     删除条件注入
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string InjectionDeleteCondition(string condition)
        {
            if (Provider.Option.DataStruct.InterfaceFeature == null ||
                Provider.Option.DataStruct.InterfaceFeature.Count == 0 ||
                !Provider.Option.InjectionLevel.HasFlag(InjectionLevel.DeleteCondition))
                return condition;
            var conditions = new List<string>();

            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(IStateData)))
            {
                conditions.Add($"{IStateData.IsFreeze.FieldName} = 0");
                conditions.Add($"{IStateData.DataState.FieldName} < 255");
            }
            if (Provider.Option.DataStruct.InterfaceFeature.Contains(nameof(GlobalDataInterfaces.ILogicDeleteData)))
            {
                conditions.Add($"{GlobalDataInterfaces.ILogicDeleteData.IsDeleted.FieldName} = 0");
            }
            if (conditions.Count > 0)
                return string.IsNullOrEmpty(condition)
                     ? string.Join(" AND ", conditions)
                     : $"({condition}) AND {string.Join(" AND ", conditions)}";
            return condition;
        }

        #endregion
        #region 扩展 

        /// <summary>
        ///     实体保存完成后期处理(Insert/Update/Delete)
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="operatorType">操作类型</param>
        /// <remarks>
        ///     对当前对象的属性的更改,请自行保存,否则将丢失
        /// </remarks>
        public async Task AfterSave(TEntity entity, DataOperatorType operatorType)
        {
            if (!Provider.Option.CanRaiseEvent)
                return;
            await OnStatusChanged(operatorType, EntityEventValueType.Entity, entity);
        }

        /// <summary>
        ///     更新语句后处理(单个实体操作不引发)
        /// </summary>
        /// <param name="condition">执行条件</param>
        /// <param name="parameter">参数值</param>
        /// <param name="operatorType">操作类型</param>
        /// <param name="sql"></param>
        public async Task AfterExecute(DataOperatorType operatorType, string sql, string condition, DbParameter[] parameter)
        {
            if (!Provider.Option.CanRaiseEvent)
                return;
            var queryCondition = new MulitCondition
            {
                SQL = sql,
                Condition = condition,
                Parameters = new ConditionParameter[parameter.Length]
            };
            for (int i = 0; i < parameter.Length; i++)
            {
                queryCondition.Parameters[i] = new ConditionParameter
                {
                    Name = parameter[i].ParameterName,
                    Value = parameter[i].Value == DBNull.Value ? null : parameter[i].Value.ToString(),
                    Type = parameter[i].DbType
                };
            }
            await OnStatusChanged(operatorType, EntityEventValueType.CustomSQL, queryCondition);
        }

        /// <summary>
        /// 状态修改事件
        /// </summary>
        /// <param name="oType">操作</param>
        /// <param name="valueType">值类型</param>
        /// <param name="val">内容</param>
        /// <remarks>
        /// 如果内容为实体,使用JSON序列化,
        /// 如果为批量操作,内容为QueryCondition的JSON序列化
        /// </remarks>
        Task OnStatusChanged(DataOperatorType oType, EntityEventValueType valueType, object val)
        {
            return MessagePoster.CallAsync("EntityEvent",
                $"{Provider.Option.DataStruct.ProjectName }/{Provider.Option.DataStruct.EntityName}",
                Provider.Option.EventLevel == EventEventLevel.Simple
                    ? new EntityEventArgument
                    {
                        OperatorType = oType
                    }
                    : new EntityEventArgument
                    {
                        OperatorType = oType,
                        ValueType = valueType,
                        Value = val?.ToJson()
                    });
        }
        #endregion
    }
}