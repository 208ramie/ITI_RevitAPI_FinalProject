using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using ITI_RevitAPI_FinalProject.ViewModel;

namespace ITI_RevitAPI_FinalProject.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();
            DataContext = SettingsVM.Instance;
        }
        private void OnInitializeCompleted(object sender, DependencyPropertyChangedEventArgs e) => SettingsVM.Instance.OnWindowInitialized(sender);

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}
