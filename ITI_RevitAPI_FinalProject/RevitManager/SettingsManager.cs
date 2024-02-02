using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ITI_RevitAPI_FinalProject.View;

namespace ITI_RevitAPI_FinalProject.RevitManager
{
    [Transaction(TransactionMode.Manual)]
    public class SettingsManager : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                SettingsView settingsView = new SettingsView();
                settingsView.ShowDialog();

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                RHelper.TDError(e);
                return Result.Failed;
            }
        }
    }
}
