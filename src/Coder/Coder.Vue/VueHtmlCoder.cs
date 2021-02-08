using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.Common;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.VUE
{

    partial class VueHtmlCoder
    {
        public IEntityConfig Model { get; set; }
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
    <title>{Model.Project.Caption} > {Model.Caption}</title>
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
    <script type='text/javascript' src='{Project.PageFolder.FromatByNotEmpty("/{0}")}/option.js'></script>
</head>
<body>
    <div id='work_space' class='tiled' v-cloak>
        <el-container>
            <el-header height='48px'>
                <div style='display: inline-block;padding:6px'>{NavigateTitle(5)}
                </div>
                <div class='toolRange'>
                    <el-input placeholder='请输入搜索内容' v-model='list.keyWords' class='input-with-select' clearable>{QueryList(6)}
                        <el-button slot='append' icon='el-icon-search' @click='doQuery'></el-button>
                    </el-input>
                </div>{HtmlExButton(4)}
            </el-header>{HtmlMainCode(3)}{Footer(3)}
        </el-container>{HtmlDialogCode(2)}
    </div>
    <script type='text/javascript' src='script.js'></script>
</body>
</html>";
        }

        #endregion
        #region Helper

        static string HtmlAttribute(string attr, string value)
        {
            return value.IsEmpty() ? "" : $" {attr}= '{value}'";
        }
        string ToNoFromSection(string html) => Model.DetailsPage
            ? $@"
<template v-if='!form.visible'>{FormatSpace(1, html)}
</template>"
            : html;

        string ToFromSection(string html) => Model.DetailsPage
            ? $@"
<template v-if='form.visible'>{FormatSpace(1, html)}
</template>"
            : html;

        string FormatSpace(int level, StringBuilder html)
        {
            return html.Length == 0 ? null : html.ToString().SpaceLine(level * 4);
        }

        string FormatSpace(int level, string html)
        {
            return html.IsEmpty() ? null : html.SpaceLine(level * 4);
        }

        string Formater(IPropertyConfig property)
        {
            var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
            if (property.DataFormater != null)
            {
                return $" | {property.DataFormater}";
            }
            if (property.EnumConfig != null)
            {
                return $" | {property.EnumConfig.Name.ToLWord()}Formater";
            }
            else if (field.IsLinkKey)
            {
                return $" | {field.LinkTable.ToLWord()}Formater";
            }
            else if (property.CsType == nameof(DateTime))
            {
                return property.IsTime ? "| formatTime" : "| formatDate";
            }
            else if (property.CsType == "bool")
            {
                return " | boolFormater";
            }
            else if (property.IsMoney)
            {
                return " | formatMoney";
            }
            else if (property.CsType == "decimal")
            {
                return " | thousandsNumber";
            }
            return null;
        }
        #endregion
        #region Main
        public string HtmlMainCode(int level)
        {
            var details = ToFromSection(HtmlDetailsCode(1));
            string main;
            if (Model.TreeUi)
            {
                main = ToNoFromSection($@"
<el-container>
    <el-aside style='height: 100%; width: 200px'>{Tree(2)}
    </el-aside>
    <el-main style='margin: 0; padding: 0;'>{FormatSpace(2, HtmlGridCode(0, Model, "list.rows", true))}
    </el-main>
</el-container>");
            }
            else
            {
                main = ToNoFromSection(HtmlGridCode(0, Model, "list.rows", true));
            }
            return FormatSpace(level, $@"
<el-main style='margin: 0; padding: 0;'>{FormatSpace(1, main)}{FormatSpace(1, details)}
</el-main>");
        }

        string DetailsField(IPropertyConfig field)
        {
            return $@"
                            <div class='detailsField'>
                                <div class='myLabel'>{field.Caption}</div>
                                <div class='myValue'>{{{{details.data.{field.JsonName}{Formater(field)}}}}}</div>
                            </div>";
        }

        #endregion
        #region Tree

        string Tree(int level) => FormatSpace(level, @"
<el-tree :data='tree.nodes' ref='tree' :props='tree.props' node-key='id'
         @current-change='onTreeNodeChanged' default-expand-all highlight-current :expand-on-click-node='false'>
    <span slot-scope='{node, data}'>
        <i :class='data.type | typeIcon'></i><span>{{node.label}}</span>
    </span>
</el-tree>");

        #endregion

        #region 表格

        string HtmlGridCode(int level, IEntityConfig entity, string data, bool details)
        {
            var main = entity == Model ? @"
           ref='dataTable' @sort-change='onSort' @row-dblclick='dblclick' 
           @current-change='currentRowChange' @selection-change='selectionRowChange'" : null;
            var code = new StringBuilder();
            code.Append($@"
<!-- {entity.Caption}-->
<el-table :data='{data}' border highlight-current-row style='width:99%'{main}>");
            //if (entity.Interfaces.Contains("IInnerTree") && entity.LastProperties.Any(p => p.Name == "ParentId"))
            //{
            //    code.Append($@"
            //              lazy row-key='{entity.PrimaryColumn.JsonName}' :load = 'load'");
            //}
            if (entity == Model)
                code.Append(@"
    <el-table-column type='selection'align='center'header-align='center'></el-table-column>");
            if (entity.Interfaces.Contains("IStateData"))
            {
                code.Append(stateColumn);
            }
            foreach (var property in entity.ClientProperty.Where(p => !p.NoneGrid && !p.GridDetails).ToArray())
            {
                var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
                var label = property.Caption;
                if (field.IsLinkKey)
                {
                    var table = GlobalConfig.Find(field.LinkTable);
                    if (table != null)
                        label = table.Caption;
                }
                GridField(code, property, label);
            }
            GridDetailsField(code, entity);

            if (details)
            {
                DetailsCommandsColumn(code);
            }
            code.Append(@"
</el-table>");
            return FormatSpace(level, code);
        }

        void DetailsCommandsColumn(StringBuilder code)
        {
            var cmds = Model.Commands.Where(p => p.IsSingleObject);
            if (!Model.DetailsPage && !cmds.Any())
                return;
            int cnt = 32 + (cmds.Count() * 1);
            if (Model.DetailsPage) cnt += 32;
            code.Append($@"
    <el-table-column fixed='right' label='操作' width='{cnt}'>
        <template slot-scope='scope'>");
            bool first = Model.DetailsPage;
            if (Model.DetailsPage)
                code.Append(@"
            <label class='labelButton' @click='handleClick(scope.row)'>详细</label>");

            foreach (var cmd in cmds)
            {
                if (first) first = false;
                else
                    code.Append("&nbsp;&nbsp;");
                code.Append($@"
            <label class='labelButton' @click='{cmd.JsMethod}(scope.row.{Model.PrimaryColumn.JsonName})'>{cmd.Caption}</label>");
            }
            code.Append($@"
        </template>
    </el-table-column>");
        }

        const string stateColumn = @"
    <el-table-column label='状态' align='center' header-align='center' width='50'>
        <template slot-scope='scope'>
            <i :class='scope.row.dataState | dataStateIcon'></i>
        </template>
    </el-table-column>";

        void GridField(StringBuilder code, IPropertyConfig field, string caption)
        {
            var align = string.IsNullOrWhiteSpace(field.GridAlign) ? "left" : field.GridAlign;
            var fmt = Formater(field);
            code.Append($@"
    <el-table-column prop='{field.JsonName}' header-align='center' align='{align}' label='{caption}'");
            if (field.UserOrder)
                code.Append(" sortable='true'");
            if (field.GridWidth > 0)
                code.Append($" width={field.GridWidth}");
            code.Append($@">
        <template slot-scope='scope'>
            <span style='margin-left: 3px'>{field.Prefix}{{{{scope.row.{field.JsonName}{fmt}}}}}{field.Suffix}</span>
        </template>
    </el-table-column>");
        }

        static void GridDetailsField(StringBuilder code, IEntityConfig entity)
        {
            var details = entity.ClientProperty.Where(p => p.GridDetails).ToArray();
            if (details.Length <= 0)
                return;
            code.Append(@"
    <el-table-column type='expand'>
        <template slot-scope='props'>");
            foreach (var property in details)
            {
                var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
                var caption = property.Caption;
                if (field.IsLinkKey)
                {
                    var friend = entity.DataTable.Fields.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
                    if (friend != null)
                        caption = friend.Property.Caption;
                }
                if (field.IsMemo || property.MulitLine)
                    code.Append($@"
            <div class='expand_line_block'>");
                else
                {
                    var sp = property.FormCloumnSapn <= 0
                        ? 1
                        : property.FormCloumnSapn >= 4
                            ? 4
                            : property.FormCloumnSapn;
                    code.Append($@"
            <div class='expand_block_{sp}'>");

                }
                code.Append($@"
                <label class='expand_label'>{caption}：</label>");

                if (property.IsImage)
                {
                    code.Append($@"
                <el-image :src='{property.JsonName}' lazy></el-image>");
                }
                else
                {
                    code.Append($@"
                <span class='expand_value'>{property.Prefix}{{{{props.row.{property.JsonName}");
                    if (property.EnumConfig != null)
                    {
                        code.Append($@" | {property.EnumConfig.Name.ToLWord()}Formater");
                    }
                    else if (property.CsType == nameof(DateTime))
                    {
                        var fmt = property.IsTime ? "formatTime" : "formatDate";
                        code.Append($@" | {fmt}");
                    }
                    else if (property.CsType == "bool")
                    {
                        code.Append(@" | boolFormater");
                    }
                    else if (property.IsMoney)
                    {
                        code.Append(@" | formatMoney");
                    }
                    else if (property.CsType == "decimal")
                    {
                        code.Append(@" | thousandsNumber");
                    }
                    code.Append($@"}}}}{property.Suffix}</span>");
                }
                code.Append($@"
            </div>");
            }

            code.Append(@"
        </template>
    </el-table-column>");
        }

        #endregion
        #region 表单

        public string HtmlDialogCode(int level)
        {
            if (Model.DetailsPage)
                return "";
            var col = Model.FormCloumn <= 0 ? 2 : Model.FormCloumn;
            var wid = col * 422 + 50;
            var code = new StringBuilder();
            code.Append($@"
<el-dialog title='【{Model.Caption}】编辑'
        :visible.sync='form.visible'
        :show-close='false'
        :close-on-click-modal='false'
        width='{wid}px'
        v-loading='form.loading'
        element-loading-text='正在处理'
        element-loading-spinner='el-icon-loading'
        element-loading-background='rgba(0, 0, 0, 0.8)'>
<div class='el-dialog__body_form'>{HtmlFormCode(1, true)}
</div>
<div slot='footer'>");
            if (!Model.IsUiReadOnly)
            {
                code.Append(@"
    <el-button icon='el-icon-check' @click='save' type='primary' v-if='!form.readonly'>保存</el-button>");
            }
            code.Append(@"
    <el-button icon='el-icon-close' @click='form.visible = false'>取消</el-button>
</div>
</el-dialog>");
            return FormatSpace(level, code);
        }


        public string HtmlDetailsCode(int level)
        {
            var code = new StringBuilder();
            code.Append($@"
<el-tabs value='details'>
    <el-tab-pane label='{Model.Caption}' name='details'>{HtmlFormCode(2, false)}
    </el-tab-pane>");
            if (Model is ModelConfig model && model.Releations.Count > 0)
            {
                foreach (var ch in model.Releations)
                {
                    switch (ch.JoinType)
                    {
                        case EntityJoinType.none:
                            break;
                        case EntityJoinType.Inner:
                            continue;
                        case EntityJoinType.Left:
                            continue;
                    }
                    var name = ch.Name.ToLWord().ToPluralism();
                    code.Append($@"
    <el-tab-pane label='{ch.Caption}' name='{name}'>{HtmlGridCode(level + 2, ch.ForeignEntity, "form." + name, false)}
    </el-tab-pane>");
                }
            }
            code.Append(@"
</el-tabs>");
            return code.ToString();
        }

        public string HtmlFormCode(int level, bool dialog)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"
<el-form ref='dataForm' :rules='form.rules' :model='form.data' label-width='100px' label-position='left' @submit.native.prevent>");
            //string vif = null;
            //if (Entity.Interfaces.Contains("IAuditData"))
            //{
            //    vif = "form.data.auditState <= 1";
            //}
            //else if (Entity.Interfaces.Contains("IStateData"))
            //{
            //    vif = "form.data.dataState <= 1";
            //}
            foreach (var property in Model.ClientProperty.Where(p => !p.NoneDetails).ToArray())
            {
                var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
                var caption = property.Caption;
                var description = property.Description;

                if (field.IsLinkKey)
                {
                    var friend = Model.DataTable.Fields.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
                    if (friend != null)
                    {
                        caption = friend.Property.Caption;
                        description = friend.Property.Description;
                    }
                    else
                    {
                        var table = GlobalConfig.Find(field.LinkTable);
                        if (table != null)
                        {
                            caption = table.Caption;
                            description = table.Description;
                        }
                    }
                }
                FormField(code, property, caption, description ?? property.Caption, dialog);
            }
            code.Append(@"
</el-form>");
            return FormatSpace(level, code);
        }
        private void FormField(StringBuilder code, IPropertyConfig property, string caption, string description, bool dialog)
        {
            var field = property;
            string style;
            var sapn = field.FormCloumnSapn > Model.FormCloumn ? Model.FormCloumn : field.FormCloumnSapn;
            switch (sapn)
            {
                case 2:
                    style = HtmlAttribute("style", "width: 813px;");
                    break;
                case 3:
                    style = HtmlAttribute("style", "width: 1228px;");
                    break;
                case 4:
                    style = HtmlAttribute("style", "width: 1640px;");
                    break;
                default:
                    style = null;
                    break;
            }
            code.Append($@"
    <el-form-item label='{caption}' prop='{property.JsonName}'{style}>");

            if (field.Prefix.IsNotEmpty())
                code.Append($@"
        <span>{field.Prefix}&nbsp;</span>");
            if (Model.IsUiReadOnly || property.IsUserReadOnly)
                ReadonlyField(code, field);
            else
                EditField(code, field, description);
            if (field.Suffix.IsNotEmpty())
                code.Append($@"
        <span>{field.Suffix}</span>");
            code.Append(@"
    </el-form-item>");
        }
        void ReadonlyField(StringBuilder code, IPropertyConfig field)
        {
            code.Append($@"
        <span>&nbsp;{{{{form.data.{field.JsonName}{Formater(field)}}}}}</span>");
        }
        void EditField(StringBuilder code, IPropertyConfig property, string description)
        {
            static void SetDisabled(bool disabled, StringBuilder code, IPropertyConfig field)
            {
                var placeholder = HtmlAttribute("placeholder", field.Description);
                if (disabled)
                {
                    code.Append(field.IsUserReadOnly ? " disabled" : " :disabled='form.readonly'");
                }
                else
                {
                    code.Append(field.IsUserReadOnly ? " readonly" : " :readonly='form.readonly'");
                }
                code.Append(placeholder);
                code.Append(" clearable");
            }
            var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
            if (property.EnumConfig != null)
            {
                code.Append($@"
        <el-select v-model='form.data.{property.JsonName}'");
                SetDisabled(true, code, property);
                code.Append($@" style='width:100%'>
            <el-option :key='0' :value='0' label='-'></el-option>
            <el-option v-for='item in types.{property.EnumConfig.Name.ToLWord()}' :key='item.value' :label='item.label' :value='item.value'></el-option>
        </el-select>");
            }
            else if (field.IsLinkKey)
            {
                var name = GlobalConfig.GetEntity(field.LinkTable)?.Name.ToLWord().ToPluralism();
                code.Append($@"
        <el-select v-model='form.data.{property.JsonName}'");
                SetDisabled(true, code, property);
                code.Append($@" style='width:100%'>
            <el-option :key='0' :value='0' label='-'></el-option>
            <template v-for='item in combos.{name}'>
                <el-option :key='item.id' :value='item.id' :label='item.text'></el-option>
            </template>
        </el-select>");
            }
            else if (property.CsType == "bool")
            {
                code.Append($@"
        <el-switch v-model='form.data.{property.JsonName}'");
                SetDisabled(true, code, property);
                code.Append(@"></el-switch>");
            }
            else if (property.CsType == nameof(DateTime))
            {
                code.Append($@"
        <el-date-picker v-model='form.data.{property.JsonName}'");
                SetDisabled(false, code, property);
                code.Append(property.IsTime
                    ? "value-format='yyyy-MM-ddTHH:mm:ss' type='datetime'"
                    : "value-format='yyyy-MM-dd' type='date'");
                code.Append(@" style='width:100%'></el-date-picker>");
            }
            else if (property.CsType == "string")
            {
                code.Append($@"
        <el-input v-model='form.data.{property.JsonName}'auto-complete='off'");

                if (property.MulitLine)
                {
                    code.Append($" type='textarea' rows='{property.Rows}'");
                }
                SetDisabled(false, code, property);

                code.Append("></el-input>");
            }
            else
            {
                code.Append($@"
        <el-input v-model='form.data.{property.JsonName}' auto-complete='off'");
                SetDisabled(false, code, property);

                code.Append("></el-input>");
            }
        }
        #endregion

        #region Header & Footer

        string HtmlExButton(int level)
        {
            StringBuilder exButton = new StringBuilder();
            exButton.Append(@"
<div class='toolRange'>
    <el-button-group v-if='!form.visible'>
        <el-button icon='el-icon-refresh' @click='refresh'>刷新</el-button>");

            if (!Model.IsUiReadOnly)
            {
                exButton.Append(@"
        <el-button icon='el-icon-plus' @click='doAddNew'>新增</el-button>");
            }
            if (!Model.DetailsPage)
            {
                if (!Model.IsUiReadOnly)
                {
                    exButton.Append(@"
        <el-button icon='el-icon-edit' @click='doEdit'>编辑</el-button>");
                }
                else
                {
                    exButton.Append(@"
        <el-button icon='el-icon-edit' @click='doEdit'>详细</el-button>");
                }
            }
            if (!Model.IsUiReadOnly)
            {
                exButton.Append(@"
        <el-button icon='el-icon-close' @click='doDelete'>删除</el-button>");
            }
            exButton.Append(@"
        <el-button icon='el-icon-document' @click='exportExcel'>导出</el-button>
    </el-button-group>");

            if (Model.DetailsPage)
            {
                exButton.Append(@"
    <el-button-group v-if='form.visible'>
        <el-button icon='el-icon-back' @click='showList'>返回</el-button>");
                if (!Model.IsUiReadOnly)
                    exButton.Append(@"
        <el-button icon='el-icon-check' @click='save' v-if='!form.readonly'>保存</el-button>");
                exButton.Append(@"
    </el-button-group>");
            }
            CommandsButton(exButton);
            if (!Model.IsUiReadOnly)
            {
                if (Model.Interfaces.Contains("IAuditData"))
                {
                    exButton.Append(@"
    <el-dropdown v-if='!form.visible' split-button @click='doPass' @command='handleDataCommand'>
        <i class='el-icon-circle-check'></i>审核通过
        <el-dropdown-menu slot='dropdown'>
            <el-dropdown-item type='text' command='Deny' icon='el-icon-remove-outline'>不通过</el-dropdown-item>
            <el-dropdown-item type='text' command='Back' icon='el-icon-edit'>退回编辑</el-dropdown-item>
            <el-dropdown-item type='text' command='ReDo' icon='el-icon-edit'>重新审核</el-dropdown-item>
        </el-dropdown-menu>
    </el-dropdown>");
                }
                if (Model.Interfaces.Contains("IStateData"))
                {
                    exButton.Append(@"
    <el-dropdown v-if='!form.visible' split-button @click='doEnable'>
        <i class='el-icon-lock'></i>启用
        <el-dropdown-menu slot='dropdown' style='padding:3px;'>
            <el-dropdown-item style='padding:3px;'>
                <el-button type='text' icon='el-icon-remove'  @click='doDisable'>禁用</el-button>
            </el-dropdown-item>
            <el-dropdown-item style='padding:3px;'>
                <el-button type='text' icon='el-icon-unlock' @click='doReset'>解锁</el-button>
            </el-dropdown-item>
        </el-dropdown-menu>
    </el-dropdown>");
                }
            }
            exButton.Append(@"
</div>");
            return FormatSpace(level, exButton);
        }
        private void CommandsButton(StringBuilder code)
        {
            var cmds = Model.Commands.Where(p => p.IsMulitOperator).ToArray();
            if (cmds.Length == 0)
                return;
            code.Append(@"
    <el-button-group v-if='!form.visible'>");
            foreach (var cmd in cmds)
            {
                code.Append($@"
        <el-button icon='{cmd.Icon}' @click='{cmd.JsMethod}'>{cmd.Caption}</el-button>");

            }
            code.Append(@"
    </el-button-group>");
        }

        private string NavigateTitle(int level)
        {
            var code = new StringBuilder();
            code.Append($@"
<template>
    <span>{Model.Project.Caption}</span>&nbsp;<i class='el-icon-arrow-right'></i>");
            if (!Model.Project.NoClassify)
            {
                var cls = Model.Project.Classifies.FirstOrDefault(p => p.Name == Model.Classify);
                if (cls != null)
                    code.Append($"&nbsp;<span>{cls.Caption}</span>&nbsp;<i class='el-icon-arrow-right'></i>");
            }
            if (!Model.DetailsPage)
                code.Append($"&nbsp;<span>{Model.Caption}</span>");
            else
            {
                code.Append($@"
    <label class='labelButton' @click='showList'>{Model.Caption}</label>");
                
                if (Model.CaptionColumn != null)
                    code.Append($@"
    <template v-if='form.visible'>&nbsp;<i class='el-icon-arrow-right'></i>&nbsp;<span>{{{{form.data.{Model.CaptionColumn.JsonName}}}}}</span></template>");
                else code.Append($@"
    <template v-if='form.visible'>&nbsp;<i class='el-icon-arrow-right'></i>&nbsp;<span>详细内容</span></template>");
            }
            code.Append(@"
</template>");
            return FormatSpace(level, code);
        }

        string Footer(int level) => FormatSpace(level, ToNoFromSection(@"
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
</el-footer>"));

        #endregion

        #region 查询

        string QueryList(int level)
        {
            StringBuilder quButton = new StringBuilder();
            var properties = Model.DataTable.Fields.Where(p => p.Property.CanUserQuery && p.Property.UserSee &&
                !p.NoStorage && !p.Property.NoneDetails && !p.IsLinkKey && !p.Property.IsPrimaryKey);
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
            foreach (var property in properties)
            {
                quButton.Append($@"
    <el-option value='{property.Property.JsonName}' label='{property.Caption}'></el-option>");
            }
            quButton.Append(@"
</el-select>");
            return FormatSpace(level, quButton);
        }

        #endregion
        #endregion

    }
}