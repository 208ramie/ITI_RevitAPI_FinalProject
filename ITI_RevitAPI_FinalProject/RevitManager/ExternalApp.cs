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
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Create");

            // Create a button
            RHelper.CreateButton("Settings", ribbonPanel,
                "ITI_RevitAPI_FinalProject.RevitManager.LevelCreation",
                "Sets up the project and modify its settings",
                "Resources/Settings.ico");

            RHelper.CreateButton("Create\nLevel", ribbonPanel, "ITI_RevitAPI_FinalProject.RevitManager.LevelCreation",
                "Creates a level according to the naming convention",
                "Resources/Level.ico");

            RHelper.CreateButton("Create\nView", ribbonPanel, "ITI_RevitAPI_FinalProject.RevitManager.LevelCreation",
                "Creates a view according to naming convention",
                "Resources/View.ico");

            RHelper.CreateButton("Create\nWorkset", ribbonPanel, "ITI_RevitAPI_FinalProject.RevitManager.LevelCreation",
                "Creates a level according to the naming convention",
                "Resources/Worksets.ico");

            RHelper.CreateButton("Import\nExcel", ribbonPanel, "ITI_RevitAPI_FinalProject.RevitManager.LevelCreation",
                "Creates a level according to the naming convention",
                "Resources/Excel.ico");



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


        private void OnDocOpened(object sender, DocumentOpenedEventArgs args)
        {
             
            //Autodesk.Revit.ApplicationServices.Application app = (Autodesk.Revit.ApplicationServices.Application)sender;
            //Document doc = args.Document;
        }


        void OnViewActivated(object sender, ViewActivatedEventArgs e)
        {
            View vCurrent = e.CurrentActiveView;
            Document doc = e.Document;
        }


    }
}
