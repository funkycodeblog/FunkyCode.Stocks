namespace FunkyCode.Stocks.DataUploadService
{
    public interface IXlsDataProvider
    {
        object[,] GetSheetAsTable(string path, string sheetName);
    }
}