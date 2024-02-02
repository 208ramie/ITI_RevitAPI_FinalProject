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
    public class LevelCreation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RHelper.UIDoc = commandData.Application.ActiveUIDocument;
            new LevelCreatorView().ShowDialog(); 
            //RHelper.CreateLevel("LevelRamie", RHelper.ToIU(5000), false);
            return Result.Succeeded;
        }
    }
}
