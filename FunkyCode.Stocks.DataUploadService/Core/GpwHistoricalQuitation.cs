using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Stocks.DataUploadService.Core
{
    public class GpwHistoricalQuotation
    {
        public object Data { get; set; }
        public string Nazwa { get; set; }
        public string ISIN { get; set; }
        public string Waluta { get; set; }
        public decimal Kurs_otwarcia { get; set; }
        public decimal Kurs_max { get; set; }
        public decimal Kurs_min { get; set; }
        public decimal Kurs_zamknięcia { get; set; }
        public decimal Zmiana { get; set; }
        public int Wolumen { get; set; }
        public int Liczba_Transakcji { get; set; }
        public decimal Obrót { get; set; }
        public int Liczba_otwartych_pozycji { get; set; }
        public decimal Wartość_otwartych_pozycji { get; set; }
        public decimal Cena_nominalna { get; set; }
    }
}
