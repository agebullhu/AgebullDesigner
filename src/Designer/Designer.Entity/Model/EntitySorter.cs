using System.Collections.Generic;
using System.Linq;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体排序器
    /// </summary>
    public class EntitySorter : ConfigModelBase
    {
        List<IPropertyConfig> columns;
        List<IPropertyConfig> dcs;
        private IEntityConfig entity;
        IPropertyConfig pk, pc;
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
                pc = Entity.Entity.CaptionColumn;
                columns = entity.Properties.Where(p => !p.IsDiscard && p != pc && p != pk).OrderBy(p => p.Index).ToList();
                dcs = entity.Properties.Where(p => p.IsDiscard && p != pc && p != pk).OrderBy(p => p.Index).ToList();
            }
        }

        void End()
        {
            foreach (var field in Entity.Properties)
            {
                field.Option.Identity = field.Option.Index;
            }
            if (entity is EntityConfig e)
            {
                e.Properties.Sort(p => p.OrderBy(p => p.Index));
            }
            if (entity is ModelConfig m)
            {
                m.Properties.Sort(p => p.OrderBy(p => p.Index));
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
            IEnumerable<IPropertyConfig> fields = columns.Where(p => !string.IsNullOrWhiteSpace(p.Group));
            if (!string.IsNullOrEmpty(pk.Group))
            {
                foreach (var column in columns.Where(p => p.Group == pk.Group).OrderBy(p => p.Index))
                {
                    column.Option.Index = ++idx;
                }
                fields = fields.Where(p => p.Group != pk.Group);
            }
            foreach (var group in fields.GroupBy(p => p.Group))
            {
                foreach (var column in group.OrderBy(p => p.Index))
                {
                    column.Option.Index = ++idx;
                }
            }
            foreach (var column in columns.Where(p => string.IsNullOrWhiteSpace(p.Group)).OrderBy(p => p.Index))
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

            foreach (var field in columns.OrderBy(p => p.Index))
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