using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using ITI_RevitAPI_FinalProject.RevitManager;
using ITI_RevitAPI_FinalProject.Utilities;
using ITI_RevitAPI_FinalProject.View;

namespace ITI_RevitAPI_FinalProject.ViewModel
{
    public class WorksetCreatorVM : ViewModelBase<WorksetCreatorVM>
    {
		private string _discipline;
		private string _name;

		public string Discipline
		{
			get => _discipline;
            set { _discipline = value; OnPropertyChanged(); }
		}

		public string Name
		{
			get => _name;
			set { _name = value; OnPropertyChanged(); }
		}


        public string WorksetFullName => Discipline + "-" + Name;

        public RelayCommand CreateCommand { get; set; }

        public WorksetCreatorVM()
            => CreateCommand = new RelayCommand(CreateM);

        private void CreateM(object obj)
        {
            RHelper.CreateWorkset(WorksetFullName);
            Window.Close();
        }
    }
}
