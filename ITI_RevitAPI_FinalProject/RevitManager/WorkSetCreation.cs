using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ITI_RevitAPI_FinalProject.RevitManager
{
    [Transaction(TransactionMode.Manual)]
    public class WorkSetCreation : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument; 
            Document doc = uidoc.Document;
            RHelper.CreateWorkset(doc, "RamieWorkset"); 
            return Result.Succeeded;
        }
    }
}
