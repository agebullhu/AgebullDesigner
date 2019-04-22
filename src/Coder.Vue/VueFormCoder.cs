using System;
using System.ComponentModel.Composition;
using System.Globalization;
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

        #region 页面
        #region HTML
        public string HtmlCode()
        {
            StringBuilder exButton = new StringBuilder();
            StringBuilder quButton = new StringBuilder();

            if (Entity.Interfaces.Contains("IStateData"))
            {
                exButton.Append(stateButton);
                quButton.Append(satateQuery);
            }
            if (Entity.Interfaces.Contains("IAuditData"))
            {
                exButton.Append(auditButton);
                quButton.Append(auditQuery);
            }
            var cls = Entity.Parent.Classifies.FirstOrDefault(p => p.Name == Entity.Classify);
            var folder = cls == null
               ? $"{Entity.Parent.Caption} > {Entity.Caption}"
               : $"{Entity.Parent.Caption} > {cls.Caption } > {Entity.Caption}";
            return $@"<!DOCTYPE html>
<html>
<head>
    <meta name='renderer' content='webkit'/>
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=21'/>
    <title>
        {folder}
    </title>
    <meta charset='utf-8'/>
    <meta name='viewport' content='width=device-width'/>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8-bom'/>
    <link rel='stylesheet' type='text/css' href='/styles/font-awesome/css/font-awesome.min.css'>
    <!--JQuery-->
    <script type='text/javascript' src='/scripts/jquery-3.3.1.min.js?v=201910001'></script>
    <!--VUE-->
    <link href='http://cdn.staticfile.org/element-ui/2.6.1/theme-chalk/index.css' rel='stylesheet'>
    <script src='http://cdn.staticfile.org/vue/2.6.10/vue.js'></script>
    <script src='http://cdn.staticfile.org/element-ui/2.6.1/index.js'></script>
    <script src='http://cdn.staticfile.org/element-ui/2.6.1/locale/zh-CN.min.js'></script>
    <!--Extend-->
    <script type='text/javascript' src='/scripts/extend/vue_ex.js?v=201910001'></script>
    <script type='text/javascript' src='/scripts/extend/core.js?v=201910001'></script>
    <script type='text/javascript' src='/scripts/extend/ajax.js?v=201910001'></script>
    <script type='text/javascript' src='/scripts/extend/ajax_vue.js?v=201910001'></script>
    <script type='text/javascript' src='/scripts/extend/extend.js?v=201910001'></script>
    <script type='text/javascript' src='/scripts/extend/type.js?v=201910001'></script>
    <link rel='stylesheet' type='text/css' href='/styles/vuePage.css?v=201910001'/>
</head>
<body>
<div id='work_space' class='tiled' v-cloak>
    <el-container style='height: 100%; width: 100%'>
        <el-header style='height: 50px; padding: 5px'>
            <div style='display: inline-block;'>
                <el-button-group>
                    <el-tooltip placement='top' effect='light'>
                        <div slot='content'>新增一条数据</div>
                        <el-button type='primary' icon='fa fa-plus' @click='doAddNew'></el-button>
                    </el-tooltip>
                    <el-tooltip placement='top' effect='light'>
                        <div slot='content'>编辑当前选行的数据</div>
                        <el-button type='primary' icon='el-icon-edit' @click='doEdit'></el-button>
                    </el-tooltip>
                    <el-tooltip placement='top' effect='light'>
                        <div slot='content'>删除当前选中的一或多行数据的数据</div>
                        <el-button type='primary' icon='fa fa-times' @click='doDelete'></el-button>
                    </el-tooltip>
                </el-button-group>{exButton}
            </div>
            <div style='display: inline-block; float: right'>
                <el-input placeholder='请输入搜索内容' v-model='list.keyWords' class='input-with-select'>{quButton}
                    <el-button slot='append' icon='el-icon-search' @click='loadList'></el-button>
                </el-input>
            </div>
        </el-header>
        <el-main style='margin: 0; padding: 0'>
            <template>{GridCode()}
            </template>
        </el-main>
        <el-footer>
            <el-pagination @size-change='pageChange'
                           @current-change='pageChange'
                           background
                           :current-page='list.page'
                           :page-sizes='[20, 30, 50, 100]'
                           :page-size='list.pageSize'
                           layout='total, sizes, prev, pager, next, jumper'
                           :total='list.total'>
            </el-pagination>
        </el-footer>
    </el-container>
    <!-- Form -->
    {FormCode()}
</div>
<script type='text/javascript' src='script.js'></script>
</body>
</html>";
        }

        const string auditButton = @"";
        const string auditQuery = @"
                    <el-select v-model='list.dataState' slot='prepend' placeholder='数据状态' style='width: 130px;'>
                        <el-option :value='256' label='-'></el-option>
                        <el-option :value='0' label='草稿'></el-option>
                        <el-option :value='1' label='启用'></el-option>
                        <el-option :value='2' label='停用'></el-option>
                        <el-option :value='14' label='查看'></el-option>
                        <el-option :value='15' label='锁定'></el-option>
                        <el-option :value='16' label='废弃'></el-option>
                        <el-option :value='255' label='删除'></el-option>
                    </el-select>";
        const string satateQuery = @"
                    <el-select v-model='list.dataState' slot='prepend' placeholder='数据状态' style='width: 130px;'>
                        <el-option :value='256' label='-'></el-option>
                        <el-option :value='0' label='草稿'></el-option>
                        <el-option :value='1' label='启用'></el-option>
                        <el-option :value='2' label='停用'></el-option>
                        <el-option :value='14' label='查看'></el-option>
                        <el-option :value='15' label='锁定'></el-option>
                        <el-option :value='16' label='废弃'></el-option>
                        <el-option :value='255' label='删除'></el-option>
                    </el-select>";
        const string stateButton = @"
                <el-button-group>
                    <el-tooltip placement='top' effect='light'>
                        <div slot='content'>使当前选中的一或多行数据的数据成为<b>启用</b>状态</div>
                        <el-button type='primary' icon='fa fa-star' @click='doEnable'>启用</el-button>
                    </el-tooltip>
                    <el-tooltip placement='top' effect='light'>
                        <div slot='content'>使当前选中的一或多行数据的数据成为<b>禁用</b>状态</div>
                        <el-button type='primary' icon='fa fa-star-o' @click='doDisable'>禁用</el-button>
                    </el-tooltip>
                    <el-tooltip placement='top' effect='light'>
                        <div slot='content'>使当前选中的一或多行数据的数据成为<b>废弃</b>状态</div>
                        <el-button type='primary' icon='el-icon-delete' @click='doDiscard'>废弃</el-button>
                    </el-tooltip>
                    <el-tooltip placement='top' effect='light'>
                        <div slot='content'>使当前选中的一或多行数据的数据<b>还原</b>为草稿状态,使之可编辑</div>
                        <el-button type='primary' icon='fa fa-pencil' @click='doReset'>还原</el-button>
                    </el-tooltip>
                </el-button-group>";
        #endregion
        #region 表格

        public string GridCode()
        {
            var code = new StringBuilder();
            if (Entity.Interfaces.Contains("IStateData"))
            {
                code.Append(stateColumn);
            }
            foreach (var field in Entity.ClientProperty.Where(p => !p.NoneGrid).ToArray())
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
                GridField(code, field, caption, description ?? field.Caption);
            }
            return $@"
                <!-- Grid -->
                <el-table :data='list.rows'
                          border
                          ref='dataTable'
                          highlight-current-row
                          @row-dblclick='dblclick'
                          @current-change='currentRowChange'
                          @selection-change='selectionRowChange'
                          style='width: 100%'>
                    <el-table-column type='selection'
                                     align='center'
                                     header-align='center'>
                    </el-table-column>{code}
                </el-table>";
        }

        const string stateColumn = @"<el-table-column label='状态'
                                     align='center'
                                     header-align='center'
                                     width='50'>
                        <template slot-scope='scope'>
                            <i :class='scope.row.dataState | dataStateIcon'></i>
                        </template>
                    </el-table-column>";


        private void GridField(StringBuilder code, PropertyConfig field, string caption, string description)
        {
            if (field.EnumConfig != null)
            {
                code.Append($@"
                    <el-table-column prop='{field.JsonName}'
                                     header-align='center'
                                     label='{field.Caption}'>
                        <template slot-scope='scope'>
                            <span style='margin-left: 10px'>
                                {{{{scope.row.{field.JsonName} | {field.EnumConfig.Name.ToLWord()}Formater}}}}
                            </span>
                        </template>
                    </el-table-column>");
            }
            else if (field.CsType == nameof(DateTime))
            {
                var fmt = field.IsTime ? "formatTime" : "formatDate";
                code.Append($@"
                    <el-table-column prop='{field.JsonName}'
                                     header-align='center'
                                     label='{field.Caption}'>
                        <template slot-scope='scope'>
                            <span style='margin-left: 10px'>
                                {{{{scope.row.{field.JsonName} | {fmt}}}}}
                            </span>
                        </template>
                    </el-table-column>");
            }
            else if (field.IsMoney)
            {
                code.Append($@"
                    <el-table-column prop='{field.JsonName}'
                                     header-align='center'
                                     label='{field.Caption}'>
                        <template slot-scope='scope'>
                            <span style='margin-left: 10px'>
                                {{{{scope.row.{field.JsonName} | formatMoney}}}}
                            </span>
                        </template>
                    </el-table-column>");
            }
            else
            {
                code.Append($@"
                    <el-table-column prop='{field.JsonName}'
                                     header-align='center'
                                     label='{field.Caption}'>
                    </el-table-column>");
            }

        }

        #endregion
        #region 表单

        public string FormCode()
        {
            var code = new StringBuilder();
            foreach (var field in Entity.ClientProperty.Where(p => !p.NoneDetails && !p.ExtendConfigListBool["easyui_userFormHide"]).ToArray())
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
            return $@"
     <el-dialog title='{Entity.Caption}编辑'
                :visible.sync='form.visible'
                v-loading='form.loading'
                element-loading-text='正在处理'
                element-loading-spinner='el-icon-loading'
                element-loading-background='rgba(0, 0, 0, 0.8)'>
            <div class='el-dialog__body_form'>
            <el-form :model='form.data'
                     :rules='form.rules'
                     label-position='left'
                     label-width='100px'
                     ref='dataForm' 
                     @submit.native.prevent>{code}
            </el-form>
        </div>
        <div slot='footer'>
            <el-button icon='el-icon-close' @click='form.visible = false'>取消</el-button>
            <el-button icon='el-icon-check' @click='save' type='primary'>保存</el-button>
        </div>
    </el-dialog>";
        }
        private void FormField(StringBuilder code, PropertyConfig field, string caption, string description)
        {

            code.Append(
                field.FormCloumnSapn > 1 || field.IsMemo || field.MulitLine
                    ? $@"
            <el-form-item label='{caption}' prop='{field.JsonName}' size='large'>"
                        : $@"
            <el-form-item label='{caption}' prop='{field.JsonName}'>");
            if (field.EnumConfig != null)
            {
                code.Append($@"
                <el-select v-model='form.data.{field.JsonName}' style='width:100%'>");

                foreach (var item in field.EnumConfig.Items)
                {
                    if (item.Value.IndexOf("0X", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        item.Value = Convert.ToInt32(item.Value.Substring(2), 16).ToString();
                    }
                    code.Append($@"
                    <el-option :value='{item.Value}' label='{item.Caption}'></el-option>");
                }
                code.Append(@"
                </el-select>");
            }
            else if (field.CsType == nameof(DateTime))
            {
                code.Append(
                    field.IsTime
                     ? $@"
                <el-date-picker v-model='form.data.{field.JsonName}' placeholder='{description}'  value-format='yyyy-MM-ddTHH:mm:ss' type='datetime' style='width:100%' clearable></el-date-picker>"
                            : $@"
                <el-date-picker v-model='form.data.{field.JsonName}' placeholder='{description}'  value-format='yyyy-MM-dd' type='date' style='width:100%' clearable></el-date-picker>");
            }
            else if (field.IsLinkKey)
            {
                var entity = GlobalConfig.GetEntity(field.LinkTable);
                var name = entity?.Name.ToLWord();
                code.Append($@"
                <el-select v-model='form.data.{field.JsonName}' style='width:100%'>
                    <el-option value='0' label='无'></el-option>
                    <template v-for='item in {name}'>
                        <el-option :value='item.id' :label='item.text'></el-option>
                    </template>
                </el-select>");
            }
            else if (field.IsMemo || field.MulitLine)
            {
                code.Append($@"
                <el-input type='textarea' :rows='3' v-model='form.data.{field.JsonName}' placeholder='{description}' auto-complete='off' clearable></el-input>");
            }
            else
            {
                code.Append($@"
                <el-input v-model='form.data.{field.JsonName}' placeholder='{description}' auto-complete='off' clearable></el-input>");
            }

            code.Append(@"
            </el-form-item>");
        }

        #endregion
        #endregion
        #region 脚本
        public string ScriptCode()
        {
            var fields = Entity.ClientProperty.Where(p => !p.NoneDetails && !p.ExtendConfigListBool["easyui_userFormHide"]).ToArray();
            var form_default = DefaultValue(fields);
            var form_rules = Rules(fields);
            var functions = LinkFunctions(fields);
            return $@"
var defRow = {{
    selected : false{form_default}
}};
{form_rules}
{Filter()}
function doReady() {{
    try {{
        vue_option.data.apiPrefix = '{Project.ApiName}/{Entity.Abbreviation}/v1';
        vue_option.data.idField = '{Entity.PrimaryColumn.JsonName}';
        vue_option.data.form.def = defRow;
        vue_option.data.form.data = defRow;
        {(form_rules == null ? null : "vue_option.data.form.rules = rules; ")}
        globalOptions.api.customHost = globalOptions.api.{Project.ApiName}ApiHost;
        vueObject = new Vue(vue_option);
        vue_option.methods.loadList();
        
    }} catch (e) {{
        console.error(e);
    }}
}}
doReady();
{functions}";

        }
        #region 关联数据
        string LinkFunctions(PropertyConfig[] columns)
        {
            var array = columns.Where(p => p.IsLinkKey).Select(p => GlobalConfig.GetEntity(p.LinkTable)).Distinct().ToArray();
            if (array.Length == 0)
                return null;
            StringBuilder code = new StringBuilder();
            foreach (var entity in array)
            {
                code.Append($@"

function load{entity.Name}() {{
    call_ajax('载入{entity.Caption}','{Entity.Abbreviation}/v1/edit/combo',null,function(result) {{
        if (result.success) {{
            vue_option.data.{entity.Name.ToLWord()} = result.data;
        }}
    }}, null,'{entity.Parent.ApiName}');
}}
load{entity.Name}();");
            }
            return code.ToString();
        }
        #endregion
        #region 枚举
        string Filter()
        {
            var array = Entity.Properties.Where(p => p.EnumConfig != null && !p.IsSystemField && p.IsEnum).Select(p => p.EnumConfig).Distinct().ToArray();
            if (array.Length == 0)
                return null;
            StringBuilder code = new StringBuilder();
            code.Append("extend_filter({");
            bool first = true;
            foreach (var enu in array)
            {
                if (first)
                    first = false;
                else
                    code.AppendLine(",");
                code.Append(Filter(enu));
            }
            code.AppendLine();
            code.AppendLine("});");
            return code.ToString();
        }
        string Filter(EnumConfig config)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"
    //{config.Caption}枚举转文本
    {config.Name.ToLWord()}Formater(val) {{
        switch (val) {{");
            foreach (var item in config.Items)
            {
                code.Append($@"
            case {item.Value}: return '{item.Caption}';");
            }
            code.Append(@"
            default:return '错误';
        }
    }");
            return code.ToString();
        }
        #endregion
        #region 数据

        /// <summary>
        ///     生成Form录入字段界面
        /// </summary>
        /// <param name="columns"></param>
        private string DefaultValue(PropertyConfig[] columns)
        {
            var code = new StringBuilder();
            foreach (var field in columns)
            {
                if (field.CsType == "string" || field.CsType == nameof(DateTime) || field.Nullable)
                {
                    code.Append($@",
    {field.JsonName} : null");
                }
                else if (field.CsType == nameof(DateTime))
                {
                    code.Append($@",
    {field.JsonName} : '2012-12-24T23:59:59'");
                }
                else if (field.IsPrimaryKey || field.IsLinkKey || field.CsType == "long")
                {
                    code.Append($@",
    {field.JsonName} : '0'");
                }
                else if (field.CsType == "bool")
                {
                    code.Append($@",
    {field.JsonName} : false");
                }
                else
                {
                    code.Append($@",
    {field.JsonName} : 0");
                }
            }
            return code.ToString();
        }

        #endregion
        #region 规则

        /// <summary>
        ///     生成Form录入字段界面
        /// </summary>
        /// <param name="columns"></param>
        private string Rules(PropertyConfig[] columns)
        {
            bool first = true;
            var code = new StringBuilder();
            foreach (var field in columns)
            {
                var sub = new StringBuilder();
                var dot = "";
                if (field.IsRequired && field.CanUserInput)
                {
                    sub.Append($@"{dot}{{ required: true, message: '请输入{field.Caption}', trigger: 'blur' }}");
                    dot = @",
    ";
                }
                switch (field.CsType)
                {
                    case "string":
                        StringCheck(field, sub, ref dot);
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
                        NumberCheck(field, sub, ref dot);
                        break;
                    case "DateTime":
                        DateTimeCheck(field, sub, ref dot);
                        break;
                }
                if (sub.Length == 0)
                    continue;
                if (first)
                    first = false;
                else
                    code.Append(',');
                code.Append($@"
    '{field.JsonName}':[{sub}]");
            }
            return first ? null : $@"var rules = {{{code}
}};";
        }
        private static void DateTimeCheck(PropertyConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max != null && field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, max: {field.Max}, message: '时间从 {field.Min} 到 {field.Max} 之间', trigger: 'blur' }}");
            else if (field.Max != null)
                code.Append($@"{dot}{{ max: {field.Max}, message: '时间不大于 {field.Max}', trigger: 'blur' }}");
            else if (field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, message: '时间不小于 {field.Min}', trigger: 'blur' }}");
            else return;
            dot = @",
    ";
        }

        private static void NumberCheck(PropertyConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max != null && field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, max: {field.Max}, message: '数值从 {field.Min} 到 {field.Max} 之间', trigger: 'blur' }}");
            else if (field.Max != null)
                code.Append($@"{dot}{{ max: {field.Max}, message: '数值不大于 {field.Max}', trigger: 'blur' }}");
            else if (field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, message: '数值不小于 {field.Min}', trigger: 'blur' }}");
            else return;
            dot = @",
    ";
        }

        private static void StringCheck(PropertyConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max != null && field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, max: {field.Max}, message: '长度在 {field.Min} 到 {field.Max} 个字符', trigger: 'blur' }}");
            else if (field.Max != null)
                code.Append($@"{dot}{{ max: {field.Max}, message: '长度不大于 {field.Max} 个字符', trigger: 'blur' }}");
            else if (field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, message: '长度不小于 {field.Min} 个字符', trigger: 'blur' }}");
            else return;
            dot = @",
    ";
        }

        #endregion
        #endregion
    }
}