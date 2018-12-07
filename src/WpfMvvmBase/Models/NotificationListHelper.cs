using System.Collections;

namespace Agebull.EntityModel
{
    public static class NotificationListHelper
    {

        public static NotificationList<T> ToNotificationList<T>(this IEnumerable source)
        {
            switch (source)
            {
                case null:
                    return new NotificationList<T>();
                case NotificationList<T> observableCollection1:
                    return observableCollection1;
            }

            var observableCollection2 = new NotificationList<T>();
            foreach (T obj in source)
                observableCollection2.Add(obj);
            return observableCollection2;
        }
    }
}