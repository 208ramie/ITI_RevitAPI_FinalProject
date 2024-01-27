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
    internal class ViewCreation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            StringBuilder sb = new StringBuilder();

            List<ViewFamilyType> viewFamilyTypes = new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
                .Cast<ViewFamilyType>().ToList();

            sb.AppendLine(viewFamilyTypes.Count.ToString()); 

            foreach (ViewFamilyType item in viewFamilyTypes)
            {
                sb.AppendLine($"Type name{item.Name}:Family name{item.FamilyName}"); 
            }

            TaskDialog.Show("Information", sb.ToString()); 
            return Result.Succeeded; 
        }
    }
}
