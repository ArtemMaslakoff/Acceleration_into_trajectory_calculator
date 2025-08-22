using System.Globalization;
using System.IO;

namespace AIT_Calculator.Services
{
    public static class CSVParsingService
    {
        public static Tuple<double[], double[]> ParseCSV(string filePath)
        {
            var secondColumn = new System.Collections.Generic.List<double>();
            var fifthColumn = new System.Collections.Generic.List<double>();
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Пропускаем строки, которые не содержат данных
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith(";;;"))
                        continue;
                    var values = line.Split(';');
                    // Проверяем, что в строке достаточно столбцов
                    if (values.Length >= 5)
                    {
                        // Преобразуем значения в double и добавляем в массивы
                        if (double.TryParse(values[1].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double secondValue))
                        {
                            secondColumn.Add(secondValue);
                        }
                        if (double.TryParse(values[4].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double fifthValue))
                        {
                            fifthColumn.Add(fifthValue);
                        }
                    }
                }
            }
            return Tuple.Create(secondColumn.ToArray(), fifthColumn.ToArray());
        }
    }
}
