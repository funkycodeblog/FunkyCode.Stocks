namespace FunkyCode.Stocks.DataUploadService.Entities
{
    public interface ISellSpecification
    {
        bool IsToSell(QuotationSet quotationSet);
    }
}