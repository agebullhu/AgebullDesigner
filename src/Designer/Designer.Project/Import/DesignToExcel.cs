// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Gboxt.Common.SimpleDataAccess
// 建立:2014-12-03
// 修改:2014-12-03
// *****************************************************/

#region 引用

using Agebull.EntityModel.Config;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using HorizontalAlignment = NPOI.SS.UserModel.HorizontalAlignment;
using VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class DesignToExcel
    {
        /// <summary>
        ///     导出到Excel
        /// </summary>
        /// <param name="file"></param>
        /// <param name="tables"></param>
        public static void Import(string file, IEnumerable<EntityConfig> tables)
        {
            var project = tables.FirstOrDefault()?.Project;
            if (project == null)
            {
                MessageBox.Show("提示：没有选择！");
                return;
            }
            var workbook = new HSSFWorkbook();
            using (CodeGeneratorScope.CreateScope(project))
            {
                DoImport(workbook, project);
            }
            using (var fs = File.OpenWrite(file)) //打开一个xls文件，如果没有则自行创建，如果存在myxls.xls文件则在创建是不要打开该文件！
            {
                workbook.Write(fs);   //向打开的这个xls文件中写入mySheet表并保存。
                MessageBox.Show("提示：创建成功！");
            }
            try
            {
                Process.Start(file);
            }
            catch
            {
                MessageBox.Show("提示：无法打开Excel！");
            }
        }

        static void DoImport(HSSFWorkbook workbook, ProjectConfig project)
        {
            foreach (var cls in project.Classifies)
            {
                string name = cls.Caption ?? cls.Name;
                if (name.Length > 31)
                {
                    name = name.Substring(0, 31);
                }

                if (workbook.GetSheet(name) != null)
                {
                    continue;
                }

                ISheet sheet = workbook.CreateSheet(name);
                int i = 0;
                sheet.SetColumnWidth(i++, 4000);
                sheet.SetColumnWidth(i++, 4000);
                sheet.SetColumnWidth(i++, 2000);
                sheet.SetColumnWidth(i++, 2000);
                sheet.SetColumnWidth(i++, 2000);
                sheet.SetColumnWidth(i++, 2000);
                sheet.SetColumnWidth(i++, 6000);
                sheet.SetColumnWidth(i++, 4000);
                sheet.SetColumnWidth(i++, 2000);
                sheet.SetColumnWidth(i, 4000);

                int rowIdx = 0;
                foreach (var entity in cls?.Items?.Where(p => p != null && p.EnableDataBase))
                {
                    rowIdx = ImportTable(workbook, sheet, entity, rowIdx);
                }
            }
        }

        /// <summary>
        ///     读取表与实体关联表,初始化表结构
        /// </summary>
        private static int ImportTable(HSSFWorkbook workbook, ISheet sheet, EntityConfig entity, int rowBase)
        {
            var labelCell = GetCellStyle(workbook, HorizontalAlignment.Left, VerticalAlignment.Center, true, CreateFontStyle(workbook, "宋体", 9, true));
            var valueCell = GetCellStyle(workbook, HorizontalAlignment.Left, VerticalAlignment.Center, true, CreateFontStyle(workbook, "宋体"));


            var row = sheet.CreateRow(rowBase);
            row.HeightInPoints = 20;//行高
            row.CreateCell(0).SetCell("TABLE:", labelCell);
            row.CreateCell(1).SetCellValue(entity.ReadTableName);
            SetCellRangeAddress(sheet, valueCell, rowBase, rowBase, 1, 3);

            row.CreateCell(4).SetCell("变动时间:", labelCell);
            SetCellRangeAddress(sheet, labelCell, rowBase, rowBase, 4, 5);
            row.CreateCell(6).SetCell(DateTime.Today.ToLongDateString(), valueCell);
            SetCellRangeAddress(sheet, valueCell, rowBase, rowBase, 6, 9);

            row = sheet.CreateRow(rowBase + 1);
            row.HeightInPoints = 20;//行高
            row.CreateCell(0).SetCell("中文名:", labelCell);
            row.CreateCell(1).SetCell(entity.Caption, valueCell);
            SetCellRangeAddress(sheet, valueCell, rowBase + 1, rowBase + 1, 1, 3);
            row.CreateCell(4).SetCell("变动类型:", labelCell);
            SetCellRangeAddress(sheet, labelCell, rowBase + 1, rowBase + 1, 4, 5);
            //row.CreateCell(6).SetCell("新建", valueCell);
            SetCellRangeAddress(sheet, valueCell, rowBase + 1, rowBase + 1, 6, 9);

            row = sheet.CreateRow(rowBase + 2);
            row.HeightInPoints = 20;//行高
            var i = 0;
            row.CreateCell(i++).SetCell("字段", labelCell);
            row.CreateCell(i++).SetCell("字段类型", labelCell);
            row.CreateCell(i++).SetCell("长度", labelCell);
            row.CreateCell(i++).SetCell("可为空", labelCell);
            row.CreateCell(i++).SetCell("主键", labelCell);
            row.CreateCell(i++).SetCell("默认值", labelCell);
            row.CreateCell(i++).SetCell("备注", labelCell);
            row.CreateCell(i++).SetCell("外键", labelCell);
            row.CreateCell(i++).SetCell("索引", labelCell);
            row.CreateCell(i).SetCell("更新时间", labelCell);


            var fields = entity.DataTable.FindAndToArray(p => !p.IsReadonly);
            int line = 0;
            for (; line < fields.Length; line++)
            {
                var field = fields[line];
                row = sheet.CreateRow(rowBase + line + 3);
                row.HeightInPoints = 20;//行高
                i = 0;
                row.CreateCell(i++).SetCell(field.DbFieldName, valueCell);
                row.CreateCell(i++).SetCell(field.FieldType?.ToUpper(), valueCell);
                row.CreateCell(i++).SetCell(field.CsType == "decimal" ? $"({field.Datalen},{field.Scale})" : (field.Datalen > 0 ? field.Datalen.ToString() : "-"), valueCell);
                row.CreateCell(i++).SetCell(field.DbNullable ? "是" : "否", valueCell);
                row.CreateCell(i++).SetCell(field.IsPrimaryKey ? "是" : "否", valueCell);
                row.CreateCell(i++).SetCell(field.Nullable ? field.Initialization : "", valueCell);
                row.CreateCell(i++).SetCell(string.IsNullOrWhiteSpace(field.Description) || field.Description.Equals(field.Caption)
                    ? field.Caption
                    : field.Caption + " : " + field.Description, valueCell);
                row.CreateCell(i++).SetCell(field.IsLinkKey ? $"{field.LinkTable}:{field.LinkField}" : "", valueCell);
                row.CreateCell(i++).SetCell(field.IsDbIndex ? "是" : "否", valueCell);
                row.CreateCell(i).SetCell("", valueCell);
            }
            return rowBase + line + 5;
        }

        /// <summary>
        /// 获取字体样式
        /// </summary>
        /// <param name="hssfworkbook">Excel操作类</param>
        /// <param name="fontname">字体名</param>
        /// <param name="fontsize">字体大小</param>
        /// <param name="body">是否加粗</param>
        /// <param name="isItalic">是否斜体</param>
        /// <returns></returns>
        public static IFont CreateFontStyle(HSSFWorkbook hssfworkbook, string fontname = null, int fontsize = 9, bool body = false, bool isItalic = false)
        {
            IFont font1 = hssfworkbook.CreateFont();
            if (string.IsNullOrWhiteSpace(fontname))
            {
                font1.FontName = fontname;
            }
            font1.IsItalic = isItalic;
            font1.FontHeightInPoints = (short)fontsize;
            font1.IsBold = body;
            return font1;
        }

        /// <summary>
        /// 获取单元格样式
        /// </summary>
        /// <param name="hssfworkbook">Excel操作类</param>
        /// <param name="border">是否有边框</param>
        /// <param name="font">单元格字体</param>
        /// <param name="ha">垂直对齐方式</param>
        /// <param name="va">垂直对齐方式</param>
        /// <returns></returns>
        public static ICellStyle GetCellStyle(HSSFWorkbook hssfworkbook, HorizontalAlignment ha, VerticalAlignment va, bool border = true, IFont font = null)
        {
            ICellStyle cellstyle = hssfworkbook.CreateCellStyle();
            cellstyle.Alignment = ha;
            cellstyle.VerticalAlignment = va;

            if (font != null)
            {
                cellstyle.SetFont(font);
            }
            //有边框
            if (border)
            {
                cellstyle.BorderBottom = BorderStyle.Thin;
                cellstyle.BorderLeft = BorderStyle.Thin;
                cellstyle.BorderRight = BorderStyle.Thin;
                cellstyle.BorderTop = BorderStyle.Thin;
            }
            return cellstyle;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="style">样式</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, ICellStyle style, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
            for (int row = rowstart; row <= rowend; row++)
            {
                var r = sheet.GetRow(row);
                for (var col = colstart; col <= colend; col++)
                {
                    var cell = r.GetCell(col) ?? r.CreateCell(col);
                    cell.CellStyle = style;
                }
            }
        }
    }

    public static class ExcelHelper
    {
        public static void SetCell<T>(this ICell cell, T value, ICellStyle style = null)
        {
            cell.SetCellValue(Equals(value, default(T)) ? string.Empty : value.ToString());
            cell.CellStyle = style;

        }
        /// <summary>
        /// 获取字体样式
        /// </summary>
        /// <param name="hssfworkbook">Excel操作类</param>
        /// <param name="fontname">字体名</param>
        /// <param name="fontsize">字体大小</param>
        /// <param name="body">是否加粗</param>
        /// <param name="isItalic">是否斜体</param>
        /// <returns></returns>
        public static IFont CreateFontStyle(HSSFWorkbook hssfworkbook, string fontname = null, int fontsize = 12, bool body = false, bool isItalic = false)
        {
            IFont font1 = hssfworkbook.CreateFont();
            if (string.IsNullOrWhiteSpace(fontname))
            {
                font1.FontName = fontname;
            }
            font1.IsItalic = isItalic;
            font1.FontHeightInPoints = (short)fontsize;
            if (body)
            {
                font1.IsBold = true;
            }

            return font1;
        }

        /// <summary>
        /// 获取单元格样式
        /// </summary>
        /// <param name="hssfworkbook">Excel操作类</param>
        /// <param name="border">是否有边框</param>
        /// <param name="font">单元格字体</param>
        /// <param name="ha">垂直对齐方式</param>
        /// <param name="va">垂直对齐方式</param>
        /// <returns></returns>
        public static ICellStyle GetCellStyle(HSSFWorkbook hssfworkbook, HorizontalAlignment ha, VerticalAlignment va, bool border = true, IFont font = null)
        {
            ICellStyle cellstyle = hssfworkbook.CreateCellStyle();
            cellstyle.Alignment = ha;
            cellstyle.VerticalAlignment = va;

            if (font != null)
            {
                cellstyle.SetFont(font);
            }
            //有边框
            if (border)
            {
                cellstyle.BorderBottom = BorderStyle.Thin;
                cellstyle.BorderLeft = BorderStyle.Thin;
                cellstyle.BorderRight = BorderStyle.Thin;
                cellstyle.BorderTop = BorderStyle.Thin;
            }
            return cellstyle;
        }

    }
}