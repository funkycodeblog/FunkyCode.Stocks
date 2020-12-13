using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObj
{
    public class EnumTranslator
    {



        #region <singleton>
        
        EnumTranslator() { }

        static EnumTranslator _instance;

        public static EnumTranslator Instance
        {
            get
            {
                if (null == _instance) _instance = new EnumTranslator();
                return _instance;
            }
        }

        #endregion




        #region <members>
        Dictionary<string, string> _items = new Dictionary<string, string>();
        #endregion

        public void AddItem(Enum enumItem, string translation)
        {
            string name = getTypeName(enumItem);
            _items.Add(name, translation);
        }



        string getTypeName(Enum enumItem)
        {

            string type = enumItem.GetType().FullName;
            string en = enumItem.ToString();
            string fullName = type + '.' + en;
            return fullName;

        }


    

    
    }
}
