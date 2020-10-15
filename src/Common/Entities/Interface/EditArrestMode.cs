using System;

namespace Agebull.EntityModel
{
    /// <summary>
    ///     �༭����ֹģʽ
    /// </summary>
    [Flags]
    public enum EditArrestMode : short
    {
        /// <summary>
        ///     ��
        /// </summary>
        None = 0x0,

        /// <summary>
        ///     �ڲ������޸�ǰ�߼�
        /// </summary>
        InnerCheck = 0x1,

        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        PropertyChangingEvent = 0x2,

        /// <summary>
        ///     �ڲ������޸ĺ��߼�
        /// </summary>
        PropertyChanged = 0x4,

        /// <summary>
        ///     ���������޸ĺ��¼�
        /// </summary>
        PropertyChangedEvent = 0x8,

        /// <summary>
        ///     ��¼�޸�
        /// </summary>
        RecordChanged = 0x10,

        /// <summary>
        ///     �ڲ��߼�����
        /// </summary>
        InnerLogical = 0x20,

        /// <summary>
        ///     �����¼�
        /// </summary>
        Events = PropertyChangingEvent | PropertyChangedEvent,

        /// <summary>
        ///     ��չ����
        /// </summary>
        ExtendProcess = InnerCheck | PropertyChanged,

        /// <summary>
        ///     ��ֹ����
        /// </summary>
        All = Events | ExtendProcess | InnerLogical
    }
}