using System.Collections;
using System.Linq;

namespace Agebull.EntityModel.Config
{
    public static class ConfigListHelper
    {

        public static NotificationList<TParent, TConfig> ToNotificationList<TParent, TConfig>(this IEnumerable source)
            where TParent : ConfigBase
            where TConfig : ConfigBase, IChildrenConfig
        {
            switch (source)
            {
                case null:
                    return new NotificationList<TParent, TConfig>();
                case NotificationList<TParent, TConfig> observableCollection1:
                    return observableCollection1;
            }

            var observableCollection2 = new NotificationList<TParent, TConfig>();
            foreach (var obj in source.OfType<TConfig>())
                observableCollection2.Add(obj);
            return observableCollection2;
        }
    }
}