/*design by:agebull designer date:2018/9/2 0:27:02*/



using Agebull.Common.Context;
using Agebull.Common.OAuth.DataAccess;
using Agebull.EntityModel.MySql.BusinessLogic;

namespace Agebull.Common.OAuth.BusinessLogic
{
    /// <summary>
    /// 行级权限关联
    /// </summary>
    public sealed partial class PositionPersonnelBusinessLogic : BusinessLogicByAudit<PositionPersonnelData, PositionPersonnelDataAccess, UserCenterDb>
    {


        #region 同步用户

        public bool IsTest;
        /// <summary>
        ///     构造
        /// </summary>
        public PositionPersonnelBusinessLogic()
        {
            unityStateChanged = true;
        }
        /// <summary>
        ///     状态改变后的统一处理(unityStateChanged不设置为true时不会产生作用--基于性能的考虑)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override void DoStateChanged(PositionPersonnelData data)
        {
            //Task.Factory.StartNew(() => CacheTask(data.PersonnelId));
            //base.DoStateChanged(data);
        }

        //protected override void OnAuditPassed(PositionPersonnelData data)
        //{
        //    base.OnAuditPassed(data);
        //    if (!IsTest)
        //        CacheTask(data.UserId);
        //}

        //static void CacheTask(long pid)
        //{
        //    //var orb = new UserBusinessLogic();
        //    using (SystemContextScope.CreateScope())
        //    {
        //        //orb.SyncUser(orb._posAccess.First(pid));
        //        RoleCache cache = new RoleCache();
        //        cache.CachePageAuditUser();
        //        cache.CacheTypeUser();
        //    }
        //}
        #endregion

        #region 扩展保存
        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool PrepareSaveByUser(PositionPersonnelData data, bool isAdd)
        {
            if (null == data.PhoneNumber)
            {
                GlobalContext.Current.LastMessage = "手机号码用于登录系统不能为空";
                return false;
            }
            //if (Access.Any(p => p.Id != data.Id && p.Mobile == data.Mobile))
            //{
            //    GlobalContext.Current.LastMessage = "手机号码用于登录系统必须唯一";
            //    return false;
            //}
            return base.PrepareSaveByUser(data, isAdd);
        }

        /// <summary>
        ///     保存前的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaving(PositionPersonnelData data, bool isAdd)
        {
            var oAccess = new OrganizePositionDataAccess();
            var post = oAccess.LoadByPrimaryKey(data.OrganizePositionId);
            if (post == null)
                return false;
            data.AreaId = post.AreaId;
            //手机号检验,已注册的，直接登录
            var aAccess = new UserDataAccess();
            var pAccess = new PersonDataAccess();
            var person = data.UserId > 0
                ? pAccess.LoadByPrimaryKey(data.UserId)
                : pAccess.FirstOrDefault(p => p.PhoneNumber.Equals(data.PhoneNumber));
            UserData user;
            if (person == null)
            {
                aAccess.Insert(user = UserHelper.NewUser());
                pAccess.Insert(person = new PersonData
                {
                    UserId = user.UserId,
                    Sex = data.Sex,
                    Birthday = data.Birthday,
                    PhoneNumber = data.PhoneNumber,
                    RealName = data.RealName,
                    NickName = data.Appellation,
                    AvatarUrl = UserHelper.DefaultAvatarUrlPath
                });
            }
            else
            {
                user = aAccess.LoadByPrimaryKey(person.Id);
                if(user == null)
                    aAccess.Insert(user = UserHelper.NewUser());
                person.RealName = data.RealName;
                person.NickName = data.Appellation;
                person.PhoneNumber = data.PhoneNumber;
                person.Sex = data.Sex;
                person.Birthday = data.Birthday;
                pAccess.Update(person);
            }
            UserHelper.CacheUserInfo(user, person);
            data.UserId = user.UserId;
            var old = Access.FirstOrDefault(p => p.UserId == user.Id && p.OrganizePositionId == data.OrganizePositionId);
            if (old == null)
                return base.OnSaving(data, isAdd);
            data.Id = old.Id;
            data.__EntityStatus.IsExist = true;//强变为更新
            return base.OnSaving(data, isAdd);
        }

        #endregion
    }
}