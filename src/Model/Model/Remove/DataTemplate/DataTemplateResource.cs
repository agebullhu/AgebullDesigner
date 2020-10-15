using System.Collections.Generic;
using System.Windows;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     数据模板资源
    /// </summary>
    public static class DataTemplateResource
    {
        public static List<ResourceDictionary> Resources { get; } = new List<ResourceDictionary>();

        public static void RegistResource(ResourceDictionary resource)
        {
            Resources.Add(resource);
        }
    }
}