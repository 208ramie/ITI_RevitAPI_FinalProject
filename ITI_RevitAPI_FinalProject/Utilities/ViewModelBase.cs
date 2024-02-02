using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Autodesk.Revit.UI;

namespace ITI_RevitAPI_FinalProject.Utilities
{
    public class ViewModelBase<T> : INotifyPropertyChanged
        where T : ViewModelBase<T> , new()
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string Name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
		
        private static T _instance;
		public static T Instance
		{
			get
            {
				if(_instance == null) _instance = new T();
				return _instance;
			}
		}

        public static Window Window;
        protected void SwitchToDarkTheme()
        {
            // Replace the current theme with the dark theme
            SwitchTheme("E:\\ITI\\Revit api\\RevitAPI_Proj\\Styles\\Dark_Theme.xaml");
        }

        protected void SwitchToLightTheme()
        {
            // Replace the current theme with the light theme
            SwitchTheme("E:\\ITI\\Revit api\\RevitAPI_Proj\\Styles\\Light_Theme.xaml");
        }

        private void SwitchTheme(string themePath)
        {
            try
            {
                var mergedDictionaries = Window.Resources.MergedDictionaries;
                var newTheme = new ResourceDictionary { Source = new Uri(themePath, UriKind.Absolute) };
                switchDictInsideMergedDict(mergedDictionaries, newTheme);


                foreach (ResourceDictionary resourceDictionary in mergedDictionaries)
                {
                    RecursiveSetDictionaryInsideAnother(resourceDictionary, newTheme);
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
        }
        private void switchDictInsideMergedDict(Collection<ResourceDictionary> mergedDictionaries, ResourceDictionary newTheme)
        {
            var existingTheme = mergedDictionaries.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.EndsWith("_Theme.xaml"));
            if (existingTheme != null)
            {
                mergedDictionaries.Remove(existingTheme);
                mergedDictionaries.Add(newTheme);
            }
        }
        private void RecursiveSetDictionaryInsideAnother(ResourceDictionary Dictionary, ResourceDictionary newTheme)
        {
            switchDictInsideMergedDict(Dictionary.MergedDictionaries, newTheme);
            foreach (ResourceDictionary dictionary in Dictionary.MergedDictionaries)
            {
                RecursiveSetDictionaryInsideAnother(dictionary, newTheme);
            }
        }

        
    }
}
