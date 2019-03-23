/*design by:agebull designer date:2017/11/2 22:22:24*/
using System;
using System.Diagnostics;
using Agebull.Common.Context;
using Agebull.Common.Ioc;
using Agebull.Common.Logging;
using Agebull.Common.Organizations.DataAccess;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.MySql;
using Agebull.EntityModel.Redis;
using Agebull.MicroZero.ZeroApis;

namespace Agebull.Common.Organizations
{
    /// <summary>
    /// 用户信息API
    /// </summary>
    sealed partial class UserInfoApiLogical
    {

        #region 修改信息
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="result"></param>
        partial void QueryUserInfo(UserBaseInfo arg, ApiResult<UserBaseInfo> result)
        {
            var uAccess = new UserDataAccess();
            var pAccess = new PersonDataAccess();
            if (arg.UserId > 0)
            {
                var user = uAccess.LoadByPrimaryKey(arg.UserId);
                if (user == null)
                {
                    result.Success = false;
                    result.Status = new ApiStatusResult(ErrorCode.LogicalError, "参数错误");
                    return;
                }

                var ps = pAccess.LoadByPrimaryKey(arg.UserId);
                result.Success = true;
                result.ResultData = new UserBaseInfo
                {
                    UserId = user.UserId,
                    Phone = ps?.PhoneNumber,
                    OpenId = user.OpenId
                };
                return;
            }
            if (!string.IsNullOrWhiteSpace(arg.Phone))
            {
                var ps = pAccess.First(p => p.PhoneNumber == arg.Phone);
                if (ps == null)
                {
                    result.Success = false;
                    result.Status = new ApiStatusResult(ErrorCode.LogicalError, "参数错误");
                    return;
                }
                var user = uAccess.FirstOrDefault();
                result.Success = true;
                result.ResultData = new UserBaseInfo
                {
                    UserId = ps.UserId,
                    Phone = ps.PhoneNumber,
                    OpenId = user?.OpenId
                };
                return;
            }
            if (!string.IsNullOrWhiteSpace(arg.OpenId))
            {
                var user = uAccess.FirstOrDefault(p => p.OpenId == arg.OpenId);
                if (user == null)
                {
                    result.Success = false;
                    result.Status = new ApiStatusResult(ErrorCode.LogicalError, "参数错误");
                    return;
                }
                var ps = pAccess.LoadByPrimaryKey(arg.UserId);
                result.Success = true;
                result.ResultData = new UserBaseInfo
                {
                    UserId = user.UserId,
                    Phone = ps.PhoneNumber,
                    OpenId = user.OpenId
                };
                return;
            }
            result.Success = false;
            result.Status = new ApiStatusResult(ErrorCode.LogicalError, "参数错误");
        }
        /// <summary>
        /// 通过手机验证码 重置密码
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="result"></param>
        partial void FindPassword(FindPasswordRequest arg, ApiResult result)
        {
            //短信验证码校验
            var smsResult = VerificationCodeHelper.ValidateSms(arg.SMSVerificationCode, arg.MobilePhone);
            if (!smsResult)
            {
                result.Success = false;
                result.Status = new ApiStatusResult(50003, "请输入正确验证码！");
                return;
            }

            if (string.IsNullOrWhiteSpace(arg.UserPassword))
            {
                result.Success = false;
                result.Status = new ApiStatusResult
                {
                    ErrorCode = ErrorCode.LogicalError,
                    ClientMessage = "密码格式不正确"
                };
                return;
            }
            var pAccess = new PersonDataAccess();
            var person = pAccess.FirstOrDefault(p => p.PhoneNumber == arg.MobilePhone);
            if (person == null)
            {
                result.Success = false;
                result.Status = new ApiStatusResult
                {
                    ErrorCode = 40403,
                    ClientMessage = "您的手机号尚未注册，请直接通过短信登录"
                };
                return;
            }
            CheckAccount(arg.UserPassword, person.UserId, arg.MobilePhone);
            result.Success = true;
            result.Status = new ApiStatusResult
            {
                ErrorCode = ErrorCode.Success,
                ClientMessage = "恭喜您成功修改密码"
            };
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="result"></param>
        partial void UpdateAvatar(AvatarRequest arg, ApiResult result)
        {
            if (string.IsNullOrWhiteSpace(arg.AvatarUrlPath))
            {
                arg.AvatarUrlPath = UserHelper.DefaultAvatarUrlPath;
            }
            LogRecorder.MonitorTrace(arg.AvatarUrlPath);
            var access = new PersonDataAccess();
            access.SetValue(p => p.AvatarUrl, arg.AvatarUrlPath, GlobalContext.Customer.UserId);

            GlobalContext.Customer.AvatarUrl = arg.AvatarUrlPath;
            result.Success = true;
            result.Status = new ApiStatusResult
            {
                ErrorCode = ErrorCode.Success,
                ClientMessage = "修改成功"
            };
            UserHelper.SyncUserProfile(GlobalContext.Customer.UserId);
        }


        /// <summary>
        /// 修改昵称
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="result"></param>
        partial void UpdateNickName(NickNameRequest arg, ApiResult result)
        {
            var access = new PersonDataAccess();
            access.SetValue(p => p.NickName, arg.Name, GlobalContext.Customer.UserId);

            GlobalContext.Customer.NickName = arg.Name;
            result.Success = true;
            result.Status = new ApiStatusResult
            {
                ErrorCode = ErrorCode.Success,
                ClientMessage = "修改成功"
            };
            UserHelper.SyncUserProfile(GlobalContext.Customer.UserId);
        }
        partial void UpdatePassword(UpdatePasswordRequest arg, ApiResult result)
        {
            CheckAccount(arg.UserPassword, GlobalContext.Customer.UserId, GlobalContext.Customer.Phone);
            result.Success = true;
            result.Status = new ApiStatusResult
            {
                ErrorCode = ErrorCode.Success,
                ClientMessage = "修改成功"
            };
        }

        partial void LoginErrorCount(MobilePhoneRequest arg, ApiResult<LoginErrorCountResponse> result)
        {
            result.Success = true;
            result.ResultData = new LoginErrorCountResponse
            {
                ErrorCount = GetErrorCount(arg.MobilePhone)
            };
        }
        #endregion



        #region ErrorCount 通过手机号记录ErrorCount

        /// <summary>
        /// 记录错误次数
        /// </summary>
        /// <param name="account"></param>
        void WriteErrorCount(string account)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                var key = RedisKeyBuilder.ToBusinessKey("login", "count", account);
                proxy.Redis.Incr(key);
            }
        }
        /// <summary>
        /// 清除错误次数
        /// </summary>
        /// <param name="account"></param>
        public void ClearErrorCount(string account)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                var key = RedisKeyBuilder.ToBusinessKey("login", "count", account);
                proxy.RemoveKey(key);
            }
        }

        long GetErrorCount(string phone)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                var key = RedisKeyBuilder.ToBusinessKey("login", "count", phone);
                return proxy.GetValue<long>(key);
            }
        }
        #endregion

        #region ValidateImgVerificationCode


        /// <summary>
        /// 校验图形验证码
        /// </summary>
        /// <param name="phone">电话号码</param>
        /// <param name="imgCode"></param>
        /// <returns></returns>
        public bool ValidateImgVerificationCode(string phone, string imgCode)
        {
            var cnt = GetErrorCount(phone);
            if (cnt > 3)
            {
                return false;
            }
            //if (string.IsNullOrWhiteSpace(imgCode))
            //{
            //    return false;
            //}
            return VerificationCodeHelper.ValidateImg(imgCode);
        }



        #endregion


        #region 密码加密

        static readonly IEncrypt passwordEncrypt = IocHelper.Create<IEncrypt>();

        #endregion

        #region 用户注册核心

        /// <summary>
        /// 基于手机号注册方式写入用户(会根据是否有密码选择写入账号表)
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="person"></param>
        public static UserData InsertUserByPhone(RegByPhoneRequest arg, out PersonData person)
        {
            UserDataAccess aAccess = new UserDataAccess();
            //用户基本信息
            var user = UserHelper.NewUser();
            var re = aAccess.Insert(user);
            LogRecorder.MonitorTrace($"{re}插入用户{user.UserId}--{aAccess.DataBase.ConnectionString}");
            person = InsertPerson(user, arg.MobilePhone, UserHelper.GetDefaultNikeName(arg.MobilePhone));
            CheckAccount(arg.UserPassword, user.UserId, arg.MobilePhone);
            UserHelper.CacheUserInfo(user, person);
            return user;
        }

        private static PersonData InsertPerson(UserData user, string phone, string nike)
        {
            var pAccess = new PersonDataAccess();
            var person = pAccess.LoadByPrimaryKey(user.Id);
            if (person != null)
            {
                if (!string.IsNullOrWhiteSpace(phone))
                    person.PhoneNumber = phone;
                if (!string.IsNullOrWhiteSpace(nike))
                    person.NickName = nike;
                person.DataState = DataStateType.Enable;
                pAccess.Update(person);
                return person;
            }
            pAccess.Insert(person = new PersonData
            {
                UserId = user.UserId,
                PhoneNumber = phone,
                NickName = nike,
                AvatarUrl = UserHelper.DefaultAvatarUrlPath,
                AddDate = DateTime.Now,
                DataState = DataStateType.Enable
            });
            return person;
        }

        /// <summary>
        /// 完善账号信息（若账号不存在 则新建；若存在，则重置为指定数据）
        /// 若pwd为空，则什么都不做
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static void CheckAccount(string pwd, long userId, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(pwd))
                return;
            var acAccess = new AccountDataAccess();
            var account = acAccess.FirstOrDefault(p => p.AccountName == name);
            if (account != null)
            {
                account.UserId = userId;
                account.Password = passwordEncrypt.Encrypt(pwd);
                acAccess.Update(account);
            }
            else
            {
                acAccess.Insert(new AccountData
                {
                    UserId = userId,
                    AccountName = name,
                    Password = passwordEncrypt.Encrypt(pwd)
                });
            }
        }



        /// <summary>
        /// 生成并返回登录信息
        /// </summary>
        private void CreateAccessToken(ApiResult<LoginResponse> result, UserData user, PersonData person, string account, string type)
        {
            try
            {
                ClearErrorCount(account);
                var bl = IocHelper.Create<IOAuthBusiness>();
                var re = bl.CreateAccessToken(user, type, account, GlobalContext.Current.Token);
                result.Success = re.Success;
                result.Status = re.Status;
                if (!result.Success)
                    return;
                result.ResultData = new LoginResponse
                {
                    AccessToken = re.ResultData?.AccessToken,
                    RefreshToken = re.ResultData?.RefreshToken,
                    Profile = new PersonPublishInfo
                    {
                        NickName = re.ResultData?.Profile?.NickName ?? UserHelper.GetDefaultNikeName(person.PhoneNumber),
                        AvatarUrl = re.ResultData?.Profile?.AvatarUrl ?? UserHelper.DefaultAvatarUrlPath,
                        PhoneNumber = person.PhoneNumber
                    }
                };
                //#region 添加微信票据信息
                //WechatHelp.FillLoginResponseWithWechatData(user.UserId, result.ResultData);
                ////WechatHelp.FillLoginResponseWithWechatData(user.UserId, result.ResultData,result);
                //#endregion
            }
            catch (Exception e)
            {
                LogRecorder.Exception(e);
                result.Success = false;
                result.Status = new ApiStatusResult
                {
                    ErrorCode = ErrorCode.LocalException
                };
            }
        }


        [Conditional("LoginLog")]
        private static void LoginLog(long userId, string channal, AuthorizeType loginType, string loginName, bool state = true)
        {
            var access = new LoginLogDataAccess();
            access.Insert(new LoginLogData
            {
                UserId = userId,
                DeviceId = GlobalContext.Current.Token,
                LoginType = loginType,
                LoginName = loginName,
                Channal = channal,
                Os = GlobalContext.Customer?.Os,
                App = GlobalContext.Customer?.App,
                Success = state,
                AddDate = DateTime.Now
            });
        }

        #endregion


        #region 手机

        //static Dictionary<string, DateTime> LoginLock = new Dictionary<string, DateTime>();


        partial void MobileCheck(Argument arg, ApiResult result)
        {
            string PhoneNumber = arg.Value;
            var pAccess = new PersonDataAccess();
            if (!pAccess.Any(p => p.PhoneNumber == PhoneNumber))
            {
                result.Success = false;
                result.Status = new ApiStatusResult(40403, "您的手机号尚未注册，请直接通过短信登录");
            }
            else
            {
                result.Success = true;
                result.Status = new ApiStatusResult(200, "手机号已注册");
            }
        }



        /// <summary>
        /// 手机短信注册
        /// </summary>
        /// <param name="arg">基于手机号的注册请求参数</param>
        /// <param name="result">登录返回数据</param>
        partial void RegisteByPhone(RegByPhoneRequest arg, ApiResult<LoginResponse> result)
        {
            using (MySqlDataBaseScope.CreateScope<UserCenterDb>())
            {
                //短信验证码校验
                var smsResult = VerificationCodeHelper.ValidateSms(arg.SMSVerificationCode, arg.MobilePhone);
                if (!smsResult)
                {
                    result.Success = false;
                    result.Status = new ApiStatusResult(50003, "请输入正确验证码！");
                    return;
                }
                //手机号检验,已注册的，直接登录
                var pAccess = new PersonDataAccess();
                var person = pAccess.FirstOrDefault(p => p.PhoneNumber.Equals(arg.MobilePhone));
                if (person != null)
                {
                    CheckAccount(arg.UserPassword, person.UserId, person.PhoneNumber);
                    LoginLog(person.UserId, arg.Channel, AuthorizeType.MobilePhone, arg.MobilePhone);
                    var aAccess = new UserDataAccess();
                    var user = aAccess.LoadByPrimaryKey(person);
                    if (user != null)
                    {
                        CheckAccount(arg.UserPassword, user.UserId, person.PhoneNumber);
                    }
                    else
                    {
                        aAccess.Insert(user = UserHelper.NewUser());
                    }
                    LoginLog(user.UserId, arg.Channel, AuthorizeType.MobilePhone, arg.MobilePhone);
                    CreateAccessToken(result, user, person, person.PhoneNumber, "login");
                }
                else
                {
                    var user = InsertUserByPhone(arg, out person);
                    LoginLog(user.UserId, arg.Channel, AuthorizeType.MobilePhone, arg.MobilePhone);
                    CreateAccessToken(result, user, person, person.PhoneNumber, "registe");
                }
            }
        }





        /// <summary>
        /// 短信登录(会校验短信验证码)。校验短信成功后，若用户不存在 则自动创建用户
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="result"></param>
        partial void LoginBySms(LoginbySmsRequest arg, ApiResult<LoginResponse> result)
        {
            //while (true)
            //{
            //    lock (LoginLock)
            //    {
            //        if (!LoginLock.ContainsKey(arg.MobilePhone))
            //            break;
            //    }
            //    Thread.Sleep(50);
            //}
            //lock (LoginLock)
            //{
            //    LoginLock.Add(arg.MobilePhone, DateTime.Now);
            //}

            try
            {
                using (var scope = MySqlDataBaseScope.CreateScope<UserCenterDb>())
                {
                    //短信验证码校验
                    var smsResult = VerificationCodeHelper.ValidateSms(arg.SMSVerificationCode, arg.MobilePhone);
                    if (!smsResult)
                    {
                        WriteErrorCount(arg.MobilePhone);
                        result.Success = false;
                        result.Status = new ApiStatusResult(50003, "请输入正确验证码！");
                        return;
                    }
                    using (var tc = TransactionScope.CreateScope(scope.DataBase))
                    {
                        UserData user;
                        var pAccess = new PersonDataAccess();
                        var person = pAccess.FirstOrDefault(p => p.PhoneNumber.Equals(arg.MobilePhone));
                        if (person == null)
                        {
                            user = InsertUserByPhone(new RegByPhoneRequest
                            {
                                MobilePhone = arg.MobilePhone,
                                Channel = arg.Channel,
                                TraceMark = arg.TraceMark
                            }, out person);
                            CreateAccessToken(result, user, person, person.PhoneNumber, "regist");
                        }
                        else
                        {
                            //手机号检验,已注册的，直接登录
                            var aAccess = new UserDataAccess();
                            user = aAccess.LoadByPrimaryKey(person.UserId);
                            CreateAccessToken(result, user, person, person.PhoneNumber, "login");
                        }

                        if (result.Success)
                        {
                            result.ResultData.Profile.IsRegist = person.IdCard == null;
                        }
                        LoginLog(user.UserId, arg.Channel, AuthorizeType.MobilePhone, arg.MobilePhone);
                        tc.SetState(true);
                    }
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Status = new ApiStatusResult(50003, "系统忙,请稍后重试！");
                LogRecorder.Exception(e);
            }
            //lock (LoginLock)
            //{
            //    LoginLock.Remove(arg.MobilePhone);
            //}
        }


        /// <summary>
        /// 手机登录
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="result"></param>
        partial void PhoneAccountLogin(PhoneLoginRequest arg, ApiResult<LoginResponse> result)
        {
            AccountLogin(arg, result);
        }




        /// <summary>
        /// 手机账号密码登录
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="result"></param>
        partial void AccountLogin(PhoneLoginRequest arg, ApiResult<LoginResponse> result)
        {
            using (MySqlDataBaseScope.CreateScope<UserCenterDb>())
            {
                var imgResult = ValidateImgVerificationCode(arg.MobilePhone, arg.VerificationCode);
                if (!imgResult)
                {
                    WriteErrorCount(arg.MobilePhone);
                    result.Success = false;
                    result.Status = new ApiStatusResult(50003, "请输入正确验证码！");
                    return;
                }

                //手机号检验,已注册的，直接登录
                var aAccess = new AccountDataAccess();
                var account = aAccess.FirstOrDefault(p => p.AccountName.Equals(arg.MobilePhone));
                var error = 0;
                if (string.IsNullOrWhiteSpace(account?.Password))
                {
                    error = 1;
                    LogRecorder.MonitorTrace("用户无密码");
                }
                else
                {
                    var old = passwordEncrypt.Decrypt(account.Password);
                    if (old != arg.UserPassword)
                    {
                        error = 2;
                        LogRecorder.MonitorTrace($"用户错误密码【{old}】【{arg.UserPassword}】");
                    }
                }

                if (error > 0 || account == null)
                {
                    WriteErrorCount(arg.MobilePhone);
                    LoginLog(account?.UserId ?? 0, null, AuthorizeType.Account, arg.MobilePhone, false);
                    result.Success = false;
                    result.Status = new ApiStatusResult
                    {
                        ErrorCode = 40500,
                        ClientMessage = "用户名或密码不正确"
                    };
                    LoginLog(0, null, AuthorizeType.Account, arg.MobilePhone);
                    return;
                }

                var uAccess = new UserDataAccess();
                var user = uAccess.LoadByPrimaryKey(account.UserId);
                var pAccess = new PersonDataAccess();
                var person = pAccess.LoadByPrimaryKey(account.UserId);
                LoginLog(user.UserId, null, AuthorizeType.Account, arg.MobilePhone);
                CreateAccessToken(result, user, person, arg.MobilePhone, "login");
            }
        }

        #endregion


    }
}