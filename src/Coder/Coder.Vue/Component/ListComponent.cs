using Agebull.EntityModel.Config;
using System;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.VueComponents
{

    public class ListComponent
    {
        #region JS

        public static string JsCode(IEntityConfig model)
        {
            return $@"EntityComponents.add('{model.Project.PageFolder}-{model.PageFolder}','list', {{
    enableList:true,
    enableDetails:false
}});";
        }
        #endregion

        #region Html

        public static string HtmlCode(IEntityConfig model)
        {
            var component = new ListComponent
            {
                entity = model
            };
            return $@"<el-card class='box-card region-full-5' shadow='never'>
    <div class='region-body-0'>{component.HtmlGridCode().SpaceLine(2)}
    </div>
    <div class='region-foot'>
        <div class='viewWorkSpace'>
            <div class='viewList-pagination-refresh'>
                <el-button icon='el-icon-refresh' @click='refresh'></el-button>
            </div>
            <el-pagination @size-change='sizeChange' @current-change='pageChange' background
                layout='total, sizes, prev, pager, next, jumper' :current-page='list.page' :page-sizes='list.pageSizes'
                :page-size='list.pageSize' :total='list.total'>
            </el-pagination>
        </div>
    </div>
</el-card>";
        }

        IEntityConfig entity;
        string HtmlGridCode()
        {
            var code = new StringBuilder();
            code.Append($@"
<el-table ref='dataTable' height='100%' highlight-current-row border stripe :data='list.rows'
          @sort-change='onSort' @current-change='setCurrentRow' @selection-change='selectionRowChange' @row-dblclick='onListDbClick'>
    <el-table-column type='selection'align='center'header-align='center'></el-table-column>");


            if (entity.Interfaces.Contains("IStateData"))
            {
                code.Append(@"
    <el-table-column label='×´Ì¬' align='center' header-align='center' width='50'>
        <template slot-scope='scope'>
            <i :class='scope.row.dataState | dataStateIcon'></i>
        </template>
    </el-table-column>");
            }
            foreach (var property in entity.ClientProperty.Where(p => !p.NoneGrid && !p.GridDetails).ToArray())
            {
                GridField(code, property);
            }
            GridDetailsField(code);

            DetailsCommandsColumn(code);
            code.Append(@"
</el-table>");
            return code.ToString();
        }

        void DetailsCommandsColumn(StringBuilder code)
        {
            var cmds = entity.Commands.Where(p => p.IsSingleObject);
            if (!entity.DetailsPage && !cmds.Any())
                return;
            int cnt = 32 + (cmds.Count() * 1);
            if (entity.DetailsPage) cnt += 32;
            code.Append($@"
    <el-table-column fixed='right' label='²Ù×÷' width='{cnt}'>
        <template slot-scope='scope'>");
            bool first = entity.DetailsPage;
            if (entity.DetailsPage)
                code.Append(@"
            <label class='labelButton' @click='setAndShowCurrentRow(scope.row)'>ÏêÏ¸</label>");

            foreach (var cmd in cmds)
            {
                if (first) first = false;
                else
                    code.Append("&nbsp;&nbsp;");
                code.Append($@"
            <label class='labelButton' @click='{cmd.JsMethod}(scope.row.{entity.PrimaryColumn.JsonName})'>{cmd.Caption}</label>");
            }
            code.Append($@"
        </template>
    </el-table-column>");
        }

        void GridField(StringBuilder code, IPropertyConfig property)
        {
            var align = string.IsNullOrWhiteSpace(property.GridAlign) ? "left" : property.GridAlign;
            code.Append($@"
    <el-table-column prop='{property.JsonName}' header-align='center' align='{align}' label='{property.Caption}'");
            if (property.UserOrder)
                code.Append(" sortable='true'");
            if (property.GridWidth > 0)
                code.Append($" width={property.GridWidth}");
            code.Append($@">
        <template slot-scope='scope'>
            <span style='margin-left: 3px'>{property.Prefix}{{{{scope.row.{property.JsonName}{property.Formater()}}}}}{property.Suffix}</span>
        </template>
    </el-table-column>");
        }

        void GridDetailsField(StringBuilder code)
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
                    var friend = entity.DataTable.FindLast(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
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
                <label class='expand_label'>{caption}£º</label>");

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
    }
}