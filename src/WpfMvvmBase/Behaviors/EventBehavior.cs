// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Gboxt.Common.WpfMvvmBase
// ����:2014-11-27
// �޸�:2014-11-29
// *****************************************************/

#region ����

using System.Windows;

#endregion

namespace Gboxt.Common.WpfMvvmBase.Behaviors
{
    /// <summary>
    ///     �ؼ��¼���Ϊ����,��ʹ��ElementEventBehavior������ǿ���͵Ķ���,�Ա���XAML�й���
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    public class EventBehavior<TControl> : DependencyBehavior<TControl>
            where TControl : UIElement
    {
    }

    /// <summary>
    ///     �ؼ��¼���Ϊ����,��ʹ��ElementEventBehavior������ǿ���͵Ķ���,�Ա���XAML�й���
    /// </summary>
    public class EventBehavior : EventBehavior<FrameworkElement>
    {
    }
}
