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

namespace export_sheets_dwg
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class exportSheets : IExternalCommand
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
            List<string> listSheetNums = new List<string>();
            foreach (ViewSheet view in viewCollector)
            {
                listSheetNums.Add(view.SheetNumber);
            }



            //initate list of sheets form
            var l = new Export_Sheets.sheetList(listSheetNums);
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
                foreach (var item in l.ExportSheetList)
                    if (view.SheetNumber == item)
                    {
                        List<ElementId> SheetlistID = new List<ElementId>();
                        SheetlistID.Add(view.Id);
                        Sheetlist.Add(view.SheetNumber);
                        doc.Export(folderPath, view.SheetNumber + ".dwg", SheetlistID,  opt);
                    }
            }

            //Display form listing sheets that were exported
            var d = new Export_Sheets_Final.ExportedList(Sheetlist);
            d.ShowDialog();

            return Result.Succeeded;

        }
    }
}
