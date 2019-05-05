/*design by:agebull designer date:2019/4/10 10:44:36*/
#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using System.Data.Sql;

using Agebull.Common;
using Agebull.Common.OAuth;
using Agebull.EntityModel.Common;

using Agebull.EntityModel.SqlServer;
using Agebull.EntityModel.BusinessLogic.SqlServer;



using HPC.Projects.DataAccess;
using System.Security.Cryptography;
using Agebull.Common.Context;
#endregion

namespace HPC.Projects.BusinessLogic
{

    public class MessageProtocol
    {
        public string State { get; set; }
        public string Message { get; set; }


        public const string StateSuccess = "success";
        public const string StateError = "error";


        #region 获得返回的信息对象
        /// <summary>
        /// 获得返回的信息对象
        /// </summary>
        /// <param name="State">状态：success|error</param>
        /// <param name="Message">数据对象字符串|报错字符串</param>
        /// <returns></returns>
        public static string getReturnMessage(string State, string Message)
        {
            return JsonConvert.SerializeObject(new MessageProtocol
            {
                State = State,
                Message = Message
            });

        }

        #endregion


    }
    /// <summary>
    /// 员工
    /// </summary>
    public sealed partial class EmployeeBusinessLogic : UiBusinessLogicBase<EmployeeData, EmployeeDataAccess, HpcSqlServerDb>
    {

        /// <summary>
        /// MD5 32位加密 加密后密码为小写
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetMd5Str32(string text)
        {
            try
            {
                using (MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider())
                {
                    byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));
                    StringBuilder sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }
                    return sBuilder.ToString().ToLower();
                }
            }
            catch (Exception e)
            {
                Agebull.Common.Logging.LogRecorder.Exception(e);
                Agebull.Common.Logging.LogRecorder.MonitorTrace(e.Message);
                return null;

            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">手机号</param>
        /// <param name="password"></param>
        /// <param name="response_result"></param>
        public EmployeeData Login(string username, string password, out string response_result)
        {

            string pwMD5 = GetMd5Str32(password + "admin@xjxx");

            var employee = Access.FirstOrDefault(q => q.Phone == username);

            //判断是否存在
            if (employee == null)
            {
                //response_result =("error|用户名不存在");
                response_result = (MessageProtocol.getReturnMessage(MessageProtocol.StateError, "手机号不存在！"));
                return null;
            }
            //判断用户是否删除
            if (employee.StateDelete == true)
            {
                //response_result =("error|该用户未启用"); 
                response_result = (MessageProtocol.getReturnMessage(MessageProtocol.StateError, "该用户已删除！"));
                return null;
            }
            //判断是否启用
            if (employee.StateLogin == false)
            {
                //response_result =("error|该用户未启用"); 
                response_result = (MessageProtocol.getReturnMessage(MessageProtocol.StateError, "该用户未启用！"));
                return null;
            }
            //判断密码是否正确
            if (employee.Password != pwMD5)
            {
                //response_result =("error|用户名或密码错误"); 
                response_result = (MessageProtocol.getReturnMessage(MessageProtocol.StateError, "手机号或密码错误！"));
                return null;
            }
            var eAccess = new EmployeeHasSiteDataAccess();
            //获取用户平台
            if (!eAccess.Any(q => q.EmpEID == employee.EID))
            {
                response_result = (MessageProtocol.getReturnMessage(MessageProtocol.StateError, "该用户还没有配置站点信息！"));
                return null;
            }
            response_result = null;
            return employee;
        }

        /// <summary>
        /// 登录
        /// </summary>
        public string LoginInfo(EmployeeData employee, string token)
        {
            if (token == null)
            {
                return (MessageProtocol.getReturnMessage(MessageProtocol.StateError, "系统错误！"));
            }
            Access.SetValue(p => p.Token, token, employee.Id);
            Access.SetValue(p => p.LastLoginIP, GlobalContext.Current.Request.Ip, employee.Id);
            Access.SetValue(p => p.LastLoginDate, DateTime.Now, employee.Id);

            var eAccess = new EmployeeHasSiteDataAccess();
            var empHasSiteList = eAccess.All(q => q.EmpEID == employee.EID);
            //返回站点列表
            var SiteSIDArr = empHasSiteList.Select(q => q.SiteSID).Distinct().ToArray();
            var sAccess = new SiteDataAccess();
            //设备列表
            var SiteList = new List<SiteData>();
            foreach (var e in  empHasSiteList)
            {
                SiteList.Add(sAccess.LoadByPrimaryKey(e.SiteSID));
               
            }
            var list = SiteList.Select(q => new
            {
                SiteSID = q.SID.ToString(),
                SiteName = q.SiteNameWhole,
                SiteLogo = q.SiteLogo
            });
            string JSONstring = $@"{{""token"":""{token}"",""SiteList"":{JsonConvert.SerializeObject(list)}}}";

            return (MessageProtocol.getReturnMessage(MessageProtocol.StateSuccess, JSONstring));
        }
        /*// <summary>
        ///     保存前的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaving(EmployeeData data, bool isAdd)
        {
             return base.OnSaving(data, isAdd);
        }

        /// <summary>
        ///     保存完成后的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaved(EmployeeData data, bool isAdd)
        {
             return base.OnSaved(data, isAdd);
        }
        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool LastSavedByUser(EmployeeData data, bool isAdd)
        {
            return base.LastSavedByUser(data, isAdd);
        }

        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool PrepareSaveByUser(EmployeeData data, bool isAdd)
        {
            return base.PrepareSaveByUser(data, isAdd);
        }*/
    }
}