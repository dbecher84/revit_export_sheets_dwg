using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Drawing;

namespace export_sheets_dwg_revit
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            //Get application and documnet objects
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            //DWG Settings export settings
            DWGExportOptions opt = new DWGExportOptions
            {
                HideScopeBox = true,
                HideReferencePlane = true,
                SharedCoords = false,
                MergedViews = true,
                NonplotSuffix = "NPL",
                ExportingAreas = false,
                FileVersion = ACADVersion.R2013,
            };

            //collect sheets in revit
            FilteredElementCollector viewCollector = new FilteredElementCollector(doc);
            viewCollector.OfClass(typeof(ViewSheet));

            //list of sheets to send to the windows form viewList
            List<string> listSheetNames = new List<string>();
            foreach (ViewSheet view in viewCollector)
            {
                listSheetNames.Add(view.Name);
            }



            //initate list of sheets form
            var l = new Export_Sheets.viewList(listSheetNames);
            l.ShowDialog();

            //select save folder
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
            }

            //list of sheets exported
            List<string> Sheetlist = new List<string>();
    

            //check if revit sheet is in list and export to DWG if it is
            foreach (ViewSheet view in viewCollector)
            {
                foreach (var item in l.ExportViewList)
                    if (view.Name == item)
                    {
                        List<ElementId> SheetlistID = new List<ElementId>();
                        SheetlistID.Add(view.Id);
                        Sheetlist.Add(view.Name);
                        doc.Export(folderPath, view.Name + ".dwg", SheetlistID,  opt);
                    }
            }

            //Combine all view into string to show user what was exported
            string fullList = "";
            for (int i = 0; i < Sheetlist.Count; i++)
            {
                if (i < Sheetlist.Count - 1)
                    fullList = fullList + Sheetlist[i] + ", ";
                else
                    fullList += Sheetlist[i];
            }
            //Display list of exported sheets
            TaskDialog.Show("The following view were exported.", fullList);

            return Result.Succeeded;

        }
    }
}
