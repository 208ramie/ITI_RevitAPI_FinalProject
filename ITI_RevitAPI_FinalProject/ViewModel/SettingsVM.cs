using Autodesk.Revit.UI;
using ITI_RevitAPI_FinalProject.Utilities;

namespace ITI_RevitAPI_FinalProject.ViewModel
{
    public class SettingsVM : ViewModelBase<SettingsVM>
    {

        private bool _darkModeCheck;
        public bool DarkModeCheck { 
            get => _darkModeCheck;
            set
            {
                _darkModeCheck = value;
                OnPropertyChanged();
                if(_darkModeCheck) SwitchToDarkTheme();
                else SwitchToLightTheme();
            }
        }

        #region trials
        private string _levelName;

        public string LevelName
        {
            get => _levelName;
            set
            {
                _levelName = value;
                OnPropertyChanged();
            }
        }



        #endregion


        public RelayCommand Run { get; set; }


        public SettingsVM()
        {
            Run = new RelayCommand(RunM);
        }

        public void RunM(object obj)
        {
            TaskDialog.Show("Title", "Body");

            RevitManager.RSettings.LevelName = LevelName; 
        }
    }
}
