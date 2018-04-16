using Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.BLL.category.viewmodels
{
    public class CategoryView
    {
        public string text { get; set; }
        public int categoryid { get; set; }
        public int? parentid { get; set; }
        public dynamic nodes { get; set; }//子菜单
        public int? sortid { get; set; }
    }
    public class CategoryDistinct<T> : IEqualityComparer<T>
        where T : CategoryView
    {
        public bool Equals(T x, T y)
        {
            return x.categoryid == y.categoryid;
        }
        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }

    public static class HandleMenu<T>
        where T : CategoryView
    {
        public static List<T> SubMenu(List<T> menuItem, int topid)
        {
            var handleSubMenu = Combitator.Fix<List<T>, int, List<T>>(f => (m, t) =>
            {
                var lm = new List<T>();
                foreach (var item in m)
                {
                    if (item.parentid == t)
                    {
                        var hsm = f(m, item.categoryid);
                        if (hsm.Count > 0)
                            item.nodes = hsm;
                        else
                            item.nodes = "";
                        lm.Add(item);
                    }
                }
                return lm;
            });
            return handleSubMenu(menuItem, topid).OrderBy(o => o.sortid).ToList();
        }
    }
}
