using System;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.VUE
{

    public class VueCoder<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        public TModel Model { get; set; }
        public ProjectConfig Project { get; set; }

        #region ҳ��
        #region HTML
        public string HtmlCode()
        {
            return $@"<!DOCTYPE html>
<html>
<head>
    <meta name='renderer' content='webkit'/>
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=21'/>
    <title>{Model.Parent.Caption} > {Model.Caption}</title>
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
    <link rel='stylesheet' type='text/css' href='/styles/element-ui.css' />
    <script type='text/javascript' src='/scripts/extend.js'></script>
    <script type='text/javascript' src='/scripts/object.js'></script>
</head>
<body>
    <div id='work_space' class='tiled' v-cloak>
        <el-container>
            <el-header height='53px'>
                <div style='display: inline-block;padding:6px'>
                    <span>{Model.Parent.Caption}</span>&nbsp;<i class='el-icon-arrow-right'></i>&nbsp;<span>{Model.Caption}</span>
                    &nbsp;&nbsp;&nbsp;
                </div>
                <div class='toolRange'>
                    <el-input placeholder='��������������' v-model='list.keyWords' class='input-with-select' clearable>{QueryList()}
                        <el-button slot='append' icon='el-icon-search' @click='doQuery'></el-button>
                    </el-input>
                </div>{HtmlExButton()}
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

        string HtmlExButton()
        {
            StringBuilder exButton = new StringBuilder();
            exButton.Append(@"
                <div class='toolRange'>
                    <el-button-group>
                        <el-button icon='el-icon-refresh' @click='loadList'>ˢ��</el-button>
                        <!--el-button icon='fa fa-file-excel-o' type='success' @click='exportExcel'>����</el-button-->");
            if (!Model.IsUiReadOnly)
            {
                exButton.Append(@"
                        <el-button icon='el-icon-plus' @click='doAddNew'>����</el-button>
                        <el-button icon='el-icon-edit' @click='doEdit'>�༭</el-button>
                        <el-button icon='el-icon-close' @click='doDelete'>ɾ��</el-button>");
            }
            exButton.Append(@"
                    </el-button-group>");
            if (!Model.IsUiReadOnly)
            {
                if (Model.Interfaces.Contains("IAuditData"))
                {
                    exButton.Append(@"
                    <el-dropdown split-button type='primary' @click='doPass' @command='handleDataCommand'>
                        <i class='el-icon-circle-check'></i>���ͨ��
                        <el-dropdown-menu slot='dropdown'>
                            <el-dropdown-item command='Enable' icon='el-icon-video-play'>����</el-dropdown-item>
                            <el-dropdown-item command='Disable' icon='el-icon-video-pause'>����</el-dropdown-item>
                            <el-dropdown-item command='Reset' icon='el-icon-edit'>����</el-dropdown-item>
                            <el-dropdown-item command='Deny' icon='el-icon-remove-outline'>��ͨ��</el-dropdown-item>
                            <el-dropdown-item command='Back' icon='el-icon-edit'>�˻ر༭</el-dropdown-item>
                            <el-dropdown-item command='ReDo' icon='el-icon-edit'>�������</el-dropdown-item>
                        </el-dropdown-menu>
                    </el-dropdown>");
                }
                else if (Model.Interfaces.Contains("IStateData"))
                {
                    exButton.Append(@"
                    <el-dropdown split-button @click='doEnable'>
                        <i class='el-icon-video-play'></i>����
                        <el-dropdown-menu slot='dropdown'>
                            <el-dropdown-item command='Disable' icon='el-icon-video-pause'>����</el-dropdown-item>
                            <el-dropdown-item command='Reset' icon='el-icon-edit'>����</el-dropdown-item>
                        </el-dropdown-menu>
                    </el-dropdown>");
                }
            }
            exButton.Append(@"
            </div>");
            return exButton.ToString();
        }
        #endregion
        #region ���
        public string HtmlMainCode()
        {
            return HtmlGridCode();

        }

        #endregion
        #region ���

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
            if (Model.Interfaces.Contains("IInnerTree") && Model.LastProperties.Any(p => p.Name == "ParentId"))
            {
                code.Append($@"
                        lazy row-key='{Model.PrimaryColumn.JsonName}' :load = 'load'");
            }
            code.Append(@">
                <el-table-column type='selection'
                                 align='center'
                                 header-align='center'>
                </el-table-column>");
            if (Model.Interfaces.Contains("IStateData"))
            {
                code.Append(stateColumn);
            }
            foreach (var property in Model.ClientProperty.Where(p => !p.NoneGrid && !p.GridDetails).ToArray())
            {
                var caption = property.Caption;
                var description = property.Description;

                if (property.IsLinkKey)
                {
                    var friend = Model.LastProperties.FirstOrDefault(p => p.LinkTable == property.LinkTable && p.IsLinkCaption);
                    if (friend != null)
                        caption = friend.Caption;
                    if (friend != null)
                        description = friend.Description;
                }
                GridField(code, property, caption, description ?? property.Caption);
            }
            GridDetailsField(code);
            code.Append(@"
            </el-table>");
            return code.ToString();
        }

        const string stateColumn = @"
                <el-table-column label='״̬'
                                 align='center'
                                 header-align='center'
                                 width='50'>
                    <template slot-scope='scope'>
                        <i :class='scope.row.dataState | dataStateIcon'></i>
                    </template>
                </el-table-column>";


        private void GridField(StringBuilder code, IFieldConfig property, string caption, string description)
        {

            var field = property;
            var align = string.IsNullOrWhiteSpace(field.GridAlign) ? "left" : field.GridAlign;
            code.Append($@"
                <el-table-column prop='{property.JsonName}'
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
                            {field.Prefix}{{{{scope.row.{property.JsonName} | {field.EnumConfig.Name.ToLWord()}Formater}}}}{field.Suffix}
                        </span>
                    </template>");
            }
            else if (field.CsType == nameof(DateTime))
            {
                var fmt = field.IsTime ? "formatTime" : "formatDate";
                code.Append($@"
                    <template slot-scope='scope'>
                        <span style='margin-left: 3px'>{field.Prefix}{{{{scope.row.{property.JsonName} | {fmt}}}}}{field.Suffix}</span>
                    </template>");
            }
            else if (field.CsType == "bool")
            {
                code.Append($@"
                    <template slot-scope='scope'>
                        <span style='margin-left: 3px'>{field.Prefix}{{{{scope.row.{property.JsonName} | boolFormater}}}}{field.Suffix}</span>
                    </template>");
            }
            else if (field.IsMoney)
            {
                code.Append($@"
                    <template slot-scope='scope'>
                        <span style='margin-left: 3px'>{field.Prefix}{{{{scope.row.{property.JsonName} | formatMoney}}}}{field.Suffix}</span>
                    </template>");
            }
            else if (field.CsType == "decimal")
            {
                code.Append($@"
                    <template slot-scope='scope'>
                        <span style='margin-left: 3px'>{field.Prefix}{{{{scope.row.{property.JsonName} | thousandsNumber}}}}{field.Suffix}</span>
                    </template>");
            }
            code.Append(@"
                </el-table-column>");
        }

        private void GridDetailsField(StringBuilder code)
        {
            var details = Model.ClientProperty.Where(p => p.GridDetails).ToArray();
            if (details.Length <= 0)
                return;
            code.Append(@"
                    <el-table-column type='expand'>
                        <template slot-scope='props'>");
            foreach (var property in details)
            {
                var field = property;
                var caption = property.Caption;
                if (field.IsLinkKey)
                {
                    var friend =
                        Model.LastProperties.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
                    if (friend != null)
                        caption = friend.Caption;
                }
                if (field.IsMemo || field.MulitLine)
                    code.Append($@"
                            <div class='expand_line_block'>");
                else
                {
                    var sp = field.FormCloumnSapn <= 0
                        ? 1
                        : field.FormCloumnSapn >= 4
                            ? 4
                            : field.FormCloumnSapn;
                    code.Append($@"
                            <div class='expand_block_{sp}'>");

                }
                code.Append($@"
                                <label class='expand_label'>{caption}��</label>");

                if (field.IsImage)
                {
                    code.Append($@"
                                <el-image :src='{property.JsonName}' lazy></el-image>");
                }
                else
                {
                    code.Append($@"
                                <span class='expand_value'>{field.Prefix}{{{{props.row.{property.JsonName}");
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
                code.Append($@"
                            </div>");
            }

            code.Append(@"
                        </template>
                    </el-table-column>");
        }

        #endregion
        #region ��

        public string HtmlFormCode()
        {
            if (Model.IsUiReadOnly)
                return null;
            var col = Model.FormCloumn <= 1 ? 1 : Model.FormCloumn;

            var wid = col * 422 + 50;
            var code = new StringBuilder();
            code.Append($@"
     <!--Form-->
     <el-dialog title='��{Model.Caption}���༭'
                :visible.sync='form.visible'
                :show-close='false'
                :close-on-click-modal='false'
                width='{wid}px'
                v-loading='form.loading'
                element-loading-text='���ڴ���'
                element-loading-spinner='el-icon-loading'
                element-loading-background='rgba(0, 0, 0, 0.8)'>
        <div class='el-dialog__body_form'>
            <el-form ref='dataForm' 
                     :rules='form.rules'
                     :model='form.data'
                     label-width='100px'
                     label-position='left'
                     @submit.native.prevent");
            if (Model.IsUiReadOnly)
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
            foreach (var property in Model.ClientProperty.Where(p => !p.NoneDetails && !p.ExtendConfigListBool["easyui_userFormHide"]).ToArray())
            {
                var field = property;
                var caption = property.Caption;
                var description = field.Description;

                if (field.IsLinkKey)
                {
                    var friend = Model.LastProperties.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
                    if (friend != null)
                        caption = friend.Caption;
                    if (friend != null)
                        description = friend.Description;
                }
                FormField(code, property, caption, description ?? property.Caption/*,vif*/);
            }
            code.Append(@"
            </el-form>
        </div>
        <div slot='footer'>");
            if (!Model.IsUiReadOnly)
            {
                code.Append(@"
            <el-button icon='el-icon-check' @click='save' type='primary' v-if='!form.readonly'>����</el-button>");
            }
            code.Append(@"
            <el-button icon='el-icon-close' @click='form.visible = false'>ȡ��</el-button>
        </div>
    </el-dialog>");
            return code.ToString();
        }

        void SetDisabled(bool disabled, StringBuilder code, IFieldConfig field)
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
        private void FormField(StringBuilder code, IFieldConfig property, string caption, string description/*,string vif*/)
        {
            var field = property;
            code.Append($@"
            <el-form-item label='{caption}' prop='{property.JsonName}' label-suffix='{field.Suffix}'");
            if (field.Entity.FormCloumn > 1 && (field.FormCloumnSapn > 1 || field.IsMemo || field.MulitLine))
                code.Append(" size='large'");
            code.Append('>');
            if (field.EnumConfig != null || field.IsLinkKey)
            {
                code.Append($@"
                <el-select v-model='form.data.{property.JsonName}'");
                SetDisabled(true, code, property);
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
                    <el-option :value='0' label='��'></el-option>
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
                <el-switch v-model='form.data.{property.JsonName}'");
                SetDisabled(true, code, field);
                code.Append(@"></el-switch>");
            }
            else if (field.CsType == nameof(DateTime))
            {
                code.Append($@"
                <el-date-picker v-model='form.data.{property.JsonName}' placeholder='{description}' clearable");
                SetDisabled(false, code, field);
                code.Append(field.IsTime
                    ? "value-format='yyyy-MM-ddTHH:mm:ss' type='datetime'"
                    : "value-format='yyyy-MM-dd' type='date'");
                code.Append(@" style='width:100%'></el-date-picker>");
            }
            else
            {
                code.Append($@"
                <el-input v-model='form.data.{property.JsonName}' placeholder='{description}' auto-complete='off' clearable");
                SetDisabled(false, code, field);
                if (field.IsMemo || field.MulitLine)
                {
                    code.Append(" type='textarea' :rows='5'");
                }
                code.Append("></el-input>");
            }
            code.Append(@"
            </el-form-item>");
        }

        #endregion
        #region ��ѯ

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
                    <el-select v-model='list.field' slot='prepend' placeholder='ѡ���ֶ�' style='width: 160px;'>
                        <el-option value='_any_' label='ģ����ѯ'></el-option>");
            foreach (var property in Model.ClientProperty.Where(p => !p.NoStorage
                                                                    && !p.NoneGrid
                                                                    && !p.NoneDetails
                                                                    && !p.IsLinkKey
                                                                    && !p.IsLinkKey))
            {
                quButton.Append($@"
                        <el-option value='{property.JsonName}' label='{property.Caption}'></el-option>");
            }
            quButton.Append(@"
                    </el-select>");
            return quButton.ToString();
        }

        #endregion
        #endregion

        #region �ű�
        public string ScriptCode()
        {
            var form_rules = Rules();
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
{LinkFunctions()}
{EnumScript()}
{Filter()}
{TreeScript()}
function doReady() {{
    try {{
        vue_option.data.apiPrefix = '/{Project.ApiName}/{Model.ApiName}/v1';
        vue_option.data.idField = '{Model.PrimaryColumn.JsonName}';
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
        #region ��������
        string TreeScript()
        {
            if (!Model.Interfaces.Contains("IInnerTree"))
                return null;
            var par = Model.LastProperties.FirstOrDefault(p => p.Name == "ParentId");
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
        this.list.{par.JsonName} = row.{Model.PrimaryColumn.JsonName};
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
        string LinkFunctions()
        {
            if (Model.IsUiReadOnly)
                return null;
            var array = Model.ClientProperty
                .Where(p => p.CanUserInput && p.IsLinkCaption)
                .Select(p => GlobalConfig.GetEntity(p.LinkTable))
                .Distinct().ToArray();
            if (array.Length == 0)
                return null;
            StringBuilder code = new StringBuilder();
            foreach (var entity in array.Where(p => p.Properties.Any(a => a.IsCaption)))
            {
                code.Append($@"

function load{entity.Name}() {{
    vue_option.data.{entity.Name.ToLWord()} = [];
    call_ajax('����{entity.Caption}','{entity.Parent.Abbreviation}/{entity.Abbreviation}/v1/edit/combo',null,function(result) {{
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
        #region ö��

        string EnumScript()
        {
            var array = Model.LastProperties.Where(p => p.EnumConfig != null && !p.IsSystemField && p.IsEnum).Select(p => p.EnumConfig).Distinct().ToArray();
            if (array.Length == 0)
                return null;
            var code = new StringBuilder();
            code.AppendLine(@"
/**
*   ö���б�
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
            var array = Model.LastProperties.Where(p => p.EnumConfig != null && !p.IsSystemField && p.IsEnum).Select(p => p.EnumConfig).Distinct().ToArray();
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
    *   {config.Caption}ö��ת�ı�
    */
    {config.Name.ToLWord()}Formater(val) {{
        switch (val) {{");
            string def = "����";
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
        #region ����

        /// <summary>
        ///     ȱʡ�ն���
        /// </summary>
        private string DefaultValue()
        {
            bool isInner = Model.Interfaces.Contains("IInnerTree");
            var code = new StringBuilder();
           
            foreach (var property in Model.LastProperties.Where(p => !p.IsSystemField && !p.NoneJson))
            {
                var field = property;
                if (isInner && field.Name == "ParentId")
                {
                    code.Append($@",
        {property.JsonName} : !this.currentRow ? 0 : this.currentRow.{Model.PrimaryColumn.JsonName}");
                }
                else if (field.CsType == "string" || field.CsType == nameof(DateTime) || field.Nullable)
                {
                    code.Append($@",
        {property.JsonName} : ''");
                }
                else if (field.IsEnum)
                {
                    code.Append($@",
        {property.JsonName} : '{field.EnumConfig.Items.FirstOrDefault()?.Name}'");
                }
                else if (field.CsType == "bool")
                {
                    code.Append($@",
        {property.JsonName} : false");
                }
                else
                {
                    code.Append($@",
        {property.JsonName} : 0");
                }
            }
            return code.ToString();
        }

        /// <summary>
        ///     ����ֶδ���
        /// </summary>
        private string CheckValue()
        {
            var code = new StringBuilder();
            foreach (var property in Model.LastProperties.Where(p => !p.IsSystemField && !p.NoneJson))
            {
                var field = property;
                code.Append($@"
        if (typeof row.{property.JsonName} === 'undefined')
            row.{property.JsonName} = ");
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
        #region ����

        /// <summary>
        ///     ����Form¼���ֶν���
        /// </summary>
        /// <param name="columns"></param>
        private string Rules()
        {
            if (Model.IsUiReadOnly)
                return null;
            var columns = Model.ClientProperty.Where(p => p.CanUserInput);
            bool first = true;
            var code = new StringBuilder();
            foreach (var property in columns)
            {
                var field = property;
                var sub = new StringBuilder();
                var dot = "";
                if (field.IsRequired && field.CanUserInput)
                {
                    sub.Append($@"{dot}{{ required: true, message: '������{property.Caption}', trigger: 'blur' }}");
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
    '{property.JsonName}':[{sub}]");
            }
            return first ? null : $@"var rules = {{{code}
}};";
        }
        private static void DateTimeCheck(IFieldConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max != null && field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, max: {field.Max}, message: 'ʱ��� {field.Min} �� {field.Max} ֮��', trigger: 'blur' }}");
            else if (field.Max != null)
                code.Append($@"{dot}{{ max: {field.Max}, message: 'ʱ�䲻���� {field.Max}', trigger: 'blur' }}");
            else if (field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, message: 'ʱ�䲻С�� {field.Min}', trigger: 'blur' }}");
            else return;
            dot = @",
    ";
        }

        private static void NumberCheck(IFieldConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max != null && field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, max: {field.Max}, message: '��ֵ�� {field.Min} �� {field.Max} ֮��', trigger: 'blur' }}");
            else if (field.Max != null)
                code.Append($@"{dot}{{ max: {field.Max}, message: '��ֵ������ {field.Max}', trigger: 'blur' }}");
            else if (field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, message: '��ֵ��С�� {field.Min}', trigger: 'blur' }}");
            else return;
            dot = @",
    ";
        }

        private static void StringCheck(IFieldConfig  field, StringBuilder code, ref string dot)
        {
            if (field.Max != null && field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, max: {field.Max}, message: '������ {field.Min} �� {field.Max} ���ַ�', trigger: 'blur' }}");
            else if (field.Max != null)
                code.Append($@"{dot}{{ max: {field.Max}, message: '���Ȳ����� {field.Max} ���ַ�', trigger: 'blur' }}");
            else if (field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, message: '���Ȳ�С�� {field.Min} ���ַ�', trigger: 'blur' }}");
            else return;
            dot = @",
    ";
        }

        #endregion
        #endregion
    }
}