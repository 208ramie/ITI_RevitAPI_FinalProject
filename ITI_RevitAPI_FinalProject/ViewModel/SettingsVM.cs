using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ITI_RevitAPI_FinalProject.Utilities;

namespace ITI_RevitAPI_FinalProject.ViewModel
{
    public class SettingsVM : ViewModelBase<SettingsVM>
    {

        #region ViewPlan Props

        private string _planPrefix;
        private string _planSerial;
        private string _planName;
        private string _planDiscipline;

        public string PlanPrefix
        {
            get { return _planPrefix; }
            set { _planPrefix = value; OnPropertyChanged(); }
        }
        public string PlanSerial
        {
            get { return _planSerial; }
            set { _planSerial = value; OnPropertyChanged(); }
        }
        public string PlanName
        {
            get { return _planName; }
            set { _planName = value; OnPropertyChanged(); }
        }
        public string PlanDiscipline
        {
            get { return _planDiscipline; }
            set { _planDiscipline = value; OnPropertyChanged(); }
        }

        #endregion



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
