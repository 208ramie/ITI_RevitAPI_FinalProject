using System;
using ITI_RevitAPI_FinalProject.RevitManager;
using ITI_RevitAPI_FinalProject.Utilities;

namespace ITI_RevitAPI_FinalProject.ViewModel
{
    public class FromExcelVM : ViewModelBase<FromExcelVM>
    {
        private string _browseTextBox;
        public string BrowseTextBox
        {
            get => _browseTextBox;
            set
            {
                _browseTextBox = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand BrowseCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }

        // Constructor
        public FromExcelVM()
        {
            BrowseCommand = new RelayCommand(BrowseM);
            CreateCommand = new RelayCommand(CreateM);
        }

        private void CreateM(object obj)
        {
            ExcelHelper.ImportFromExcel(BrowseTextBox);
            Window.Close();
        }

        public void BrowseM(object obj)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".xlsx",
                Filter = "Excel Files (*.xlsx, *.xls)|*.xlsx;*.xls|All Files (*.*)|*.*"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
                BrowseTextBox = dlg.FileName;
        }
    }
}
