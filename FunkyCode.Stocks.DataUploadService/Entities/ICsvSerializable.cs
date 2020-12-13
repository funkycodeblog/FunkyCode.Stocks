using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObj
{
    public interface ICsvSerializable
    {

        string CsvGet();
        void CsvFetch(string value);

    }
}
