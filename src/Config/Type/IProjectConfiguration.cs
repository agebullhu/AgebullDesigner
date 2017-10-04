using System.Collections.Generic;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///     ��Ŀ����
    /// </summary>
    public interface IProjectConfiguration
    {
        /// <summary>
        ///     ���ݿ�����
        /// </summary>
        string DataBaseObjectName { get; }

        /// <summary>
        ///     �����ı�ṹ
        /// </summary>
        IEnumerable<EntityConfig> Schemas { get; }

        /// <summary>
        ///     ҳ�����·��
        /// </summary>
        string PagePath { get; }

        /// <summary>
        ///     ģ�ʹ���·��
        /// </summary>
        string ModelPath { get; }

        /// <summary>
        ///     �����ַ
        /// </summary>
        string CodePath { get; }

        /// <summary>
        ///     �����ռ�
        /// </summary>
        string NameSpace { get; }

        /// <summary>
        ///     ����ʱֻ��
        /// </summary>
        bool ReadOnly { get; }

        /// <summary>
        ///     ���ݿ�����
        /// </summary>
        DataBaseType DbType { get; }

        /// <summary>
        ///     ���ݿ��ַ
        /// </summary>
        string DbHost { get; }

        /// <summary>
        ///     ���ݿ�����
        /// </summary>
        string DbSoruce { get; }

        /// <summary>
        ///     ���ݿ�����
        /// </summary>
        string DbPassWord { get; }

        /// <summary>
        ///     ���ݿ�����
        /// </summary>
        string DbUser { get; }
        
    }
    
}