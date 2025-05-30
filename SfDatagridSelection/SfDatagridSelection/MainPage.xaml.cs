#if WINDOWS
using Microsoft.UI.Xaml;
#endif
using SfDataGridSelection;

namespace SfDatagridSelection
{
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

}
