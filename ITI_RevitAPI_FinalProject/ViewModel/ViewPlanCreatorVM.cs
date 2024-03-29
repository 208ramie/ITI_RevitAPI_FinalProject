﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using ITI_RevitAPI_FinalProject.RevitManager;
using ITI_RevitAPI_FinalProject.Utilities;
using ITI_RevitAPI_FinalProject.View;

namespace ITI_RevitAPI_FinalProject.ViewModel
{
    public class ViewPlanCreatorVM : ViewModelBase<ViewPlanCreatorVM>
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
        #region Commands
        public RelayCommand CreateViewPlanCommand { get; set; }
        public RelayCommand OpenSettings { get; set; }
        #endregion
        #region ctor
        public ViewPlanCreatorVM()
        {
            CreateViewPlanCommand = new RelayCommand(CreateViewPlanM);
            SelectedLevel = RHelper.GetAllLevelsInDocument().FirstOrDefault();
            OpenSettings = new RelayCommand(OpenSettingsCommand);
        }

        
        #endregion
        #region OnInstanceCalled

        public override void OnWindowInitialized(object obj)
        {

            base.OnWindowInitialized(obj);
            if (!string.IsNullOrEmpty(SettingsVM.Instance.PlanPrefix))
            {
                ViewPrefix = SettingsVM.Instance.PlanPrefix;
                IsEnabledPrefix = false;
            }
            else
            {
                ViewPrefix = "";
                IsEnabledPrefix = true;
            }
            if (!string.IsNullOrEmpty(SettingsVM.Instance.PlanName))
            {
                ViewName = SettingsVM.Instance.PlanName;
                IsEnabledName = false;
            }
            else
            {
                ViewName = "";
                IsEnabledName = true;
            }
            if (!string.IsNullOrEmpty(SettingsVM.Instance.PlanDiscipline))
            {
                ViewDiscipline = SettingsVM.Instance.PlanDiscipline;
                IsEnabledDiscipline = false;
            }
            else
            {
                ViewDiscipline = "";
                IsEnabledDiscipline = true;
            }
            if (!string.IsNullOrEmpty(SettingsVM.Instance.PlanSerial))
            {
                ViewSerial = SettingsVM.Instance.PlanSerial;
                IsEnabledSerial = false;
            }
            else
            {
                ViewSerial = "";
                IsEnabledSerial = true;
            }
        }

        #endregion
        #region CommandMethods
        private void CreateViewPlanM(object obj)
        {
            RHelper.CreateViewPlan(SelectedLevel, CombinedViewPlanName);
            window.Close();
        }
        private void OpenSettingsCommand(object obj)
        {
            window.Close();
            new SettingsView().ShowDialog();
        }
        #endregion
    }
}
