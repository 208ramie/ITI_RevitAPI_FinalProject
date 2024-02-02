﻿using System;
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
    public class ViewCreation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RHelper.UIDoc = commandData.Application.ActiveUIDocument;

            RHelper.CreateViewPlan(null);

            return Result.Succeeded;
        }





    }
}
