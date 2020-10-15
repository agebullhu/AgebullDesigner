using System;
using System.Collections.Generic;

#if CLIENT
namespace Agebull.EntityModel
{
    /// <summary>
    /// ʹ�õ������Ļ���
    /// </summary>
    public static class WorkContext
    {
        /// <summary>
        /// ͬ��������
        /// </summary>
        public static ISynchronousContext SynchronousContext
        {
            get; set;
        }

        /// <summary>
        /// ��ǰ����ģʽ
        /// </summary>
        [ThreadStatic] internal static WorkModel _workModel;

        /// <summary>
        /// ��ǰ����ģʽ
        /// </summary>
        public static WorkModel WorkModel => _workModel;


        /// <summary>
        /// �Ƿ������޸��¼�
        /// </summary>
        public static bool IsNoChangedNotify => WorkModel >= WorkModel.Show;

        /// <summary>
        /// ��������
        /// </summary>
        public static bool InLoding => WorkModel == WorkModel.Loding;

        /// <summary>
        /// ���ڱ���
        /// </summary>
        public static bool InSaving => WorkModel == WorkModel.Saving;

        /// <summary>
        /// �����޸�
        /// </summary>
        public static bool InRepair => WorkModel == WorkModel.Repair;


        /// <summary>
        /// ������ʾ
        /// </summary>
        public static bool InShow => WorkModel == WorkModel.Show;

        /// <summary>
        /// �������ɴ���
        /// </summary>
        public static bool InCoderGenerating => WorkModel == WorkModel.Coder;

        /// <summary>
        /// ������ʾ��
        /// </summary>
        [ThreadStatic] internal static Dictionary<string, string> codes;

        /// <summary>
        /// ������ʾ��
        /// </summary>
        public static Dictionary<string, string> FileCodes => codes;


        /// <summary>
        /// ������ʾ��
        /// </summary>
        [ThreadStatic] internal static bool? writeToFile;

        /// <summary>
        /// ������ʾ��
        /// </summary>
        public static bool WriteToFile => writeToFile == null || writeToFile.Value;

    }
}
#endif