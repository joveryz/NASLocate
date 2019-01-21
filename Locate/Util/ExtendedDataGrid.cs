using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NASLocate.Util
{
    public class ExtendedDataGrid : DataGrid
    {
        public ExtendedDataGrid()
        {
            LoadingRow += (sender, args) => RefreshRowNumber(args.Row);
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            RefreshRowNumbers();
        }
        private void RefreshRowNumbers()
        {
            for (int i = 0; i < Items.Count; i++)
                RefreshRowNumber((DataGridRow)ItemContainerGenerator.ContainerFromIndex(i));
        }

        private void RefreshRowNumber(DataGridRow row)
        {
            if (row != null)
                row.Header = ((row.GetIndex()) + 1).ToString();
        }
    }
}
