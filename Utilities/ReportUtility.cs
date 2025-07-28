using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using ClosedXML.Excel;

namespace eShift.Utilities
{
    public static class ReportUtility
    {
        public static void ExportToExcel<T>(List<T> reportList, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Report");

                var properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                    worksheet.Cell(1, i + 1).Value = properties[i].Name;

                for (int row = 0; row < reportList.Count; row++)
                {
                    for (int col = 0; col < properties.Length; col++)
                    {
                        var value = properties[col].GetValue(reportList[row]);
                        worksheet.Cell(row + 2, col + 1).Value = value?.ToString() ?? "";
                    }
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }

        public static void ExportToPdf<T>(List<T> reportList, string filePath, string reportType)
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var fontHeader = new XFont("Verdana", 11, XFontStyleEx.Bold);
            var fontCell = new XFont("Verdana", 8, XFontStyleEx.Regular);

            var properties = typeof(T).GetProperties();
            double x = 50, y = 50;
            double cellHeight = 20;
            double cellWidth = Math.Min((page.Width - 100) / properties.Length, 100);  

            //Heading
            gfx.DrawString($"Report Type: {reportType}", fontHeader, XBrushes.Black, new XPoint(x, y));
            gfx.DrawString($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}", fontHeader, XBrushes.Black, new XPoint(page.Width - 200, y));
            y += cellHeight + 10;

            //Header
            for (int i = 0; i < properties.Length; i++)
            {
                double colX = x + i * cellWidth;
                gfx.DrawRectangle(XPens.Black, colX, y, cellWidth, cellHeight);
                gfx.DrawString(properties[i].Name, fontCell, XBrushes.Black,
                    new XRect(colX, y, cellWidth, cellHeight), XStringFormats.Center);
            }
            y += cellHeight;

            //Table Rows
            foreach (var item in reportList)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    var value = properties[i].GetValue(item);
                    string text = value?.ToString() ?? "";
                    bool isNumeric = double.TryParse(text, out _);

                    double colX = x + i * cellWidth;
                    gfx.DrawRectangle(XPens.Black, colX, y, cellWidth, cellHeight);
                    gfx.DrawString(text, fontCell, XBrushes.Black,
                        new XRect(colX, y, cellWidth, cellHeight),
                        isNumeric ? XStringFormats.CenterRight : XStringFormats.Center);
                }
                y += cellHeight;

                // Page break 
                if (y > page.Height - 100)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    y = 50;
                }
            }

            document.Save(filePath);
        }


    }
}
