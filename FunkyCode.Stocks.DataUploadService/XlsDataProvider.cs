using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;


namespace FunkyCode.Stocks.DataUploadService
{
    public class XlsDataProvider : IXlsDataProvider
    {
        public object[,] GetSheetAsTable(string path, string sheetName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var stream = File.Open(path, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            var fieldCount = reader.FieldCount;
            var rowCount = reader.RowCount;

            var table = new object[rowCount, fieldCount];

            do
            {
                var row = 0;
                while (reader.Read())
                {
                    for (var c = 0; c < fieldCount; c++)
                    {
                        var obj = reader.GetValue(c);
                        table[row, c] = obj;
                    }

                    row++;
                }
            } while (reader.NextResult());

            return table;
        }
    }
}
