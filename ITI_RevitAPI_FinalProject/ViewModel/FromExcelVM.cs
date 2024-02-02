using System;
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

        public RelayCommand BTN_Browse { get; set; }
        public FromExcelVM() => BTN_Browse = new RelayCommand(BrowseFile);

        public void BrowseFile(object obj)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".xlsx",
                Filter = "Excel Files (*.xlsx, *.xls)|*.xlsx;*.xls|All Files (*.*)|*.*"
            };
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                BrowseTextBox = filename;
            }
        }
    }
}
