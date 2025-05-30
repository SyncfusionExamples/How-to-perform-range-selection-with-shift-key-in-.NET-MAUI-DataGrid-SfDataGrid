# How to perform range selection with shift key in .NET MAUI DataGrid SfDataGrid
This article shows how to implement **Range Selection** with the **Shift key** in Syncfusion [.NET MAUI DataGrid](https://help.syncfusion.com/maui/datagrid/overview) (`SfDataGrid`). It demonstrates how a user can select rows continuously using the Shift key and the Arrow Up key. Similarly, deselection of rows continuously can also be done using the Shift key and the Arrow Down key.

## Xaml
```
<ContentPage.BindingContext>
    <local:OrderInfoRepository x:Name="viewModel" />
</ContentPage.BindingContext>

<ContentPage.Content>
    <syncfusion:SfDataGrid x:Name="dataGrid"
                       SelectionMode="Multiple"
                       ItemsSource="{Binding OrderInfoCollection}">
    </syncfusion:SfDataGrid>
</ContentPage.Content>
```

## Xaml.cs
```
public partial class MainPage : ContentPage
{
    readonly CustomRowSelectionController customRowSelectionController;

    public MainPage()
    {
        InitializeComponent();
        customRowSelectionController = new CustomRowSelectionController(dataGrid);
        dataGrid.SelectionController = customRowSelectionController;
#if WINDOWS
        dataGrid.HandlerChanged += DataGrid_HandlerChanged;
#endif
    }
#if WINDOWS
    private void DataGrid_HandlerChanged(object? sender, EventArgs e)
    {
        if (dataGrid?.Handler?.PlatformView is FrameworkElement nativeElement)
        {
            nativeElement.KeyUp += DataGrid_KeyUpEvent;
            nativeElement.KeyDown += DataGrid_KeyDownEvent;
        }
    }

    private void DataGrid_KeyUpEvent(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Shift)
        {
            customRowSelectionController._isShiftPressed = false;
        }
    }

    private void DataGrid_KeyDownEvent(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Shift)
        {
            customRowSelectionController._isShiftPressed = true;
        }
    }
#endif
}
```

## CustomRowSelectionController.cs
```
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
```

### ScreenShot

Here is the expected output when executing the sample:

<img src="https://support.syncfusion.com/kb/agent/attachment/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjQwOTc0Iiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.je6YEmZlMfStikxEWG73znnJzUvvE8_G1T-RQfMlTUg" width = 404 height = 396/>

[View sample in GitHub](https://github.com/SyncfusionExamples/How-to-perform-range-selection-with-shift-key-in-.NET-MAUI-DataGrid-SfDataGrid)

 Take a moment to explore this [documentation](https://help.syncfusion.com/maui/datagrid/overview), where you can find more information about Syncfusion .NET MAUI DataGrid (SfDataGrid) with code examples. Please refer to this [link](https://www.syncfusion.com/maui-controls/maui-datagrid) to learn about the essential features of Syncfusion .NET MAUI DataGrid (SfDataGrid).

### Conclusion
I hope you enjoyed learning about How to implement select all checkbox column in SfDataGrid.

You can refer to our [.NET MAUI DataGrid’s feature tour](https://www.syncfusion.com/maui-controls/maui-datagrid) page to learn about its other groundbreaking feature representations. You can also explore our [.NET MAUI DataGrid Documentation](https://help.syncfusion.com/maui/datagrid/getting-started) to understand how to present and manipulate data. For current customers, you can check out our .NET MAUI components on the [License and Downloads](https://www.syncfusion.com/sales/teamlicense) page. If you are new to Syncfusion, you can try our 30-day [free trial](https://www.syncfusion.com/downloads/maui) to explore our .NET MAUI DataGrid and other .NET MAUI components.

If you have any queries or require clarifications, please let us know in the comments below. You can also contact us through our [support forums](https://www.syncfusion.com/forums),[Direct-Trac](https://support.syncfusion.com/create) or [feedback portal](https://www.syncfusion.com/feedback/maui?control=sfdatagrid), or the feedback portal. We are always happy to assist you!