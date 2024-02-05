using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using ITI_RevitAPI_FinalProject.RevitManager;
using ITI_RevitAPI_FinalProject.Utilities;

namespace ITI_RevitAPI_FinalProject.ViewModel
{
    internal class ViewPlanCreatorVM : ViewModelBase<ViewPlanCreatorVM>
    {
        #region Fields
        private string _viewPrefix;
        private string _viewSerial;
        private string _viewName;
        private string _viewDiscipline;
        private ObservableCollection<Level> _documentLevels;
        private Level _selectedLevel;
        #endregion"

        #region properties
        public ObservableCollection<Level> DocumentLevels
        {
            get => new ObservableCollection<Level>(RHelper.GetAllLevelsInDocument());
            set
            { _documentLevels = value; OnPropertyChanged(); }
        }
        public string ViewPrefix
        {
            get => _viewPrefix;
            set { _viewPrefix = value; OnPropertyChanged(); }
        }
        public string ViewSerial
        {
            get => _viewSerial;
            set { _viewSerial = value; OnPropertyChanged(); }
        }
        public string ViewName
        {
            get => _viewName;
            set { _viewName = value; OnPropertyChanged(); }
        }
        public string ViewDiscipline
        {
            get => _viewDiscipline;
            set { _viewDiscipline = value; OnPropertyChanged(); }
        }
        public string CombinedViewPlanName 
            => ViewPrefix + "-" + ViewSerial + "-" + ViewName + "-" + ViewDiscipline;
        public Level SelectedLevel
        {
            get => _selectedLevel;
            set
            {
                _selectedLevel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public RelayCommand CreateViewPlanCommand { get; set; }
        #endregion

        #region Constructor
        public ViewPlanCreatorVM()
        {
            CreateViewPlanCommand = new RelayCommand(CreateViewPlanM);
            SelectedLevel = RHelper.GetAllLevelsInDocument().FirstOrDefault();
        }
        #endregion

        #region CommandMethods
        private void CreateViewPlanM(object obj)
        {
            RHelper.CreateViewPlan(SelectedLevel, CombinedViewPlanName);
            Window.Close();
        }
        #endregion






    }
}
