// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ImportModel : DesignCommondBase<ProjectConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<string, ProjectConfig>(OpenAssemblyFile, AnalyzeAssemblyFile, AnalyzeAssemblyEnd)
            {
                Catalog = "文件",
                SoruceView = "entity",
                Caption = "导入程序集",
                IconName = "tree_Assembly",
                TargetType = typeof(ProjectConfig)
            });
            /*
            commands.Add(new CommandItemBuilder<ProjectConfig>
            {
                Catalog = "文件",
                Action = AnalyzeConfig,
                Caption = "导入配置对象(自升级)",
                TargetType = typeof(ProjectConfig),
                IconName = "tree_Assembly"
            });*/
            commands.Add(new CommandItemBuilder<string, List<EntityConfig>>(OpenEdmxFile, AnalyzeEdmxFile, AnalyzeEdmxFilenEnd)
            {
                Catalog = "文件",
                SoruceView = "entity",
                Caption = "导入EF配置文件",
                TargetType = typeof(ProjectConfig),
                IconName = "tree_Assembly"
            });
            commands.Add(new CommandItemBuilder<ProjectConfig>
            {
                Catalog = "文件",
                SoruceView = "entity",
                Action = ImportToExcel,
                Caption = "导出Excel文档",
                TargetType = typeof(ProjectConfig),
                IconName = "tree_Assembly"
            });
        }

        #region 添加配置文件

        internal string OpenEdmxFile()
        {
            var dialog = new OpenFileDialog
            {
                Filter = @"Entity Frame 配置文件|*.edmx",
                InitialDirectory = Path.Combine(GlobalConfig.RootPath, "DataAccess", "Model")
            };
            if (dialog.ShowDialog() != true)
            {
                return null;
            }

            Model.Context.CurrentTrace.TraceMessage = TraceMessage.DefaultTrace;
            Model.Context.CurrentTrace.TraceMessage.Clear();
            return dialog.FileName;
        }

        /// <summary>
        ///     分析SLN文件
        /// </summary>
        /// <returns></returns>
        internal List<EntityConfig> AnalyzeEdmxFile(string file)
        {
            var reader = new EdmxReader();
            reader.Open(file);
            return reader.Tables;
        }

        internal void AnalyzeEdmxFilenEnd(CommandStatus status, Exception ex, List<EntityConfig> tables)
        {
            if (status != CommandStatus.Succeed)
            {
                return;
            }
            Context.SelectProject.Entities.Clear();
            foreach (var table in tables)
                Context.SelectProject.Add(table);
        }

        #endregion

        #region 添加程序集

        internal string OpenAssemblyFile()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "程序集|*.dll"
            };
            if (dialog.ShowDialog() != true)
            {
                return null;
            }
            return dialog.FileName;
        }

        /// <summary>
        ///     分析程序集
        /// </summary>
        /// <returns></returns>
        internal ProjectConfig AnalyzeAssemblyFile(string file)
        {
            return AssemblyImporter.Import(Model.Context.SelectProject, file);
        }

        /// <summary>
        ///     分析程序集
        /// </summary>
        /// <returns></returns>
        internal void AnalyzeConfig(ProjectConfig arg)
        {
            AssemblyImporter.Import(arg, typeof(ProjectConfig).Assembly);
        }
        internal void AnalyzeAssemblyEnd(CommandStatus status, Exception ex, ProjectConfig schema)
        {
            if (status != CommandStatus.Succeed)
            {
                MessageBox.Show("错误");
                return;
            }
            MessageBox.Show("完成");
        }

        #endregion

        #region 导出到EXCEL

        internal void ImportToExcel(object arg)
        {
            var dialog = new SaveFileDialog
            {
                Filter = @"Excel文件|*.xls",
                FileName = $"{(Context.SelectProject != null ? Context.SelectProject.Name : Context.Solution.Name)}"
            };
            if (dialog.ShowDialog() != true)
            {
                return;
            }
            using (CodeGeneratorScope.CreateScope(SolutionConfig.Current))
            {
                DesignToExcel.Import(dialog.FileName, Context.GetSelectEntities());
            }
        }
        #endregion

        #region 导入表信息

        public bool ReadFieldesPrepare(string arg, Action<string> setAction)
        {
            var sfd = new OpenFileDialog
            {
                Filter = "EXCEL|*.xls|所有文件|*.*"
            };
            if (sfd.ShowDialog() == true)
            {
                setAction(sfd.FileName);
                return true;
            }
            return false;
        }


        public string DoReadFieldes(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                var workbook = new HSSFWorkbook(stream);
                foreach (EntityConfig entity in Context.Entities)
                {
                    LoadFieldInfos(workbook, entity);
                }
            }
            return string.Empty;
        }

        public static void LoadFieldInfos(HSSFWorkbook workbook, EntityConfig entity)
        {
            if (!(workbook.GetSheet(entity.Name) is HSSFSheet sheet))
                return;
            for (int idx = 0; idx < 255; idx++)
            {
                var row = sheet.GetRow(idx) as HSSFRow;
                string t1 = row?.Cells[0].StringCellValue;
                if (string.IsNullOrWhiteSpace(t1))
                    break;
                var field = entity.Properties.FirstOrDefault(p => p.Name == t1);
                if (field == null)
                    continue;
                string c = row.Cells[1].StringCellValue;
                field.Caption = c == "不详" ? t1 : c;
                field.Description = row.Cells[2].StringCellValue;
            }
        }

        internal void ReadFieldesEnd(CommandStatus status, Exception ex, string code)
        {
            if (status == CommandStatus.Succeed)
            {
                Model.ConfigIo.SaveProject();
            }
        }

        #endregion

        #region 导入策划表

        public bool ImportPlotPrepare(string arg, Action<string> setAction)
        {
            var sfd = new FolderBrowserDialog
            {
                SelectedPath = @"D:\PrjH\PrjH\策划表"
            };
            if (sfd.ShowDialog() != DialogResult.OK)
                return false;
            setAction(sfd.SelectedPath);
            return true;
        }


        public bool DoImportPlot(string path)
        {
            try
            {
                foreach (var v in Directory.GetFiles(path, "*.xls"))
                {
                    LoadData(v);
                }
            }
            catch (Exception ex)
            {
                Model.Context.CurrentTrace.TraceMessage.Track = ex.ToString();
                return false;
            }
            return true;
        }


        public void LoadData(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                Model.Context.CurrentTrace.TraceMessage.Message1 = file;
                Model.Context.CurrentTrace.TraceMessage.Message2 = "Start";
                var ex = Path.GetExtension(file);
                IWorkbook workbook = ex == ".xlsx" ? (IWorkbook)new XSSFWorkbook(stream) : new HSSFWorkbook(stream);
                var f = Path.GetFileNameWithoutExtension(file);
                switch (f)
                {
                    case "Activity（活动相关）":
                        ImportPlot("Activity", workbook.GetSheet("Activity"));
                        break;
                    case "BoxDrawList（副本宝箱翻牌表）":
                        ImportPlot("BoxDrawList", workbook.GetSheet("BoxDrawList"));
                        break;
                    case "Charge（充值选项表）":
                        ImportPlot("Charge", workbook.GetSheet("Charge"));
                        break;
                    case "DrawListNew（抽宝箱、友情、王者表）":
                        ImportPlot("DrawListNew", workbook.GetSheet("DrawListNew"));
                        break;
                    case "EquipmentStrengthen（装备强化消耗表）":
                        ImportPlot("EquipmentStrengthen", workbook.GetSheet("Sheet1"));
                        break;
                    case "vip":
                        ImportPlot("Vip", workbook.GetSheet("Vip"));
                        break;
                    case "Fb（副本表）":
                        ImportPlot("Fb", workbook.GetSheet("Fb"));
                        break;
                    case "FbDialogue（副本剧情对话表）":
                        ImportPlot("FbDialogue", workbook.GetSheet("FbDialogue"));
                        break;
                    case "FbList（副本怪物配置表）":
                        ImportPlot("FBList", workbook.GetSheet("FBList"));
                        break;
                    case "FbTarget（副本目标&三星目标表）":
                        ImportPlot("FbTarget", workbook.GetSheet("FbTarget"));
                        break;
                    case "GiftBag（礼包表）":
                        ImportPlot("GiftBag", workbook.GetSheet("GiftBag"));
                        break;
                    case "Guidance（新手引导）":
                        ImportPlot("Guidance", workbook.GetSheet("Guidance"));
                        break;
                    case "HeroAttribute（主角等级属性）":
                        ImportPlot("HeroAttribute", workbook.GetSheet("Sheet1"));
                        break;
                    case "Herolist（职业角色列表）":
                        ImportPlot("HeroList", workbook.GetSheet("HeroList"));
                        break;
                    case "Ladder（天梯表）":
                        ImportPlot("Ladder", workbook.GetSheet("Ladder"));
                        break;
                    case "MaBeastAttribute（幻兽各品阶详细属性表）":
                        ImportPlot("MaBeastAttribute", workbook.GetSheet("MaBeastAttribute"));
                        break;
                    case "MaBeastFb（幻兽副本表）":
                        ImportPlot("MaBeastFb", workbook.GetSheet("MaBeastFb"));
                        break;
                    case "MaBeastList（幻兽列表）":
                        ImportPlot("MaBeastList", workbook.GetSheet("MaBeastList"));
                        break;
                    case "MaBeastSkill（幻兽技能表-光环buff表）":
                        ImportPlot("MaBeastSkill", workbook.GetSheet("MaBeastSkill"));
                        break;
                    case "MonsterList（怪物信息表）":
                        ImportPlot("MonsterList", workbook.GetSheet("MonsterList"));
                        break;
                    case "MonsterSkill（怪物技能表）":
                        ImportPlot("Monsterskill", workbook.GetSheet("Monsterskill"));
                        break;
                    case "Npc":
                        ImportPlot("Npc", workbook.GetSheet("Npc"));
                        break;
                    case "PassiveSkill（龙魂）":
                        ImportPlot("PassiveSkill", workbook.GetSheet("PassiveSkill"));
                        break;
                    case "Piece（碎片表）":
                        ImportPlot("Piece", workbook.GetSheet("Piece"));
                        break;
                    case "PublicData":
                        ImportPlot("PublicData", workbook.GetSheet("Sheet1"));
                        break;
                    case "QianList":
                        ImportPlot("QianList", workbook.GetSheet("QianList"));
                        break;
                    case "RandomGift（随机宝箱表）":
                        ImportPlot("RandomGift", workbook.GetSheet("RandomGift"));
                        break;
                    case "RankAward（排行榜奖励表）":
                        ImportPlot("RankAward", workbook.GetSheet("RankAward"));
                        break;
                    case "Refine（精炼属性表）":
                        ImportPlot("Refine", workbook.GetSheet("Refine"));
                        break;
                    case "Remind（活动提醒）":
                        ImportPlot("Remind", workbook.GetSheet("Remind"));
                        break;
                    case "ResourceGuide":
                        ImportPlot("ResourceGuide", workbook.GetSheet("ResourceGuide"));
                        break;
                    case "Reward（悬赏任务表）":
                        ImportPlot("Reward", workbook.GetSheet("Reward"));
                        break;
                    case "SetAttribute（套装属性表）":
                        ImportPlot("SetAttribute", workbook.GetSheet("SetAttribute"));
                        break;
                    case "SetGem（宝石孔开启表）":
                        ImportPlot("SetGem", workbook.GetSheet("Sheet1"));
                        break;
                    case "Shop（商店表）":
                        ImportPlot("Shop", workbook.GetSheet("Shop"));
                        break;
                    case "Skill（技能表）":
                        ImportPlot("Skill", workbook.GetSheet("Skill"));
                        break;
                    case "SkillUpCost（技能升级消耗表）":
                        ImportPlot("SkillUpCost", workbook.GetSheet("Sheet1"));
                        break;
                    case "Target（日常&成就表）":
                        ImportPlot("Target", workbook.GetSheet("Target"));
                        break;
                    case "Task（任务表）":
                        ImportPlot("Task", workbook.GetSheet("Task"));
                        break;
                    case "Tips（小提示表）":
                        ImportPlot("Tips", workbook.GetSheet("Tips"));
                        break;
                    case "Welfare（福利表）":
                        ImportPlot("Welfare", workbook.GetSheet("Welfare"));
                        break;
                    case "Vip_Activity（活动次数跟VIP关联表）":
                        ImportPlot("Vip_Activity", workbook.GetSheet("Vip_Activity"));
                        break;
                    case "WingAttribute（神翼进阶属性表）":
                        ImportPlot("WingAttribute", workbook.GetSheet("WingAttribute"));
                        break;
                    case "WingBase（神翼基础属性表）":
                        ImportPlot("WingBase", workbook.GetSheet("WingBase"));
                        break;
                    case "MabeastUpLevel（幻兽等级属性）":
                        ImportPlot("MabeastUpLevel", workbook.GetSheet("Sheet1"));
                        break;
                    case "MabeastQualityUp（幻兽进阶属性）":
                        ImportPlot("MabeastQualityUp", workbook.GetSheet("Sheet1"));
                        break;
                    case "Equipment（道具表）":
                        ImportPlot("Equipments", workbook.GetSheet("Equipments"));
                        break;
                    default:
                        Model.Context.CurrentTrace.TraceMessage.Message2 = "未使用";
                        return;
                }
                Model.Context.CurrentTrace.TraceMessage.Message2 = "succeed";
            }
            Model.Context.CurrentTrace.TraceMessage.Message2 = "AllRight";
        }

        internal void ImportPlot(string name, ISheet sheet)
        {
            var entity = Context.Entities.FirstOrDefault(p => p.Name == name);
            if (entity == null)
            {
                Model.Context.CurrentTrace.TraceMessage.Message3 = "无对应表格";
                return;
            }
            foreach (var col in entity.Properties)
                col.Option.IsDiscard = true;

            var row = sheet.GetRow(0);
            foreach (var cell in row.Cells)
            {
                if (cell.CellType != CellType.String)
                    cell.SetCellType(CellType.String);
                var field = cell.StringCellValue;
                if (string.IsNullOrWhiteSpace(field))
                    break;
                var desc = string.Empty;
                if (cell.CellComment?.String != null)
                    desc = cell.CellComment.String.String;
                var col = entity.Properties.FirstOrDefault(p => string.Equals(p.Name, field, StringComparison.OrdinalIgnoreCase));
                if (col != null)
                {
                    col.Option.ResetState();
                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        string desco = col.Description;
                        if (!string.IsNullOrWhiteSpace(desco))
                        {
                            desco = desco.Replace(desc, string.Empty);
                        }
                        col.Description = desc + desco;
                    }
                }
                else
                {
                    Model.Context.CurrentTrace.TraceMessage.Track = "新增字段:" + field;
                    entity.Add(new FieldConfig
                    {
                        Name = field,
                        Description = desc,
                        DbFieldName = field,
                        CsType = "string",
                        FieldType = "nvarchar",
                        DbNullable = true,
                        Nullable = true,
                        CanEmpty = true
                    });
                }
            }
            foreach (var col in entity.Properties.Where(p => p.IsDiscard))
            {
                Model.Context.CurrentTrace.TraceMessage.Track = "过时字段:" + col.Name;
            }
        }
        internal void ImportPlotEnd(CommandStatus status, Exception ex, bool result)
        {
            if (status != CommandStatus.Succeed || !result)
            {
                MessageBox.Show(ex?.ToString() ?? "失败");
            }
            else
            {
                MessageBox.Show("完成");
            }
        }


        #endregion
    }
}
