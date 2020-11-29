using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.Common;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.VUE
{

    partial class VueHtmlCoder<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        public TModel Model { get; set; }
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
    <script type='text/javascript' src='{Project.PageFolder.FromatByNotEmpty("/{0}")}/option.js'></script>
</head>
<body>
    <div id='work_space' class='tiled' v-cloak>
        <el-container>
            <el-header height='48px'>
                <div style='display: inline-block;padding:6px'>
                    {NavigateTitle()}
                </div>
                <div class='toolRange'>
                    <el-input placeholder='请输入搜索内容' v-model='list.keyWords' class='input-with-select' clearable>{QueryList()}
                        <el-button slot='append' icon='el-icon-search' @click='doQuery'></el-button>
                    </el-input>
                </div>{HtmlExButton()}
            </el-header>
            <el-main>{HtmlMainCode()}
            </el-main>
        </el-container>{HtmlDialogCode()}
    </div>
    <script type='text/javascript' src='script.js'></script>
</body>
</html>";
        }

        private string NavigateTitle()
        {
            var code = new StringBuilder();
            code.Append($"<span>{Model.Parent.Caption}</span>&nbsp;<i class='el-icon-arrow-right'></i>");
            if (!Model.Parent.NoClassify)
            {
                var cls = Model.Parent.Classifies.FirstOrDefault(p => p.Name == Model.Classify);
                if (cls != null)
                    code.Append($"&nbsp;<span>{cls.Caption}</span>&nbsp;<i class='el-icon-arrow-right'></i>");
            }
            if (!Model.DetailsPage)
                code.Append($"&nbsp;<span>{Model.Caption}</span>");
            else
            {
                code.Append($"<label class='labelButton' @click='showList'>{Model.Caption}</label>");
                code.Append($"<template v-if='form.visible'>&nbsp;<i class='el-icon-arrow-right'></i>&nbsp;<span>详细信息</span></template>");
            }
            return code.ToString();
        }

        string HtmlExButton()
        {
            StringBuilder exButton = new StringBuilder();
            exButton.Append(@"
                <div class='toolRange'>
                    <el-button-group>
                        <el-button v-if='!form.visible' icon='el-icon-refresh' @click='loadList'>刷新</el-button>
                        <!--el-button icon='fa fa-file-excel-o' type='success' @click='exportExcel'>导出</el-button-->");

            if (!Model.IsUiReadOnly)
            {
                exButton.Append(@"
                        <el-button v-if='!form.visible' icon='el-icon-plus' @click='doAddNew'>新增</el-button>");
            }
            if (!Model.DetailsPage)
            {
                if (!Model.IsUiReadOnly)
                {
                    exButton.Append(@"
                        <el-button v-if='!form.visible' icon='el-icon-edit' @click='doEdit'>编辑</el-button>");
                }
                else
                {
                    exButton.Append(@"
                        <el-button v-if='!form.visible' icon='el-icon-edit' @click='doEdit'>详细</el-button>");
                }
            }
            else
            {
                exButton.Append(@"
                        <template v-if='form.visible'>
                            <el-button icon='el-icon-back' @click='showList'>返回</el-button>
                            <el-button icon='el-icon-check' @click='save' v-if='!form.readonly'>保存</el-button>
                        </template>");
            }
            if (!Model.IsUiReadOnly)
            {
                exButton.Append(@"
                        <el-button v-if='!form.visible' icon='el-icon-close' @click='doDelete'>删除</el-button>");
            }
            exButton.Append(@"
                    </el-button-group>");
            if (!Model.IsUiReadOnly)
            {
                if (Model.Interfaces.Contains("IAuditData"))
                {
                    exButton.Append(@"
                    <el-dropdown v-if='!form.visible' split-button @click='doPass' @command='handleDataCommand'>
                        <i class='el-icon-circle-check'></i>审核通过
                        <el-dropdown-menu slot='dropdown'>
                            <el-dropdown-item type='text' command='Enable' icon='el-icon-lock'>启用</el-dropdown-item>
                            <el-dropdown-item type='text' command='Disable' icon='el-icon-remove'>禁用</el-dropdown-item>
                            <el-dropdown-item type='text' command='Reset' icon='el-icon-unlock'>解除锁定</el-dropdown-item>
                            <el-dropdown-item type='text' command='Deny' icon='el-icon-remove-outline'>不通过</el-dropdown-item>
                            <el-dropdown-item type='text' command='Back' icon='el-icon-edit'>退回编辑</el-dropdown-item>
                            <el-dropdown-item type='text' command='ReDo' icon='el-icon-edit'>重新审核</el-dropdown-item>
                        </el-dropdown-menu>
                    </el-dropdown>");
                }
                else if (Model.Interfaces.Contains("IStateData"))
                {
                    exButton.Append(@"
                    <el-dropdown v-if='!form.visible' split-button @click='doEnable'>
                        <i class='el-icon-lock'></i>启用
                        <el-dropdown-menu slot='dropdown' style='padding:3px;'>
                            <el-dropdown-item style='padding:3px;'>
                                <el-button type='text' icon='el-icon-remove'  @click='doDisable'>禁用</el-button>
                            </el-dropdown-item>
                            <el-dropdown-item style='padding:3px;'>
                                <el-button type='text' icon='el-icon-unlock' @click='doReset'>解除锁定</el-button>
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

        #region Main
        public string HtmlMainCode()
        {
            var code = new StringBuilder();
            var hide = Model.DetailsPage ? " v-if='!form.visible'" : null;
            var grid = HtmlGridCode(Model, "list.rows", Model.DetailsPage);

            code.Append($@"
            <el-container{hide}>{Tree}
                <el-main style='margin: 0; padding: 0;'>{grid}
                </el-main>{footer}
            </el-container>");
            if (Model.DetailsPage)
            {
                HtmlDetailsCode(code);
            }
            return code.ToString();
        }
        string DetailsField(IFieldConfig field)
        {
            return $@"
                            <div class='detailsField'>
                                <div class='myLabel'>{field.Caption}</div>
                                <div class='myValue'>{{{{details.data.{field.JsonName}{Formater(field)}}}}}</div>
                            </div>";
        }

        #endregion
        #region Tree

        string Formater(IFieldConfig field)
        {
            if (field.DataFormater != null)
            {
                return $" | {field.DataFormater}";
            }
            if (field.EnumConfig != null)
            {
                return $" | {field.EnumConfig.Name.ToLWord()}Formater";
            }
            else if (field.CsType == nameof(DateTime))
            {
                return field.IsTime ? "| formatTime" : "| formatDate";
            }
            else if (field.CsType == "bool")
            {
                return " | boolFormater";
            }
            else if (field.IsMoney)
            {
                return " | formatMoney";
            }
            else if (field.CsType == "decimal")
            {
                return " | thousandsNumber";
            }
            return null;
        }
        string Tree => !Model.TreeUi ? null : @"
                <el-aside style='height: 100%; width: 200px'>
                    <el-tree ref='tree' :props='tree.props' @current-change='onTreeNodeChanged' default-expand-all highlight-current highlight-current node-key='id'>
                        <span slot-scope='{ node, data }'>
                            <i :class='data.type | typeIcon'></i><span>{{ node.label }}</span>
                        </span>
                    </el-tree>
                </el-aside>";

        #endregion
        #region 表格

        string HtmlGridCode(IEntityConfig entity, string data, bool details)
        {
            var main = entity == Model ? "ref='dataTable' @sort-change='onSort' @row-dblclick='dblclick' @current-change='currentRowChange' @selection-change='selectionRowChange'" : null;
            var code = new StringBuilder();
            code.Append($@"
                <!-- Grid -->
                <el-table {main} :data='{data}' border highlight-current-row style='width:99%'>");
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
                GridField(code, property, property.Caption);
            }
            GridDetailsField(code, entity);

            if (details)
                code.Append(detailsColumn);
            code.Append(@"
            </el-table>");
            return code.ToString();
        }
        const string detailsColumn = @"
                <el-table-column fixed='right' label='操作' width='68'>
                    <template slot-scope='scope'>
                        <label class='labelButton' @click='handleClick(scope.row)'>详细</label>
                    </template>
                </el-table-column>";

        const string stateColumn = @"
                <el-table-column label='状态' align='center' header-align='center' width='50'>
                    <template slot-scope='scope'>
                        <i :class='scope.row.dataState | dataStateIcon'></i>
                    </template>
                </el-table-column>";

        const string footer = @"
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
            </el-footer>";

        void GridField(StringBuilder code, IFieldConfig field, string caption)
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
                var field = property;
                var caption = property.Caption;
                if (field.IsLinkKey)
                {
                    var friend =
                        entity.LastProperties.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
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
                                <label class='expand_label'>{caption}：</label>");

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
        #region 表单

        public string HtmlDialogCode()
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
        <div class='el-dialog__body_form'>{HtmlFormCode(true)}
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
            return code.ToString();
        }

        public void HtmlDetailsCode(StringBuilder code)
        {
            code.Append($@"
                <div v-if='form.visible'>
                    <el-tabs value='details'>
                        <el-tab-pane label='{Model.Caption}' name='details'>{HtmlFormCode(false)}
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
                        <el-tab-pane label='{ch.Caption}' name='{name}'>{HtmlGridCode(ch.ForeignEntity, "form." + name, false)}
                        </el-tab-pane>");
                }
            }
            code.Append(@"
                    </el-tabs>
                </div>");
        }

        public string HtmlFormCode( bool dialog)
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
                FormField(code, property, caption, description ?? property.Caption, dialog);
            }
            code.Append(@"
            </el-form>");
            return code.ToString();
        }

        private void FormField(StringBuilder code, IFieldConfig property, string caption, string description,bool dialog)
        {
            var field = property;
            if (!dialog && !(Model.IsUiReadOnly || property.IsUserReadOnly) && (field.IsMemo || field.MulitLine))
            {
                code.Append($@"<el-form-item label='{caption}' prop='{property.JsonName}' label-suffix='{field.Suffix}' style='width: 98%;'>
                                <el-input v-model='form.data.disposalContent' placeholder='处置方法' auto-complete='off' clearable :readonly='form.readonly' type='textarea' :rows='5'></el-input>
                            </el-form-item>");
                return;
            }
            code.Append($@"
            <el-form-item label='{caption}' prop='{property.JsonName}' label-suffix='{field.Suffix}'");
            if (field.Entity.FormCloumn > 1 && (field.FormCloumnSapn > 1 || field.IsMemo || field.MulitLine))
                code.Append(" size='large'");
            code.Append('>');
            if (Model.IsUiReadOnly || property.IsUserReadOnly)
                ReadonlyField(code, field);
            else
                EditField(code, field, description);
            code.Append(@"
            </el-form-item>");
        }
        void ReadonlyField(StringBuilder code, IFieldConfig field)
        {
            code.Append($@"
                    <span>{field.Prefix}{{{{form.data.{field.JsonName}{Formater(field)}}}}}{field.Suffix}</span>");
        }
        void EditField(StringBuilder code, IFieldConfig field, string description)
        {
            static void SetDisabled(bool disabled, StringBuilder code, IFieldConfig field)
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
                
                code.Append("></el-input>");
            }
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
            foreach (var property in Model.ClientProperty.Where(p => !p.NoStorage
                                                                    && !p.NoneGrid
                                                                    && !p.NoneGrid
                                                                    && !p.NoneDetails
                                                                    && !p.IsLinkKey
                                                                    && !p.IsPrimaryKey))
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

    }
}