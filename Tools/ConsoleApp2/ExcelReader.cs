
namespace ConsoleApp2
{
    using OfficeOpenXml;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    
    public class ExcelReader
    {
        public static IEnumerable<T> ReadExcel<T>(
          string sFileName,
          string sheetName,
          Func<IDictionary<string, string>, T> func,int headerRow=1) where T : class
        {
            
            var sFullName = Path.Combine(Environment.CurrentDirectory, sFileName);
            using (var package = new ExcelPackage(new FileInfo(sFullName)))
            {
                StringBuilder sb = new StringBuilder();
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;
                for (int row = headerRow + 1; row <= rowCount; row++)
                {
                    var dict = new Dictionary<string, string>();
                    for (int col = 1; col <= ColCount; col++)
                    {
                        if (object.Equals(worksheet.Cells[headerRow, col].Value, null) == false)
                        {
                            var key = worksheet.Cells[headerRow, col].Value.ToString();
                            var value = worksheet.Cells[row, col].Value == null ? null : worksheet.Cells[row, col].Value.ToString();
                            dict.Add(key ?? "", value ?? "");
                        }
                    }
                    yield return func(dict);
                }

            }
        }
    }
}
