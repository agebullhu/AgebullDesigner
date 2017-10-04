using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Agebull.CodeRefactor.SolutionManager;
using Agebull.Common.DataModel;
using Gboxt.Common.DataAccess.Schemas;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Agebull.Common.SimpleDesign
{
    /// <summary>
    /// 配置读写模型
    /// </summary>
    public class ConfigIoModel : DesignModelBase
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void DoInitialize()
        {
            LoadLastSolution();
            if (!File.Exists(Context.FileName))
            {
                Load();
            }
            else
            {
                ReLoad();
            }
        }
        /// <summary>
        /// 强制保存
        /// </summary>
        public void SaveEntity()
        {
            if (MessageBox.Show("确认强制保存吗?\n要知道这存在一定破坏性!", "对象编辑", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }
            ConfigWriter writer = new ConfigWriter
            {
                Solution = Context.Solution,
                Directory = Path.GetDirectoryName(Context.Solution.FileName)
            };
            if (Context.SelectProject != null)
            {
                writer.SaveProject(Context.SelectProject, false);
                return;
            }
            var tables = Context.GetSelectEntities();
            foreach (var entity in tables)
            {
                writer.SaveEntity(entity, false);
            }
        }
        #region 文件读写
        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            Context.StateMessage = "正在保存...";
            if (Context.FileName == null)
            {
                var sfd = new SaveFileDialog
                {
                    Filter = @"简单数据结构文件|*.xml"
                };
                if (sfd.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                Context.FileName = sfd.FileName;
            }
            SaveSolution();
        }
        /// <summary>
        /// 保存解决方案
        /// </summary>
        public void SaveSolution()
        {
            ConfigWriter.Save(Context.Solution, Context.FileName);
            SolutionModel model = new SolutionModel
            {
                Solution = Context.Solution
            };
            model.ResetStatus();
            SaveLastSolution();
            Context.StateMessage = "保存成功";
        }
        /// <summary>
        /// 载入解决方案
        /// </summary>
        public void Load()
        {
            var sfd = new OpenFileDialog
            {
                Filter = "简单数据结构文件|*.xml",
                FileName = Context.FileName
            };

            if (sfd.ShowDialog() != true)
            {
                return;
            }
            Load(sfd.FileName);
        }
        /// <summary>
        /// 载入解决方案
        /// </summary>
        public void LoadGlobal()
        {
            Context.FileName = Path.Combine(GlobalConfig.ProgramRoot, "Global", "global.json");
            ReLoad();
        }
        /// <summary>
        /// 重新载入
        /// </summary>
        public void ReLoad()
        {
            Load(Context.FileName);
        }
        /// <summary>
        /// 载入解决方案
        /// </summary>
        /// <param name="sluFile"></param>
        public void Load(string sluFile)
        {
            Context.StateMessage = "正在载入...";
            LoadFile(sluFile);
            using (LoadingModeScope.CreateScope())
                Model.Tree.CreateTree();
            Context.NowJob = DesignContext.JobPropertyGrid;
            Context.StateMessage = "载入成功";
        }

        private void LoadFile(string sluFile)
        {
            Context.Solution = ConfigLoader.Load(sluFile);
            Context.FileName = sluFile;
            SaveLastSolution();
        }

        /// <summary>
        /// 新增解决方案
        /// </summary>
        public void CreateNew()
        {
            Context.Solution = new SolutionConfig
            {
                Entities = new ObservableCollection<EntityConfig>()
            };
            var sfd = new SaveFileDialog
            {
                Filter = @"简单数据结构文件|*.xml"
            };
            if (sfd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            SolutionConfig.SetCurrentSolution(Context.Solution);
            Context.FileName = sfd.FileName;
            SaveSolution();
            Load(sfd.FileName);
        }

        #endregion
        #region 用户操作记录

        private void SaveLastSolution()
        {
            Context.FileName = Context.FileName;
            // ReSharper disable once AssignNullToNotNullAttribute
            var file = Path.Combine(GlobalConfig.ProgramRoot, "Config", "history.bin");
            File.WriteAllText(file, Context.FileName, Encoding.UTF8);
        }

        private void LoadLastSolution()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var path = Path.Combine(GlobalConfig.ProgramRoot, "Config", "history.bin");
            if (!File.Exists(path))
                return;
            var text = File.ReadAllText(path);
            if (string.IsNullOrEmpty(text))
                return;
            var lines = text.Split('\n');
            if (lines.Length > 0)
                Context.FileName = lines[0].Trim();
        }
        #endregion
    }
}
