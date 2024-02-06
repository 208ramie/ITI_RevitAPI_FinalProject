using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ITI_RevitAPI_FinalProject.RevitManager;
using ITI_RevitAPI_FinalProject.Utilities;
using ITI_RevitAPI_FinalProject.View;

namespace ITI_RevitAPI_FinalProject.ViewModel
{
    internal class LevelCreatorVM : ViewModelBase<LevelCreatorVM>
    {

        #region Fields
        private string _levelPrefix;
        private string _levelSerial;
        private string _levelName;
        private string _levelDiscipline;
        private ObservableCollection<Level> _documentLevels;
        private Level _selectedLevel;
        private double _levelElevation = 500;
        private bool _isElevationAbsolute;
        private bool _isLevelPinned;
        private double _totalElevation;
        private bool _isViewPlan = false;
        #endregion"

        #region properties
        public bool IsViewPlan
        {
            get { return _isViewPlan; }
            set
            {
                _isViewPlan = value;
                OnPropertyChanged();
            }
        }

        public bool IsLevelPinned
        {
            get => _isLevelPinned;
            set { _isLevelPinned = value; OnPropertyChanged(); }
        }

        public double TotalElevation
        {
            get => _totalElevation; 
            set { _totalElevation = value; OnPropertyChanged(); }
        }

        public bool IsElevationAbsolute
        {
            get => _isElevationAbsolute;
            set
            {
                _isElevationAbsolute = value;
                UpdateTotalElevation();
                OnPropertyChanged();
            }
        }

        public double LevelElevation
        {
            get => _levelElevation;
            set
            {
                _levelElevation = value;
                UpdateTotalElevation();
                OnPropertyChanged();
            }
        }

        public Level SelectedLevel
        {
            get => _selectedLevel;
            set 
            { 
                _selectedLevel = value;
                UpdateTotalElevation();
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Level> DocumentLevels
        {
            get => new ObservableCollection<Level>(RHelper.GetAllLevelsInDocument());
            set 
            { _documentLevels = value; OnPropertyChanged(); }
        }

        public string LevelPrefix
        {
            get => _levelPrefix;
            set { _levelPrefix = value; OnPropertyChanged();} 
        }

        public string LevelSerial
        {
            get => _levelSerial;
            set { _levelSerial = value; OnPropertyChanged(); }
        }

        public string LevelName
        {
            get => _levelName;
            set { _levelName = value; OnPropertyChanged(); }
        }

        public string LevelDiscipline
        {
            get => _levelDiscipline;
            set { _levelDiscipline = value; OnPropertyChanged(); }
        }

        public string CombinedLevelName
        {
            get => LevelPrefix + "-" + LevelSerial + "-" + LevelName + "-" + LevelDiscipline;
        }

        #endregion

        #region Commands
        public RelayCommand CreateLevelCommand { get; set; }
        public RelayCommand OpenSettings { get; set; }
        #endregion

        #region Constructor
        public LevelCreatorVM()
        {
            CreateLevelCommand = new RelayCommand(CreateLevelM);
            UpdateTotalElevation();
            SelectedLevel = RHelper.GetAllLevelsInDocument().FirstOrDefault(); 
            OpenSettings = new RelayCommand(OpenSettingsCommand);
        }
        #endregion


        #region Enable Disable Textbox Props


        private bool _isEnabledPrefix = true;
        private bool _isEnabledSerial = true;
        private bool _isEnabledName = true;
        private bool _isEnabledDiscipline = true;
        public bool IsEnabledPrefix
        {
            get { return _isEnabledPrefix; }
            set { _isEnabledPrefix = value; OnPropertyChanged(); }
        }
        public bool IsEnabledSerial
        {
            get { return _isEnabledSerial; }
            set { _isEnabledSerial = value; OnPropertyChanged(); }
        }
        public bool IsEnabledName
        {
            get { return _isEnabledName; }
            set { _isEnabledName = value; OnPropertyChanged(); }
        }
        public bool IsEnabledDiscipline
        {
            get { return _isEnabledDiscipline; }
            set { _isEnabledDiscipline = value; OnPropertyChanged(); }
        }


        #endregion


        #region OnInstanceCalled

        protected override void onInstanceCalled()
        {
            if (!string.IsNullOrEmpty(SettingsVM.Instance.LevelPrefix))
            {
                LevelPrefix = SettingsVM.Instance.LevelPrefix;
                IsEnabledPrefix = false;
            }
            else
            {
                LevelPrefix = "";
                IsEnabledPrefix = true;
            }
            if (!string.IsNullOrEmpty(SettingsVM.Instance.LevelName))
            {
                LevelName = SettingsVM.Instance.LevelName;
                IsEnabledName = false;
            }
            else
            {
                LevelName = "";
                IsEnabledName = true;
            }
            if (!string.IsNullOrEmpty(SettingsVM.Instance.LevelDiscipline))
            {
                LevelDiscipline = SettingsVM.Instance.LevelDiscipline;
                IsEnabledDiscipline = false;
            }
            else
            {
                LevelDiscipline = "";
                IsEnabledDiscipline = true;
            }
            if (!string.IsNullOrEmpty(SettingsVM.Instance.LevelSerial))
            {
                LevelSerial = SettingsVM.Instance.LevelSerial;
                IsEnabledSerial = false;
            }
            else
            {
                LevelSerial = "";
                IsEnabledSerial = true;
            }
        }

        #endregion


        #region CommandMethods
        private void CreateLevelM(object obj)
        {
            if (IsElevationAbsolute)
                RHelper.CreateLevel(CombinedLevelName, LevelElevation, IsLevelPinned, IsViewPlan);
            else
                RHelper.CreateLevel(SelectedLevel, LevelElevation, CombinedLevelName, IsLevelPinned, IsViewPlan);

            Window.Close();
        }
        private void OpenSettingsCommand(object obj)
        {
            Window.Close();
            new SettingsView().ShowDialog();
        }
        #endregion

        #region Helpers
        private void UpdateTotalElevation()
        {
            if (IsElevationAbsolute)
            {
                TotalElevation = LevelElevation; 
            }
            else if (SelectedLevel == null)
            {
                TotalElevation = 0;
            }
            else
            {
                TotalElevation = RHelper.ToMM(SelectedLevel.Elevation) + LevelElevation;
            }
        }
        #endregion

    }
}
