// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Gboxt.Common.WpfMvvmBase
// ����:2014-11-29
// �޸�:2014-11-29
// *****************************************************/

#region ����

using System.ComponentModel;

#endregion

namespace Agebull.EntityModel
{
    public interface IEntityObject
    {
        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name="source">���Ƶ�Դ�ֶ�</param>
        void CopyValue(IEntityObject source);

        /// <summary>
        ///     �õ��ֶε�ֵ
        /// </summary>
        /// <param name="field"> �ֶε����� </param>
        /// <returns> �ֶε�ֵ </returns>
        object GetValue(string field);

        /// <summary>
        ///     �����ֶε�ֵ
        /// </summary>
        /// <param name="field"> �ֶε����� </param>
        /// <param name="value"> �ֶε�ֵ </param>
        void SetValue(string field, object value);
    }
    /// <summary>
    ///     �༭����
    /// </summary>
    public interface IEditObject : IEntityObject
#if CLIENT
        , INotifyPropertyChanging, INotifyPropertyChanged
#endif
    {
        /// <summary>
        ///     �Ƿ��޸�
        /// </summary>
        bool IsModified
        {
            get;
        }

        /// <summary>
        ///     �Ƿ���ɾ��
        /// </summary>
        bool IsDelete
        {
            get;
        }

        /// <summary>
        ///     �Ƿ�����
        /// </summary>
        bool IsAdd
        {
            get;
        }

        /// <summary>
        ///     �Ƿ��޸�
        /// </summary>
        /// <param name="field"> �ֶε����� </param>
        bool FieldIsModified(string field);

        /// <summary>
        ///     ����Ϊ�Ǹı�
        /// </summary>
        /// <param name="field"> �ֶε����� </param>
        void SetUnModify(string field);

        /// <summary>
        ///     ����Ϊ�ı�
        /// </summary>
        /// <param name="field"> �ֶε����� </param>
        void SetModify(string field);

        /// <summary>
        ///     �����޸�
        /// </summary>
        void AcceptChanged();

        /// <summary>
        ///     �����޸�
        /// </summary>
        void RejectChanged();

        /// <summary>
        /// �����޸ĵĺ��ڴ���(�����)
        /// </summary>
        /// <remarks>
        /// �Ե�ǰ��������Եĸ���,�����б���,���򽫶�ʧ
        /// </remarks>
        void LaterPeriodByModify();
    }
}
