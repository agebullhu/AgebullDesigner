// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Gboxt.Common.WpfMvvmBase
// ����:2014-11-29
// �޸�:2014-11-30
// *****************************************************/

#region ����

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

#endregion

namespace Agebull.EntityModel
{
    /// <summary>
    ///     ���������ֵ�
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public sealed class DependencyDelegates : IDependencyDelegates
    {
        #region �����ֵ�

        /// <summary>
        ///     ���������ֵ�
        /// </summary>
        [IgnoreDataMember]
        private readonly Dictionary<Type, object> _dictionary = new Dictionary<Type, object>();

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        public void AnnexDelegate<TAction>(TAction action) where TAction : class
        {
            var type = typeof(TAction);
            if (_dictionary.ContainsKey(type))
            {
                if (Equals(action, null))
                {
                    _dictionary.Remove(type);
                }
                else
                {
                    _dictionary[type] = action as Delegate;
                }
            }
            else if (!Equals(action, null))
            {
                _dictionary.Add(type, action as Delegate);
            }
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public TValue GetDelegate<TValue>() where TValue : class
        {
            var type = typeof(TValue);
            if (!_dictionary.TryGetValue(type, out object value))
            {
                return null;
            }
            if (!(value is TValue))
            {
                return null;
            }
            return value as TValue;
        }

        /// <summary>
        ///     �Ƿ��Ѹ��Ӷ���
        /// </summary>
        /// <typeparam name="TAction">��������(�������޶�ΪAction��Func���������͵�ί��)</typeparam>
        /// <returns>��������򷵻ط���,���򷵻ؿ�</returns>
        public bool HaseDelegate<TAction>() where TAction : class
        {
            return _dictionary.ContainsKey(typeof(TAction));
        }

        #endregion

        #region ���ӷ���

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TResult>(Func<TResult> value)
        {
            AnnexDelegate(value);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TResult, T2>(Func<TResult, T2> value)
        {
            AnnexDelegate(value);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TResult, T2, T3>(Func<TResult, T2, T3> value)
        {
            AnnexDelegate(value);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TResult, T2, T3, T4>(Func<TResult, T2, T3, T4> value)
        {
            AnnexDelegate(value);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TResult, T2, T3, T4, T5>(Func<TResult, T2, T3, T4, T5> value)
        {
            AnnexDelegate(value);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction(Action value)
        {
            AnnexDelegate(value);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<T>(Action<T> value)
        {
            AnnexDelegate(value);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<T1, T2>(Action<T1, T2> value)
        {
            AnnexDelegate(value);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<T1, T2, T3>(Action<T1, T2, T3> value)
        {
            AnnexDelegate(value);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4> value)
        {
            AnnexDelegate(value);
        }

        #endregion

        #region ȡ����

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        /// <returns></returns>
        public Func<TResult> GetFunction<TResult>()
        {
            return GetDelegate<Func<TResult>>();
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TArg1, TResult> GetFunction<TArg1, TResult>()
        {
            return GetDelegate<Func<TArg1, TResult>>();
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TArg1, TArg2, TResult> GetFunction<TArg1, TArg2, TResult>()
        {
            return GetDelegate<Func<TArg1, TArg2, TResult>>();
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TArg1, TArg2, TArg3, TResult> GetFunction<TArg1, TArg2, TArg3, TResult>()
        {
            return GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>();
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TArg1, TArg2, TArg3, TArg4, TResult> GetFunction<TArg1, TArg2, TArg3, TArg4, TResult>()
        {
            return GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>();
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult> GetFunction<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>()
        {
            return GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>>();
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        /// <returns></returns>
        public Action GetAction()
        {
            return GetDelegate<Action>();
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TArg1> GetAction<TArg1>()
        {
            return GetDelegate<Action<TArg1>>();
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TArg1, TArg2> GetAction<TArg1, TArg2>()
        {
            return GetDelegate<Action<TArg1, TArg2>>();
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TArg1, TArg2, TArg3> GetAction<TArg1, TArg2, TArg3>()
        {
            return GetDelegate<Action<TArg1, TArg2, TArg3>>();
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TArg1, TArg2, TArg3, TArg4> GetAction<TArg1, TArg2, TArg3, TArg4>()
        {
            return GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>();
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TArg1, TArg2, TArg3, TArg4, TArg5> GetAction<TArg1, TArg2, TArg3, TArg4, TArg5>()
        {
            return GetDelegate<Action<TArg1, TArg2, TArg3, TArg4, TArg5>>();
        }

        #endregion

        #region �з���ֵ

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TResult>()
        {
            var func = GetDelegate<Func<TResult>>();
            return func != null ? func() : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TArg1, TResult>(TArg1 arg1)
        {
            var func = GetDelegate<Func<TArg1, TResult>>();
            return func != null ? func(arg1) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TArg1, TArg2, TResult>(TArg1 arg1, TArg2 arg2)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TResult>>();
            return func != null ? func(arg1, arg2) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TArg1, TArg2, TArg3, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>();
            return func != null ? func(arg1, arg2, arg3) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TArg1, TArg2, TArg3, TArg4, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>();
            return func != null ? func(arg1, arg2, arg3, arg4) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult Execute<TResult>()
        {
            var func = GetDelegate<Func<TResult>>();
            if (func != null)
            {
                return func();
            }
            throw new ArgumentException("�����ڶ�Ӧ�ķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult Execute<TArg1, TResult>(TArg1 arg1)
        {
            var func = GetDelegate<Func<TArg1, TResult>>();
            if (func != null)
            {
                return func(arg1);
            }
            throw new ArgumentException("�����ڶ�Ӧ�ķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult Execute<TArg1, TArg2, TResult>(TArg1 arg1, TArg2 arg2)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TResult>>();
            if (func != null)
            {
                return func(arg1, arg2);
            }
            throw new ArgumentException("�����ڶ�Ӧ�ķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult Execute<TArg1, TArg2, TArg3, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>();
            if (func != null)
            {
                return func(arg1, arg2, arg3);
            }
            throw new ArgumentException("�����ڶ�Ӧ�ķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult Execute<TArg1, TArg2, TArg3, TArg4, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>();
            if (func != null)
            {
                return func(arg1, arg2, arg3, arg4);
            }
            throw new ArgumentException("�����ڶ�Ӧ�ķ���");
        }

        #endregion

        #region �޷���ֵ

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void TryRun()
        {
            var action = GetDelegate<Action>();
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
        public void TryRun<TArg1>(TArg1 arg1)
        {
            var action = GetDelegate<Action<TArg1>>();
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
        public void TryRun<TArg1, TArg2>(TArg1 arg1, TArg2 arg2)
        {
            var action = GetDelegate<Action<TArg1, TArg2>>();
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
        public void TryRun<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3>>();
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
        public void TryRun<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>();
            if (action != null)
            {
                action(arg1, arg2, arg3, arg4);
            }
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void Run()
        {
            var action = GetDelegate<Action>();
            if (action != null)
            {
                action();
            }
            throw new ArgumentException("�����ڶ�Ӧ�ķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void Run<TArg1>(TArg1 arg1)
        {
            var action = GetDelegate<Action<TArg1>>();
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
        public void Run<TArg1, TArg2>(TArg1 arg1, TArg2 arg2)
        {
            var action = GetDelegate<Action<TArg1, TArg2>>();
            if (action != null)
            {
                action(arg1, arg2);
            }
            throw new ArgumentException("�����ڶ�Ӧ�ķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void Run<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3>>();
            if (action != null)
            {
                action(arg1, arg2, arg3);
            }
            throw new ArgumentException("�����ڶ�Ӧ�ķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void Run<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>();
            if (action != null)
            {
                action(arg1, arg2, arg3, arg4);
            }
            throw new ArgumentException("�����ڶ�Ӧ�ķ���");
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
        public void AsyncExecute<TResult>(Action<TResult> asyncAction)
        {
            var func = GetDelegate<Func<TResult>>();
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
        public void AsyncExecute<TArg1, TResult>(Action<TResult> asyncAction, TArg1 arg1)
        {
            var func = GetDelegate<Func<TArg1, TResult>>();
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(arg1);
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
        public void AsyncExecute<TArg1, TArg2, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TResult>>();
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(arg1, arg2);
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
        public void AsyncExecute<TArg1, TArg2, TArg3, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>();
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(arg1, arg2, arg3);
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
        public void AsyncExecute<TArg1, TArg2, TArg3, TArg4, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>();

            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(arg1, arg2, arg3, arg4);
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
        public void AsyncRun(Action asyncAction = null)
        {
            var action = GetDelegate<Action>();
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
        public void AsyncRun<TArg1>(TArg1 arg1, Action asyncAction = null)
        {
            var action = GetDelegate<Action<TArg1>>();
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(arg1);
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
        public void AsyncRun<TArg1, TArg2>(TArg1 arg1, TArg2 arg2, Action asyncAction = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2>>();
            if (action != null)
            {
                Task.Factory.StartNew(() => action(arg1, arg2));
            }
            throw new ArgumentException("�����ڶ�Ӧ���Ƶķ���");
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void AsyncRun<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action asyncAction = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3>>();
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(arg1, arg2, arg3);
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
        public void AsyncRun<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action asyncAction = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>();
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(arg1, arg2, arg3, arg4);
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
        public void AsyncTryExecute<TResult>(Action<TResult> asyncAction)
        {
            var func = GetDelegate<Func<TResult>>();
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
        public void AsyncTryExecute<TArg1, TResult>(Action<TResult> asyncAction, TArg1 arg1)
        {
            var func = GetDelegate<Func<TArg1, TResult>>();
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(arg1);
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
        public void AsyncTryExecute<TArg1, TArg2, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TResult>>();
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(arg1, arg2);
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
        public void AsyncTryExecute<TArg1, TArg2, TArg3, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>();
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(arg1, arg2, arg3);
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
        public void AsyncTryExecute<TArg1, TArg2, TArg3, TArg4, TResult>(Action<TResult> asyncAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>();

            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(arg1, arg2, arg3, arg4);
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
        public void AsyncTryRun(Action asyncAction = null)
        {
            var action = GetDelegate<Action>();
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
        public void AsyncTryRun<TArg1>(TArg1 arg1, Action asyncAction = null)
        {
            var action = GetDelegate<Action<TArg1>>();
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(arg1);
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
        public void AsyncTryRun<TArg1, TArg2>(TArg1 arg1, TArg2 arg2, Action asyncAction = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2>>();
            if (action != null)
            {
                Task.Factory.StartNew(() => action(arg1, arg2));
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
        public void AsyncTryRun<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action asyncAction = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3>>();
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(arg1, arg2, arg3);
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
        public void AsyncTryRun<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action asyncAction = null)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>();
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(arg1, arg2, arg3, arg4);
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
        public void Execute<TResult>(ITackProxy<TResult> proxy)
        {
            var func = GetDelegate<Func<TResult>>();
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
        public void Execute<TArg1, TResult>(ITackProxy<TResult> proxy, TArg1 arg1)
        {
            var func = GetDelegate<Func<TArg1, TResult>>();
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
        public void Execute<TArg1, TArg2, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TResult>>();
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
        public void Execute<TArg1, TArg2, TArg3, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>();
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
        public void Execute<TArg1, TArg2, TArg3, TArg4, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>();

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
        public void Run(ITackProxy proxy)
        {
            var action = GetDelegate<Action>();
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
        public void Run<TArg1>(ITackProxy proxy, TArg1 arg1)
        {
            var action = GetDelegate<Action<TArg1>>();
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
        public void Run<TArg1, TArg2>(ITackProxy proxy, TArg1 arg1, TArg2 arg2)
        {
            var action = GetDelegate<Action<TArg1, TArg2>>();
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
        public void Run<TArg1, TArg2, TArg3>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3>>();
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
        public void Run<TArg1, TArg2, TArg3, TArg4>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>();
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
        public void TryExecute<TResult>(ITackProxy<TResult> proxy)
        {
            var func = GetDelegate<Func<TResult>>();
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
        public void TryExecute<TArg1, TResult>(ITackProxy<TResult> proxy, TArg1 arg1)
        {
            var func = GetDelegate<Func<TArg1, TResult>>();
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
        public void TryExecute<TArg1, TArg2, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TResult>>();
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
        public void TryExecute<TArg1, TArg2, TArg3, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TResult>>();
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
        public void TryExecute<TArg1, TArg2, TArg3, TArg4, TResult>(ITackProxy<TResult> proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            var func = GetDelegate<Func<TArg1, TArg2, TArg3, TArg4, TResult>>();

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
        public void TryRun(ITackProxy proxy)
        {
            var action = GetDelegate<Action>();
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
        public void TryRun<TArg1>(ITackProxy proxy, TArg1 arg1)
        {
            var action = GetDelegate<Action<TArg1>>();
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
        public void TryRun<TArg1, TArg2>(ITackProxy proxy, TArg1 arg1, TArg2 arg2)
        {
            var action = GetDelegate<Action<TArg1, TArg2>>();
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
        public void TryRun<TArg1, TArg2, TArg3>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3>>();
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
        public void TryRun<TArg1, TArg2, TArg3, TArg4>(ITackProxy proxy, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            var action = GetDelegate<Action<TArg1, TArg2, TArg3, TArg4>>();
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
