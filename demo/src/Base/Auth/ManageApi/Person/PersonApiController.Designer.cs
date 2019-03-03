/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/10/9 13:26:23*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Agebull.Common.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Agebull.ZeroNet.ZeroApi;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.DataModel;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.WebApi;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;
using Agebull.Common.Rpc;
using Agebull.Common.DataModel.WebUI;

using Agebull.Common.WebApi.Auth;

using Agebull.Common.OAuth;
using Agebull.Common.OAuth.BusinessLogic;
using Agebull.Common.OAuth.DataAccess;

namespace Agebull.Common.UserCenter.WebApi.Entity
{
    partial class PersonApiController
    {
        #region 设计器命令

        /// <summary>下拉列表</summary>
        /// <returns></returns>
        
        [Route("edit/combo")]
        public List<EasyComboValues> ComboData()
        {
            GlobalContext.Current.IsManageMode = false;
            var datas = Business.All();
            var combos = datas.Select(p => new EasyComboValues(p.UserId, p.PhoneNumber)).ToList();
            combos.Insert(0,new EasyComboValues(0, "-"));
            return combos;
        }

        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<PersonData> DefaultGetListData()
        {
            var filter = new LambdaItem<PersonData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name="filter">筛选器</param>
        public void SetKeywordFilter(LambdaItem<PersonData> filter)
        {
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {
                filter.AddAnd(p =>p.AvatarUrl.Contains(keyWord) || 
                                   p.RealName.Contains(keyWord) || 
                                   p.PhoneNumber.Contains(keyWord) || 
                                   p.NickName.Contains(keyWord) || 
                                   p.IdCard.Contains(keyWord) || 
                                   p.Nation.Contains(keyWord) || 
                                   p.Tel.Contains(keyWord) || 
                                   p.Email.Contains(keyWord) || 
                                   p.HomeAddress.Contains(keyWord) || 
                                   p.Company.Contains(keyWord) || 
                                   p.JobTitle.Contains(keyWord));
            }
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(PersonData data, FormConvert convert)
        {
            //-
            {
                var file = convert.ToString("icon");
                
                if (string.IsNullOrEmpty(file))
                    data.Icon_Base64 = null;
                else if(file != "*" && file.Length< 100 && file[0] == '/')
                {
                    var call = new WebApiCaller(ConfigurationManager.AppSettings["ManageAddress"]);
                    var result = call.Get<string>(ConfigurationManager.AppSettings["ManageApi"], $"action=base64&url={file}");
                    data.Icon_Base64 = result.Success ? result.ResultData : null;
                }
            }
            data.AvatarUrl = convert.ToString("avatarUrl");
            data.RealName = convert.ToString("realName");
            data.PhoneNumber = convert.ToString("phoneNumber");
            data.Sex = (SexType)convert.ToInteger("sex");
            data.NickName = convert.ToString("nickName");
            data.CertType = (CardType)convert.ToInteger("certType");
            data.IdCard = convert.ToString("idCard");
            data.Birthday = convert.ToDateTime("birthday");
            data.Nation = convert.ToString("nation");
            data.Tel = convert.ToString("tel");
            data.Email = convert.ToString("email");
            data.HomeAddress = convert.ToString("homeAddress");
            data.Company = convert.ToString("company");
            data.JobTitle = convert.ToString("jobTitle");
        }

        #endregion
    }
}