using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    internal class EntityDesignModelEx : EntityDesignModel
    {

        #region ��������
        /// <summary>
        /// ���������б�
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<CommandItem> commands)
        {
            commands.Add(new CommandItem
            {
                Signle = true,
                Command = new DelegateCommand(SortFieldByIndex),
                Catalog = "�ֶ�",
                Name = "���������(�޹���)",
                IconName = "tree_item"
            });
            commands.Add(new CommandItem
            {
                Signle = true,
                Command = new DelegateCommand(SortField),
                Name = "���������",
                Catalog = "�ֶ�",
                IconName = "img_filter"
            });
            commands.Add(new CommandItem
            {
                Signle = true,
                NoButton = true,
                Command = new DelegateCommand(SortByGroup),
                Name = "����������",
                Catalog = "�ֶ�",
                IconName = "img_filter"
            });
            commands.Add(new CommandItem
            {
                Signle = true,
                Command = new DelegateCommand(SplitTable),
                Name = "��ֵ��±�",
                Catalog = "�ֶ�",
                IconName = "img_add"
            });
            commands.Add(new CommandItem
            {
                Command = new DelegateCommand(RepairRegular),
                Name = "�����޸�",
                Signle = true,
                NoButton = true,
                Catalog = "����У��",
                IconName = "tree_item"
            });
            commands.Add(new CommandItem
            {
                Command = new DelegateCommand(AddNewProperty),
                Name = "�����ֶ�",
                Signle = true,
                NoButton = true,
                Catalog = "�ֶ�",
                IconName = "tree_Open"
            });
            commands.Add(new CommandItem
            {
                Command = new DelegateCommand(AddFields),
                Name = "��������ֶ�",
                Signle = true,
                NoButton = true,
                Catalog = "�ֶ�",
                IconName = "tree_Open"
            });
            commands.Add(new CommandItem
            {
                Command = new DelegateCommand(CopyTable),
                Name = "���Ʊ�",
                Signle = true,
                NoButton = true,
                Catalog = "�ֶ�",
                IconName = "tree_Child1"
            });
            commands.Add(new CommandItem
            {
                Command = new DelegateCommand(DeleteTable),
                Name = "ɾ����",
                Signle = true,
                NoButton = true,
                IconName = "img_del"
            });
            commands.Add(new CommandItem
            {
                NoButton = true,
                Catalog = "�ֶ�",
                Command = new DelegateCommand(ToEnglish),
                Name = "�ֶη���",
                IconName = "tree_item"
            });
        }

        #endregion



        #region �ֶα༭

        public void ToEnglish()
        {
            try
            {
                var tables = Context.GetSelectEntities();
                foreach (var entity in tables)
                {
                    var model = new EntityBusinessModel { Entity = entity };
                    model.ToEnglish();
                }
            }
            catch (Exception ex)
            {
                Context.CurrentTrace.TraceMessage.Status = ex.ToString();
            }
        }
        public void SortByGroup()
        {
            if (Context.SelectEntity == null ||
                MessageBox.Show($"ȷ���޸�{Context.SelectEntity.ReadTableName}���ֶ�˳����?", "����༭", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }

            var model = new EntitySorter { Entity = Context.SelectEntity };
            model.SortByGroup();
        }

        public void SortField()
        {
            EntityConfig entity = Context.SelectEntity;
            if (entity == null ||
                MessageBox.Show($"ȷ���޸�{entity.ReadTableName}���ֶ�˳����?", "����༭", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }

            var model = new EntitySorter { Entity = entity };
            model.SortField();
        }


        public void SortFieldByIndex()
        {
            var result = MessageBox.Show("�ǰ���Ŵ�С���򲢴�0�������,�������Ŵ�С����", "����", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }
            var tables = Context.GetSelectEntities();
            foreach (var entity in tables)
            {
                var business = new EntitySorter
                {
                    Entity = entity
                };
                business.SortFieldByIndex(result == MessageBoxResult.Yes);
            }
        }



        #endregion


        #region �༭��

        private void AddRelation(PropertyConfig column)
        {
            var config = new TableReleation
            {
                Parent = Context.SelectRelationTable,
                Name = column.Parent.Name,
                Friend = column.Parent.Name,
                ForeignKey = column.Name,
                PrimaryKey = Context.SelectRelationColumn.Name
            };
            if (CommandIoc.NewConfigCommand("����������Ϣ", config))
                Context.SelectRelationTable.Releations.Add(config);
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void AddNewProperty()
        {
            EntityConfig entity = Context.SelectEntity;
            var config = new PropertyConfig
            {
                Parent = entity,
                Name = "NewField",
                CsType = "string"
            };
            if (CommandIoc.NewConfigCommand("�����ֶ�", config))
                entity.Properties.Add(config);
        }

        private bool CanAddRelation(PropertyConfig column)
        {
            return column?.Parent != null && Context.SelectRelationTable != null
                   && Context.SelectRelationColumn != null
                   && Context.SelectRelationTable.Releations.All(p => p.Friend != column.Parent.Name);
        }
        public void RepairRegular()
        {
            var result = MessageBox.Show("�����ù滮,�����鲢�޸Ĳ���ȷ��������", "������", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }
            var tables = Context.GetSelectEntities();
            foreach (var entity in tables)
            {
                EntityBusinessModel business = new EntityBusinessModel
                {
                    Entity = entity
                };
                business.RepairRegular(result == MessageBoxResult.Yes);
            }
        }
        public void SplitTable()
        {
            if (Context.SelectEntity == null || Context.SelectColumns == null || Context.SelectColumns.Count == 0)
            {
                return;
            }
            var oldTable = Context.SelectEntity;
            var newTable = new EntityConfig();
            newTable.CopyValue(oldTable, true);
            if (!CommandIoc.NewConfigCommand("��ֵ���ʵ��", newTable))
                return;
            if (oldTable.PrimaryColumn != null)
            {
                var kc = new PropertyConfig();
                kc.CopyFrom(oldTable.PrimaryColumn);
                newTable.Properties.Add(kc);
            }
            foreach (var col in Context.SelectColumns.OfType<PropertyConfig>().ToArray())
            {
                oldTable.Properties.Remove(col);
                newTable.Properties.Add(col);
            }
            Context.Entities.Add(newTable);
            //Model.Tree.SetSelect(newTable);
            Context.SelectColumns = null;
        }


        public void DeleteTable()
        {
            EntityConfig entity = Context.SelectEntity;
            if (entity == null ||
                MessageBox.Show($"ȷ��ɾ��{entity.ReadTableName}��?", "����༭", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }
            Context.Entities.Remove(entity);
            Context.SelectColumns = null;
        }


        public void AddFields()
        {
            EntityConfig entity = Context.SelectEntity;
            if (entity == null)
            {
                return;
            }
            var nentity = CommandIoc.AddFieldsCommand();
            if (nentity == null)
            {
                return;
            }
            entity.Properties.AddRange(nentity.Properties);
        }



        public void CopyTable()
        {
            EntityConfig entity = Context.SelectEntity;
            Context.CopiedTable = new EntityConfig();
            Context.CopiedTable.CopyValue(entity);
            Context.CopiedTables.Add(Context.CopiedTable);
            RaisePropertyChanged(() => Context.CopiedTableCounts);
        }

        #endregion
    }
}