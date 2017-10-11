using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class WebServiceBuilder : FileCoder
    {
        #region 基础代码

        public override ConfigBase CurrentConfig => SolutionConfig.Current;

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_WebService";

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            var file = @"C:\work\FundManage\WebApi\DataNotice\DataNoticeService.cs";
            var code =
                $@"
using System.ServiceModel;
using Agebull.Common.Frame;
using Agebull.Common.General;
using Agebull.Common.Logging;
using Gboxt.Common.DataModel;
using GBS.Futures.Manage;
using GBS.Futures.Manage.BusinessLogic;

namespace GBS.Fuctures.Manage.WCF
{{
    /// <summary>
    ///   表示数据同步服务
    /// </summary>
    [ServiceContract]
    [ServiceKnownType(typeof(PopedomDictionary))]
    [ServiceKnownType(typeof(PopedomItem))]
    [ServiceKnownType(typeof(RequestUser))]
    [ServiceKnownType(typeof(NotificationObject))]
    [ServiceKnownType(typeof(DataObjectBase))]
    [ServiceKnownType(typeof(IndexEditStatus))]
    [ServiceKnownType(typeof(StatusDataObject<IndexEditStatus>))]
    [ServiceKnownType(typeof(EditDataObject<IndexEditStatus>))]
    [ServiceKnownType(typeof(EditDataObject))]
    [ServiceKnownType(typeof(CustomFundsData))]{TypeCode()}
    public interface IDataNoticeService
    {{{InterfaceCode()}
    }}
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    class DataNoticeService : ServiceBase, IDataNoticeService
    {{{ServiceCode()}
    }}
}}";
            SaveCode(file, code);
        }

        private string TypeCode()
        {
            var code = new StringBuilder();
            foreach (var project in SolutionConfig.Current.Projects)
            {
                foreach (var entity in project.Entities.Where(p => !p.IsClass && !p.IsReference && !p.IsInternal))
                {
                    code.Append($@"
    [ServiceKnownType(typeof({project.NameSpace}.{entity.EntityName}))]");
                }
            }
            return code.ToString();
        }
        private string InterfaceCode()
        {
            var code = new StringBuilder();
            foreach (var project in SolutionConfig.Current.Projects)
            {
                foreach (var entity in project.Entities.Where(p => !p.IsClass && !p.IsReference && !p.IsInternal))
                {
                    code.Append($@"
        /// <summary>
        ///   处理{entity.Caption}修改
        /// </summary>
        /// <param name=""data""> {entity.Caption}数据 </param>
        /// <param name=""userId""> 用户ID </param>
        [OperationContract]
        void On{entity.Name}Chanaged({project.NameSpace}.{entity.EntityName} data, int userId);");
                }
            }
            return code.ToString();
        }

        private string ServiceCode()
        {
            var code = new StringBuilder();
            foreach (var project in SolutionConfig.Current.Projects)
            {
                foreach (var entity in project.Entities.Where(p => !p.IsClass && !p.IsReference && !p.IsInternal))
                {
                    if (entity.PrimaryColumn == null)
                        continue;
                    code.Append($@"
        /// <summary>
        ///   处理{entity.Caption}修改
        /// </summary>
        /// <param name=""data""> {entity.Caption}数据 </param>
        /// <param name=""userId""> 用户ID </param>
        void IDataNoticeService.On{entity.Name}Chanaged({project.NameSpace}.{entity.EntityName} data, int userId)
        {{
            LogRecorder.BeginMonitor(""OnChanaged->{entity.Name}"");
            var model = new {project.NameSpace}.{entity.Name}Model {{ Data = data }};
            model.WriteToRedis();
            model.SendDataChanged();
            LogRecorder.EndMonitor();
        }}");
                }
            }
            return code.ToString();
        }
        #endregion
        #region 扩展代码

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            foreach (var project in SolutionConfig.Current.Projects.ToArray())
            {
                foreach (var entity in project.Entities.Where(p => !p.IsClass && !p.IsReference && !p.IsInternal))
                {
                    EntityModelCode(entity);
                }
            }
        }

        private void EntityModelCode(EntityConfig entity)
        {
            var file = Path.Combine(@"C:\work\FundManage\WebApi\DataNotice\", entity.Name + "Model.cs");
            string point;
            string key;
            string get_key;
            var ufield = entity.Properties.FirstOrDefault(p => p.IsUserId);
            var field = entity.Properties.FirstOrDefault(p => p.Name == entity.RedisKey);

            if (entity.IsClass)
            {
                point = ufield != null
                    ? $"e:{entity.Parent.Abbreviation}:{entity.Abbreviation}:{{Data.{ufield.Name}}}"
                    : $"e:{entity.Parent.Abbreviation}:{entity.Abbreviation}";

                key = ufield != null
                    ? $"int uid"
                    : $"";

                get_key = ufield != null
                    ? $"e:{entity.Parent.Abbreviation}:{entity.Abbreviation}::{{uid}}"
                    : $"e:{entity.Parent.Abbreviation}:{entity.Abbreviation}";
                
            }
            else
            {
                if (field == null)
                    field = entity.PrimaryColumn;

                point = ufield != null
                    ? $"e:{entity.Parent.Abbreviation}:{entity.Abbreviation}:{{Data.{ufield.Name}}}:{{Data.{field.Name}}}"
                    : $"e:{entity.Parent.Abbreviation}:{entity.Abbreviation}:{{Data.{field.Name}}}";

                key = ufield != null
                    ? $"int uid ,{field.CppLastType} {field.Name.ToLower()}"
                    : $"{field.CppLastType} {field.Name.ToLower()}";

                get_key = ufield != null
                    ? $"e:{entity.Parent.Abbreviation}:{entity.Abbreviation}:{{uid}}:{{{field.Name.ToLower()}}}"
                    : $"e:{entity.Parent.Abbreviation}:{entity.Abbreviation}:{{{field.Name.ToLower()}}}";
            }
            var code = $@"
namespace {entity.Parent.NameSpace}
{{
    /// <summary>
    /// {entity.Caption}同步模型类
    /// </summary>
    public class {entity.Name}Model
    {{
        /// <summary>
        /// 操作数据
        /// </summary>
        public {entity.EntityName} Data {{ get; set; }}
        /// <summary>
        /// 将修改发送到交易服务器
        /// </summary>
        public void SendDataChanged()
        {{
            {entity.Name}Helper.SendChanged(Data);
        }}

        /// <summary>
        /// 写入REDIS
        /// </summary>
        public void WriteToRedis()
        {{
            using (RedisProxy proxy = new RedisProxy())
            {{
                unsafe
                {{
                    {entity.Name}Helper helper = new {entity.Name}Helper();
                    helper.WriteTo(Data);
                    byte[] buffer = new byte[helper.m_buffer_len];
                    for (int i = 0; i < helper.m_buffer_len; i++)
                        buffer[i] = helper.m_buffer[i];
                    proxy.Client.Set($""{point}"", buffer);
                    helper.Disponse();
                }}
            }}
        }}

        /// <summary>
        /// 从REDIS读取
        /// </summary>
        public void ReadFromRedis({key})
        {{
            using (RedisProxy proxy = new RedisProxy())
            {{
                unsafe
                {{
                    var data_b = proxy.Client.Get($""{get_key}"");
                    if (data_b == null || data_b.Length == 0)
                        return;
                    {entity.Name}Helper helper = new {entity.Name}Helper();
                    helper.CreateBuffer((uint)data_b.Length);

                    for (int i = 0; i < helper.m_buffer_len; i++)
                        helper.m_buffer[i] = data_b[i];
                    Data = helper.ReadFrom();
                    helper.Disponse();
                }}
            }}
        }}
    }}
}}";
            SaveCode(file, code);
        }
        #endregion

    }
}