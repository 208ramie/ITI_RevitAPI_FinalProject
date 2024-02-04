using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ITI_RevitAPI_FinalProject.RevitManager
{
    [Transaction(TransactionMode.Manual)]
    internal class GridsRenamer : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RHelper.UIDoc = commandData.Application.ActiveUIDocument;
            try
            {
                RHelper.RenameGrids();
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
