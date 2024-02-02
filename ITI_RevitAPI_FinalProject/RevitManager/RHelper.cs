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

        #region UIDoc and Doc
        private static UIDocument _uiDoc;
        public static UIDocument UIDoc
        {
            get => _uiDoc;
            set
            {
                _uiDoc = value;
                Doc = value.Document;
            }
        }
        public static Document Doc { get; set; }
        #endregion

        #region Helpers
        public static void TDError(Exception ex)
            => TaskDialog.Show("Error", ex.Message);

        public static void TDError(string message)
            => TaskDialog.Show("Error", message);

        public static double ToMM(double internalUnits)
            => UnitUtils.ConvertFromInternalUnits(internalUnits, UnitTypeId.Millimeters);

        public static double ToIU(double millimeters)
            => UnitUtils.ConvertToInternalUnits(millimeters, UnitTypeId.Millimeters);
        #endregion

        #region Helpers to core methods
        //public List<Level> GetAllLevelsInDocument(Document doc)
        //    =>  new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels).WhereElementIsNotElementType().Cast<Level>().ToList();
        public static List<Level> GetAllLevelsInDocument()
            => new FilteredElementCollector(Doc).OfClass(typeof(Level)).Cast<Level>().ToList(); 
        #endregion

        #region Core methods
        public static ViewPlan CreateViewPlan(Level targetLevel = null, string viewName = "NewLevel")
        {
            if (targetLevel == null)
            {
                targetLevel = GetAllLevelsInDocument().OrderBy(x => x.Elevation).FirstOrDefault(); 
            }

            // Get the floor plan view family
            ViewFamilyType viewFamily = new FilteredElementCollector(Doc).OfClass(typeof(ViewFamilyType))
                .Cast<ViewFamilyType>().FirstOrDefault(f => f.ViewFamily == ViewFamily.FloorPlan);
            try
            {
                using (Transaction trans = new Transaction(Doc, "Create floor plan"))
                {
                    trans.Start();
                    // Create a new view plan
                    ViewPlan newViewPlan = ViewPlan.Create(Doc, viewFamily?.Id, targetLevel.Id);
                    newViewPlan.Name = viewName;
                    trans.Commit();
                    return newViewPlan;
                }
            }
            catch (Exception e)
            {
                TDError(e);
                return null;
            }
        }

        public static Level CreateLevel(string levelName, double levelElevationInMM, bool isLevelPinned)
        {
            using (Transaction trans = new Transaction(Doc, "Create level"))
            {
                try
                {
                    trans.Start();
                    Level newLevel = Level.Create(Doc, RHelper.ToIU(levelElevationInMM));
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

        public static Level CreateLevel(Level levelToOffsetFrom, double offsetFromValueLevelInMM, string levelName, bool isLevelPinned)
        {
            try
            {
                double inputLevelElevationInMM = ToMM(levelToOffsetFrom.Elevation);
                using (Transaction trans = new Transaction(Doc, "Create level"))
                {
                    trans.Start();
                    Level newLevel = Level.Create(Doc, RHelper.ToIU(inputLevelElevationInMM + offsetFromValueLevelInMM));
                    newLevel.Name = levelName;
                    newLevel.Pinned = isLevelPinned;
                    trans.Commit();
                    return newLevel;
                }

            }
            catch (Exception e)
            {
                RHelper.TDError(e);
                return null;
            }
        }

        public static Workset CreateWorkset(string worksetName)
        {
            Workset newWorkset = null;

            // If the document isn't workshared 
            if (!Doc.IsWorkshared)
            {
                TDError("Can't create a workset in a non-workshared document");
                return null; 
            }

            // Check that the workset name is unique
            if (!WorksetTable.IsWorksetNameUnique(Doc, worksetName))
            {
                TDError("The workset name already exists");
                return null;
            }

            // If none of the breaking conditions happened create the workset
            using (Transaction worksetTransaction = new Transaction(RHelper.Doc,"Set preview view id"))
            {
                worksetTransaction.Start();
                newWorkset = Workset.Create(RHelper.Doc, worksetName);
                worksetTransaction.Commit();
            }

            return newWorkset;
        }
        #endregion

        #region External application helpers
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
