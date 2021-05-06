using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;
using System.Collections;

namespace Agebull.EntityModel.Designer
{
    public class EntityExtendModel<TEntend, TField> : DesignModelBase, IGridSelectionBinding
        where TEntend : EntityExtendConfig
        where TField : FieldExtendConfig<TEntend>
    {
        #region 操作命令

        public EntityExtendModel()
        {
            Model = DataModelDesignModel.Current;
            Context = DataModelDesignModel.Current?.Context;
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void DoInitialize()
        {
            base.DoInitialize();
            iEntity = DesignModel as IEntityConfig;
        }

        IEntityConfig iEntity;

        /// <summary>
        /// 当前编辑的实体
        /// </summary>
        public IEntityConfig Entity
        {
            get => iEntity;
            set
            {
                iEntity = value;
                RaisePropertyChanged(nameof(Entity));
            }
        }

        TEntend extend;
        /// <summary>
        /// 当前编辑的对象
        /// </summary>
        public TEntend Extend
        {
            get => extend;
            set
            {
                extend = value;
                RaisePropertyChanged(nameof(Extend));
            }
        }

        TField selectField;
        /// <summary>
        /// 当前编辑的对象
        /// </summary>
        public TField SelectField
        {
            get => selectField;
            set
            {
                selectField = value;
                RaisePropertyChanged(nameof(SelectField));
            }
        }

        private IList _selectColumns;

        /// <summary>
        ///     当前选择
        /// </summary>
        public IList SelectColumns
        {
            get => _selectColumns;
            set
            {
                _selectColumns = value;
                if (SelectColumns != null && SelectColumns.Count > 0 && SelectColumns[0] is TField field)
                {
                    SelectField = field;
                }
                RaisePropertyChanged(() => SelectColumns);
            }
        }

        #endregion
    }

}