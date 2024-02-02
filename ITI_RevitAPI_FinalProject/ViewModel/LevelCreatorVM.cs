using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Autodesk.Revit.UI;
using ITI_RevitAPI_FinalProject.RevitManager;
using ITI_RevitAPI_FinalProject.Utilities;

namespace ITI_RevitAPI_FinalProject.ViewModel
{
    internal class LevelCreatorVM : ViewModelBase<LevelCreatorVM>
    {

        #region properties
        private string _levelPrefix = "";
        private string _levelSerial = "";
        private string _levelName = "";
        private string _levelDiscipline = "";

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

        #region Constructor
        public RelayCommand CreateLevelCommand { get; set; }
        public LevelCreatorVM() => CreateLevelCommand = new RelayCommand(CreateLevelM);


        #endregion



        private void CreateLevelM(object obj)
        {
            TaskDialog.Show("Working", "Creating a level"); 
            RHelper.CreateLevel(CombinedLevelName, 5000, false);
            Window.Close();
            
        }

    }
}
