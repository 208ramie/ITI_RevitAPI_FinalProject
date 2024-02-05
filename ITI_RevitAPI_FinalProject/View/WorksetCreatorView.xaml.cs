using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ITI_RevitAPI_FinalProject.ViewModel;

namespace ITI_RevitAPI_FinalProject.View
{
    /// <summary>
    /// Interaction logic for WorksetCreatorView.xaml
    /// </summary>
    public partial class WorksetCreatorView : Window
    {
        public WorksetCreatorView()
        {
            InitializeComponent();
            this.DataContext = WorksetCreatorVM.Instance;
            WorksetCreatorVM.Window = this; 
        }

        private void WorksetCreatorView_OnMouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Minimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized; 
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
