using System.Windows;
using System.Windows.Input;
using ITI_RevitAPI_FinalProject.ViewModel;

namespace ITI_RevitAPI_FinalProject.View
{
    /// <summary>
    /// Interaction logic for FromExcelView.xaml
    /// </summary>
    public partial class FromExcelView : Window
    {
        public FromExcelView()
        {
            DataContext = FromExcelVM.Instance;
            FromExcelVM.Window = this;
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MinimizeButton(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
