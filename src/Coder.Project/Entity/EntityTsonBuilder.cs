using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityTsonBuilder<TModel> : EntityBuilderBase<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        #region ����

        protected override string ClassExtend => ":ITson";

        public override string BaseCode => $@"
        #region Tson��ʽ���л��뷴���л�
        {IoCode()}
        #endregion";

        protected override string Folder => "Tson";

        #endregion

        #region ���л�

        #region �������


        /// <summary>
        ///     ����ʵ�����
        /// </summary>
        private string IoCode()
        {
            return $@"
        #region ���л�
{Index()}

        #region ITson
        
        /// <summary>
        /// ����ID
        /// </summary>
        public int TypeId => TYPE_INDEX_{Model.Name.ToUpper()};
        
        /// <summary>
        /// ��ȫ�Ļ��泤��
        /// </summary>
        public int SafeBufferLength => TSON_BUFFER_LEN_{Model.Name.ToUpper()};

        /// <summary>
        /// ��TSON�����л�
        /// </summary>
        public void Deserialize(TsonDeserializer reader)
        {{
            reader.Begin();
            while (!reader.IsEof)
            {{
                int idx = reader.ReadIndex();
                switch (idx)
                {{{DeserializeCode(Model)}
                }}
                break;//������ʱ��ֹ
            }}
            reader.End();
        }}

        /// <summary>
        /// ���л���Tson
        /// </summary>
        public void Serialize(TsonSerializer writer)
        {{
            writer.Begin(TYPE_INDEX_{Model.Name.ToUpper()}, 1);
            {SerializeCode(Model)}

            writer.End();
        }}
        #endregion
        #endregion ���л�";
        }

        #endregion

        #region ���������ȼ���


        private string Index()
        {
            var code = new StringBuilder();
            code.Append($@"
        #region ���ݳ���

        /// <summary>
        /// {Model.Name}���ʹ���
        /// </summary>
        public const int TYPE_INDEX_{Model.Name.ToUpper()} = 0x{Model.Identity:X};

        /// <summary>
        /// {Model.Name}���ʹ���
        /// </summary>
        public const int EntityId = 0x{Model.Identity:X};");
            foreach (var property in Model.UserProperty)
            {
                code.Append($@"

        /// <summary>
        /// {Model.Caption}-{property.Caption} ���ֶ�����
        /// </summary>
        public const byte FIELD_INDEX_{Model.Name.ToUpper()}_{property.Name.ToUpper()} = 0x{property.Identity:X};");
            }
            code.AppendLine($@"

        /// <summary>
        /// {Model.Caption}�ĺ������л�����
        /// </summary>
        public const int TSON_BUFFER_LEN_{Model.Name.ToUpper()} = {GetEntitySerializeLen(Model)};


        #endregion ���ݳ���");
            return code.ToString();
        }

        private static int GetEntitySerializeLen(IEntityConfig entity)
        {
            int len = entity.LastProperties.Count + 94;
            foreach (var property in entity.UserProperty)
            {
                len += GetFieldSerializeLen(property);
            }
            return len;
        }

        private static int GetFieldSerializeLen(IFieldConfig field)
        {
            int.TryParse(field.ArrayLen, out int len);

            switch (field.CsType.ToLower())
            {
                case "bool":
                case "byte":
                case "sbyte":
                    return len > 1 ? 1 + len : 1;
                case "short":
                case "ushort":
                case "char":
                case "int16":
                case "uint16":
                    return len > 1 ? 1 + len * 2 : 2;
                case "int":
                case "uint":
                case "int32":
                case "uint32":
                    return len > 1 ? 1 + len * 4 : 4;
                case "long":
                case "ulong":
                case "int64":
                case "uint64":
                    return len > 1 ? 1 + len * 8 : 8;
                case "guid":
                    return len > 1 ? 1 + len * 18 : 18;
                case "string":
                    return field.Datalen > 1 ? 1 + field.Datalen * 2 : 501;

            }
            var entity = GlobalConfig.GetModel(field.CsType);
            if (entity != null)
            {
                return GetEntitySerializeLen(entity);
            }
            return 64;
        }

        #endregion

        private string SerializeCode(IEntityConfig entity)
        {
            StringBuilder code = new StringBuilder();
            foreach (var field in entity.UserProperty)
            {
                if (!field.CanGet)
                    continue;
                if (field.EnumConfig != null)
                {
                    code.Append($@"
            //{field.Caption}
            if(this._{field.Name.ToLWord()} != {field.EnumConfig.Name}.{field.EnumConfig.Items[0].Name})
            {{
                writer.WriteIndex(FIELD_INDEX_{Model.Name.ToUpper()}_{field.Name.ToUpper()});
                writer.Write((int)this._{field.Name.ToLWord()});
            }}");
                }
                else
                {
                    code.Append($@"
            //{field.Caption}
            if(!writer.IsEmpty(this._{field.Name.ToLWord()}))
            {{
                writer.WriteIndex(FIELD_INDEX_{Model.Name.ToUpper()}_{field.Name.ToUpper()});
                writer.Write(this._{field.Name.ToLWord()});
            }}");
                }
            }
            return code.ToString();
        }


        private string DeserializeCode(IEntityConfig entity)
        {
            StringBuilder code = new StringBuilder();
            foreach (var field in entity.UserProperty)
            {
                if (!field.CanSet)
                    continue;
                code.Append($@"
                case FIELD_INDEX_{Model.Name.ToUpper()}_{field.Name.ToUpper()}://{field.Caption}");
                if (field.EnumConfig != null)
                {
                    code.Append($@"
                    {{
                        int val = 0;
                        reader.Read(ref val);
                        this._{field.Name.ToLWord()} = ({field.EnumConfig.Name})val;
                    }}");

                }
                else
                {
                    var en = GlobalConfig.GetEntity(field.CsType);
                    if (en != null)
                    {
                        code.Append($@"
                    this._{field.Name.ToLWord()} = reader.Read<{field.CsType}>();");
                    }
                    else
                    {
                        code.Append($@"
                    reader.Read(ref this._{field.Name.ToLWord()});");
                    }
                }
                code.Append(@"
                    continue;");
            }
            return code.ToString();
        }

        #endregion
    }
}