using Agebull.EntityModel.Config;
using System;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.VueComponents
{

    public class FormComponent
    {
        #region JS

        public static string JsCode(IEntityConfig model)
        {
            return $@"EntityComponents.add('{model.Project.PageFolder}-{model.PageFolder}','form', {{
    enableList:false,
    enableDetails:true{Combos()}
}});";
            #region ÏêÏ¸Ò³Ãæ

            string Combos()
            {
                var load = new StringBuilder();

                foreach (var ch in model.DataTable.WhereLast(p => p.IsLinkKey && p.Property.UserSee))
                {
                    var entity = GlobalConfig.Find(ch.LinkTable);
                    var name = ch.Name.ToLWord().ToPluralism();
                    load.Append($@"
            this.ajax.load('{ch.Caption}','/{entity.Project.ApiName}/{entity.ApiName}/v1/edit/list',{{
                _size_ : 999
            }},data => that.form.{name} = data.rows);");
                }
                if (model is ModelConfig mc)
                {
                    foreach (var ch in mc.Releations)
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
                        load.Append($@"
            this.ajax.load('{ch.Caption}','/{mc.Project.ApiName}/{ch.ForeignEntity.ApiName}/v1/edit/list',{{
                {ch.ForeignField.JsonName}:row.id,
                _size_ : 999
            }},data => that.form.{name} = data.rows);");
                    }
                }

                if (load.Length > 0)
                    return $@"
        mounted() {{
            var that=this;{load}
        }}";
                return null;
            }
            #endregion
        }
        #endregion

        #region Html

        public static string HtmlCode(IEntityConfig model)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"
<el-form ref='dataForm' :rules='form.rules' :model='form.data' label-width='100px' label-position='left' @submit.native.prevent>");
            foreach (var property in model.ClientProperty.Where(p => !p.NoneDetails).ToArray())
            {
                code.FormField("form.data", true, property, model.IsUiReadOnly || property.IsUserReadOnly, model.FormCloumn);
            }
            code.Append(@"
</el-form>");
            return code.ToString();
        }

        #endregion

        #region JS


        #endregion
    }
}