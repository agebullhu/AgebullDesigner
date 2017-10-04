using System;

namespace Agebull.EntityModel
{
    public interface IFunctionDictionary
    {
        #region ����

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        void AnnexDelegate<TAction>(TAction action, string name = null) where TAction : class;

        /// <summary>
        ///     �Ƿ��Ѹ��Ӷ���
        /// </summary>
        /// <typeparam name="TAction">��������(�������޶�ΪAction��Func���������͵�ί��)</typeparam>
        /// <param name="name">��������</param>
        /// <returns>��������򷵻ط���,���򷵻ؿ�</returns>
        bool HaseDelegate<TAction>(string name = null) where TAction : class;

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        /// <typeparam name="TAction">��������</typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        TAction GetDelegate<TAction>(string name = null) where TAction : class;

        #endregion

        #region ͬ��ִ��

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,�����׳��쳣
        /// </returns>
        TResult Execute<TResult>(string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,�����׳��쳣
        /// </returns>
        TResult Execute<TArg1, TResult>(TArg1 arg1, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,�����׳��쳣
        /// </returns>
        TResult Execute<TArg1, TArg2, TResult>(TArg1 arg1, TArg2 arg2, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,�����׳��쳣
        /// </returns>
        TResult Execute<TArg1, TArg2, TArg3, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,�����׳��쳣
        /// </returns>
        TResult Execute<TArg1, TArg2, TArg3, TArg4, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <remarks>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,�����׳��쳣
        /// </remarks>
        void Run(string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <remarks>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,�����׳��쳣
        /// </remarks>
        void Run<TArg1>(TArg1 arg1, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <remarks>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,�����׳��쳣
        /// </remarks>
        void Run<TArg1, TArg2>(TArg1 arg1, TArg2 arg2, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <remarks>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,�����׳��쳣
        /// </remarks>
        void Run<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <remarks>
        ///     ���������ִ��,�����׳��쳣
        /// </remarks>
        void Run<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        TResult TryExecute<TResult>(string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        TResult TryExecute<TArg1, TResult>(TArg1 arg1, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        TResult TryExecute<TArg1, TArg2, TResult>(TArg1 arg1, TArg2 arg2, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        TResult TryExecute<TArg1, TArg2, TArg3, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        TResult TryExecute<TArg1, TArg2, TArg3, TArg4, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <remarks>
        ///     �������������ִ��,�������������׳��쳣
        /// </remarks>
        void TryRun(string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <remarks>
        ///     �������������ִ��,�������������׳��쳣
        /// </remarks>
        void TryRun<TArg1>(TArg1 arg1, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <remarks>
        ///     �������������ִ��,�������������׳��쳣
        /// </remarks>
        void TryRun<TArg1, TArg2>(TArg1 arg1, TArg2 arg2, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <remarks>
        ///     �������������ִ��,�������������׳��쳣
        /// </remarks>
        void TryRun<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null);

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <remarks>
        ///     �������������ִ��,�������������׳��쳣
        /// </remarks>
        void TryRun<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null);
        #endregion

        #region �첽ִ��
        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncExecute<TResult>(Action<TResult> asyncAction, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncExecute<TArg1, TResult>(Action<TResult> asyncAction, TArg1 arg1, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncExecute<TArg1, TArg2, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncExecute<TArg1, TArg2, TArg3, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncExecute<TArg1, TArg2, TArg3, TArg4, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Action)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷����ĵ���asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncRun(Action asyncAction = null, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Action)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷����ĵ���asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncRun<TArg1>(TArg1 arg1, Action asyncAction = null, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Action)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷����ĵ���asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncRun<TArg1, TArg2>(TArg1 arg1, TArg2 arg2, Action asyncAction = null, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Action)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷����ĵ���asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncRun<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action asyncAction = null, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Action)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷����ĵ���asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncRun<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action asyncAction = null, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncTryExecute<TResult>(Action<TResult> asyncAction, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncTryExecute<TArg1, TResult>(Action<TResult> asyncAction, TArg1 arg1, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncTryExecute<TArg1, TArg2, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncTryExecute<TArg1, TArg2, TArg3, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void AsyncTryExecute<TArg1, TArg2, TArg3, TArg4, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Action)
        /// </summary>
        /// <remarks>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </remarks>
        void AsyncTryRun(Action asyncAction = null, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Action)
        /// </summary>
        /// <remarks>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </remarks>
        void AsyncTryRun<TArg1>(TArg1 arg1, Action asyncAction = null, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Action)
        /// </summary>
        /// <remarks>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </remarks>
        void AsyncTryRun<TArg1, TArg2>(TArg1 arg1, TArg2 arg2, Action asyncAction = null, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Action)
        /// </summary>
        /// <remarks>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </remarks>
        void AsyncTryRun<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action asyncAction = null, string name = null);

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Action)
        /// </summary>
        /// <remarks>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        ///  </remarks>
        void AsyncTryRun<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action asyncAction = null, string name = null);
        #endregion

        #region �첽����ִ��
        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void Execute<TResult>(ITackProxy<TResult> proxy, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void Execute<TArg1, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void Execute<TArg1, TArg2, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void Execute<TArg1, TArg2, TArg3, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void Execute<TArg1, TArg2, TArg3, TArg4, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷����ĵ���asyncAction; �����׳��쳣
        /// </remarks>
        void Run(ITackProxy proxy, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷����ĵ���asyncAction; �����׳��쳣
        /// </remarks>
        void Run<TArg1>(ITackProxy proxy, TArg1 arg1, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷����ĵ���asyncAction; �����׳��쳣
        /// </remarks>
        void Run<TArg1, TArg2>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷����ĵ���asyncAction; �����׳��쳣
        /// </remarks>
        void Run<TArg1, TArg2, TArg3>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷����ĵ���asyncAction; �����׳��쳣
        /// </remarks>
        void Run<TArg1, TArg2, TArg3, TArg4>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void TryExecute<TResult>(ITackProxy<TResult> proxy, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void TryExecute<TArg1, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void TryExecute<TArg1, TArg2, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void TryExecute<TArg1, TArg2, TArg3, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction; �����׳��쳣
        /// </remarks>
        void TryExecute<TArg1, TArg2, TArg3, TArg4, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <remarks>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </remarks>
        void TryRun(ITackProxy proxy, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <remarks>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </remarks>
        void TryRun<TArg1>(ITackProxy proxy, TArg1 arg1, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <remarks>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </remarks>
        void TryRun<TArg1, TArg2>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <remarks>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </remarks>
        void TryRun<TArg1, TArg2, TArg3>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null);

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <remarks>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        ///  </remarks>
        void TryRun<TArg1, TArg2, TArg3, TArg4>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null);
        #endregion
    }

}