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

        #region Levels Props

        private string _levelPrefix;
        private string _levelSerial;
        private string _levelName;
        private string _levelDiscipline;

        public string LevelPrefix
        {
            get { return _levelPrefix; }
            set { _levelPrefix = value; OnPropertyChanged(); }
        }
        public string LevelSerial
        {
            get { return _levelSerial; }
            set { _levelSerial = value; OnPropertyChanged(); }
        }
        public string LevelName
        {
            get { return _levelName; }
            set { _levelName = value; OnPropertyChanged(); }
        }
        public string LevelDiscipline
        {
            get { return _levelDiscipline; }
            set { _levelDiscipline = value; OnPropertyChanged(); }
        }

        #endregion


        public RelayCommand Run { get; set; }
        public SettingsVM() => Run = new RelayCommand(RunM);
        public void RunM(object obj) => window.Close();
    }
}
