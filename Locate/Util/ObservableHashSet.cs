using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASLocate.Util
{
    public class ObservableHashSet<T> : ObservableCollection<T>
    {
        protected override void InsertItem(int index, T item)
        {
            if (Contains(item))
            {
                return;
            }
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, T item)
        {
            int i = IndexOf(item);
            if (i >= 0 && i != index)
            {
                return;
            }
            base.SetItem(index, item);
        }

    }
}
