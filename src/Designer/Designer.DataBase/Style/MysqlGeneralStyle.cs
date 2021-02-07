using System.Linq;
using System.Text;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     Mysql标准风格
    /// </summary>
    public class MysqlGeneralStyle : IDatabaseCodeStyle
    {
        string ICodeStyle.StyleName => CodeStyleConst.Style.General;

        string ICodeStyle.StyleTarget => CodeStyleConst.Target.DataBase;

        DataBaseType IDatabaseCodeStyle.DataBase => DataBaseType.MySql;

        string IDatabaseCodeStyle.FormatTableName(IEntityConfig entity)
        {
            var head = new StringBuilder("tb_");
            //if (!string.IsNullOrWhiteSpace(entity.Parent.Abbreviation))
            //{
            //    head.Append(entity.Parent.Abbreviation.ToLWord());
            //    head.Append('_');
            //}
            if (!entity.Project.NoClassify && entity.Classify != null && entity.Classify != "None")
            {
                var cls = entity.Project.Classifies.FirstOrDefault(p => p.Name == entity.Classify);
                if (cls != null)
                {
                    head.Append(cls.Abbreviation.ToLWord());
                    head.Append('_');
                }
            }
            return GlobalConfig.SplitWords(entity.Name).Select(p => p.ToLower()).LinkToString(head.ToString(), "_");

        }

        string IDatabaseCodeStyle.FormatFieldName(IFieldConfig field) => GlobalConfig.ToLinkWordName(field.Name, "_", false);

        string IDatabaseCodeStyle.FormatViewName(IEntityConfig entity) => "view_" + entity.Name.Replace("tb_", "");

    }
}
