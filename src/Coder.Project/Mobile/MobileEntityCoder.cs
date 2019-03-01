using System.IO;
namespace Agebull.EntityModel.RobotCoder
{
    public sealed class MobileEntityCoder : EntityCoderBase
    {

        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_MobileEntity_cs";

        protected override bool IsClient => true;

        #region �������

        /// <summary>
        ///     ����ʵ�����
        /// </summary>
        /// <remarks></remarks>
        protected override void CreateBaCode(string path)
        {
            var code = $@"using System;
using System.Collections.Generic;
using System.Text;
using Agebull.EntityModel;

namespace {NameSpace}
{{
    /// <summary>
    /// {Entity.Caption}
    /// </summary>
    /// <remarks>
    /// {Entity.Description}
    /// </remarks>
    {(Entity.IsInternal ? "internal" : "public")} partial class {Entity.EntityName} : EntityBase , ITson
    {{
        #region ����
        
        /// <summary>
        /// ����
        /// </summary>
        public {Entity.EntityName}()
        {{
            Initialize();
        }}

        /// <summary>
        /// ��ʼ��
        /// </summary>
        partial void Initialize();

        #endregion
{GetBaseCode<EntityPropertyBuilder>()}
{GetBaseCode<EntityCopyBuilder>()}
{GetBaseCode<EntityToStringBuilder>()}
{GetBaseCode<EntityCopyBuilder>()}
{GetBaseCode<EntityDictionaryBuilder>()}
{GetBaseCode<EntityTsonBuilder>()}
    }}
}}";

            SaveCode(Path.Combine(path, Entity.Name + ".cs"), code);
        }

        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateExCode(string path)
        {

        }

        #endregion

    }
}