using System.Collections.Generic;
using System.Linq;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体排序器
    /// </summary>
    public class EntitySorter : ConfigModelBase
    {
        System.Collections.Generic.List<IFieldConfig> columns;
        System.Collections.Generic.List<IFieldConfig> dcs;
        private IEntityConfig entity;
        IFieldConfig pk, pc;
        /// <summary>
        /// 表结构对象
        /// </summary>
        public IEntityConfig Entity
        {
            get => entity;
            set
            {
                entity = value;
                pk = Entity.Entity.PrimaryColumn;
                pc = Entity.Properties.FirstOrDefault(p => p.IsCaption);
                columns = entity.Properties.Where(p => !p.IsDiscard && !p.IsPrimaryKey && !p.IsCaption).OrderBy(p => p.Index).ToList();
                dcs = entity.Properties.Where(p => p.IsDiscard).OrderBy(p => p.Index).ToList();
            }
        }

        void End()
        {
            foreach (var field in Entity.Properties)
            {
                field.Option.Identity = field.Option.Index;
            }
            var old = entity.Properties.ToArray();
            if (entity is EntityConfig e)
            {
                e.Properties.Clear();
                e.Properties.AddRange(old.Cast<FieldConfig>().OrderBy(p => p.Index));
            }
            if (entity is ModelConfig m)
            {
                m.Properties.Clear();
                m.Properties.AddRange(old.Cast<PropertyConfig>().OrderBy(p => p.Index));
            }
        }

        /// <summary>
        ///     列排序
        /// </summary>
        public void SortByGroup()
        {
            var idx = Entity.ColumnIndexStart;
            if (pk != null)
            {
                pk.Option.Index = ++idx;
            }
            if (pc != null)
            {
                pc.Option.Index = ++idx;
            }

            foreach (var column in columns.Where(p => string.IsNullOrWhiteSpace(p.Group)))
            {
                column.Option.Index = ++idx;
            }
            foreach (var column in columns.Where(p => !string.IsNullOrWhiteSpace(p.Group)).OrderBy(p=>p.LinkTable))
            {
                column.Option.Index = ++idx;
            }
            foreach (var column in columns.Where(p => string.IsNullOrWhiteSpace(p.Group)).OrderBy(p => p.LinkTable))
            {
                column.Option.Index = ++idx;
            }
            foreach (var field in dcs)
            {
                field.Option.Index = ++idx;
            }
            End();
        }

        /// <summary>
        ///     列排序
        /// </summary>
        public void SortFieldByIndex()
        {
            int idx = 0;

            if (pk != null)
            {
                pk.Option.Index = ++idx;

            }
            if (pc != null)
            {
                pc.Option.Index = ++idx;
            }

            foreach (var field in columns.OrderBy(p=>p.Index))
            {
                field.Option.Index = ++idx;
            }

            foreach (var field in dcs.OrderBy(p => p.Index))
            {
                field.Option.Index = ++idx;
            }
            End();
        }

        /// <summary>
        ///     列排序
        /// </summary>
        public void SortField()
        {
            int idx = 0;
            if (pk != null)
            {
                pk.Option.Index = ++idx;

            }
            if (pc != null)
            {
                pc.Option.Index = ++idx;
            }

            foreach (var field in columns.Where(p => !p.IsSystemField))
            {
                field.Option.Index = ++idx;
            }
            foreach (var field in columns.Where(p => p.IsSystemField))
            {
                field.Option.Index = ++idx;
            }
            foreach (var field in dcs)
            {
                field.Option.Index = ++idx;
            }
            End();
        }
    }
}