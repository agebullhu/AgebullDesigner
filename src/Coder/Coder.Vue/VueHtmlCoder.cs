using Agebull.EntityModel.Config;
using System;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.VUE
{

    partial class VueHtmlCoder
    {
        #region 对象
        public IEntityConfig Model { get; set; }
        public ProjectConfig Project { get; set; }

        #endregion

        #region Card

        public string Card()
        {
            var code = new StringBuilder();


            code.Append($@"
<div id='viewHeader'>
    <div class='viewHeader-title'>{NavigateTitle(2)}
    </div>{ToolbarButton(1)}
</div>
<div class='viewBody'");
            if (Model.DetailsPage)
                code.Append(" v-if='!form.visible'");
            code.Append('>');

            if (Model.TreeUi)
            {
                code.Append($@"
    <el-card class='box-card viewTree'>{tree.FormatLineSpace(2)}
    </el-card>");
            }
            if (Model.FormQuery)
            {
                code.Append($@"
    <el-card class='box-card viewQuery'>
        <div class='innerRegion'>{FormQueryCode(3)}
        </div>
    </el-card>");
            }
            code.Append($@"
    <el-card class='box-card viewList'>
        <div class='viewList-table'>{HtmlGridCode(3, Model, "list.rows", true)}
        </div>
        <div class='viewList-pagination'>
            <div class='viewList-pagination-refresh'>
                <el-button icon='el-icon-refresh' @click='refresh'></el-button>
            </div>{pagination.FormatLineSpace(3)}
        </div>
    </el-card>
</div>");
            if (Model.DetailsPage)
            {
                code.Append($@"
<div class='viewBody' v-if='form.visible'>
    <el-card class='box-card innerRegion'>{EditPanelCode(2)}
    </el-card>
</div>");
            }
            else
            {
                code.Append(EditDialogCode(1));
            }
            return ToPage(CardStyle(), code.ToString());
        }


        string CardStyle()
        {
            var code = new StringBuilder();
            code.Append(@"
    <link rel='stylesheet' type='text/css' href='/styles/element-ui-abs.css'/>
    <style>");
            int left = 5;
            if (Model.TreeUi)
            {
                left = 286;
                code.Append(@"
        .viewTree {
            overflow-y: hidden;
            overflow-x: auto;
            position: absolute;
            top: 5px;
            width: 280px;
            bottom: 5px;
            left: 5px;
        }");
            }

            int top = 5;
            if (Model.FormQuery)
            {
                var cnt = Model.Properties.Count(p => p.CanUserQuery);
                if (cnt % 3 > 0)
                    cnt = cnt / 3 + 1;
                else cnt /= 3;
                var hei = cnt * 39 + 53;
                code.Append($@"
        .viewQuery {{
            position: absolute;
            top: 5px;
            right: 5px;
            height: {hei}px;
            left: 5px;
        }}");
                top += hei + 5;
            }

            code.Append($@"
        .viewList {{
            position: absolute;
            left: {left}px;
            top: {top}px;
            right: 5px;
            bottom: 5px;
        }}");
            code.Append(@"
    </style>");
            return code.ToString();
        }

        #endregion

        #region Container

        public string Container()
        {
            return ToPage(containerStyle, $@"
< el-container>
    <el-header height='48px'>
        <div style='display: inline-block;padding:6px'>{NavigateTitle(5)}
        </div>{QueryList(2)}{ToolbarButton(2)}
    </el-header>{HtmlMainCode(1)}{Footer(1)}
</el-container>{EditDialogCode(0)}");
        }
        string ToPage(string head, string body)
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
{head}
    <!--Extend-->
    <script type='text/javascript' src='/scripts/extend.js'></script>
    <script type='text/javascript' src='/scripts/object.js'></script>
    <script type='text/javascript' src='{Project.PageFolder.FromatByNotEmpty("/{0}")}/option.js'></script>
</head>
<body>
    <div id='work_space' class='tiled' v-cloak>
{body.FormatLineSpace(2)}
    </div>
    <script type='text/javascript' src='script.js'></script>
</body>
</html>";
        }
        string Footer(int level) => ToNoFromSection($@"
<el-footer height='42px'>{pagination.FormatLineSpace(1)}
</el-footer>").FormatLineSpace(level);

        #endregion

        #region Main
        public string HtmlMainCode(int level)
        {
            var details = ToFromSection(EditPanelCode(1));
            string main;
            if (Model.TreeUi)
            {
                main = ToNoFromSection($@"
<el-container>
    <el-aside style='height: 100%; width: 200px'>{tree.FormatLineSpace(2)}
    </el-aside>
    <el-main style='margin: 0; padding: 0;'>{HtmlGridCode(0, Model, "list.rows", true).FormatLineSpace(2)}
    </el-main>
</el-container>");
            }
            else
            {
                main = ToNoFromSection(HtmlGridCode(0, Model, "list.rows", true));
            }
            return $@"
<el-main style='margin: 0; padding: 0;'>{main.FormatLineSpace(1)}{ details.FormatLineSpace(1)}
</el-main>".FormatLineSpace(level);
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

        #region 表格

        string HtmlGridCode(int level, IEntityConfig entity, string data, bool details)
        {
            var code = new StringBuilder();
            code.Append($@"
<el-table :data='{data}' stripe border highlight-current-row");
            if (entity == Model)
            {
                code.Append(@" ref='dataTable' @sort-change='onSort'
          @current-change='currentRowChange' @selection-change='selectionRowChange' @row-dblclick='dblclick'>
    <el-table-column type='selection'align='center'header-align='center'></el-table-column>");
            }
            else
            {
                code.Append('>');
            }
            if (entity.Interfaces.Contains("IStateData"))
            {
                code.Append(@"
    <el-table-column label='状态' align='center' header-align='center' width='50'>
        <template slot-scope='scope'>
            <i :class='scope.row.dataState | dataStateIcon'></i>
        </template>
    </el-table-column>");
            }
            foreach (var property in entity.ClientProperty.Where(p => !p.NoneGrid && !p.GridDetails).ToArray())
            {
                GridField(code, property);
            }
            GridDetailsField(code, entity);

            if (details)
            {
                DetailsCommandsColumn(code);
            }
            code.Append(@"
</el-table>");
            return code.FormatLineSpace(level);
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

        void GridField(StringBuilder code, IPropertyConfig property)
        {
            var align = string.IsNullOrWhiteSpace(property.GridAlign) ? "left" : property.GridAlign;
            var fmt = Formater(property);
            code.Append($@"
    <el-table-column prop='{property.JsonName}' header-align='center' align='{align}' label='{property.Caption}'");
            if (property.UserOrder)
                code.Append(" sortable='true'");
            if (property.GridWidth > 0)
                code.Append($" width={property.GridWidth}");
            code.Append($@">
        <template slot-scope='scope'>
            <span style='margin-left: 3px'>{property.Prefix}{{{{scope.row.{property.JsonName}{fmt}}}}}{property.Suffix}</span>
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
                var field = property.DataBaseField;
                var caption = property.Caption;
                if (field.IsLinkKey)
                {
                    var friend = entity.DataTable.Find(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
                    if (friend != null)
                        caption = friend.Property.Caption;
                }
                if (field.IsText || property.MulitLine)
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

        public string EditDialogCode(int level)
        {
            if (Model.DetailsPage)
                return "";
            var col = Model.FormCloumn <= 0 ? 2 : Model.FormCloumn;
            var wid = col * 422 + 50;
            var code = new StringBuilder();
            code.Append($@"
<el-dialog title='【{Model.Caption}】' :modal='false' :visible.sync='form.visible' :show-close='false' :close-on-click-modal='false' width='{wid}px'
           v-loading='form.loading' element-loading-text='正在处理' element-loading-spinner='el-icon-loading' element-loading-background='rgba(0, 0, 0, 0.8)'>
    <div class='el-dialog__body_form'>{EditFormCode(2)}
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
            return code.FormatLineSpace(level);
        }

        public string EditPanelCode(int level)
        {
            var code = new StringBuilder();
            code.Append($@"
<el-tabs value='details'>
    <el-tab-pane label='{Model.Caption}' name='details'>{EditFormCode(2)}
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

        string EditFormCode(int level)
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
                FormField(code, "form.data", true, property, Model.IsUiReadOnly || property.IsUserReadOnly, Model.FormCloumn);
            }
            code.Append(@"
</el-form>");
            return code.FormatLineSpace(level);
        }
        #endregion


        #region Header & Footer

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
            if (Model.DetailsPage)
            {
                code.Append($@"
    <label class='labelButton' @click='showList'>{Model.Caption}</label>");

                if (Model.CaptionColumn != null)
                    code.Append($@"
    <template v-if='form.visible'>&nbsp;<i class='el-icon-arrow-right'></i>&nbsp;<span>{{{{form.data.{Model.CaptionColumn.JsonName}}}}}</span></template>");
                else code.Append($@"
    <template v-if='form.visible'>&nbsp;<i class='el-icon-arrow-right'></i>&nbsp;<span>详细内容</span></template>");
            }
            else
            {
                code.Append($"&nbsp;<span>{Model.Caption}</span>");
            }
            code.Append(@"
</template>");
            return code.FormatLineSpace(level);
        }

        #endregion

        #region Toolbar Button


        string ToolbarButton(int level)
        {
            StringBuilder exButton = new StringBuilder();
            exButton.Append(@"
<div class='toolRange'>
    <el-button-group v-if='!form.visible'>");

            if (!Model.IsUiReadOnly)
            {
                exButton.Append(@"
        <el-button icon='el-icon-plus' @click='doAddNew' type='success' plain>新增</el-button>");
            }
            if (!Model.DetailsPage)
            {
                if (!Model.IsUiReadOnly)
                {
                    exButton.Append(@"
        <el-button icon='el-icon-edit' @click='doEdit' type='success' plain>编辑</el-button>");
                }
                else
                {
                    exButton.Append(@"
        <el-button icon='el-icon-edit' @click='doEdit' type='primary' plain>详细</el-button>");
                }
            }
            if (!Model.IsUiReadOnly)
            {
                exButton.Append(@"
        <el-button icon='el-icon-close' @click='doDelete' type='danger' plain>删除</el-button>");
            }
            exButton.Append(@"
        <el-button icon='el-icon-document' @click='exportExcel' type='primary' plain>导出</el-button>");
            exButton.Append(@"
    </el-button-group>");

            if (Model.DetailsPage)
            {
                exButton.Append(@"
    <el-button-group v-if='form.visible'>
        <el-button icon='el-icon-back' @click='showList' type='primary' plain>返回</el-button>");
                if (!Model.IsUiReadOnly)
                    exButton.Append(@"
        <el-button icon='el-icon-check' @click='save' v-if='!form.readonly' type='success' plain>保存</el-button>");
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
            return exButton.FormatLineSpace(level);
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
        <el-button icon='{cmd.Icon}' @click='{cmd.JsMethod}' type='success' plain>{cmd.Caption}</el-button>");

            }
            code.Append(@"
    </el-button-group>");
        }

        #endregion

        #region 查询

        public string FormQueryCode(int level)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"
<div class='region-body'>
    <el-form ref='queryForm' :model='query' label-width='100px' label-position='left' @submit.native.prevent>");
            foreach (var property in Model.Where(p => p.CanUserQuery).ToArray())
            {
                FormField(code, "query", false, property, false, 2);
            }
            code.Append(@"
    </el-form>
</div>
<div class='region-foot'>
    <div style='padding: 5px;'>
        <el-button type='primary' icon='el-icon-search' @click='doQuery' plain>查询</el-button>
        <el-button type='warning' icon='el-icon-refresh-left' @click='clearQuery' plain>清除</el-button>
    </div>
</div>");
            return code.FormatLineSpace(level);
        }
        string QueryList(int level)
        {
            if (Model.FormQuery)
            {
                return null;
            }
            StringBuilder quButton = new StringBuilder();
            var fields = Model.DataTable.FindAndToArray(p => p.Property.CanUserQuery && p.Property.UserSee && !p.NoStorage && !p.IsLinkKey && !p.Property.IsPrimaryKey);
            //if (Entity.Interfaces.Contains("IStateData"))
            //{
            //    quButton.Append(satateQuery);
            //}
            //if (Entity.Interfaces.Contains("IAuditData"))
            //{
            //    quButton.Append(auditQuery);
            //}
            quButton.Append(@"
<div class='toolRange'>
    <el-input placeholder='请输入搜索内容' v-model='list.keyWords' class='input-with-select' clearable>
        <el-select v-model='list.field' slot='prepend' placeholder='选择字段' style='width: 160px;'>
            <el-option value='_any_' label='模糊查询'></el-option>");
            foreach (var field in fields)
            {
                quButton.Append($@"
                <el-option value='{field.Property.JsonName}' label='{field.Caption}'></el-option>");
            }
            quButton.Append(@"
            </el-select>
        <el-button slot='append' icon='el-icon-search' @click='doQuery'></el-button>
    </el-input>
</div>");
            return quButton.FormatLineSpace(level);
        }

        #endregion

        #region 基础代码

        #region 表单字段

        static void FormField(StringBuilder code, string model, bool isEdit, IPropertyConfig property, bool isReadonly, int maxColumn)
        {
            var caption = property.Caption;
            var description = property.Description;

            /*var field = property.DataBaseField;
            if (field != null && field.IsLinkKey)
            {
                if (field.Parent.TryGet(p => p.LinkTable == field.LinkTable && p.IsLinkCaption, out var friend))
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
            }*/

            code.Append($@"
    <el-form-item label='{caption}' prop='{property.JsonName}'");
            if (isEdit)
            {
                var sapn = property.FormCloumnSapn > maxColumn ? maxColumn : property.FormCloumnSapn;
                switch (sapn)
                {
                    case 2:
                        if (property.MulitLine)
                            HtmlAttribute(code, "style", "width: 813px;display: block");
                        else
                            HtmlAttribute(code, "style", "width: 813px;");
                        break;
                    case 3:
                        if (property.MulitLine)
                            HtmlAttribute(code, "style", "width: 1228px;display: block");
                        else
                            HtmlAttribute(code, "style", "width: 1228px;");
                        break;
                    case 4:
                        if (property.MulitLine)
                            HtmlAttribute(code, "style", "width: 1640px;display: block");
                        else
                            HtmlAttribute(code, "style", "width: 1640px;");
                        break;
                    default:
                        if (property.MulitLine)
                            HtmlAttribute(code, "style", "display: block");
                        break;
                }
            }
            code.Append(">");

            if (isReadonly)
            {
                ReadonlyField(code, model, property);
            }
            else
                EditField(code, model, isEdit, property, description);
            code.Append(@"
    </el-form-item>");
        }
        static void ReadonlyField(StringBuilder code, string model, IPropertyConfig property)
        {
            code.Append($@"
        <span>{property.Prefix}{{{{{model}.{property.JsonName}{Formater(property)}}}}}{property.Suffix}</span>");
        }
        static void EditField(StringBuilder code, string model, bool isEdit, IPropertyConfig property, string description)
        {
            void SetDisabled(bool disabled)
            {
                code.Append(HtmlAttribute("placeholder", property.Description));
                code.Append(" clearable");
                if (!isEdit)
                {
                    return;
                }
                if (disabled)
                {
                    code.Append(property.IsUserReadOnly ? " disabled" : " :disabled='form.readonly'");
                }
                else
                {
                    code.Append(property.IsUserReadOnly ? " readonly" : " :readonly='form.readonly'");
                }
            }
            var field = property.DataBaseField;
            if (property.EnumConfig != null)
            {
                code.Append($@"
        <el-select v-model='{model}.{property.JsonName}'");
                SetDisabled(true);
                code.Append($@" style='width:100%'>
            <el-option :key='0' :value='0' label='-'></el-option>
            <el-option v-for='item in types.{property.EnumConfig.Name.ToLWord()}' :key='item.value' :label='item.label' :value='item.value'></el-option>
        </el-select>");
            }
            else if (field != null && field.IsLinkKey)
            {
                var name = GlobalConfig.GetEntity(field.LinkTable)?.Name.ToLWord().ToPluralism();
                code.Append($@"
        <el-select v-model='{model}.{property.JsonName}'");
                SetDisabled(true);
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
        <el-switch v-model='{model}.{property.JsonName}'");
                SetDisabled(true);
                code.Append(@"></el-switch>");
            }
            else if (property.CsType == nameof(DateTime))
            {
                code.Append($@"
        <el-date-picker v-model='{model}.{property.JsonName}'");
                code.Append(property.IsTime
                    ? "value-format='yyyy-MM-ddTHH:mm:ss' type='datetime'"
                    : "value-format='yyyy-MM-dd' type='date'");
                SetDisabled(false);
                code.Append(@" style='width:100%'></el-date-picker>");
            }
            else if (property.CsType == "string")
            {
                code.Append($@"
        <el-input v-model='{model}.{property.JsonName}'auto-complete='off'");

                if (isEdit && property.MulitLine)
                {
                    code.Append($" type='textarea' rows='{property.Rows}'");
                }
                SetDisabled(false);

                code.Append("></el-input>");
            }
            else
            {
                code.Append($@"
        <el-input v-model='{model}.{property.JsonName}' auto-complete='off'");
                SetDisabled(false);

                code.Append("></el-input>");
            }
        }
        #endregion

        #region 格式化器

        static string Formater(IPropertyConfig property)
        {
            var field = property.DataBaseField;
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

        #region Helper

        static string HtmlAttribute(string attr, string value)
        {
            return value.IsMissing() ? "" : $" {attr}= '{value}'";
        }
        static void HtmlAttribute(StringBuilder code, string attr, string value)
        {
            code.Append(value.IsMissing() ? "" : $" {attr}= '{value}'");
        }
        string ToNoFromSection(string html) => Model.DetailsPage
            ? $@"
<template v-if='!form.visible'>{html.FormatLineSpace(1)}
</template>"
            : html;

        string ToFromSection(string html) => Model.DetailsPage
            ? $@"
<template v-if='form.visible'>{html.FormatLineSpace(1)}
</template>"
            : html;
        #endregion

        #region 固定元素

        const string containerStyle = @"
    <link rel='stylesheet' type='text/css' href='/styles/element-ui.css'/>";

        const string tree = @"
<el-tree :data='tree.nodes' ref='tree' node-key='id' default-expand-all highlight-current 
         @current-change='onTreeNodeChanged' :props='tree.props' :expand-on-click-node='false'>
    <span slot-scope='{node, data}'>
        <i :class='data.type | typeIcon'></i><span>{{node.label}}</span>
    </span>
</el-tree>";
        const string pagination = @"
<el-pagination @size-change='sizeChange' @current-change='pageChange' background layout='total, sizes, prev, pager, next, jumper'
               :current-page='list.page' :page-sizes='list.pageSizes' :page-size='list.pageSize' :total='list.total'>
</el-pagination>";
        #endregion
        #endregion
    }
}