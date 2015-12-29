using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DrinkApp.Model {
    public class GroupInfoList : List<object> {
        public object Key { get; set; }

        public static ObservableCollection<GroupInfoList> GetListGrouped(ObservableCollection<dynamic> list) {
            ObservableCollection<GroupInfoList> groups = new ObservableCollection<GroupInfoList>();

            var query = from item in list
                        group item by item.LastName[0] into g
                        orderby g.Key
                        select new { GroupName = g.Key, Items = g };

            foreach (var g in query) {
                GroupInfoList info = new GroupInfoList();
                info.Key = g.GroupName;
                foreach (var item in g.Items) {
                    info.Add(item);
                }
                groups.Add(info);
            }

            return groups;
        }
    }
}
