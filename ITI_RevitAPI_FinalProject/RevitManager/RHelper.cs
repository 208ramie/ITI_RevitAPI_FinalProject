using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using TypedReference = System.TypedReference;

namespace ITI_RevitAPI_FinalProject.RevitManager
{
    [Transaction(TransactionMode.Manual)]
    public class RHelper
    {
        //// Global variables
        //public UIDocument UiDocument { get; set; }
        //public Document Document { get; set; }

        //public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        //{
        //    UiDocument = commandData.Application.ActiveUIDocument;
        //    Document = UiDocument.Document;
        //    return Result.Succeeded;
        //}

        //public List<Level> GetDocumentLevels(Document doc)
        //{
        //    return null;
        //}



        public static void TDError(Exception ex)
            => TaskDialog.Show("Error", ex.Message);

        public static void TDError(string message)
            => TaskDialog.Show("Error", message);

        public static double ToMM(double internalUnits)
            => UnitUtils.ConvertFromInternalUnits(internalUnits, UnitTypeId.Millimeters);

        public static double ToIU(double millimeters)
            => UnitUtils.ConvertToInternalUnits(millimeters, UnitTypeId.Millimeters);





        #region Get all levels in project

        public List<Level> GetAllLevelsInDocument(Document doc)
            =>  new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels).WhereElementIsNotElementType().Cast<Level>().ToList(); 


        #endregion





        #region Create View

        // You should understand what is a view plan type

        public ViewPlan CreateViewPlan(Document doc, Level targetLevel, string viewName,
            ViewPlanType viewPlanType = ViewPlanType.FloorPlan)
        {
            if (!GetAllLevelsInDocument(doc).Contains(targetLevel))
            {
                TDError("This level doesn't exist in the project");
                return null;
            }

            using (Transaction trans = new Transaction(doc, "Create a view plan"))
            {
                ViewPlan createdViewPlan = ViewPlan.Create(doc, , targetLevel.Id);
            }
        }



        public List<ViewFamilyType> GetAllViewFamilyTypes(Document doc)
            => new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
                .Cast<ViewFamilyType>().ToList();


        #endregion

        #region Create level
        public static Level CreateLevel(Document doc, string levelName, double levelElevationInMM, bool isLevelPinned)
        {
            using (Transaction trans = new Transaction(doc, "Create level"))
            {
                try
                {
                    trans.Start();
                    Level newLevel = Level.Create(doc, RHelper.ToIU(levelElevationInMM));
                    newLevel.Name = levelName;
                    newLevel.Pinned = isLevelPinned;
                    trans.Commit();
                    return newLevel;
                }
                catch (Exception ex)
                {
                    RHelper.TDError(ex);
                    return null;
                }
            }
        }
        #endregion

        #region Create workset
        public static Workset CreateWorkset(Document doc, string worksetName)
        {
            Workset newWorkset = null;

            // If the document isn't workshared 
            if (!doc.IsWorkshared)
            {
                TDError("Can't create a workset in a non-workshared document");
                return null; 
            }

            // Check that the workset name is unique
            if (!WorksetTable.IsWorksetNameUnique(doc, worksetName))
            {
                TDError("The workset name already exists");
                return null;
            }

            // If none of the breaking conditions happened create the workset
            using (Transaction worksetTransaction = new Transaction(doc, "Set preview view id"))
            {
                worksetTransaction.Start();
                newWorkset = Workset.Create(doc, worksetName);
                worksetTransaction.Commit();
            }

            return newWorkset;
        }
        #endregion

        #region CreateButton
        public static void CreateButton(string buttonName, RibbonPanel ribbonPanel, string fullClassName, string toolTip, string iconPath)
        {
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            PushButtonData pushButtonData =
                new PushButtonData(buttonName, buttonName, assemblyPath, fullClassName);

            PushButton pushButton = ribbonPanel.AddItem(pushButtonData) as PushButton;

            pushButton.ToolTip = toolTip;

            BitmapImage bitmapImage =
                new BitmapImage(new Uri($"pack://application:,,,/ITI_RevitAPI_FinalProject;component/{iconPath}"));

            pushButton.LargeImage = bitmapImage;
        }
        #endregion


    }
}
