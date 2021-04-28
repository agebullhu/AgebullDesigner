﻿using Agebull.EntityModel.Config;
using System;
using System.Text;
using System.Web;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 配置扩展
    /// </summary>
    public static class ConfigCodeExtension
    {
        public static string FormatLineSpace(this StringBuilder html,int level)
        {
            return html.Length == 0 ? null : level <= 0 ? html.ToString() : html.ToString().SpaceLine(level * 4);
        }

        public static string FormatLineSpace(this string html,int level)
        {
            return html.IsMissing() ? null : level <= 0 ? html : html.SpaceLine(level * 4);
        }

        public static string RemCode(this ConfigBase config)
        {
            var code = new StringBuilder();
            code.Append($@"/// <summary>
{config.Caption.RemCode()}
/// </summary>");
            if (config.Description.IsPresent()
                && !config.Description.IsMe(config.Name)
                && !config.Description.IsMe(config.Caption))
            {
                code.Append($@"/// <remarks>
{config.Description.RemCode()}
/// </remarks>");
            }
            return code.ToString();
        }

        /// <summary>
        /// 多行转为指定内容打头的文本
        /// </summary>
        /// <param name="str">要转换后的文本</param>
        /// <param name="head">前导内容</param>
        /// <remarks></remarks>
        /// <returns>正确表示为C#注释的文本</returns>
        public static string RemCode(this string str, string head = "/// ")
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;
            var sp = str.Split(new[] { '\n', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new();
            bool isFirst = true;
            foreach (var line in sp)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sb.AppendLine();
                }
                sb.Append(head);
                sb.Append(HttpUtility.HtmlEncode(line.Trim()));
            }
            return sb.ToString();
        }


    }
}