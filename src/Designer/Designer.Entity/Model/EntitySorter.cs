using System.Collections.Generic;
using System.Linq;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ʵ��������
    /// </summary>
    public class EntitySorter : ConfigModelBase
    {
        List<IFieldConfig> columns;
        List<IFieldConfig> dcs;
        private IEntityConfig entity;
        IFieldConfig pk, pc;
        /// <summary>
        /// ��ṹ����
        /// </summary>
        public IEntityConfig Entity
        {
            get => entity;
            set
            {
                entity = value;
                pk = Entity.Entity.PrimaryColumn;
                pc = Entity.Entity.CaptionColumn;
                columns = entity.Properties.Where(p => !p.IsDiscard && p!=pc && p!=pk).OrderBy(p => p.Index).ToList();
                dcs = entity.Properties.Where(p => p.IsDiscard && p != pc && p != pk).OrderBy(p => p.Index).ToList();
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
        ///     ������
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
            IEnumerable<IFieldConfig> fields = columns.Where(p => !string.IsNullOrWhiteSpace(p.Group));
            if (!string.IsNullOrEmpty(pk.Group))
            {
                foreach (var column in columns.Where(p => p.Group == pk.Group).OrderBy(p => p.Index))
                {
                    column.Option.Index = ++idx;
                }
                fields = fields.Where(p => p.Group != pk.Group);
            }
            foreach (var group in fields.GroupBy(p=> p.Group))
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
        ///     ������
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
        ///     ������
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