using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ITI_RevitAPI_FinalProject.View;

namespace ITI_RevitAPI_FinalProject.Revit
{
    [Transaction(TransactionMode.Manual)]
    public class OpenSettings : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            (new SettingsView()).Show();
            return Result.Succeeded;
        }
    }
}
