using System.IO;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// �汾���ƽڵ�
    /// </summary>
    public class VersionControlItem
    {
        #region ����
        /// <summary>
        /// ��ǰ��������ļ�
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// ��ǰ�������Ŀ¼
        /// </summary>
        public string Directory => string.IsNullOrEmpty(FilePath) ? null : Path.GetDirectoryName(FilePath);
        /// <summary>
        /// ����
        /// </summary>
        private static VersionControlItem _current;
        /// <summary>
        /// ����
        /// </summary>
        public static VersionControlItem Current => _current ?? (_current = new VersionControlItem());
        /// <summary>
        /// ����
        /// </summary>
        private VersionControlItem()
        {
        }
        /// <summary>
        /// ����Դ����
        /// </summary>
        public void UpdateSourseCode()
        {
            TfsCheckOut();
        }
        /// <summary>
        /// �ύԴ����
        /// </summary>
        public void CommitChanged()
        {
            TfsCheckIn();
        }
        #endregion

        #region TFSԴ�������

        public void TfsUpdate()
        {
            //using (TfsSourceCodeHelper helper = new TfsSourceCodeHelper(this.FilePath))
            //{
            //    helper.Update();
            //}
        }

        public void TfsCheckOut()
        {
            //using (TfsSourceCodeHelper helper = new TfsSourceCodeHelper(this.FilePath))
            //{
            //    helper.CheckOut();
            //}
        }

        public void TfsCheckIn()
        {
            //using (TfsSourceCodeHelper helper = new TfsSourceCodeHelper(this.FilePath))
            //{
            //    helper.CheckIn();
            //}
        }

        #endregion
    }
}