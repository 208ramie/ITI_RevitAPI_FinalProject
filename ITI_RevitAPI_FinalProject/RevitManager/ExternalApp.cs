using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace ITI_RevitAPI_FinalProject.RevitManager
{
    public class ExternalApp : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            //application.ViewActivated += new EventHandler<Autodesk.Revit.UI.Events.ViewActivatedEventArgs>(OnViewActivated);

            // Make the method OnDocOpened subscribe to the event document opened
            //application.ControlledApplication.DocumentOpened += OnDocOpened;


            // Create the plugin's tab
            //Variables
            string tabName = "R.A.G";
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            // Create a new tab 
            application.CreateRibbonTab(tabName);

            // Create a panel in the tab
            RibbonPanel settingsRibbonPanel = application.CreateRibbonPanel(tabName, "Settings");
            RibbonPanel createRibbonPanel = application.CreateRibbonPanel(tabName, "Create");
            RibbonPanel modifyRibbonPanel = application.CreateRibbonPanel(tabName, "Modify");
            RibbonPanel excelRibbonPanel = application.CreateRibbonPanel(tabName, "Excel");

            // Create a button
            RHelper.CreateButton("Settings", settingsRibbonPanel,
                "ITI_RevitAPI_FinalProject.RevitManager.SettingsManager",
                "Sets up the project and modify its settings",
                "Resources/Settings.ico");

            RHelper.CreateButton("Create\nLevel", createRibbonPanel, 
                "ITI_RevitAPI_FinalProject.RevitManager.LevelCreation",
                "Creates a level according to the naming convention",
                "Resources/Level.ico");

            RHelper.CreateButton("Create\nView", createRibbonPanel,
                "ITI_RevitAPI_FinalProject.RevitManager.ViewCreation",
                "Creates a view according to naming convention",
                "Resources/View.ico");

            RHelper.CreateButton("Create\nWorkset", createRibbonPanel, 
                "ITI_RevitAPI_FinalProject.RevitManager.WorkSetCreation",
                "Creates a workset according to naming convention",
                "Resources/Worksets.ico");

            RHelper.CreateButton("Adjust\nGrids", modifyRibbonPanel,
                "ITI_RevitAPI_FinalProject.RevitManager.GridsRenamer",
                "Renames the grid",
                "Resources/GridRenamer.ico");


            RHelper.CreateButton("Import\nExcel", excelRibbonPanel, 
                "ITI_RevitAPI_FinalProject.RevitManager.ExcelImporter",
                "Imports stuff from excel file",
                "Resources/Excel.ico");

            RHelper.CreateButton("Export\nExcel", excelRibbonPanel,
                "ITI_RevitAPI_FinalProject.RevitManager.ExcelImporter",
                "Exports stuff from excel",
                "Resources/ExcelExport.ico");


            //// Create a button
            //PushButtonData createLevelPushButtonData = new PushButtonData("Show window", "Create Level", assemblyPath,
            //    "ITI_RevitAPI_FinalProject.RevitManager.LevelCreation"); 
            //// Add the button to the panel
            //PushButton createLevelPushButton = ribbonPanel.AddItem(createLevelPushButtonData) as PushButton;
            //// Change the tooltip
            //createLevelPushButton.ToolTip = "Tab here to create a level with naming convention";

            //BitmapImage createLevelImage = new BitmapImage(new Uri("pack://application:,,,/ITI_RevitAPI_FinalProject;component/Resources/favicon.ico"));

            //createLevelPushButton.LargeImage = createLevelImage;

            // Return success
            return Result.Succeeded;
        }






        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }


        //private void OnDocOpened(object sender, DocumentOpenedEventArgs args)
        //{
             
        //    //Autodesk.Revit.ApplicationServices.Application app = (Autodesk.Revit.ApplicationServices.Application)sender;
        //    //Document doc = args.Document;
        //}


        //void OnViewActivated(object sender, ViewActivatedEventArgs e)
        //{
        //    View vCurrent = e.CurrentActiveView;
        //    Document doc = e.Document;
        //}


    }
}
