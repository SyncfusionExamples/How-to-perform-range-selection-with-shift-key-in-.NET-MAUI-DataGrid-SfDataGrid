using Syncfusion.Maui.Data;
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.DataGrid.Helper;
using Syncfusion.Maui.GridCommon.ScrollAxis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfDataGridSelection
{

	public class CustomRowSelectionController : DataGridRowSelectionController
	{
		private SfDataGrid grid;
		public bool _isShiftPressed { get; set; }

		public CustomRowSelectionController(SfDataGrid dataGrid) : base(dataGrid)
		{
			this.grid = dataGrid;
		}

		public override void HandlePointerOperation(RowColumnIndex rowColumnIndex)
		{
			if (_isShiftPressed)
			{
				if (grid.SelectedRows.Count > 0)
				{
					var selectedRow = grid.SelectedRows[0] as OrderInfo;
					var selectCollection = new ObservableCollection<object>();
					var startIndex = (grid.ItemsSource as ObservableCollection<OrderInfo>)!.IndexOf(selectedRow);
					var endIndex = grid.ResolveToRecordIndex(rowColumnIndex.RowIndex);
					for (int i = startIndex; i <= endIndex; i++)
					{
						selectCollection.Add(grid.View!.Records[i].Data);
					}
					grid.SelectedRows = selectCollection;
					return;
				}

			}
			base.HandlePointerOperation(rowColumnIndex);
		}
	}
}
