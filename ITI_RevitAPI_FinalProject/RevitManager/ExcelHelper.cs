using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using static ITI_RevitAPI_FinalProject.RevitManager.RHelper; 

namespace ITI_RevitAPI_FinalProject.RevitManager
{
    public class ExcelHelper
    {
        public static void ImportFromExcel(string filePath)
        {
            try
            {
                FileInfo fi = new FileInfo(filePath);
                StringBuilder sb = new StringBuilder();

                var viewFamily = new FilteredElementCollector(Doc)
                   .OfClass(typeof(ViewFamilyType)).Cast<ViewFamilyType>()
                   .FirstOrDefault(f => f.ViewFamily == ViewFamily.FloorPlan);

                var titleBlock = new FilteredElementCollector(Doc)
                    .OfCategory(BuiltInCategory.OST_TitleBlocks)
                    .WhereElementIsElementType().FirstOrDefault();

                using (Transaction trans = new Transaction(Doc, "Create Levels From Excel"))
                {
                    trans.Start();
                    using (ExcelPackage excelPackage = new ExcelPackage(fi))
                    {
                        ExcelWorksheet sheet_01 = excelPackage.Workbook.Worksheets[1];

                        int startRow = sheet_01.Dimension.Start.Row;
                        int endRow = sheet_01.Dimension.End.Row;

                        int startCol = sheet_01.Dimension.Start.Column;
                        int endCol = sheet_01.Dimension.End.Column;

                        ExcelWorksheet sheet_02 = excelPackage.Workbook.Worksheets[2];

                        int startRow02 = sheet_02.Dimension.Start.Row;
                        int endRow02 = sheet_02.Dimension.End.Row;

                        int startCol02 = sheet_02.Dimension.Start.Column;
                        int endCol02 = sheet_02.Dimension.End.Column;


                        for (int i = 2; i <= endRow02; i++)
                        {
                            if (sheet_02.Cells[i, startCol02].Value is null) break;
                            //for (int j = 1 ; j <= endCol; j++)
                            //{

                            sb.Append(sheet_02.Cells[i, 1].Value.ToString() + "_");
                            //}
                        }

                        string levelName = sb.ToString().TrimEnd('_');

                        for (int i = startRow + 1; true; i++)
                        {
                            // Break when you hit a null
                            if (sheet_01.Cells[i, startCol].Value is null) break;

                            double elevation = double.Parse(sheet_01.Cells[i, startCol].Value.ToString());

                            // Create the level and rename it
                            Level level = Level.Create(Doc, RHelper.ToIU(elevation));
                            level.Name = sheet_01.Cells[i, startCol + 1].Value.ToString() + levelName;


                            // Create the view plan of the level
                            ViewPlan vPlan_01 = ViewPlan.Create(Doc, viewFamily.Id, level.Id);
                            vPlan_01.Name = sheet_01.Cells[i, startCol + 1].Value.ToString() + levelName;
                            vPlan_01.Scale = 200;

                            // Create the view sheet 
                            ViewSheet viewSheet = ViewSheet.Create(Doc, titleBlock.Id);
                            viewSheet.Name = sheet_01.Cells[i, startCol + 1].Value.ToString() + levelName;

                            // Center the view sheet
                            BoundingBoxUV outLine = viewSheet.Outline;
                            double yV = (outLine.Max.V + outLine.Min.V) / 2;
                            double xU = (outLine.Max.U + outLine.Min.U) / 2;
                            XYZ midPoint = new XYZ(xU, yV, 0);

                            Viewport viewport = Viewport.Create(Doc, viewSheet.Id, vPlan_01.Id, midPoint);
                        }
                    }
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                TDError(ex);
            }
        }

    }
}
