using System.Linq;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体排序器
    /// </summary>
    public class EntitySorter: ConfigModelBase
    {
        /// <summary>
        /// 表结构对象
        /// </summary>
        public EntityConfig Entity { get; set; }

        /// <summary>
        ///     列排序
        /// </summary>
        public void SortByGroup()
        {
            var pc = Entity.Properties.FirstOrDefault(p => p.IsCaption);
            var pk = Entity.PrimaryColumn;
            var columns = Entity.Properties.Where(p => !p.IsPrimaryKey && !p.IsCaption).OrderBy(p => p.Index).ToArray();
            Entity.Properties.Clear();
            var idx = Entity.ColumnIndexStart;
            if (pk != null)
            {
                pk.Option.Index = ++idx;
                Entity.Add(pk);
            }
            if (pc != null)
            {
                pc.Option.Index = ++idx;
                Entity.Add(pc);
            }
            var groups = columns.Where(p => !string.IsNullOrWhiteSpace(p.Group)).Select(p => p.Group).Distinct();
            foreach (var group in groups)
            {
                foreach (var column in columns.Where(p => p.Group == group).OrderBy(p => p.Index))
                {
                    column.Option.Index = ++idx;
                    Entity.Add(column);
                }
            }
            foreach (var column in columns.Where(p => string.IsNullOrWhiteSpace(p.Group)).OrderBy(p => p.Index))
            {
                column.Option.Index = ++idx;
                Entity.Add(column);
            }
        }
        
        /// <summary>
        ///     标识与序号相同
        /// </summary>
        public void IdentityByIndex()
        {
            foreach (var field in Entity.Properties)
            {
                field.Option.Identity  = field.Option.Index;
            }
        }
        /// <summary>
        ///     列排序
        /// </summary>
        public void SortFieldByIndex(bool reset)
        {
            var columns = Entity.Properties.Where(p => !p.IsDiscard && !p.IsPrimaryKey).OrderBy(p => p.Index).ToList();
            var dcs = Entity.Properties.Where(p => p.IsDiscard).OrderBy(p => p.Index).ToList();

            int idx = 0;
            var pk = Entity.PrimaryColumn;
            Entity.Properties.Clear();
            if (pk != null)
            {
                if (reset)
                    pk.Option.Index = ++idx;
                Entity.Add(pk);
            }

            foreach (var field in columns.OrderBy(p => p.Index))
            {
                if (reset)
                    field.Option.Index = ++idx;
                Entity.Add(field);
            }

            foreach (var field in dcs.OrderBy(p => p.Index))
            {
                if (reset)
                    field.Option.Index = ++idx;
                Entity.Add(field);
            }
        }
        /// <summary>
        ///     列排序
        /// </summary>
        public void SortField()
        {
            var pc = Entity.Properties.FirstOrDefault(p => p.IsCaption);
            var pk = Entity.PrimaryColumn;
            var columns = Entity.Properties.Where(p => !p.IsDiscard && !p.IsPrimaryKey && !p.IsCaption).OrderBy(p => p.Index).ToList();
            var dcs = Entity.Properties.Where(p => p.IsDiscard).OrderBy(p => p.Index).ToList();
            var idx = Entity.ColumnIndexStart;
            foreach (var field in columns)
            {
                field.Option.Index = ++idx;
            }
            Entity.Properties.Clear();
            idx = Entity.ColumnIndexStart;
            if (pk != null)
            {
                pk.Option.Index = ++idx;

                Entity.Add(pk);
            }
            if (pc != null)
            {
                pc.Option.Index = ++idx;
                Entity.Add(pc);
            }

            foreach (var group in columns.GroupBy(f => f.LinkTable))
            {
                if (!string.IsNullOrWhiteSpace(group.Key))
                {
                    var gidx = group.First().Index;
                    foreach (var ch in group)
                        ch.Option.Index = gidx;
                }
            }

            foreach (var field in columns.OrderBy(p => p.Index))
            {
                field.Option.Index = ++idx;
                Entity.Add(field);
            }

            foreach (var field in dcs.OrderBy(p => p.Index))
            {
                field.Option.Index = ++idx;
                Entity.Add(field);
            }
        }

        /// <summary>
        ///     列排序
        /// </summary>
        public void SortField(bool reInsert)
        {
            var columns = Entity.Properties.ToArray();
            if (reInsert)
            {
                Entity.Properties.Clear();
            }
            var idx = Entity.ColumnIndexStart;
            var pk = columns.FirstOrDefault(p => p.IsPrimaryKey);
            if (pk != null)
            {
                pk.Option.Index = ++idx;
                if (reInsert)
                {
                    Entity.Add(pk);
                }
            }
            var tColumns =
                columns.Where(p => !p.IsPrimaryKey && !p.IsDiscard && string.IsNullOrWhiteSpace(p.Group))
                    .OrderBy(p => p.Index)
                    .ToArray();

            var cols = tColumns.Where(p => !p.IsSystemField).OrderBy(p => p.Index).ToArray();
            foreach (var field in cols)
            {
                field.Option.Index = ++idx;
                if (reInsert)
                {
                    Entity.Add(field);
                }
            }
            foreach (
                var field in
                columns.Where(p => !p.IsPrimaryKey && !string.IsNullOrWhiteSpace(p.Group))
                    .OrderBy(p => p.Group)
                    .ThenBy(p => p.Index))
            {
                field.Option.Index = ++idx;
                if (reInsert)
                {
                    Entity.Add(field);
                }
            }

            var sysColumns =
                tColumns.Where(p => !p.IsPrimaryKey && p.IsSystemField && !p.IsDiscard).OrderBy(p => p.Index).ToArray();
            foreach (var field in sysColumns.Where(p => !p.IsInterfaceField && !p.CustomWrite).OrderBy(p => p.Index))
            {
                field.Option.Index = ++idx;
                if (reInsert)
                {
                    Entity.Add(field);
                }
            }
            foreach (var field in sysColumns.Where(p => !p.IsInterfaceField && p.CustomWrite).OrderBy(p => p.Index))
            {
                field.Option.Index = ++idx;
                if (reInsert)
                {
                    Entity.Add(field);
                }
            }
            foreach (var field in sysColumns.Where(p => p.IsInterfaceField))
            {
                field.Option.Index = ++idx;
                if (reInsert)
                {
                    Entity.Add(field);
                }
            }
            foreach (var field in columns.Where(p => p.IsDiscard).OrderBy(p => p.Index))
            {
                field.Option.Index = ++idx;
                if (reInsert)
                {
                    Entity.Add(field);
                }
            }

        }
    }
}