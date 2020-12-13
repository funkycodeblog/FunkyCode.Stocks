using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObj
{

    public enum TypeOrder
    {
        OP_BUY = 0,	        //	Buy operation
        OP_SELL = 1,	    //	Sell operation
        OP_BUYLIMIT = 2,	//	Buy limit pending order
        OP_SELLLIMIT = 3,	//	Sell limit pending order
        OP_BUYSTOP = 4,	    //	Buy stop pending order
        OP_SELLSTOP = 5		//	Sell stop pending order
    }

    public class OrderInfo
    {
        public double ClosePrice { get; set; }
        public DateTime CloseTime { get; set; }
        public string Comment { get; set; }
        public DateTime Expiration { get; set; }
        public double Lots { get; set; }
        public int MagicNumber { get; set; }
        public double OpenPrice { get; set; }
        public DateTime OpenTime { get; set; }
        public double Profit { get; set; }
        public double StopLoss { get; set; }
        public double Swap { get; set; }
        public string Symbol { get; set; }
        public double TakeProfit { get; set; }
        public int Ticket { get; set; }
        public TypeOrder OrderType { get; set; }
    }
}
