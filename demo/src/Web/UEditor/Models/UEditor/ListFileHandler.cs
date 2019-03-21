using System;
using System.Linq;
using System.Linq.Expressions;
using Agebull.EntityModel.Common;
using Agebull.ZeroNet.ManageApplication.DataAccess;

namespace Agebull.ZeroNet.ManageApplication
{
    /// <summary>
    /// FileManager 的摘要说明
    /// </summary>
    internal class ListFileManager : Handler
    {
        private readonly int _start;
        private readonly int _size;

        private readonly AnnexType _annexType;

        public ListFileManager(AnnexType type, int start, int limit)
        {
            _annexType = type;
            _start = start;
            _size = limit;
        }

        public override void Process()
        {
            try
            {
                Expression<Func<AnnexData, bool>> lambda;

                if (_annexType == AnnexType.None)
                    lambda = p => p.AnnexType != _annexType && p.DataState <= DataStateType.Enable;
                else
                    lambda = p => p.AnnexType == _annexType && p.DataState <= DataStateType.Enable;
                var access = new AnnexDataAccess();
                //var convert = access.Compile(lambda);
                var cnt = access.Count(lambda);
                var datas = access.PageData(_start / _size + 1, _size, p => p.Id, true, lambda);

                Result = new
                {
                    state = "SUCCESS",
                    start = _start,
                    size = _size,
                    total = cnt,
                    list = datas?.Select(x => new { url = x.Url, title = x.Title, original = x.Title }),
                };
            }
            catch
            {
                Result = new
                {
                    state = "未知错误",
                    start = _start,
                    size = _size,
                    total = 0,
                    list = new char[0]
                };
            }
        }
    }
}