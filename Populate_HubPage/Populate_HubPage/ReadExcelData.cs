using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace Populate_HubPage
{
    public class ReadExcelData
    {
        public void getExcelFile()
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"E:\kanna\study 4_2\Remote internship problem statements.xlsx");
            Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            Console.WriteLine(rowCount);
            Console.WriteLine(colCount);
        }
    }
}
