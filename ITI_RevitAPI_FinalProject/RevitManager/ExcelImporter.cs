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
    public class ExcelImporter : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RHelper.UIDoc = commandData.Application.ActiveUIDocument;
            try
            {
                FromExcelView excelWindow = new FromExcelView();
                excelWindow.ShowDialog();

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
