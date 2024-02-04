using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using static System.Math;
using CharacterCasing = System.Windows.Controls.CharacterCasing;
using Grid = Autodesk.Revit.DB.Grid;

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
            => new FilteredElementCollector(Doc).OfClass(typeof(Level)).Cast<Level>()
                .OrderByDescending(l => l.Elevation).ToList();  
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

        public static Level CreateLevel(string levelName, double levelElevationInMM, bool isLevelPinned, bool createViewPlan)
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

                    if (createViewPlan)
                        CreateViewPlan(newLevel, levelName);

                    return newLevel;
                }
                catch (Exception ex)
                {
                    RHelper.TDError(ex);
                    return null;
                }
            }
        }

        public static Level CreateLevel(Level levelToOffsetFrom, double offsetFromValueLevelInMM, string levelName, bool isLevelPinned, bool createViewPlan)
        {
            try
            {
                double inputLevelElevationInMM = ToMM(levelToOffsetFrom.Elevation);
                using (Transaction trans = new Transaction(Doc, "Create level"))
                {
                    trans.Start();
                    Level newLevel = Level.Create(Doc, ToIU(inputLevelElevationInMM + offsetFromValueLevelInMM));
                    newLevel.Name = levelName;
                    newLevel.Pinned = isLevelPinned;

                    trans.Commit();

                    if (createViewPlan)
                        CreateViewPlan(newLevel, levelName);

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

        #region RenameGrids
        public static void RenameGrids()
        {
            try
            {
                // Get all grids in the document
                List<Grid> allGrids = GetAllGridsInDocument();

                // Filter them according to their orientation
                List<Grid> hzGrids, vtGrids, nonOrthoGrids;
                FilterGrids(allGrids, out hzGrids, out vtGrids, out nonOrthoGrids);
                
                // Order the grids
                var orderHzGrids = GetOrderedGridsByAxis(hzGrids, Direction.Horizontal);
                var orderVtGrids = GetOrderedGridsByAxis(vtGrids, Direction.Vertical);

                // rename them to sth weird, then desired
                using (Transaction trans = new Transaction(Doc, "Change grid names"))
                {
                    Random rand = new Random();
                    trans.Start();
                    RenameGrids(orderHzGrids,rand.Next(int.MaxValue/2));
                    RenameGrids(orderVtGrids, rand.Next(int.MaxValue/2));

                    RenameGrids(orderHzGrids, 'A');
                    RenameGrids(orderVtGrids, 1);

                    trans.Commit();
                }
            }
            catch (Exception e)
            {
                TDError(e);
                throw;
            }
        }

        public static List<Grid> GetAllGridsInDocument() 
            => new FilteredElementCollector(Doc)
                .OfClass(typeof(Grid))
                .ToElements()
                .Cast<Grid>().ToList();

        public static void FilterGrids(List<Grid> gridsToBeFiltered,
            out List<Grid> horizontalGrids, out List<Grid> verticalGrids, out List<Grid> nonOrthogonalGrids)
        {
            horizontalGrids = new List<Grid>(); 
            verticalGrids = new List<Grid>();
            nonOrthogonalGrids = new List<Grid>();

            foreach (Grid grid in gridsToBeFiltered)
            {
                Line gridLine = grid.Curve as Line;

                if (Abs(gridLine.Direction.X) == 1)
                    horizontalGrids.Add(grid);

                else if (Abs(gridLine.Direction.Y) == 1)
                    verticalGrids.Add(grid);

                else
                    nonOrthogonalGrids.Add(grid);
            }
            #region  Obs code
            //horizontalGrids = gridsToBeFiltered.Where(g => Abs((g.Curve as Line).Direction.X) == 1).ToList();
            //verticalGrids = gridsToBeFiltered.Where(g => Abs((g.Curve as Line).Direction.Y) == 1).ToList();
            //nonOrthogonalGrids = gridsToBeFiltered.Where(g
            //    => Abs((g.Curve as Line).Direction.X) != 1
            //       &&
            //       Abs((g.Curve as Line).Direction.Y) != 1).ToList();
            #endregion
        }

        public static List<Grid> GetOrderedGridsByAxis(List<Grid> gridsToAlign, Direction direction, bool reverse = false)
        {
            List<Grid> returnedGrid;

            if (direction == Direction.Horizontal)
                returnedGrid = gridsToAlign.OrderByDescending(g => g.Curve.Evaluate(0.5, true).Y).ToList();
            else
                returnedGrid = gridsToAlign.OrderBy(g => g.Curve.Evaluate(0.5, true).X).ToList();

            if (reverse)
                returnedGrid.Reverse(); 

            return returnedGrid;
        }

        public static void RenameGrids(List<Grid> grids,  int startNumber)
        {
            foreach (var grid in grids)
            {
                grid.Name = (startNumber++).ToString(); 
            }
        }

        public static void RenameGrids(List<Grid> grids, char startCharacter)
        {
            foreach (var grid in grids)
            {
                grid.Name = startCharacter++.ToString();
            }
        }

        public enum Direction {Horizontal, Vertical}
        #endregion


        //public static List<Grid> FilterHorizontalGrids(List<Grid> grids)
        //    => grids.Where(g => Math.Abs((g.Curve as Line).Direction.X) == 1).ToList();

        //public static List<Grid> FilterVerticalGrids(List<Grid> grids) 
        //    => grids.Where(g => Math.Abs((g.Curve as Line).Direction.Y) == 1).ToList();



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
