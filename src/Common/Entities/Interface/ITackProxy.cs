using System;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel
{

    /// <summary>
    /// ��ʾTask��ʾ�첽�������
    /// </summary>
    public interface ITackProxy
    {
        /// <summary>
        ///     ��ǰ״̬
        /// </summary>
        CommandStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// �ܷ�ִ��
        /// </summary>
        /// <returns>null��ʾȡ������,treu��ʾ��������ִ��,false��ʾӦ�õȴ�ǰһ��������ɺ�ִ��</returns>
        bool? CanDo();

        void Run(Action task);

        void Exist();
    }


    /// <summary>
    /// ��ʾ�첽�������
    /// </summary>
    public interface ITackProxy<in TResult>
    {
        void Run(Func<TResult> task);

        void Exist(TResult result = default(TResult));
    }
}