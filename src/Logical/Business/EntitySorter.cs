using System.Linq;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///  µÃÂ≈≈–Ú∆˜
    /// </summary>
    public class EntitySorter: EntityBusinessBase
    {
        /// <summary>
        ///     ¡–≈≈–Ú
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
                pk.Index = idx++;
                Entity.Properties.Add(pk);
            }
            if (pc != null)
            {
                pc.Index = idx++;
                Entity.Properties.Add(pc);
            }
            var groups = columns.Where(p => !string.IsNullOrEmpty(p.Group)).Select(p => p.Group).Distinct();
            foreach (var group in groups)
            {
                foreach (var column in columns.Where(p => p.Group == group).OrderBy(p => p.Index))
                {
                    column.Index = idx++;
                    Entity.Properties.Add(column);
                }
            }
            foreach (var column in columns.Where(p => string.IsNullOrEmpty(p.Group)).OrderBy(p => p.Index))
            {
                column.Index = idx++;
                Entity.Properties.Add(column);
            }
        }
        /// <summary>
        ///     ¡–≈≈–Ú
        /// </summary>
        public void SortFieldByIndex(bool reset)
        {
            var columns = Entity.Properties.Where(p => !p.Discard && !p.IsPrimaryKey).OrderBy(p => p.Index).ToList();
            var dcs = Entity.Properties.Where(p => p.Discard).OrderBy(p => p.Index).ToList();

            int idx = 0;
            var pk = Entity.PrimaryColumn;
            Entity.Properties.Clear();
            if (pk != null)
            {
                if (reset)
                    pk.Index = idx++;
                Entity.Properties.Add(pk);
            }

            foreach (var field in columns.OrderBy(p => p.Index))
            {
                if (reset)
                    field.Index = idx++;
                Entity.Properties.Add(field);
            }

            foreach (var field in dcs.OrderBy(p => p.Index))
            {
                if (reset)
                    field.Index = idx++;
                Entity.Properties.Add(field);
            }
        }
        /// <summary>
        ///     ¡–≈≈–Ú
        /// </summary>
        public void SortField()
        {
            var pc = Entity.Properties.FirstOrDefault(p => p.IsCaption);
            var pk = Entity.PrimaryColumn;
            var columns = Entity.Properties.Where(p => !p.Discard && !p.IsPrimaryKey && !p.IsCaption).OrderBy(p => p.Index).ToList();
            var dcs = Entity.Properties.Where(p => p.Discard).OrderBy(p => p.Index).ToList();
            var idx = Entity.ColumnIndexStart;
            foreach (var field in columns)
            {
                field.Index = idx++;
            }
            Entity.Properties.Clear();
            idx = Entity.ColumnIndexStart;
            if (pk != null)
            {
                pk.Index = idx++;

                Entity.Properties.Add(pk);
            }
            if (pc != null)
            {
                pc.Index = idx++;
                Entity.Properties.Add(pc);
            }

            foreach (var group in columns.GroupBy(f => f.LinkTable))
            {
                if (!string.IsNullOrWhiteSpace(group.Key))
                {
                    var gidx = group.First().Index;
                    foreach (var ch in group)
                        ch.Index = gidx;
                }
            }

            foreach (var field in columns.OrderBy(p => p.Index))
            {
                field.Index = idx++;
                Entity.Properties.Add(field);
            }

            foreach (var field in dcs.OrderBy(p => p.Index))
            {
                field.Index = idx++;
                Entity.Properties.Add(field);
            }
        }

        /// <summary>
        ///     ¡–≈≈–Ú
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
                pk.Index = idx++;
                if (reInsert)
                {
                    Entity.Properties.Add(pk);
                }
            }
            var tColumns =
                columns.Where(p => !p.IsPrimaryKey && !p.Discard && string.IsNullOrWhiteSpace(p.Group))
                    .OrderBy(p => p.Index)
                    .ToArray();

            var cols = tColumns.Where(p => !p.IsSystemField).OrderBy(p => p.Index).ToArray();
            foreach (var field in cols)
            {
                field.Index = idx++;
                if (reInsert)
                {
                    Entity.Properties.Add(field);
                }
            }
            foreach (
                var field in
                columns.Where(p => !p.IsPrimaryKey && !string.IsNullOrWhiteSpace(p.Group))
                    .OrderBy(p => p.Group)
                    .ThenBy(p => p.Index))
            {
                field.Index = idx++;
                if (reInsert)
                {
                    Entity.Properties.Add(field);
                }
            }

            var sysColumns =
                tColumns.Where(p => !p.IsPrimaryKey && p.IsSystemField && !p.Discard).OrderBy(p => p.Index).ToArray();
            foreach (var field in sysColumns.Where(p => !p.IsInterfaceField && !p.CustomWrite).OrderBy(p => p.Index))
            {
                field.Index = idx++;
                if (reInsert)
                {
                    Entity.Properties.Add(field);
                }
            }
            foreach (var field in sysColumns.Where(p => !p.IsInterfaceField && p.CustomWrite).OrderBy(p => p.Index))
            {
                field.Index = idx++;
                if (reInsert)
                {
                    Entity.Properties.Add(field);
                }
            }
            foreach (var field in sysColumns.Where(p => p.IsInterfaceField))
            {
                field.Index = idx++;
                if (reInsert)
                {
                    Entity.Properties.Add(field);
                }
            }
            foreach (var field in columns.Where(p => p.Discard).OrderBy(p => p.Index))
            {
                field.Index = idx++;
                if (reInsert)
                {
                    Entity.Properties.Add(field);
                }
            }

        }
    }
}