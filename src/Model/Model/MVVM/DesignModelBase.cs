using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agebull.EntityModel.Designer
{
    public class DesignModelBase : ModelBase
    {
        #region ��������

        public DesignModelBase()
        {
            CommondFilter = cmd =>
            {
                if (cmd.Editor.IsMissing() || EditorName.IsMissing() || !cmd.Editor.Contains(EditorName, StringComparison.OrdinalIgnoreCase))
                    return false;
                return true;
            };
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected override void DoInitialize()
        {
            DesignModelType = DesignModel?.GetType();
        }

        /// <summary>
        /// ��ǰ�༭�Ķ���
        /// </summary>
        public ConfigBase DesignModel
        {
            get => designModel;
            set
            {
                designModel = value;
                RaisePropertyChanged(nameof(DesignModel));
            }
        }

        /// <summary>
        /// ��ǰ�����������
        /// </summary>
        public Func<ICommandItemBuilder, bool> CommondFilter { get; set; }

        /// <summary>
        ///     ����
        /// </summary>
        public string EditorName { get; set; }

        /// <summary>
        ///     ����
        /// </summary>
        public Type DesignModelType { get; set; }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="commands"></param>
        public NotificationList<CommandItemBase> CreateCommands()
        {
            var commands = new List<CommandItemBase>();
            CreateCommands(commands);

            if (DesignModelType != null && CommondFilter != null)
                CommandCoefficient.EditorToolbar(commands, CommondFilter, DesignModelType);
            return commands.Where(FilterCommand).ToNotificationList<CommandItemBase>();
        }

        /// <summary>
        /// ����ICommandModel
        /// </summary>
        public NotificationList<CommandItemBase> Commands => CreateCommands();

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="commands"></param>
        public virtual void CreateCommands(IList<CommandItemBase> commands)
        {
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected virtual bool FilterCommand(CommandItemBase command)
        {
            if (command.CanButton)
                command.IsButton = true;
            command.Source = DesignModel;
            //command.SignleSoruce = true;
            return true;
        }
        #endregion

        #region ��������

        /// <summary>
        /// ͬ������������
        /// </summary>
        public virtual void OnSolutionChanged()
        {
        }

        /// <summary>
        /// ������
        /// </summary>
        private DesignContext _context;

        private DataModelDesignModel _model;
        private EditorModel _editor;
        private ConfigBase designModel;

        /// <summary>
        /// ����ģ��
        /// </summary>
        public DataModelDesignModel Model
        {
            get => _model;
            set
            {
                _model = value;
                RaisePropertyChanged(nameof(Model));
            }
        }

        /// <summary>
        /// �༭��ģ��
        /// </summary>
        public EditorModel Editor
        {
            get => _editor;
            set
            {
                _editor = value;
                RaisePropertyChanged(nameof(Editor));
            }
        }


        /// <summary>
        /// ������
        /// </summary>
        public DesignContext Context
        {
            get => _context;
            set
            {
                if (_context == value)
                    return;
                if (_context != null)
                    _context.PropertyChanged -= Context_PropertyChanged;
                _context = value;
                if (_context != null)
                    _context.PropertyChanged += Context_PropertyChanged;
                RaisePropertyChanged(nameof(Context));

            }
        }

        /// <summary>
        /// ���������Ա仯
        /// </summary>
        private void Context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnContextPropertyChanged(e.PropertyName);
        }

        /// <summary>
        /// ���������Ա仯
        /// </summary>
        protected virtual void OnContextPropertyChanged(string name)
        {
        }

        #endregion

    }

}