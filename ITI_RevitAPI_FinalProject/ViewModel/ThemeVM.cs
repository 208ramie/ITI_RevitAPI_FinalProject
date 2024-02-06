using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using ITI_RevitAPI_FinalProject.Utilities;

namespace ITI_RevitAPI_FinalProject.ViewModel
{
    public class ThemeVM : ViewModelBase<ThemeVM>
    {
        private bool _darkModeCheck;
        public bool DarkModeCheck
        {
            get => _darkModeCheck;
            set
            {
                _darkModeCheck = value;
                OnPropertyChanged();
                if (_darkModeCheck) SwitchToDarkTheme(true);
                else SwitchToLightTheme(true);
            }
        }
    }
}
