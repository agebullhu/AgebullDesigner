using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.VUE
{

    public class VueFormCoder 
    {
        public EntityConfig Entity { get; set; }
        public ProjectConfig Project { get; set; }
        public string Code()
        {
            if (Entity.FormCloumn <= 0)
                Entity.FormCloumn = 1;
            var ext = Entity.MaxForm ? " isPanel='true'" : $" style='width:{Entity.FormCloumn * 485}px;'";
            string name = Entity.Name.ToLWord();
            var fields = Entity.ClientProperty.Where(p => !p.NoneDetails).ToArray();
            var form_items = FormFields(fields);
            var form_default = DefaultValue(fields);
            var form_rules = Rules(fields);
            return $@"
<!-- Form -->
<el-dialog title='{Entity.Caption}�༭'
           :visible.sync='{name}.form.visible' 
           v-loading='{name}.form.loading'
           element-loading-text='���ڴ���'
           element-loading-spinner='el-icon-loading'
           element-loading-background='rgba(0, 0, 0, 0.8)'>
    <div style='border:solid 1px silver;background-color:white;padding:10px'>
        <el-form :model='{name}.form' 
                 :rules='{name}.rules' 
                 label-position='left' 
                 label-width='100px' 
                 ref='{name}Form' 
                 @@submit.native.prevent>{form_items}
        </el-form>
    </div>
    <div slot='footer' class='dialog-footer'>
        <el-button @@click='{name}.form.visible = false'>ȡ ��</el-button>
        <el-button @@click= 'save_{name}' type='primary'>ȷ ��</el-button>
    </div>
</el-dialog>
<script>
    extend_data({{
        {name} : {{
            visible: false,
            loading: false,
            edit:false,
            form: {{{form_default}
            }},
            rules: {{{form_rules}
            }}
        }}
    }});
    extend_methods({{
        save_{name}() {{
            var that = this;
            this.$refs['{name}Form'].validate((valid) => {{
                if (!valid) {{
                    that.$message.error('���ݲ�����');
                    return false;
                }}
                data.loading = true;
                $.post(data.edit ? '{Entity.Name}/Update' : '{Entity.Name}/Add', data.form, function (result) {{
                    data.loading = false;
                    if (result.success) {{
                        that.$message({{
                            message: '�����ɹ�',
                            type: 'success'
                        }});
                        data.visible = false;
                    }}
                    else {{
                        that.$message.error('����ʧ��:' + result.status.msg);
                    }}
                    data.loading = false;
                }}).error(function () {{
                    data.loading = false;
                    data.visible = false;
                    that.$message.error('����ʧ��');
                }});
            }});
        }}
    }});
</script>";
        }

        /// <summary>
        ///     ����Form¼���ֶν���
        /// </summary>
        /// <param name="columns"></param>
        private string DefaultValue(PropertyConfig[] columns)
        {
            bool first = true;
            var code = new StringBuilder();
            foreach (var field in columns)
            {
                if (first)
                    first = false;
                else
                    code.Append(',');
                if (field.CsType == "string")
                {
                    code.Append($@"
            '{field.JsonName}':null");
                }
                else
                {
                    code.Append($@"
            '{field.JsonName}':0");
                }
            }
            return code.ToString();
        }

        /// <summary>
        ///     ����Form¼���ֶν���
        /// </summary>
        /// <param name="columns"></param>
        private string FormFields(PropertyConfig[] columns)
        {
            var code = new StringBuilder();
            foreach (var field in columns)
            {
                var caption = field.Caption;
                var description = field.Description;

                if (field.IsLinkKey)
                {
                    var friend = Entity.LastProperties.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
                    if (friend != null)
                        caption = friend.Caption;
                    if (friend != null)
                        description = friend.Description;
                }
                FormField(code, field, caption, description ?? field.Caption);
            }
            return code.ToString();
        }

        private void FormField(StringBuilder code, PropertyConfig field, string caption, string description)
        {
            if (field.EnumConfig != null)
            {
                code.Append($@"
            <el-form-item label='{field.Caption}' prop='{field.JsonName}'>
                <el-radio-group v-model='{Entity.Name.ToLWord()}.form.{field.JsonName}'>");

                foreach (var item in field.EnumConfig.Items)
                {
                    code.Append($@"
                    <el-radio label='{item.Value}'>{item.Caption}</el-radio>");
                }

                code.Append($@"
                </el-radio-group>
            </el-form-item>");
            }
            else
            {
                code.Append($@"
            <el-form-item label='{field.Caption}' prop='{field.JsonName}'>
                <el-input v-model='{Entity.Name.ToLWord()}.form.{field.JsonName}' placeholder='{field.Description}' auto-complete='off'></el-input>
            </el-form-item>");
            }

        }


        #region ���ݹ������

        /// <summary>
        ///     ����Form¼���ֶν���
        /// </summary>
        /// <param name="columns"></param>
        private string Rules(PropertyConfig[] columns)
        {
            bool first = true;
            var code = new StringBuilder();
            foreach (var field in columns)
            {
                if (first)
                    first = false;
                else
                    code.Append(',');
                code.Append($@"
            '{field.JsonName}':[");
                var dot = "";
                if (field.IsRequired && field.CanUserInput)
                {
                    code.Append($@"{dot}
                {{ required: true, message: '������{field.Caption}', trigger: 'blur' }}");
                    dot = ",";
                }
                switch (field.CsType)
                {
                    case "string":
                        StringCheck(field, code, ref dot);
                        break;
                    case "int":
                    case "long":
                    case "uint":
                    case "ulong":
                    case "short":
                    case "ushort":
                    case "decimal":
                    case "float":
                    case "double":
                        NumberCheck(field, code, ref dot);
                        break;
                    case "DateTime":
                        DateTimeCheck(field, code, ref dot);
                        break;
                }
                code.Append($@"
            ]");
            }
            return code.ToString();
        }
        private static void DateTimeCheck(PropertyConfig field, StringBuilder code,ref string dot)
        {
            if (field.Max != null && field.Min != null)
                code.Append($@"{dot}
                {{ min: {field.Min}, max: {field.Max}, message: 'ʱ��� {field.Min} �� {field.Max} ֮��', trigger: 'blur' }}");
            else if (field.Max != null)
                code.Append($@"{dot}
                {{ max: {field.Max}, message: 'ʱ�䲻���� {field.Max}', trigger: 'blur' }}");
            else if (field.Min != null)
                code.Append($@"{dot}
                {{ min: {field.Min}, message: 'ʱ�䲻С�� {field.Min}', trigger: 'blur' }}");
        }

        private static void NumberCheck(PropertyConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max != null && field.Min != null)
                code.Append($@"{dot}
                {{ min: {field.Min}, max: {field.Max}, message: '��ֵ�� {field.Min} �� {field.Max} ֮��', trigger: 'blur' }}");
            else if (field.Max != null)
                code.Append($@"{dot}
                {{ max: {field.Max}, message: '��ֵ������ {field.Max}', trigger: 'blur' }}");
            else if (field.Min != null)
                code.Append($@"{dot}
                {{ min: {field.Min}, message: '��ֵ��С�� {field.Min}', trigger: 'blur' }}");
        }

        private static void StringCheck(PropertyConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max != null && field.Min != null)
                code.Append($@"{dot}
                {{ min: {field.Min}, max: {field.Max}, message: '������ {field.Min} �� {field.Max} ���ַ�', trigger: 'blur' }}");
            else if (field.Max != null)
                code.Append($@"{dot}
                {{ max: {field.Max}, message: '���Ȳ����� {field.Max} ���ַ�', trigger: 'blur' }}");
            else if (field.Min != null)
                code.Append($@"{dot}
                {{ min: {field.Min}, message: '���Ȳ�С�� {field.Min} ���ַ�', trigger: 'blur' }}");
        }

        #endregion
    }
}