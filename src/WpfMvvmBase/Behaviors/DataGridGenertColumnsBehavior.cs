using System.Collections;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Gboxt.Common.WpfMvvmBase.Behaviors
{
    public interface IGridSelectionBinding
    {
        IList SelectColumns { set; }
    }
    /// <summary>
    ///     ����Զ������ֶε�ί��,��DisplayNameAttribute��Ϊ��ͷ,��BotwerableAttribute�����Ƿ���ʾ
    /// </summary>
    public sealed class DataGridGenertColumnsBehavior : Behavior<DataGrid>
    {
        /// <summary>
        ///     ����Ϊ���ӵ� AssociatedObject ����á�
        /// </summary>
        /// <remarks>
        ///     ������Ա㽫���ܹҹ��� AssociatedObject��
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            //this.AssociatedObject.AutoGenerateColumns = true;
            AssociatedObject.AutoGeneratingColumn += AssociatedObject_AutoGeneratingColumn;
            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var binding = AssociatedObject.DataContext as IGridSelectionBinding;
            if (binding != null)
                binding.SelectColumns = AssociatedObject.SelectedItems;
        }

        private void AssociatedObject_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var descriptor = e.PropertyDescriptor as MemberDescriptor;
            if (descriptor == null)
                return;
            if (!descriptor.IsBrowsable)
            {
                e.Cancel = true;
            }
            else if (!string.IsNullOrEmpty(descriptor.DisplayName))
            {
                e.Column.Header = descriptor.DisplayName;
            }
        }

        /// <summary>
        ///     ����Ϊ���� AssociatedObject ����ʱ��������ʵ�ʷ���֮ǰ�����á�
        /// </summary>
        /// <remarks>
        ///     ������Ա㽫���ܴ� AssociatedObject �н���ҹ���
        /// </remarks>
        protected override void OnDetaching()
        {
            AssociatedObject.AutoGeneratingColumn -= AssociatedObject_AutoGeneratingColumn;
            if (AssociatedObject.DataContext is IGridSelectionBinding)
                AssociatedObject.SelectionChanged -= OnSelectionChanged;
            base.OnDetaching();
        }
    }
}