using OfficeOpenXml;
using System.ComponentModel;
using System.IO;

namespace AIT_Calculator.Services
{
    public static class ExcelParsingService
    {
        public static Tuple<double[], double[]> ParseExcel(string filePath)
        {
            var secondColumn = new List<double>();
            var fifthColumn = new List<double>();

            ExcelPackage.License.SetNonCommercialPersonal("Maslakov Artem Olegovich");

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Получаем первый лист
                int rowCount = worksheet.Dimension.Rows + 1;
                // Изменяем цикл, чтобы включить последнюю строку
                for (int row = 1; row <= rowCount; row++)
                {
                    // Предполагаем, что данные находятся в 2-м и 5-м столбцах
                    if (double.TryParse(worksheet.Cells[row, 2].Text, out double secondValue))
                    {
                        secondColumn.Add(secondValue);
                    }
                    if (double.TryParse(worksheet.Cells[row, 5].Text, out double fifthValue))
                    {
                        fifthColumn.Add(fifthValue);
                    }
                }
            }
            return Tuple.Create(secondColumn.ToArray(), fifthColumn.ToArray());
        }
    }
}
