using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using Autodesk.Revit.UI;
using ITI_RevitAPI_FinalProject.Properties;
using ITI_RevitAPI_FinalProject.ViewModel;

namespace ITI_RevitAPI_FinalProject.Utilities
{
    public class ViewModelBase<T> : INotifyPropertyChanged
        where T : ViewModelBase<T> , new()
    {
        public static Window window;

        #region Close & Minimize & OnWinowMouseDown
        public RelayCommand OnWindowInitializedCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand MinimizeCommand { get; set; }
        public RelayCommand OnWindowMouseDown { get; set; }
        public ViewModelBase()
        {
            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);
            OnWindowMouseDown = new RelayCommand(DragMode);
            OnWindowInitializedCommand = new RelayCommand(OnWindowInitialized);
        }

        private void Minimize(object obj) => window.WindowState = WindowState.Minimized;
        private void Close(object obj) => window.Close();
        private void DragMode(object obj) => window.DragMove();

        #endregion
        #region Notify Prop Changed
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string Name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        #endregion
        #region OnWindowInitialized
        public virtual void OnWindowInitialized(object sender)
        {
            window = (Window)sender;
            //if (SettingsVM.Instance == null) SettingsVM._instance = new SettingsVM(); //if user opened Any other window before settings
            if (SettingsVM.Instance.DarkModeCheck) SwitchToDarkTheme();
            else SwitchToLightTheme();
        }
        #endregion
        #region Singleton
        private static T _instance;
		public static T Instance
        {
			get
            {
                if (_instance == null) _instance = new T();
                return _instance;
			}
		}
        #endregion
        #region Dark Mode
        private bool _darkModeCheck;
        public bool DarkModeCheck
        {
            get => _darkModeCheck;
            set
            {
                _darkModeCheck = value;
                OnPropertyChanged();
                if (_darkModeCheck) SwitchToDarkTheme();
                else SwitchToLightTheme();
            }
        }
        protected void SwitchToDarkTheme()
        {
            string themePath = "pack://application:,,,/ITI_RevitAPI_FinalProject;component/Styles/Dark_Theme.xaml";
            var newTheme = new ResourceDictionary { Source = new Uri(themePath, UriKind.RelativeOrAbsolute) }; 
            window.Resources.Clear();
            window.Resources.MergedDictionaries.Add(newTheme);
        }
        protected void SwitchToLightTheme()
        {
            string themePath = "pack://application:,,,/ITI_RevitAPI_FinalProject;component/Styles/Light_Theme.xaml";
            var newTheme = new ResourceDictionary { Source = new Uri(themePath, UriKind.RelativeOrAbsolute) };
            window.Resources.Clear();
            window.Resources.MergedDictionaries.Add(newTheme);
        }
        #endregion
    }
}