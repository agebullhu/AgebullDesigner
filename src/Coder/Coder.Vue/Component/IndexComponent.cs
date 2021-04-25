using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.VueComponents
{

    public class IndexComponent
    {
        IEntityConfig model;

        #region JS

        public static string JsCode(IEntityConfig model)
        {
            var com = new IndexComponent
            {
                model = model
            };

            com.LinkFunctions();
            com.TreeMethod();
            com.Command();
            var path = $"/{model.Project.PageFolder.ToLinuxPath()}/{model.PageFolder.ToLinuxPath()}";
            return $@"EntityComponents.loadCommon('{path}', '{model.ComponentName}', () => 
    routes['page-{model.ComponentName}'].option = new EditOption('{model.ComponentName}').toVueOption({{
        title: '{model.Caption}',
        stateData: { (model.Interfaces != null && model.Interfaces.IndexOf("IStateData") >= 0 ? "true" : "false")},
        auditData: { (model.Interfaces != null && model.Interfaces.IndexOf("IAuditData") >= 0 ? "true" : "false")},
        enableList: true,
        enableDetails: true,
        css: '{path}/index.css',
        mounted() {{
            let that = this;{com.readys.LinkToString(@"
            ,")}
        }}{com.trees},
        commands:{{{com.commands.LinkToString(@"
            ,")}
        }},
        entityComponents: [{{
            entity: '{model.ComponentName}',
            path: '{path}',
            item: ['list', 'form']
        }}]
    }})
);";
        }

        string trees;
        readonly List<string> commands = new List<string>();
        readonly List<string> readys = new List<string>();


        #region 下拉列表支持

        /// <summary>
        /// 下拉列表支持
        /// </summary>
        /// <returns></returns>
        void LinkFunctions()
        {
            var array = model.ClientProperty
                .Where(p => p.UserSee && !p.NoStorage && p.DataBaseField.IsLinkKey)
                .Select(p => GlobalConfig.GetEntity(p.DataBaseField.LinkTable))
                .Distinct().ToArray();
            if (array.Length == 0)
                return;
            foreach (var entity in array.Where(p => p.CaptionColumn != null))
            {
                var comboName = entity.Name.ToLWord().ToPluralism();

                readys.Add($@"
        this.ajax.load('载入{entity.Caption}','/{entity.Project.ApiName}/{entity.ApiName}/v1/edit/combo',null, d => v.combos.{comboName} = d);");

            }
        }
        #endregion

        #region 树

        void TreeMethod()
        {
            if (!model.TreeUi)
                return;
            trees = @",
        tree: {
            pid: 0,
            type: 'root',
            node: null,
            current: null,
            nodes: [],
            props: {
                label: 'label',
                children: 'children',
                isLeaf: 'isLeaf'
            },
            onTreeNodeChanged(data, node) {
                this.tree.pid = data.id;
                this.tree.type = data.type;
                this.tree.node = node;
                this.tree.current = data;
                this.refresh();
            }
        }";
            readys.Add(@"
        ajax_load('导航数据', `${v.apiPrefix}/edit/tree`, { pid: 0, type: 'root' }, d => v.tree.nodes = d);");
        }

        #endregion
        #region 命令

        void Command()
        {
            foreach (var cmd in model.Commands)
            {
                if (cmd.IsSingleObject)
                    commands.Add($@"
        {cmd.JsMethod}(id) {{
            this.confirmCall('{cmd.Caption}', `${{this.apiPrefix}}/{cmd.Api}`, {{id: id}})
        }}");
                else if (cmd.IsMulitOperator)
                    commands.Add($@"
        {cmd.JsMethod}() {{
            let ids = this.getSelectedRows(row => true);
            this.doConfirm('{cmd.Caption}', `${{this.apiPrefix}}/{cmd.Api}`, {{
                selects: ids
            }}));
        }}");
                else
                    commands.Add($@"
        {cmd.JsMethod}() {{
            this.doConfirm('{cmd.Caption}', `${{this.apiPrefix}}/{cmd.Api}`)
        }}");
            }
        }

        #endregion

        #endregion


        #region CSS

        public static string CssCode(IEntityConfig model)
        {
            var code = new StringBuilder();
            int left = 5;
            if (model.TreeUi)
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
            if (model.FormQuery)
            {
                var cnt = model.Properties.Count(p => p.CanUserQuery);
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
            return code.ToString();
        }

        #endregion Html

        #region Html
        public static string HtmlCode(IEntityConfig model)
        {
            var component = new IndexComponent
            {
                model = model
            };
            return component.Card();
        }

        public string Card()
        {
            var code = new StringBuilder();


            code.Append($@"
<div class='innerRegion'>
    <div id='viewHeader'>
        <div class='viewHeader-title'>{NavigateTitle(3)}
        </div>{ToolbarButton(2)}
    </div>
    <div class='viewBody'");
            if (model.DetailsPage)
                code.Append(" v-if='!detailsVisiable'");
            code.Append('>');

            if (model.TreeUi)
            {
                code.Append($@"
        <el-card class='box-card viewTree'>{tree.FormatLineSpace(3)}
        </el-card>");
            }
            if (model.FormQuery)
            {
                code.Append($@"
        <el-card class='box-card viewQuery'>
            <div class='innerRegion'>{FormQueryCode(4)}
            </div>
        </el-card>");
            }
            code.Append($@"
        <div class='viewList'{(model.DetailsPage ? " v-if='!detailsVisiable'" : null)}>
            <el-entity-{model.ComponentName}-list ref='listPanel' :queryFilter='queryModel' @showDetails='showDetails' @selectionRowChange='selectionRowChange' @selectChanged='selectChanged'></el-entity-{model.ComponentName}-list>
        </div>
    </div>");
            if (model.DetailsPage)
            {
                code.Append($@"
    <div class='viewBody' v-if='detailsVisiable'>
        <el-card class='box-card innerRegion'>
            <el-entity-{model.ComponentName}-form ref='formPanel' :id='currentId'> </el-entity-{model.ComponentName}-form>
        </el-card>
    </div>");
            }
            else
            {
                var col = model.FormCloumn <= 0 ? 2 : model.FormCloumn;
                var wid = col * 422 + 50;
                code.Append($@"
    <el-dialog title='【{model.Caption}】' :modal='false' :visible.sync='detailsVisiable' :show-close='false' :close-on-click-modal='false' width='{wid}px'>
        <div class='el-dialog__body_form' v-if='detailsVisiable'>
            <el-entity-{model.ComponentName}-form ref='formPanel' :id='currentId'> </el-entity-{model.ComponentName}-form>
        </div>
        <div slot='footer'>");
                if (!model.IsUiReadOnly)
                {
                    code.Append(@"
            <el-button icon='el-icon-check' @click='save' type='primary'>保存</el-button>");
                }
                code.Append(@"
            <el-button icon='el-icon-close' @click='detailsVisiable = false'>取消</el-button>
        </div>
    </el-dialog>");
            }
            code.Append($@"
</div>");
            return code.ToString();
        }

        #region Header & Footer

        private string NavigateTitle(int level)
        {
            var code = new StringBuilder();
            code.Append($@"
<template>
    <span>{model.Project.Caption}</span>&nbsp;<i class='el-icon-arrow-right'></i>");
            if (!model.Project.NoClassify)
            {
                var cls = model.Project.Classifies.FirstOrDefault(p => p.Name == model.Classify);
                if (cls != null)
                    code.Append($"&nbsp;<span>{cls.Caption}</span>&nbsp;<i class='el-icon-arrow-right'></i>");
            }
            if (model.DetailsPage)
            {
                code.Append($@"
    <label class='labelButton' @click='showList'>{model.Caption}</label>");

                if (model.CaptionColumn != null)
                    code.Append($@"
    <template v-if='detailsVisiable'>&nbsp;<i class='el-icon-arrow-right'></i>&nbsp;<span>{{{{form.data.{model.CaptionColumn.JsonName}}}}}</span></template>");
                else code.Append($@"
    <template v-if='detailsVisiable'>&nbsp;<i class='el-icon-arrow-right'></i>&nbsp;<span>详细内容</span></template>");
            }
            else
            {
                code.Append($"&nbsp;<span>{model.Caption}</span>");
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
    <el-button-group v-if='!detailsVisiable'>");

            if (!model.IsUiReadOnly)
            {
                exButton.Append(@"
        <el-button icon='el-icon-plus' @click='doAddNew' type='success' plain>新增</el-button>");
            }
            if (!model.DetailsPage)
            {
                if (!model.IsUiReadOnly)
                {
                    exButton.Append(@"
        <el-button icon='el-icon-edit' @click='showDetails' type='success' plain>编辑</el-button>");
                }
                else
                {
                    exButton.Append(@"
        <el-button icon='el-icon-edit' @click='showDetails' type='primary' plain>详细</el-button>");
                }
            }
            if (!model.IsUiReadOnly)
            {
                exButton.Append(@"
        <el-button icon='el-icon-close' @click='doDelete' type='danger' plain>删除</el-button>");
            }
            exButton.Append(@"
        <el-button icon='el-icon-document' @click='exportExcel' type='primary' plain>导出</el-button>");
            exButton.Append(@"
    </el-button-group>");

            if (model.DetailsPage)
            {
                exButton.Append(@"
    <el-button-group v-if='detailsVisiable'>
        <el-button icon='el-icon-back' @click='hideDetails' type='primary' plain>返回</el-button>");
                if (!model.IsUiReadOnly)
                    exButton.Append(@"
        <el-button icon='el-icon-check' @click='save' v-if='!form.readonly' type='success' plain>保存</el-button>");
                exButton.Append(@"
    </el-button-group>");
            }
            CommandsButton(exButton);
            if (!model.IsUiReadOnly)
            {
                if (model.Interfaces.Contains("IAuditData"))
                {
                    exButton.Append(@"
    <el-dropdown v-if='!detailsVisiable' split-button @click='doPass' @command='handleDataCommand'>
        <i class='el-icon-circle-check'></i>审核通过
        <el-dropdown-menu slot='dropdown'>
            <el-dropdown-item type='text' command='Deny' icon='el-icon-remove-outline'>不通过</el-dropdown-item>
            <el-dropdown-item type='text' command='Back' icon='el-icon-edit'>退回编辑</el-dropdown-item>
            <el-dropdown-item type='text' command='ReDo' icon='el-icon-edit'>重新审核</el-dropdown-item>
        </el-dropdown-menu>
    </el-dropdown>");
                }
                if (model.Interfaces.Contains("IStateData"))
                {
                    exButton.Append(@"
    <el-dropdown v-if='!detailsVisiable' split-button @click='doEnable'>
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
            var cmds = model.Commands.Where(p => p.IsMulitOperator).ToArray();
            if (cmds.Length == 0)
                return;
            code.Append(@"
    <el-button-group v-if='!detailsVisiable'>");
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
            foreach (var property in model.Where(p => p.CanUserQuery).ToArray())
            {
                code.FormField("query", false, property, false, 2);
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
            if (model.FormQuery)
            {
                return null;
            }
            StringBuilder quButton = new StringBuilder();
            var fields = model.DataTable.FindLastAndToArray(p => p.Property.CanUserQuery && p.Property.UserSee && !p.NoStorage && !p.IsLinkKey && !p.Property.IsPrimaryKey);
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

        #region 固定元素

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