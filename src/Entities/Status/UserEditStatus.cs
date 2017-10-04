#if CLIENT
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.Common.DataModel
{
    /// <summary>
    ///     �û��༭֧�ֵ�״̬����
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class UserEditStatus : OriginalRecordStatus
    {
        /// <summary>
        /// ��Ӧ�Ķ���
        /// </summary>
        [IgnoreDataMember]
        public UserEditEntityObject<UserEditStatus> UserEditObject
        {
            get;
            private set;
        }

        /// <summary>
        /// ��ʼ����ʵ��
        /// </summary>
        protected override void InitializeInner()
        {
            base.InitializeInner();
            UserEditObject = this.Object as UserEditEntityObject<UserEditStatus>;
        }

        /// <summary>
        ///     ��¼ÿһ���޸�
        /// </summary>
        [IgnoreDataMember]
        private List<EditRecordItem> _editIndexRecords;

        /// <summary>
        ///     ��¼ÿһ���޸�
        /// </summary>
        public List<EditRecordItem> EditIndexRecords
        {
            get
            {
                return this._editIndexRecords ?? (this._editIndexRecords = new List<EditRecordItem>());
            }
        }

        /// <summary>
        ///     ��ǰ�༭��¼�ڵڼ���
        /// </summary>
        public int CurrentEditIndex
        {
            get;
            private set;
        }

        /// <summary>
        ///     �ܷ�����
        /// </summary>
        public bool CanReDo
        {
            get
            {
                return this.EditIndexRecords.Count > 0 && this.CurrentEditIndex < (this.EditIndexRecords.Count - 1);
            }
        }

        /// <summary>
        ///     �ܷ���
        /// </summary>
        public bool CanUnDo
        {
            get
            {
                return this.EditIndexRecords.Count > 0 && this.CurrentEditIndex > 0 && this.CurrentEditIndex <= (this.EditIndexRecords.Count - 1);
            }
        }

        /// <summary>
        /// ���ü�¼
        /// </summary>
        protected override void ResetRecords()
        {
            base.ResetRecords();
            this._editIndexRecords.Clear();
            this.CurrentEditIndex = -1;
            this.Object.OnStatusChanged(NotificationStatusType.CanReDo);
            this.Object.OnStatusChanged(NotificationStatusType.CanUnDo);
        }

        /// <summary>
        ///     �����޸�֮ǰ��״̬�����ʵ��
        /// </summary>
        /// <param name="args">����</param>
        protected override void OnEndPropertyChangingInner(PropertyChangingEventArgsEx args)
        {
            base.OnEndPropertyChangingInner(args);
            this.AddEditIndexRecord(args.PropertyName, args.OldValue);
        }

        private void AddEditIndexRecord(string property, object value)
        {
            if (this.CanReDo)
            {
                this.EditIndexRecords.RemoveRange(this.CurrentEditIndex, this.EditIndexRecords.Count - this.CurrentEditIndex - 1);
            }
            this.EditIndexRecords.Add(new EditRecordItem
            {
                Property = property,
                Value = value
            });
            this.CurrentEditIndex = this.EditIndexRecords.Count - 1;

            this.Object.OnStatusChanged(NotificationStatusType.CanReDo);
            this.Object.OnStatusChanged(NotificationStatusType.CanUnDo);
        }

        /// <summary>
        ///     ����
        /// </summary>
        public void ReDo()
        {
            this.CurrentEditIndex++;
            if (!this.CanReDo)
            {
                return;
            }
            this.EditObject.SetValue(this.EditIndexRecords[this.CurrentEditIndex].Property, this.EditIndexRecords[this.CurrentEditIndex].Value);
        }

        /// <summary>
        ///     ����
        /// </summary>
        public void UnDo()
        {
            if (!this.CanUnDo)
            {
                return;
            }
            this.SetEditIndexRecordValue();
            this.CurrentEditIndex--;
        }

        private void SetEditIndexRecordValue()
        {
            this.EditObject.SetValue(this.EditIndexRecords[this.CurrentEditIndex].Property, this.EditIndexRecords[this.CurrentEditIndex].Value);
            this.UserEditObject.RaiseFocusPropertyChanged(this.EditIndexRecords[this.CurrentEditIndex].Property);
        }
    }
}
#endif