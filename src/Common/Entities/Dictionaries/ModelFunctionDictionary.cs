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
    ///     ģ����չ�����ֵ�
    /// </summary>
    /// <remarks>
    ///     ��һ����������ģ��
    /// </remarks>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public sealed class ModelFunctionDictionary<TModel> : FunctionDictionaryBase, IFunctionDictionary
            where TModel : class
    {
        /// <summary>
        ///     ����
        /// </summary>
        public ModelFunctionDictionary()
        {
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="model">ģ��</param>
        public ModelFunctionDictionary(TModel model)
        {
            Model = model;
        }

        /// <summary>
        ///     ��Ӧģ��
        /// </summary>
        public TModel Model
        {
            get;
            set;
        }

        #region ȡ�����

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Func<TModel, TResult> GetFunction<TResult>(string name = null)
        {
            return GetDelegate<Func<TModel, TResult>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TModel, TArg1, TResult> GetFunction<TArg1, TResult>(string name = null)
        {
            return GetDelegate<Func<TModel, TArg1, TResult>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TModel, TArg1, TArg2, TResult> GetFunction<TArg1, TArg2, TResult>(string name = null)
        {
            return GetDelegate<Func<TModel, TArg1, TArg2, TResult>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TModel, TArg1, TArg2, TArg3, TResult> GetFunction<TArg1, TArg2, TArg3, TResult>(string name = null)
        {
            return GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TResult>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TModel, TArg1, TArg2, TArg3, TArg4, TResult> GetFunction<TArg1, TArg2, TArg3, TArg4, TResult>(string name = null)
        {
            return GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TArg4, TResult>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Func<TModel, TArg1, TArg2, TArg3, TArg4, TArg5, TResult> GetFunction<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(string name = null)
        {
            return GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Action<TModel> GetAction(string name = null)
        {
            return GetDelegate<Action<TModel>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TModel, TArg1> GetAction<TArg1>(string name = null)
        {
            return GetDelegate<Action<TModel, TArg1>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TModel, TArg1, TArg2> GetAction<TArg1, TArg2>(string name = null)
        {
            return GetDelegate<Action<TModel, TArg1, TArg2>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TModel, TArg1, TArg2, TArg3> GetAction<TArg1, TArg2, TArg3>(string name = null)
        {
            return GetDelegate<Action<TModel, TArg1, TArg2, TArg3>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TModel, TArg1, TArg2, TArg3, TArg4> GetAction<TArg1, TArg2, TArg3, TArg4>(string name = null)
        {
            return GetDelegate<Action<TModel, TArg1, TArg2, TArg3, TArg4>>(name);
        }

        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        public Action<TModel, TArg1, TArg2, TArg3, TArg4, TArg5> GetAction<TArg1, TArg2, TArg3, TArg4, TArg5>(string name = null)
        {
            return GetDelegate<Action<TModel, TArg1, TArg2, TArg3, TArg4, TArg5>>(name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc(Func<TModel> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TResult>(Func<TModel, TResult> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TArg1, TResult>(Func<TModel, TArg1, TResult> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TArg1, TArg2, TResult>(Func<TModel, TArg1, TArg2, TResult> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TArg1, TArg2, TArg3, TResult>(Func<TModel, TArg1, TArg2, TArg3, TResult> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TArg1, TArg2, TArg3, TArg4, TResult>(Func<TModel, TArg1, TArg2, TArg3, TArg4, TResult> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(Func<TModel, TArg1, TArg2, TArg3, TArg4, TArg5, TResult> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction(Action<TModel> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<TArg1>(Action<TModel, TArg1> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<TArg1, TArg2>(Action<TModel, TArg1, TArg2> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<TArg1, TArg2, TArg3>(Action<TModel, TArg1, TArg2, TArg3> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<TArg1, TArg2, TArg3, TArg4>(Action<TModel, TArg1, TArg2, TArg3, TArg4> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void AnnexAction<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TModel, TArg1, TArg2, TArg3, TArg4, TArg5> value, string name = null)
        {
            AnnexDelegate(value, name);
        }

        #endregion

        #region ͬ��

        #region ִ��

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult Execute<TResult>(string name = null)
        {
            var func = GetDelegate<Func<TModel, TResult>>(name);
            if (func != null)
            {
                return func(Model);
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
            var func = GetDelegate<Func<TModel, TArg1, TResult>>(name);
            if (func != null)
            {
                return func(Model, arg1);
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TResult>>(name);
            if (func != null)
            {
                return func(Model, arg1, arg2);
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TResult>>(name);
            if (func != null)
            {
                return func(Model, arg1, arg2, arg3);
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TArg4, TResult>>(name);
            if (func != null)
            {
                return func(Model, arg1, arg2, arg3, arg4);
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
            var action = GetDelegate<Action<TModel>>(name);
            if (action != null)
            {
                action(Model);
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
            var action = GetDelegate<Action<TModel, TArg1>>(name);
            if (action != null)
            {
                action(Model, arg1);
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2>>(name);
            if (action != null)
            {
                action(Model, arg1, arg2);
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2, TArg3>>(name);
            if (action != null)
            {
                action(Model, arg1, arg2, arg3);
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2, TArg3, TArg4>>(name);
            if (action != null)
            {
                action(Model, arg1, arg2, arg3, arg4);
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
            var func = GetDelegate<Func<TModel, TResult>>(name);
            return func != null ? func(Model) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TArg1, TResult>(TArg1 arg1, string name = null)
        {
            var func = GetDelegate<Func<TModel, TArg1, TResult>>(name);
            return func != null ? func(Model, arg1) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TArg1, TArg2, TResult>(TArg1 arg1, TArg2 arg2, string name = null)
        {
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TResult>>(name);
            return func != null ? func(Model, arg1, arg2) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TArg1, TArg2, TArg3, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null)
        {
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TResult>>(name);
            return func != null ? func(Model, arg1, arg2, arg3) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Func),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public TResult TryExecute<TArg1, TArg2, TArg3, TArg4, TResult>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null)
        {
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TArg4, TResult>>(name);
            return func != null ? func(Model, arg1, arg2, arg3, arg4) : default(TResult);
        }

        /// <summary>
        ///     ����ж�Ӧ���͵ķ���(Action),ִ��������������
        /// </summary>
        /// <returns>
        ///     ����������ڲ��ɹ�ִ��,���ض�Ӧ��ֵ,���򷵻ؿ�
        /// </returns>
        public void TryRun(string name = null)
        {
            var action = GetDelegate<Action<TModel>>(name);
            if (action != null)
            {
                action(Model);
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
            var action = GetDelegate<Action<TModel, TArg1>>(name);
            if (action != null)
            {
                action(Model, arg1);
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2>>(name);
            if (action != null)
            {
                action(Model, arg1, arg2);
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2, TArg3>>(name);
            if (action != null)
            {
                action(Model, arg1, arg2, arg3);
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2, TArg3, TArg4>>(name);
            if (action != null)
            {
                action(Model, arg1, arg2, arg3, arg4);
            }
        }

        #endregion

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
            var func = GetDelegate<Func<TModel, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(Model);
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
            var func = GetDelegate<Func<TModel, TArg1, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(Model, arg1);
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(Model, arg1, arg2);
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(Model, arg1, arg2, arg3);
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TArg4, TResult>>(name);

            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(Model, arg1, arg2, arg3, arg4);
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
            var action = GetDelegate<Action<TModel>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(Model);
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
            var action = GetDelegate<Action<TModel, TArg1>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(Model, arg1);
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() => action(Model, arg1, arg2));
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2, TArg3>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(Model, arg1, arg2, arg3);
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2, TArg3, TArg4>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(Model, arg1, arg2, arg3, arg4);
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
            var func = GetDelegate<Func<TModel, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(Model);
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
            var func = GetDelegate<Func<TModel, TArg1, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(Model, arg1);
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(Model, arg1, arg2);
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TResult>>(name);
            if (func != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var result = func(Model, arg1, arg2, arg3);
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TArg4, TResult>>(name);

            if (func != null)
            {
                Task.Factory.StartNew(() =>
                    {
                        var result = func(Model, arg1, arg2, arg3, arg4);
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
            var action = GetDelegate<Action<TModel>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(Model);
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
            var action = GetDelegate<Action<TModel, TArg1>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(Model, arg1);
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() => action(Model, arg1, arg2));
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2, TArg3>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(Model, arg1, arg2, arg3);
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2, TArg3, TArg4>>(name);
            if (action != null)
            {
                Task.Factory.StartNew(() =>
                {
                    action(Model, arg1, arg2, arg3, arg4);
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
            var func = GetDelegate<Func<TModel, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(Model));
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
            var func = GetDelegate<Func<TModel, TArg1, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(Model, arg1));
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(Model, arg1, arg2));
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(Model, arg1, arg2, arg3));
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TArg4, TResult>>(name);

            if (func != null)
            {
                proxy.Run(() => func(Model, arg1, arg2, arg3, arg4));
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
            var action = GetDelegate<Action<TModel>>(name);
            if (action != null)
            {
                proxy.Run(() => action(Model));
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
            var action = GetDelegate<Action<TModel, TArg1>>(name);
            if (action != null)
            {
                proxy.Run(() => action(Model, arg1));
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2>>(name);
            if (action != null)
            {
                proxy.Run(() => action(Model, arg1, arg2));
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2, TArg3>>(name);
            if (action != null)
            {
                proxy.Run(() => action(Model, arg1, arg2, arg3));
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2, TArg3, TArg4>>(name);
            if (action != null)
            {
                proxy.Run(() => action(Model, arg1, arg2, arg3, arg4));
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
            var func = GetDelegate<Func<TModel, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(Model));
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
            var func = GetDelegate<Func<TModel, TArg1, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(Model, arg1));
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(Model, arg1, arg2));
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TResult>>(name);
            if (func != null)
            {
                proxy.Run(() => func(Model, arg1, arg2, arg3));
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
            var func = GetDelegate<Func<TModel, TArg1, TArg2, TArg3, TArg4, TResult>>(name);

            if (func != null)
            {
                proxy.Run(() => func(Model, arg1, arg2, arg3, arg4));
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
            var action = GetDelegate<Action<TModel>>(name);
            if (action != null)
            {
                proxy.Run(() => action(Model));
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
            var action = GetDelegate<Action<TModel, TArg1>>(name);
            if (action != null)
            {
                proxy.Run(() => action(Model, arg1));
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2>>(name);
            if (action != null)
            {
                proxy.Run(() => action(Model, arg1, arg2));
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2, TArg3>>(name);
            if (action != null)
            {
                proxy.Run(() => action(Model, arg1, arg2, arg3));
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
            var action = GetDelegate<Action<TModel, TArg1, TArg2, TArg3, TArg4>>(name);
            if (action != null)
            {
                proxy.Run(() => action(Model, arg1, arg2, arg3, arg4));
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
