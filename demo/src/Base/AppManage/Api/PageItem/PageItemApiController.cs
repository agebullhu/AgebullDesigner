// // /*****************************************************
// // (c)2016-2016 Copy right www.gboxt.com
// // 作者:
// // 工程:Agebull.DataModel
// // 建立:2016-06-12
// // 修改:2016-06-29
// // *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using Agebull.EntityModel.Common;

using Agebull.Common.OAuth;
using Agebull.Common.AppManage.DataAccess;
using Agebull.Common.AppManage.BusinessLogic;
using Agebull.Common.Ioc;
using Agebull.Common.OAuth.BusinessLogic;

using Agebull.MicroZero.ZeroApis;
using Agebull.MicroZero;
using Agebull.Common.Context;
using Agebull.EntityModel.EasyUI;

#endregion

namespace Agebull.Common.AppManage.Entity
{
    [RoutePrefix("app/page/v1")]
    public partial class PageItemApiController : ApiController<PageItemData, PageItemDataAccess, AppManageDb, PageItemBusinessLogic>
    {

        /// <summary>
        ///     绑定页面数据类型
        /// </summary>
        [Route("bind/type")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        public ApiResult BindPageType(PageConfig argument)
        {
            Business.BindPageType(argument);
            return ApiResult.Ok;
        }
        /// <summary>
        ///     保存页面对应的按钮信息
        /// </summary>
        [ Route("help/button")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        public ApiResult SaveButton(ButtonInfoArgument argument)
        {
            PageItemBusinessLogic.SavePageAction(argument.PageId, argument.Element, argument.Title, argument.Action, "button");
            return ApiResult.Ok;
        }
        [Route("v1/sys/bind_type")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        public ApiResult BindType(Argument<long> argument)
        {

            var bl = new PageItemBusinessLogic();
            List<Type> types = new List<Type>();
            var entityType = typeof(DataObjectBase);
            foreach (var type in typeof(PageItemData).Assembly.GetTypes())
            {
                if (type.IsSubclassOf(entityType))
                {
                    types.Add(type);
                }
            }
            foreach (var type in typeof(OrganizationBusinessLogic).Assembly.GetTypes())
            {
                if (type.IsSubclassOf(entityType))
                {
                    types.Add(type);
                }
            }
            var success = bl.BindType(argument.Value, types);
            return success ? ApiResult.Succees() : ApiResult.Error(ErrorCode.LogicalError);
        }
        /// <summary>
        ///     获取页面信息
        /// </summary>
        [ Route("global/info")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        public ApiResult GetPageInfo(Argument arg)
        {
            var page = IocHelper.Create<IPowerChecker>().LoadPageConfig(arg.Value);
            if (page == null)
            {
                return new ApiResult<PageInfo>
                {
                    ResultData = new PageInfo
                    {
                        PageId = -1,
                        AllAction = true
                    }
                };
                //return (ApiResult.Error(ErrorCode.LogicalError, "参数错误"));
            }

            var pc = IocHelper.Create<IPowerChecker>();
            var actions = pc?.LoadPageButtons(GlobalContext.Current.User, page);

            return new ApiResult<PageInfo>
            {
                ResultData = new PageInfo
                {
                    PageId = page.Id,
                    AllAction = true,//page.IsPublic
                    RoleAllowActions = actions
                }
            };
        }
        /// <summary>
        ///     页面树
        /// </summary>
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        [Route("page/static")]
        public ApiResult StaticHtml()
        {
            Business.ToHtml(GetLongArg("id", 0), GetArg("host"), GetBoolArg("all"));
            return new ApiArrayResult<EasyUiTreeNode>
            {
                Success = true
            };
        }
        /// <summary>
        ///     页面树
        /// </summary>
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        [Route("edit/tree")]
        public ApiResult GetTree()
        {

            var tree = Business.LoadTreeForUi(GetLongArg("id", 0));
            return new ApiArrayResult<EasyUiTreeNode>
            {
                Success = true,
                ResultData = tree
            };
        }

        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        [Route("edit/set_parent")]
        public ApiResult SetParent()
        {

            if (!Business.SetParent(GetLongArrayArg("selects"), GetArg("parent")))
                GlobalContext.Current.LastState = ErrorCode.LogicalError;

            return IsFailed ? ApiResult.Error(-1) : ApiResult.Succees();
        }

        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        [Route("edit/normal_buttons")]
        public ApiResult NormalButtons()
        {

            if (!Business.CheckNormalButtons(GetLongArg("id"), GetArg("type")))
                GlobalContext.Current.LastState = ErrorCode.LogicalError;

            return IsFailed ? ApiResult.Error(-1) : ApiResult.Succees();
        }


        /// <summary>
        ///     新增一条带默认值的数据
        /// </summary>
        protected override PageItemData CreateData()
        {
            return new PageItemData
            {
                ParentId = GetIntArg("fid")
            };
        }

        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected override ApiPageData<PageItemData> GetListData()
        {
            var root = new LambdaItem<PageItemData>();
            var fid = GetIntArg("fid", 0);
            if (fid > 0)
            {
                root.Root = p => p.ParentId == fid;
            }
            else
            {
                root.Root = p => p.ParentId == 0;
            }
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {
                root.AddAnd(p => p.Name.Contains(keyWord) || p.Caption.Contains(keyWord) || p.Url.Contains(keyWord));
            }
            return base.GetListData(root);
        }

        /// <summary>
        ///     读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected override void ReadFormData(PageItemData data, FormConvert convert)
        {
            data.Name = convert.ToString("Name", false);
            data.Json = convert.ToString("Json", false);
            data.ExtendValue = convert.ToString("ExtendValue", false);
            data.Caption = convert.ToString("Caption", false);
            //data.Folder = convert.ToString("Folder", false);
            data.Url = convert.ToString("Url", true);
            data.Icon = convert.ToString("Icon", true);
            data.ItemType = (PageItemType)convert.ToInteger("ItemType", 0);
            data.Index = convert.ToInteger("Index", 0);
            data.ParentId = convert.ToInteger("ParentId", 0);
            data.Memo = convert.ToString("Memo", true);


            data.SystemType = convert.ToString("type");
            data.IsHide = convert.ToBoolean("hide");
            data.Audit = convert.ToBoolean("audit");
            data.LevelAudit = convert.ToBoolean("level_audit");
            data.DataState = convert.ToBoolean("data_state");
            data.AuditPage = convert.ToInteger("audit_page");
            data.Edit = convert.ToBoolean("edit");
            data.MasterPage = convert.ToInteger("master_page");
        }

    }
}