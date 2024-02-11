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
            InitializeComponent();
            DataContext = FromExcelVM.Instance;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void OnInitializeCompleted(object sender, DependencyPropertyChangedEventArgs e) => FromExcelVM.Instance.OnWindowInitialized(sender);
    }
}
