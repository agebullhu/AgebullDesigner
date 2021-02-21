namespace Agebull.EntityModel.Config
{

    /// <summary>
    ///     Mysql精简风格
    /// </summary>
    public class MysqlSuccinctStyle : IDatabaseCodeStyle
    {
        string ICodeStyle.StyleName => CodeStyleConst.Style.Succinct;

        string ICodeStyle.StyleTarget => CodeStyleConst.Target.DataBase;


        DataBaseType IDatabaseCodeStyle.DataBase => DataBaseType.MySql;

        string IDatabaseCodeStyle.FormatTableName(IEntityConfig entity) => GlobalConfig.ToLinkWordName(entity.Name, "_", false);

        string IDatabaseCodeStyle.FormatFieldName(IPropertyConfig field) => GlobalConfig.ToLinkWordName(field.Name, "_", false);

        string IDatabaseCodeStyle.FormatViewName(IEntityConfig entity) => "view_" + GlobalConfig.ToLinkWordName(entity.Name, "_", false);

    }
}
