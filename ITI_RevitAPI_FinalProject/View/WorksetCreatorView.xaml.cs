using System.Windows;
using System.Windows.Input;
using ITI_RevitAPI_FinalProject.ViewModel;

namespace ITI_RevitAPI_FinalProject.View
{
    /// <summary>
    /// Interaction logic for FromExcelView.xaml
    /// </summary>
    public partial class WorksetCreatorView : Window
    {
        public WorksetCreatorView()
        {
            InitializeComponent();
            DataContext = WorksetCreatorVM.Instance;
        }
        private void OnInitializeCompleted(object sender, DependencyPropertyChangedEventArgs e) => WorksetCreatorVM.Instance.OnWindowInitialized(sender);
        private void WorksetCreatorView_OnMouseDown(object sender, MouseButtonEventArgs e) => DragMove();
    }
}
