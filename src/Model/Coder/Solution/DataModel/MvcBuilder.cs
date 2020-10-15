using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    public sealed class MvcBuilder : SchemaCodeBuilder
    {
        #region 页面代码

        private const string HtmlTemplate = @"
@{{
    Layout = ""~/Views/Shared/_stdLayout.cshtml"";
    ViewBag.Title = ""〖龙之战谷〗 {0} 之 {1}"";
}}
@section scripts{{
    <script type='text/javascript'>
        function editRow(id) {{
            var sel = id < 0 ? {{ {4}: 0 }} : $('#grid').datagrid('getRows')[id];
            $('#editDlg').dialog({{
                modal: true,
                closed: true,
                onClose: function () {{
                }},
                buttons: createEditButton('#editDlg', function () {{
                    doSave('#fm', '@Url.Action(""Save"")/' + sel.{4}, function () {{
                        syncToSave();
                    }}, function () {{
                        closeDialog('#editDlg');
                        $('#grid').datagrid('reload');
                    }});
                }})
            }});

            ajaxLoadValue('编辑', '@Url.Action(""Detail"")/' + sel.{4}, null, function (data) {{
                $('#fm').form('load', data);
                openDialog('#editDlg', '编辑');
            }});
        }}

        function delRow(id) {{
            $.messager.confirm('确认', '确认删除吗?', function (r) {{
                if (r) {{
                    var sel = $('#grid').datagrid('getRows')[id];
                    ajaxOperator('删除', '@Url.Action(""Delete"")/' + sel.{4});
                    $('#grid').datagrid('reload');
                }}
            }});
        }}
        $(document).ready(function (){{
            //子列表
            var cols = [
                [
                    {{ align: 'center', field: 'checked', title: '选择', checkbox: true, width: 30 }},{2}
                    {{ field: 'opt', title: '修改', align: 'center', formatter: function (value, rec, index) {{
                            return iconFilter(rec, 'icon-edit', 'editRow(' + index + ' )');
                        }}
                    }},
                    {{ field: 'opt1', title: '删除', align: 'center', formatter: function (value, rec, index) {{
                            return iconFilter(rec, 'icon-cancel', 'delRow(' + index + ' )');
                        }}
                    }}
                ]
            ];
            $('#grid').datagrid({{
                url: '@Url.Action(""List"")/',
                height: $('#rBody').height() - 2,
                idField: '{4}',
                pageSize: 20,
                columns: cols,
                plain: 'true',
                striped: true,
                pagination: 'true',
                rownumbers: 'true',
                fitColumns: 'true',
                singleSelect: 'true',
                loadMsg: '正在载入数据,请稍候……',
                onLoadError: function () {{
                    showServerNoFind('数据载入');
                }},
                onDblClickRow: function (idx, data) {{
                    editRow(idx);
                }}
            }});
            var pager = $('#grid').datagrid('getPager');
            pager.pagination({{
                buttons: ['-', {{
                    iconCls: 'icon-add',
                    handler: function () {{ editRow(-1); }}
                }}]
            }});
        }});
    </script>
}}
@section detail
{{{3}
}}";

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        public override void CreateExCode(string path)
        {
            string dir = IOHelper.CheckPath(path, "Views", Table.EntityName);
            string file = Path.Combine(dir, "Index.cshtml");
            //if (File.Exists(file))
            //    return;
            string code = string.Format(HtmlTemplate
                , this.Project.DataBaseObjectName == "Plot" ? "策划数据" : "用户数据"
                , Table.Caption ?? Table.Description ?? Table.EntityName
                , EasyUiGrid()
                , EasyUiForm()
                , Table.PrimaryColumn.PropertyName);
            this.SaveCode(file, code);
        }

        private string EasyUiForm()
        {
            var jsonBuilder = new StringBuilder();
            int id = 0;
            foreach (PropertyConfig field in Table.PublishProperty)
            {
                id++;
                if (id == 1)
                    jsonBuilder.Append(@"
<div>");
                jsonBuilder.AppendFormat(@"
    <div style='width:480px;margin:5px;display: inline-block;'>
        <div style='width:100px;display: inline-block;'>{0}:</div>"
                    , field.Caption ?? field.PropertyName);
                if (field.IsPrimaryKey)
                {
                    jsonBuilder.AppendFormat(@"
        <input name='{0}' class='easyui-textbox' style='width:350px;height:16px' readonly='readonly'/>"
                        , field.PropertyName);
                }
                else
                {
                    string required = field.Nullable ? "" : @" data-options='required:true'";

                    switch (field.CsType.ToLower())
                    {
                        case "short":
                        case "int16":
                        case "int":
                        case "int32":
                        case "bigint":
                        case "long":
                        case "int64":
                            jsonBuilder.AppendFormat(@"
        <input name='{0}' class='easyui-numberbox' style='width:350px;height:24px'{1}/>"
                                , field.PropertyName
                                , required);
                            break;
                        case "decimal":
                        case "numeric":
                        case "real":
                        case "double":
                        case "float":
                            jsonBuilder.AppendFormat(@"
        <input name='{0}' class='easyui-numberbox' style='width:350px;height:24px'{1}/>"
                                , field.PropertyName
                                , required);
                            break;
                        case "datetime":
                        case "datetime2":
                            jsonBuilder.AppendFormat(@"
        <input name='{0}' class='easyui-datebox' style='width:350px;height:16px'{1}/>"
                                , field.PropertyName
                                , required);
                            break;
                        case "bool":
                        case "boolean":
                            if (field.Nullable)
                                jsonBuilder.AppendFormat(@"
        <input name='{0}'class='easyui-combobox' style='width:350px;height:24px' 
                data-options=""{1},valueField: 'id',textField: 'text',data: [{{id: null,text: '-'}},{{id: true,text: '是'}},{{id: false,text: '否'}}]""/>"
                                    , field.PropertyName
                                    , required);
                            else
                                jsonBuilder.AppendFormat(@"
        <input name='{0}'class='easyui-combobox' style='width:350px;height:24px' 
                data-options=""{1},valueField: 'id',textField: 'text',data: [{{id: true,text: '是'}},{{id: false,text: '否'}}]""/>"
                                    , field.PropertyName
                                    , required);
                            break;
                            //case "byte":
                            //case "char":
                            //case "guid":
                            //case "uniqueidentifier":
                            //case "nchar":
                            //case "varchar":
                            //case "nvarchar":
                            //case "string":
                            //case "text":
                        default:
                            jsonBuilder.AppendFormat(@"
        <input name='{0}' class='easyui-validatebox' style='width:350px;height:16px'{1}/>"
                                , field.PropertyName
                                , required);
                            break;
                    }
                }
                jsonBuilder.AppendFormat(@"
        <div style='margin-left:106px;width:350px;color:red;display: block;'>{0}</div>
    </div>", field.Description);
                if (id == 2)
                {
                    jsonBuilder.Append(@"
</div>");
                    id = 0;
                }
            }
            if (id != 0)
            {
                jsonBuilder.Append(@"
</div>");
            }
            return jsonBuilder.ToString();
        }

        private string EasyUiGrid()
        {
            var jsonBuilder = new StringBuilder();
            foreach (PropertyConfig field in Table.PublishProperty)
            {
                string align = field.CsType == "string" ? "left " : "center";
                string sortable = field.CsType == "string" ? "true " : "false";
                string extend = field.CsType == "bool" ? string.Format(",formatter: function (v, r, i) {{return r.{0} ? '是':'';}} ",field.PropertyName) : "";
                jsonBuilder.AppendFormat(@"
                    {{align: '{0}', sortable: {1}, field: '{2}', title: '{3}'{4}}},"
                    , align, sortable, field.PropertyName, field.Caption ?? field.PropertyName, extend);
            }
            return jsonBuilder.ToString();
        }

        #endregion

        #region 控制器代码

        private const string ControlTemplate = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HY.Model;
using HY.Web.Apis.Context;

namespace HY.GameApi.Lzzg.GameManager.Controllers
{{
    public sealed class {1}Controller : {0}ControllerBase<{1}>
    {{
        public ActionResult Index()
        {{
            return View();
        }}

        public MvcHtmlString List()
        {{
            return new MvcHtmlString(ListInner());
        }}

        public MvcHtmlString Save(int id)
        {{
            return new MvcHtmlString(SaveInner(id));
        }}


        public MvcHtmlString Delete(int id)
        {{
            return new MvcHtmlString(DeleteInner(id));
        }}

        public MvcHtmlString Detail(int id)
        {{
            return new MvcHtmlString(DetailInner(id));
        }}

        protected override void ReadFormData({1} entity, FormConvert convert)
        {{{2}
        }}
    }}
}}";

        /// <summary>
        ///     控制器代码
        /// </summary>
        public override void CreateBaCode(string path)
        {
            string dir = IOHelper.CheckPath(path, "Controllers", this.Project.DataBaseObjectName);
            string file = Path.Combine(dir, Table.EntityName + "Controller.cs");
            //if (File.Exists(file))
            //    return;
            string code = string.Format(ControlTemplate
                , this.Project.DataBaseObjectName
                , Table.EntityName
                , Save());
            this.SaveCode(file, code);
        }

        private string Save()
        {
            var code = new StringBuilder();
            foreach (PropertyConfig field in Table.PublishProperty.Where(p => !p.IsPrimaryKey))
            {
                switch (field.CsType.ToLower())
                {
                    case "short":
                    case "int16":
                    case "int":
                    case "int32":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullInteger(""{0}"");"
                                , field.PropertyName);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToInteger(""{0}"");"
                                , field.PropertyName);
                        break;
                    case "bigint":
                    case "long":
                    case "int64":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullLong(""{0}"");"
                                , field.PropertyName);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToLong(""{0}"");"
                                , field.PropertyName);
                        break;
                    case "decimal":
                    case "numeric":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullDecimal(""{0}"");"
                                , field.PropertyName);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToDecimal(""{0}"");"
                                , field.PropertyName);
                        break;
                    case "real":
                    case "double":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullDouble(""{0}"");"
                                , field.PropertyName);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToDouble(""{0}"");"
                                , field.PropertyName);
                        break;
                    case "float":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullSingle(""{0}"");"
                                , field.PropertyName);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToSingle(""{0}"");"
                                , field.PropertyName);
                        break;
                    case "datetime":
                    case "datetime2":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullDateTime(""{0}"");"
                                , field.PropertyName);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToDateTime(""{0}"");"
                                , field.PropertyName);
                        break;
                    case "bool":
                    case "boolean":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullBoolean(""{0}"");"
                                , field.PropertyName);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToBoolean(""{0}"");"
                                , field.PropertyName);
                        break;
                    case "guid":
                    case "uniqueidentifier":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullGuid(""{0}"");"
                                , field.PropertyName);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToGuid(""{0}"");"
                                , field.PropertyName);
                        break;
                        //case "byte":
                        //case "char":
                        //case "nchar":
                        //case "varchar":
                        //case "nvarchar":
                        //case "string":
                        //case "text":
                    default:
                        code.AppendFormat(@"
            entity.{0} = convert.ToString(""{0}"",{1});"
                            , field.PropertyName
                            , field.Nullable ? "true" : "false");
                        break;
                }
            }
            return code.ToString();
        }

        #endregion
    }
}