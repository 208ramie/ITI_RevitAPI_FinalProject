﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Autodesk.Revit.UI;
using ITI_RevitAPI_FinalProject.ViewModel;
using TextBox = System.Windows.Controls.TextBox;

namespace ITI_RevitAPI_FinalProject.View
{
    /// <summary>
    /// Interaction logic for LevelCreatorView.xaml
    /// </summary>
    public partial class LevelCreatorView : Window
    {
        public LevelCreatorView()
        {
            InitializeComponent();
            DataContext = LevelCreatorVM.Instance;
            LevelPickerComboBox.SelectedIndex = 0; 
        }
        private void OnInitializeCompleted(object sender, DependencyPropertyChangedEventArgs e) => LevelCreatorVM.Instance.OnWindowInitialized(sender);

        private void LevelCreatorView_OnMouseDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void AbsoluteElevationCheckBox_OnChecked(object sender, RoutedEventArgs e) => LevelPickerComboBox.IsEnabled = false;
        private void AbsoluteElevationCheckBox_OnUnchecked(object sender, RoutedEventArgs e) => LevelPickerComboBox.IsEnabled = true;
        private void TextBox_OnGotFocus(object sender, RoutedEventArgs e) => (sender as TextBox)?.SelectAll();
        private void IsPinnedTextBlock_OnMouseDown(object sender, MouseButtonEventArgs e) => IsPinnedCheckBox.IsChecked = !IsPinnedCheckBox.IsChecked;
        private void AbsoluteElevationTextBlock_OnMouseDown(object sender, MouseButtonEventArgs e) => AbsoluteElevationCheckBox.IsChecked = !AbsoluteElevationCheckBox.IsChecked;
        private void IsViewPlanTextBlock_OnMouseDown(object sender, MouseButtonEventArgs e) => IsViewPlanCheckBox.IsChecked = !IsViewPlanCheckBox.IsChecked;
    }
}
