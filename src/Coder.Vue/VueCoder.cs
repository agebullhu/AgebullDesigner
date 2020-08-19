using System;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.VUE
{

    public class VueCoder
    {
        public EntityConfig Entity { get; set; }
        public ProjectConfig Project { get; set; }

        #region 页面
        #region HTML
        public string HtmlCode()
        {
            return $@"<!DOCTYPE html>
<html>
<head>
    <meta name='renderer' content='webkit'/>
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=21'/>
    <title>{Entity.Parent.Caption} > {Entity.Caption}</title>
    <meta charset='utf-8'/>
    <meta name='viewport' content='width=device-width'/>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8-bom'/>
    <!--font-awesome-->
    <link rel='stylesheet' type='text/css' href='http://cdn.staticfile.org/font-awesome/5.8.2/css/all.min.css'>
    <!--VUE-->
    <link rel='stylesheet' type='text/css' href='http://cdn.staticfile.org/element-ui/2.8.2/theme-chalk/index.css'>
    <script type='text/javascript' src='http://cdn.staticfile.org/vue/2.6.10/vue.js'></script>
    <script type='text/javascript' src='https://cdn.staticfile.org/axios/0.19.2/axios.min.js'></script>
    <script type='text/javascript' src='http://cdn.staticfile.org/element-ui/2.8.2/index.js'></script>
    <script type='text/javascript' src='http://cdn.staticfile.org/element-ui/2.8.2/locale/zh-CN.min.js'></script>
    <!--Extend-->
    <link rel='stylesheet' type='text/css' href='/styles/vuePage.css' />
    <script type='text/javascript' src='/scripts/extend/core.js'></script>
    <script type='text/javascript' src='/scripts/extend/ajax.js'></script>
    <script type='text/javascript' src='/scripts/extend/ajax_vue.js'></script>
    <script type='text/javascript' src='/scripts/extend/type.js'></script>
    <script type='text/javascript' src='/scripts/extend/vue_ex.js'></script>{HtmlStyle()}
</head>
<body>
<div id='work_space' class='tiled' v-cloak>
    <el-container>
        <el-header height='53px'>
            <div style='display: inline-block;padding:6px'>
                <span>{Entity.Parent.Caption}</span>&nbsp;<i class='el-icon-arrow-right'></i>&nbsp;<span>{Entity.Caption}</span>
                &nbsp;&nbsp;&nbsp;
            </div>{HtmlExButton()}
            <div style='display: inline-block; float: right'>
                <el-input placeholder='请输入搜索内容' v-model='list.keyWords' class='input-with-select' clearable>{QueryList()}
                    <el-button slot='append' icon='el-icon-search' @click='doQuery'></el-button>
                </el-input>
            </div>
        </el-header>
        <el-main>{HtmlMainCode()}
        </el-main>
        <el-footer height='42px'>
            <el-pagination @size-change='sizeChange'
                           @current-change='pageChange'
                           background
                           layout='total, sizes, prev, pager, next, jumper'
                           :current-page='list.page'
                           :page-sizes='list.pageSizes'
                           :page-size='list.pageSize'
                           :total='list.total'>
            </el-pagination>
        </el-footer>
    </el-container>{HtmlFormCode()}
</div>
<script type='text/javascript' src='script.js'></script>
</body>
</html>";
        }

        string HtmlStyle()
        {
            if (Entity.IsUiReadOnly || Entity.FormCloumn > 1)
                return null;
            return @"
    <style>
        .el-dialog {
            width: 480px;
        }
        .el-dialog__body {
            width: 440px;
        }
        .el-dialog__body_form {
            width: 410px;
        }
    </style>";
        }
        string HtmlExButton()
        {
            StringBuilder exButton = new StringBuilder();
            exButton.Append(@"
            <div style='display: inline-block;'>
                <el-button-group>
                   <el-button icon='el-icon-refresh' type='success' @click='loadList'>刷新</el-button>
                   <!--el-button icon='fa fa-file-excel-o' type='success' @click='exportExcel'>导出</el-button-->");
            if (!Entity.IsUiReadOnly)
            {
                exButton.Append(@"
                        <el-button icon='el-icon-circle-plus-outline' type='primary' @click='doAddNew'>新增</el-button>
                        <el-button icon='el-icon-edit' type='success' @click='doEdit'>编辑</el-button>
                        <el-button icon='el-icon-circle-close' type='info' @click='doDelete'>删除</el-button>");
            }
            exButton.Append(@"
                </el-button-group>");
            if (!Entity.IsUiReadOnly)
            {
                if (Entity.Interfaces.Contains("IAuditData"))
                {
                    exButton.Append(@"
                    <el-dropdown split-button type='primary' @click='doPass' @command='handleDataCommand'>
                        <i class='el-icon-circle-check'></i>审核通过
                        <el-dropdown-menu slot='dropdown'>
                            <el-dropdown-item command='Enable'><i class='el-icon-circle-check'></i>启用</el-dropdown-item>
                            <el-dropdown-item command='Disable'><i class='el-icon-remove-outline'></i>禁用</el-dropdown-item>
                            <el-dropdown-item command='Deny'><i class='el-icon-remove-outline'></i>不通过</el-dropdown-item>
                            <el-dropdown-item command='Back'><i class='el-icon-edit'></i>退回编辑</el-dropdown-item>
                            <el-dropdown-item command='ReDo'><i class='el-icon-edit'></i>重新审核</el-dropdown-item>
                        </el-dropdown-menu>
                    </el-dropdown>");
                }
                else if (Entity.Interfaces.Contains("IStateData"))
                {
                    exButton.Append(@"
                    <el-dropdown split-button type='primary' @click='doEnable'>
                        <i class='el-icon-circle-check'></i>启用
                        <el-dropdown-menu slot='dropdown' style='padding:3px;background-color:#606266'>
                            <el-dropdown-item style='padding:3px;background-color:#606266'>
                                <el-button icon='el-icon-remove-outline' type='info' @click='doDisable'>禁用</el-button>
                            </el-dropdown-item>
                            <el-dropdown-item style='padding:3px;background-color:#606266'>
                                <el-button icon='el-icon-remove-outline' type='success' @click='doReset'>重置</el-button>
                            </el-dropdown-item>
                        </el-dropdown-menu>
                    </el-dropdown>");
                }
            }
            exButton.Append(@"
            </div>");
            return exButton.ToString();
        }
        #endregion
        #region 表格
        public string HtmlMainCode()
        {
            return HtmlGridCode();

        }

        #endregion
        #region 表格

        public string HtmlGridCode()
        {
            var code = new StringBuilder();
            code.Append(@"
            <!-- Grid -->
            <el-table  :data='list.rows'
                        border
                        ref='dataTable'
                        highlight-current-row
                        @sort-change='onSort'
                        @row-dblclick='dblclick'
                        @current-change='currentRowChange'
                        @selection-change='selectionRowChange'
                        style='width:99%'");
            if (Entity.Interfaces.Contains("IInnerTree") && Entity.Properties.Any(p => p.Name == "ParentId"))
            {
                code.Append($@"
                        lazy row-key='{Entity.PrimaryColumn.JsonName}' :load = 'load'");
            }
            code.Append(@">
                <el-table-column type='selection'
                                 align='center'
                                 header-align='center'>
                </el-table-column>");
            if (Entity.Interfaces.Contains("IStateData"))
            {
                code.Append(stateColumn);
            }
            foreach (var field in Entity.ClientProperty.Where(p => !p.NoneGrid && !p.GridDetails).ToArray())
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
            GridDetailsField(code);
            code.Append(@"
            </el-table>");
            return code.ToString();
        }

        const string stateColumn = @"
                <el-table-column label='状态'
                                 align='center'
                                 header-align='center'
                                 width='50'>
                    <template slot-scope='scope'>
                        <i :class='scope.row.dataState | dataStateIcon'></i>
                    </template>
                </el-table-column>";


        private void GridField(StringBuilder code, PropertyConfig field, string caption, string description)
        {
            var align = string.IsNullOrWhiteSpace(field.GridAlign) ? "left" : field.GridAlign;
            code.Append($@"
                <el-table-column prop='{field.JsonName}'
                                    header-align='center' align='{align}'
                                    label='{caption}'");
            if (field.UserOrder)
                code.Append(" sortable='true'");
            if (field.GridWidth > 0)
                code.Append($" width={field.GridWidth}");
            code.Append(">");
            if (field.EnumConfig != null)
            {
                code.Append($@"
                    <template slot-scope='scope'>
                        <span style='margin-left: 3px'>
                            {field.Prefix}{{{{scope.row.{field.JsonName} | {field.EnumConfig.Name.ToLWord()}Formater}}}}{field.Suffix}
                        </span>
                    </template>");
            }
            else if (field.CsType == nameof(DateTime))
            {
                var fmt = field.IsTime ? "formatTime" : "formatDate";
                code.Append($@"
                    <template slot-scope='scope'>
                        <span style='margin-left: 3px'>{field.Prefix}{{{{scope.row.{field.JsonName} | {fmt}}}}}{field.Suffix}</span>
                    </template>");
            }
            else if (field.CsType == "bool")
            {
                code.Append($@"
                    <template slot-scope='scope'>
                        <span style='margin-left: 3px'>{field.Prefix}{{{{scope.row.{field.JsonName} | boolFormater}}}}{field.Suffix}</span>
                    </template>");
        }
            else if (field.IsMoney)
            {
                code.Append($@"
                    <template slot-scope='scope'>
                        <span style='margin-left: 3px'>{field.Prefix}{{{{scope.row.{field.JsonName} | formatMoney}}}}{field.Suffix}</span>
                    </template>");
        }
            else if (field.CsType == "decimal")
            {
                code.Append($@"
                    <template slot-scope='scope'>
                        <span style='margin-left: 3px'>{field.Prefix}{{{{scope.row.{field.JsonName} | thousandsNumber}}}}{field.Suffix}</span>
                    </template>");
        }
        code.Append(@"
                </el-table-column>");
        }

        private void GridDetailsField(StringBuilder code)
        {
            var details = Entity.ClientProperty.Where(p => p.GridDetails).ToArray();
            if (details.Length <= 0)
                return;
            code.Append(@"
                    <el-table-column type='expand'>
                        <template slot-scope='props'>
                            <el-form label-position='left' inline>");
            foreach (var field in details)
            {
                var caption = field.Caption;
                if (field.IsLinkKey)
                {
                    var friend =
                        Entity.LastProperties.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
                    if (friend != null)
                        caption = friend.Caption;
                }
                code.Append($@"
                            <el-form-item label='{caption}' prop='{field.JsonName}'");
                if (field.FormCloumnSapn > 1 || field.IsMemo || field.MulitLine)
                    code.Append(" size='large'");

                code.Append(@">
                                ");
                if (field.IsImage)
                {
                    code.Append($@"<el-image :src='{field.JsonName}' lazy></el-image>");
                }
                else
                {
                    code.Append($@"<span>{field.Prefix}{{{{props.row.{field.JsonName}");
                    if (field.EnumConfig != null)
                    {
                        code.Append($@" | {field.EnumConfig.Name.ToLWord()}Formater");
                    }
                    else if (field.CsType == nameof(DateTime))
                    {
                        var fmt = field.IsTime ? "formatTime" : "formatDate";
                        code.Append($@" | {fmt}");
                    }
                    else if (field.CsType == "bool")
                    {
                        code.Append(@" | boolFormater");
                    }
                    else if (field.IsMoney)
                    {
                        code.Append(@" | formatMoney");
                    }
                    else if (field.CsType == "decimal")
                    {
                        code.Append(@" | thousandsNumber");
                    }
                    code.Append($@"}}}}{field.Suffix}</span>");
                }

                code.Append(@"
                            </el-form-item>");
            }

            code.Append(@"
                        </el-form>
                    </template>
                </el-table-column>");
        }

        #endregion
        #region 表单

        public string HtmlFormCode()
        {
            if (Entity.IsUiReadOnly)
                return null;
            var code = new StringBuilder();
            code.Append($@"
     <!--Form-->
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
                     @submit.native.prevent");
            if (Entity.IsUiReadOnly)
                code.Append(@"
                     disabled");

            code.Append('>');
            //string vif = null;
            //if (Entity.Interfaces.Contains("IAuditData"))
            //{
            //    vif = "form.data.auditState <= 1";
            //}
            //else if (Entity.Interfaces.Contains("IStateData"))
            //{
            //    vif = "form.data.dataState <= 1";
            //}
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
                FormField(code, field, caption, description ?? field.Caption/*,vif*/);
            }
            code.Append(@"
            </el-form>
        </div>");
            if (!Entity.IsUiReadOnly)
            {
                code.Append(@"
        <div slot='footer' v-if='!form.readonly'>
            <el-button icon='el-icon-close' @click='form.visible = false'>取消</el-button>
            <el-button icon='el-icon-check' @click='save' type='primary'>保存</el-button>
        </div>");
            }
            code.Append(@"
    </el-dialog>");
            return code.ToString();
        }

        void SetDisabled(bool disabled, StringBuilder code, PropertyConfig field)
        {
            if (disabled)
            {
                code.Append(field.IsUserReadOnly ? " disabled" : " :disabled='form.readonly'");
            }
            else
            {
                code.Append(field.IsUserReadOnly ? " readonly" : " :readonly='form.readonly'");
            }
        }
        private void FormField(StringBuilder code, PropertyConfig field, string caption, string description/*,string vif*/)
        {
            code.Append($@"
            <el-form-item label='{caption}' prop='{field.JsonName}' label-suffix='{field.Suffix}'");
            if (field.FormCloumnSapn > 1 || field.IsMemo || field.MulitLine)
                code.Append(" size='large'");
            code.Append('>');
            if (field.EnumConfig != null || field.IsLinkKey)
            {
                code.Append($@"
                <el-select v-model='form.data.{field.JsonName}'");
                SetDisabled(true, code, field);
                code.Append(@" style='width:100%'>");
                if (field.EnumConfig != null)
                {
                    code.Append($@"
                    <el-option v-for='item in types.{field.EnumConfig.Name.ToLWord()}'
                               :key='item.value'
                               :label='item.label'
                               :value='item.value'>
                    </el-option>");
                }
                else if (field.IsLinkKey)
                {
                    var name = GlobalConfig.GetEntity(field.LinkTable)?.Name.ToLWord();
                    code.Append($@"
                    <el-option :value='0' label='无'></el-option>
                    <template v-for='item in {name}'>
                        <el-option :value='item.id' :label='item.text'></el-option>
                    </template>");
                }
                code.Append(@"
                </el-select>");
            }
            else if (field.CsType == "bool")
            {
                code.Append($@"
                <el-switch v-model='form.data.{field.JsonName}'");
                SetDisabled(true, code, field);
                code.Append(@"></el-switch>");
            }
            else if (field.CsType == nameof(DateTime))
            {
                code.Append($@"
                <el-date-picker v-model='form.data.{field.JsonName}' placeholder='{description}' clearable");
                SetDisabled(false, code, field);
                code.Append(field.IsTime
                    ? "value-format='yyyy-MM-ddTHH:mm:ss' type='datetime'"
                    : "value-format='yyyy-MM-dd' type='date'");
                code.Append(@" style='width:100%'></el-date-picker>");
            }
            else
            {
                code.Append($@"
                <el-input v-model='form.data.{field.JsonName}' placeholder='{description}' auto-complete='off' clearable");
                SetDisabled(false, code, field);
                if (field.IsMemo || field.MulitLine)
                {
                    code.Append(" type='textarea' :rows='3'");
                }
                code.Append("></el-input>");
            }
            code.Append(@"
            </el-form-item>");
        }

        #endregion
        #region 查询

        string QueryList()
        {
            StringBuilder quButton = new StringBuilder();
            //if (Entity.Interfaces.Contains("IStateData"))
            //{
            //    quButton.Append(satateQuery);
            //}
            //if (Entity.Interfaces.Contains("IAuditData"))
            //{
            //    quButton.Append(auditQuery);
            //}
            quButton.Append(@"
                    <el-select v-model='list.field' slot='prepend' placeholder='选择字段' style='width: 160px;'>
                        <el-option value='_any_' label='模糊查询'></el-option>");
            foreach (var field in Entity.ClientProperty.Where(p => !p.NoStorage
                                                                    && !p.NoneGrid
                                                                    && !p.NoneDetails
                                                                    && !p.IsLinkKey
                                                                    && !p.IsLinkKey))
            {
                quButton.Append($@"
                        <el-option value='{field.JsonName}' label='{field.Caption}'></el-option>");
            }
            quButton.Append(@"
                    </el-select>");
            return quButton.ToString();
        }

        #endregion
        #endregion
        #region 脚本
        public string ScriptCode()
        {
            var fields = Entity.ClientProperty.Where(p => p.CanUserInput).ToArray();
            var form_rules = Rules(fields);
            return $@"
function createEntity() {{
    return {{
        selected: false{DefaultValue()}
    }};
}}

function checkEntity(row) {{{CheckValue()}
}}
extend_methods({{
    getDef() {{
        return createEntity();
    }},
    checkListData(row) {{
        checkEntity(row);
    }}
}});
{form_rules}
{LinkFunctions(fields)}
{EnumScript()}
{Filter()}
{TreeScript()}
function doReady() {{
    try {{
        vue_option.data.apiPrefix = '/{Project.ApiName}/{Entity.ApiName}/v1';
        vue_option.data.idField = '{Entity.PrimaryColumn.JsonName}';
        var data = createEntity();
        vue_option.data.form.def = data
        vue_option.data.form.data = data;
        {(form_rules == null ? null : "vue_option.data.form.rules = rules; ")}

        vueObject = new Vue(vue_option);
        vue_option.methods.loadList();
        
    }} catch (e) {{
        console.error(e);
    }}
}}
doReady();";

        }
        #region 关联数据
        string TreeScript()
        {
            if (!Entity.Interfaces.Contains("IInnerTree"))
                return null;
            var par = Entity.Properties.FirstOrDefault(p => p.Name == "ParentId");
            if (par == null)
                return null;
            return $@"vue_option.data.list.{par.JsonName} = 0;
extend_methods({{
    getQueryArgs() {{
        return {{
            _field_: vue_option.data.list.field,
            _value_: vue_option.data.list.keyWords, 
            _state_: vue_option.data.list.dataState,
            _audit_: vue_option.data.list.audit,
            _page_: vue_option.data.list.page,
            _size_: vue_option.data.list.pageSize,
            _sort_: vue_option.data.list.sort,
            _order_: vue_option.data.list.order
            {par.JsonName}: vue_option.data.list.{par.JsonName}
        }};
    }},
    checkListData(row) {{
        if (typeof row.hasChildren === 'undefined')
            row.hasChildren = true;
    }},
    load(row, treeNode, resolve) {{
        this.list.{par.JsonName} = row.{Entity.PrimaryColumn.JsonName};
        this.list.keyWords = null;
        this.list.page = 0;
        this.list.sort = null;
        this.list.order = 'asc';
        this.loadList((data) => {{
            resolve(data.rows);
            this.list.{par.JsonName} = 0;
        }});
    }}
}});";
        }
        string LinkFunctions(PropertyConfig[] columns)
        {
            if (Entity.IsUiReadOnly)
                return null;
            var array = columns.Where(p => p.IsLinkCaption).Select(p => GlobalConfig.GetEntity(p.LinkTable)).Distinct().ToArray();
            if (array.Length == 0)
                return null;
            StringBuilder code = new StringBuilder();
            foreach (var entity in array.Where(p => p.Properties.Any(a => a.IsCaption)))
            {
                code.Append($@"

function load{entity.Name}() {{
    vue_option.data.{entity.Name.ToLWord()} = [];
    call_ajax('载入{entity.Caption}','{entity.Parent.Abbreviation}/{entity.Abbreviation}/v1/edit/combo',null,function(result) {{
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

        string EnumScript()
        {
            var array = Entity.Properties.Where(p => p.EnumConfig != null && !p.IsSystemField && p.IsEnum).Select(p => p.EnumConfig).Distinct().ToArray();
            if (array.Length == 0)
                return null;
            var code = new StringBuilder();
            code.AppendLine(@"
/**
*   枚举列表
*/
extend_data({
    types : {");
            bool enumFirst = true;
            foreach (var enumConfig in array)
            {
                if (enumFirst)
                    enumFirst = false;
                else
                    code.Append(",");

                code.Append($@"
        /**
        *   {enumConfig.Caption}
        */
        {enumConfig.Name.ToLWord()} : [");
                bool first = true;
                foreach (var item in enumConfig.Items)
                {
                    if (first)
                        first = false;
                    else
                        code.Append(",");
                    code.Append($@"
            {{
                key: '{item.Value}',
                value: '{item.Name}',
                label: '{item.Caption}'
            }}");
                }
                code.Append(@"
        ]");
            }
            code.Append(@"
    }
});");
            return code.ToString();
        }

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
    /**
    *   {config.Caption}枚举转文本
    */
    {config.Name.ToLWord()}Formater(val) {{
        switch (val) {{");
            string def = "错误";
            foreach (var item in config.Items)
            {
                if (item.Value == "0")
                    def = item.Caption;
                code.Append($@"
            case '{item.Name}': return '{item.Caption}';");
            }
            code.Append($@"
            default:
                return '{def}';
        }}
    }}");
            return code.ToString();
        }
        #endregion
        #region 数据

        /// <summary>
        ///     缺省空对象
        /// </summary>
        private string DefaultValue()
        {
            bool isInner = Entity.Interfaces.Contains("IInnerTree");
            var code = new StringBuilder();
            foreach (var field in Entity.ClientProperty.Where(p => !p.IsSystemField))
            {
                if (isInner && field.Name == "ParentId")
                {
                    code.Append($@",
        {field.JsonName} : !this.currentRow ? 0 : this.currentRow.{Entity.PrimaryColumn.JsonName}");
                }
                else if (field.CsType == "string" || field.CsType == nameof(DateTime) || field.Nullable)
                {
                    code.Append($@",
        {field.JsonName} : ''");
                }
                else if (field.IsEnum)
                {
                    code.Append($@",
        {field.JsonName} : '{field.EnumConfig.Items.FirstOrDefault()?.Name}'");
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

        /// <summary>
        ///     检查字段存在
        /// </summary>
        private string CheckValue()
        {
            var code = new StringBuilder();
            foreach (var field in Entity.ClientProperty)
            {
                code.Append($@"
        if (typeof row.{field.JsonName} === 'undefined')
            row.{field.JsonName} = ");
                if (field.CsType == "string" || field.CsType == nameof(DateTime) || field.Nullable)
                {
                    code.Append("'';");
                }
                else if (field.CsType == "bool")
                {
                    code.Append("false;");
                }
                else if (field.IsEnum)
                {
                    code.Append($"'{field.EnumConfig.Items.FirstOrDefault()?.Name}';");
                }
                else
                {
                    code.Append("0;");
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
            if (Entity.IsUiReadOnly)
                return null;
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