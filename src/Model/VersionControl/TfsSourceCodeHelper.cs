using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Agebull.Common.Base;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// TFSԴ������ư�����
    /// </summary>
    public class TfsSourceCodeHelper : ScopeBase
    {
        private string _codeFile;
        /// <param name="file">�ļ���</param>
        public TfsSourceCodeHelper(string file)
        {
            _codeFile = file;

            //string path = Path.GetDirectoryName(file);

            ////���÷������ļ���
            //if (path == null)
            //    return;
            //string serverFolder = "$/GBS_Trade_1202/" + path.ToLower().Replace(@"c:\work\", "").Replace('\\', '/');
            ////���ñ���ӳ���ļ�
            //string localFolder = path;



            //_projectCollection = new TfsTeamProjectCollection(new Uri("http://192.168.1.65:8080/tfs/"));
            ////���ð汾����Server
            
            //var versionControl = _projectCollection.GetService<VersionControlServer>();

            ////���ù����ռ�����
            //var workspaceName = Environment.MachineName;
            
            //_workspace = versionControl.GetWorkspace(workspaceName, versionControl.AuthorizedUser);
            // ���������ռ�ı���ӳ���ַ
            //workspace.Map(serverFolder, localFolder);
        }

        private TfsTeamProjectCollection _projectCollection;
        private readonly Workspace _workspace;
        /// <summary>
        /// �����ļ�
        /// </summary>
        public void Update()
        {
            _workspace.PendEdit(_codeFile);
            _workspace.PendAdd(_codeFile);
            var pendingAdds = new List<PendingChange>(_workspace.GetPendingChanges());
            var array = pendingAdds.ToArray();
            _workspace.CheckIn(array, "��������ɴ�����Զ��ύ");
        }

        /// <summary>
        /// ǩ������
        /// </summary>
        public void CheckOut()
        {
            //_workspace.PendEdit(_codeFile);
        }

        /// <summary>
        /// ǩ�����
        /// </summary>
        public void CheckIn()
        {
            int changesetForAdd;
            //������ļ��Ŷӵȴ�Ǩ��TFS����
            _workspace.PendAdd(_codeFile);

            //  �����ȴ���ӵ��ļ����
            var pendingAdds = new List<PendingChange>(_workspace.GetPendingChanges(_codeFile));
            var array = pendingAdds.ToArray();
            if (array.Length <= 0 ||
                !array.Any(p => string.Equals(p.FileName, _codeFile, StringComparison.InvariantCultureIgnoreCase)))
            {
                changesetForAdd = _workspace.Undo(_codeFile);
                Debug.WriteLine(changesetForAdd);
                return;
            }
            var a2 =
                array.Where(p => string.Equals(p.FileName, _codeFile, StringComparison.InvariantCultureIgnoreCase))
                    .ToArray();
            // ��������CheckInϵͳ��
            changesetForAdd = _workspace.CheckIn(a2, "������Զ��ύ");
            Debug.WriteLine(changesetForAdd);
        }

        #region Overrides of ScopeBase

        protected override void OnDispose()
        {
            _projectCollection?.Dispose();
            _projectCollection = null;
        }

        #endregion
    }
}