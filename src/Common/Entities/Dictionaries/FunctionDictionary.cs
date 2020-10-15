// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Gboxt.Common.WpfMvvmBase
// ����:2014-11-29
// �޸�:2014-11-29
// *****************************************************/

#region ����

using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

#endregion

namespace Agebull.EntityModel
{
    /// <summary>
    ///     ��չ�����ֵ�
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public sealed class FunctionDictionary : FunctionDictionaryBase, IFunctionDictionary
    {
        #region ȡ�����

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Func<TResult> GetFunction<TResult>(string name = null)
        {
            return GetDelegate<Func<TResult>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TArg1, TResult> GetFunction<TArg1, TResult>(string name = null)
        {
            return GetDelegate<Func<TArg1, TResult>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TArg1, TArg2, TResult> GetFunction<TArg1, TArg2, TResult>(string name = null)
        {
            return GetDelegate<Func<TArg1, TArg2, TResult>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TArg1, TArg2, TArg3, TResult> GetFunction<TArg1, TArg2, TArg3, TResult>(string name = null)
        {
            return GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TArg1, TArg2, TArg3, TArg4, TResult> GetFunction<TArg1, TArg2, TArg3, TArg4, TResult>(string name = null)
        {
            return GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult> GetFunction<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(string name = null)
        {
            return GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Action GetAction(string name = null)
        {
            return GetDelegate<Action>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TArg1> GetAction<TArg1>(string name = null)
        {
            return GetDelegate<Action<TArg1>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TArg1, TArg2> GetAction<TArg1, TArg2>(string name = null)
        {
            return GetDelegate<Action<TArg1, TArg2>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TArg1, TArg2, TArg3> GetAction<TArg1, TArg2, TArg3>(string name = null)
        {
            return GetDelegate<Action<TArg1, TArg2, TArg3>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TArg1, TArg2, TArg3, TArg4> GetAction<TArg1, TArg2, TArg3, TArg4>(string name = null)
        {
            return GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TArg1, TArg2, TArg3, TArg4, TArg5> GetAction<TArg1, TArg2, TArg3, TArg4, TArg5>(string name = null)
        {
            return GetDelegate<Action<TArg1, TArg2, TArg3, TArg4, TArg5>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> GetAction<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(string name = null)
        {
            return GetDelegate<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>>(name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TResult>(Func<TResult> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TArg1, TResult>(Func<TArg1, TResult> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TArg1, TArg2, TResult>(Func<TArg1, TArg2, TResult> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, TResult> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TArg1, TArg2, TArg3, TArg4, TResult>(Func<TArg1, TArg2, TArg3, TArg4, TResult> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<TArg1>(Action<TArg1> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<TArg1, TArg2>(Action<TArg1, TArg2> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        #endregion

        #region ִ��

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult Execute<TResult>(string name = null)
        {
            var func = GetDelegate<Func<TResult>>(name);
            if (func != null)
            {
                return func();
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult Execute<TArg1, TResult>(TArg1 arg1, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TResult>>(name);
            if (func != null)
            {
                return func(arg1);
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult Execute<TArg1, TArg2, TResult>(TArg1 arg1, TArg2 arg2, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TResult>>(name);
            if (func != null)
            {
                return func(arg1, arg2);
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult Execute<TArg1, TArg2, TArg3, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>(name);
            if (func != null)
            {
                return func(arg1, arg2, arg3);
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult Execute<TArg1, TArg2, TArg3, TArg4, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>(name);
            if (func != null)
            {
                return func(arg1, arg2, arg3, arg4);
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void Run(string name = null)
        {
            var action = GetDelegate<Action>(name);
            if (action != null)
            {
                action();
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void Run<TArg1>(TArg1 arg1, string name = null)
        {
            var action = GetDelegate<Action<TArg1>>(name);
            if (action != null)
            {
                action(arg1);
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void Run<TArg1, TArg2>(TArg1 arg1, TArg2 arg2, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2>>(name);
            if (action != null)
            {
                action(arg1, arg2);
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void Run<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3>>(name);
            if (action != null)
            {
                action(arg1, arg2, arg3);
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void Run<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>(name);
            if (action != null)
            {
                action(arg1, arg2, arg3, arg4);
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        #endregion

        #region ����ִ��

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TResult>(string name = null)
        {
            var func = GetDelegate<Func<TResult>>(name);
            return func != null ? func() : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TArg1, TResult>(TArg1 arg1, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TResult>>(name);
            return func != null ? func(arg1) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TArg1, TArg2, TResult>(TArg1 arg1, TArg2 arg2, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TResult>>(name);
            return func != null ? func(arg1, arg2) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TArg1, TArg2, TArg3, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>(name);
            return func != null ? func(arg1, arg2, arg3) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TArg1, TArg2, TArg3, TArg4, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>(name);
            return func != null ? func(arg1, arg2, arg3, arg4) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void TryRun(string name = null)
        {
            var action = GetDelegate<Action>(name);
            if (action != null)
            {
                action();
            }
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void TryRun<TArg1>(TArg1 arg1, string name = null)
        {
            var action = GetDelegate<Action<TArg1>>(name);
            if (action != null)
            {
                action(arg1);
            }
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void TryRun<TArg1, TArg2>(TArg1 arg1, TArg2 arg2, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2>>(name);
            if (action != null)
            {
                action(arg1, arg2);
            }
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void TryRun<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3>>(name);
            if (action != null)
            {
                action(arg1, arg2, arg3);
            }
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void TryRun<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>(name);
            if (action != null)
            {
                action(arg1, arg2, arg3, arg4);
            }
        }

        #endregion


        #region �첽


        #region ִ��

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncExecute<TResult>(Action<TResult> asyncAction, string name = null)
        {
            var func = GetDelegate<Func<TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func();
                    asyncAction(result);
                });
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncExecute<TArg1, TResult>(Action<TResult> asyncAction, TArg1 arg1, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func( arg1);
                    asyncAction(result);
                });
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncExecute<TArg1, TArg2, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func( arg1, arg2);
                    asyncAction(result);
                });
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncExecute<TArg1, TArg2, TArg3, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func( arg1, arg2, arg3);
                    asyncAction(result);
                });
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction,
        ///     ����ͬ����Ĭ��ֵΪ��������asyncAction
        ///  </remarks>
        public void AsyncExecute<TArg1, TArg2, TArg3, TArg4, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>(name);

            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func( arg1, arg2, arg3, arg4);
                    asyncAction(result);
                });
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncRun(Action asyncAction = null, string name = null)
        {
            var action = GetDelegate<Action>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action();
                    if (asyncAction != null)
                        asyncAction();
                });
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncRun<TArg1>(TArg1 arg1, Action asyncAction = null, string name = null)
        {
            var action = GetDelegate<Action<TArg1>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action( arg1);
                    if (asyncAction != null)
                        asyncAction();
                });
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncRun<TArg1, TArg2>(TArg1 arg1, TArg2 arg2, Action asyncAction = null, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() => action( arg1, arg2));
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncRun<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action asyncAction = null, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action( arg1, arg2, arg3);
                    if (asyncAction != null)
                        asyncAction();
                });
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction,
        ///     ����ͬ����Ĭ��ֵΪ��������asyncAction
        ///  </remarks>
        public void AsyncRun<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action asyncAction = null, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action( arg1, arg2, arg3, arg4);
                    if (asyncAction != null)
                        asyncAction();
                });
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        #endregion

        #region ����ִ��

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncTryExecute<TResult>(Action<TResult> asyncAction, string name = null)
        {
            var func = GetDelegate<Func<TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func();
                    asyncAction(result);
                });
            }
            else
            {
                asyncAction(default(TResult));
            }
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncTryExecute<TArg1, TResult>(Action<TResult> asyncAction, TArg1 arg1, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func( arg1);
                    asyncAction(result);
                });
            }
            else
            {
                asyncAction(default(TResult));
            }
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncTryExecute<TArg1, TArg2, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func( arg1, arg2);
                    asyncAction(result);
                });
            }
            else
            {
                asyncAction(default(TResult));
            }
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncTryExecute<TArg1, TArg2, TArg3, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func( arg1, arg2, arg3);
                    asyncAction(result);
                });
            }
            else
            {
                asyncAction(default(TResult));
            }
        }

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction,
        ///     ����ͬ����Ĭ��ֵΪ��������asyncAction
        ///  </remarks>
        public void AsyncTryExecute<TArg1, TArg2, TArg3, TArg4, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>(name);

            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func( arg1, arg2, arg3, arg4);
                    asyncAction(result);
                });
            }
            else
            {
                asyncAction(default(TResult));
            }
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncTryRun(Action asyncAction = null, string name = null)
        {
            var action = GetDelegate<Action>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action();
                    if (asyncAction != null)
                        asyncAction();
                });
            }
            else if (asyncAction != null)
            {
                asyncAction();
            }
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncTryRun<TArg1>(TArg1 arg1, Action asyncAction = null, string name = null)
        {
            var action = GetDelegate<Action<TArg1>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action( arg1);
                    if (asyncAction != null)
                        asyncAction();
                });
            }
            else if (asyncAction != null)
            {
                asyncAction();
            }
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncTryRun<TArg1, TArg2>(TArg1 arg1, TArg2 arg2, Action asyncAction = null, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() => action( arg1, arg2));
            }
            else if (asyncAction != null)
            {
                asyncAction();
            }
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncTryRun<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action asyncAction = null, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action( arg1, arg2, arg3);
                    if (asyncAction != null)
                        asyncAction();
                });
            }
            else if (asyncAction != null)
            {
                asyncAction();
            }
        }

        /// <summary>
        ///     �첽ִ�ж�Ӧ���͵ķ���(Func)
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction,
        ///     ����ͬ����Ĭ��ֵΪ��������asyncAction
        ///  </remarks>
        public void AsyncTryRun<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action asyncAction = null, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action( arg1, arg2, arg3, arg4);
                    if (asyncAction != null)
                        asyncAction();
                });
            }
            else if (asyncAction != null)
            {
                asyncAction();
            }
        }

        #endregion
        #endregion


        #region ͨ���첽����ִ��


        #region ִ��

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,�����׳��쳣
        /// </returns>
        public void Execute<TResult>(ITackProxy<TResult> proxy, string name = null)
        {
            var func = GetDelegate<Func<TResult>>(name);
            if (func != null)
            {
                proxy.Run(func);
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,�����׳��쳣
        /// </returns>
        public void Execute<TArg1, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(arg1));
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,�����׳��쳣
        /// </returns>
        public void Execute<TArg1, TArg2, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(arg1, arg2));
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,�����׳��쳣
        /// </returns>
        public void Execute<TArg1, TArg2, TArg3, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(arg1, arg2, arg3));
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction,
        ///     �����׳��쳣
        ///  </remarks>
        public void Execute<TArg1, TArg2, TArg3, TArg4, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>(name);

            if (func != null)
            {
                proxy.Run(() => func(arg1, arg2, arg3, arg4));
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,�����׳��쳣
        /// </returns>
        public void Run(ITackProxy proxy, string name = null)
        {
            var action = GetDelegate<Action>(name);
            if (action != null)
            {
                proxy.Run(action);
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,�����׳��쳣
        /// </returns>
        public void Run<TArg1>(ITackProxy proxy, TArg1 arg1, string name = null)
        {
            var action = GetDelegate<Action<TArg1>>(name);
            if (action != null)
            {
                proxy.Run(() => action(arg1));
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,�����׳��쳣
        /// </returns>
        public void Run<TArg1, TArg2>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2>>(name);
            if (action != null)
            {
                proxy.Run(() => action(arg1, arg2));
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,�����׳��쳣
        /// </returns>
        public void Run<TArg1, TArg2, TArg3>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3>>(name);
            if (action != null)
            {
                proxy.Run(() => action(arg1, arg2, arg3));
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction,
        ///     �����׳��쳣
        ///  </remarks>
        public void Run<TArg1, TArg2, TArg3, TArg4>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>(name);
            if (action != null)
            {
                proxy.Run(() => action(arg1, arg2, arg3, arg4));
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        #endregion

        #region ����ִ��

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </returns>
        public void TryExecute<TResult>(ITackProxy<TResult> proxy, string name = null)
        {
            var func = GetDelegate<Func<TResult>>(name);
            if (func != null)
            {
                proxy.Run(func);
            }
            else
            {
                proxy.Exist();
            }
        }

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </returns>
        public void TryExecute<TArg1, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(arg1));
            }
            else
            {
                proxy.Exist();
            }
        }

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </returns>
        public void TryExecute<TArg1, TArg2, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(arg1, arg2));
            }
            else
            {
                proxy.Exist();
            }
        }

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </returns>
        public void TryExecute<TArg1, TArg2, TArg3, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(arg1, arg2, arg3));
            }
            else
            {
                proxy.Exist();
            }
        }

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction,
        ///     ����ͬ������proxy.Exist
        ///  </remarks>
        public void TryExecute<TArg1, TArg2, TArg3, TArg4, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>(name);

            if (func != null)
            {
                proxy.Run(() => func(arg1, arg2, arg3, arg4));
            }
            else
            {
                proxy.Exist();
            }
        }

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </returns>
        public void TryRun(ITackProxy proxy, string name = null)
        {
            var action = GetDelegate<Action>(name);
            if (action != null)
            {
                proxy.Run(action);
            }
            else
            {
                proxy.Exist();
            }
        }

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </returns>
        public void TryRun<TArg1>(ITackProxy proxy, TArg1 arg1, string name = null)
        {
            var action = GetDelegate<Action<TArg1>>(name);
            if (action != null)
            {
                proxy.Run(() => action(arg1));
            }
            else
            {
                proxy.Exist();
            }
        }

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </returns>
        public void TryRun<TArg1, TArg2>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2>>(name);
            if (action != null)
            {
                proxy.Run(() => action(arg1, arg2));
            }
            else
            {
                proxy.Exist();
            }
        }

        /// <summary>
        ///     ͨ���첽����ִ��Action
        /// </summary>
        /// <returns>
        ///     ������������첽����proxy.Run,����ͬ������proxy.Exist
        /// </returns>
        public void TryRun<TArg1, TArg2, TArg3>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3>>(name);
            if (action != null)
            {
                proxy.Run(() => action(arg1, arg2, arg3));
            }
            else
            {
                proxy.Exist();
            }
        }

        /// <summary>
        ///     ͨ���첽����ִ��Func
        /// </summary>
        /// <remarks>
        ///     �����������,���첽���÷���,�Է���ֵΪ��������asyncAction,
        ///     ����ͬ������proxy.Exist
        ///  </remarks>
        public void TryRun<TArg1, TArg2, TArg3, TArg4>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>(name);
            if (action != null)
            {
                proxy.Run(() => action(arg1, arg2, arg3, arg4));
            }
            else
            {
                proxy.Exist();
            }
        }

        #endregion

        #endregion
    }
}
