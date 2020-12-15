namespace FunkyCode.Stocks.DataUploadService.Entities
{
    public interface IBuySpecification
    {
        bool IsToBuy(QuotationSet quotationSet);
    }
}