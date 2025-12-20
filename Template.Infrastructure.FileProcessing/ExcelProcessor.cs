using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Infrastructure.FileProcessing
{
    public class ExcelProcessor
    {
        public static void Process(string path)
        {
            using var package = new ExcelPackage(new FileInfo(path));
            var sheet = package.Workbook.Worksheets[0];
        }
    }
}
