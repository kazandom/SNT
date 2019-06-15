using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class PaymentsModel : FirstModel
    {        
        public PaymentsModel() : base()
        {
            firstSqlSelect =
                "SELECT ID, ID_UCHASTOK, SUMMA, PERIOD, DATA_ FROM oplata ";              
            secondAllSqlSelect =
                "ORDER BY PERIOD DESC, DATA_ DESC";
            secondFilterSqlSelect =
                "WHERE PERIOD = (SELECT PERIOD FROM period_selection WHERE OPERATING IS TRUE) " +
                "ORDER BY PERIOD DESC, DATA_ DESC";            
            TableInit();                        
        }
        
        public ObservableCollection<DictEntry> GetDictionary(int type)
        {
            if (type == 0)            
                dictionarySqlSelect = "SELECT ID, (N || ' / ' || COALESCE(ALLEYA, '-')) AS N " +
                    "FROM uchastok ORDER BY ALLEYA, N";
            else
                // выбрано 2 столбца, чтобы соответствовало функции base.GetDictionary()
                dictionarySqlSelect = "SELECT PERIOD, OPERATING FROM period_selection " +
                    "WHERE PERIOD <= (SELECT PERIOD FROM period_selection WHERE OPERATING IS TRUE) " +
                    "ORDER BY PERIOD DESC";            
            var dictionary = base.GetDictionary();
            return dictionary;
        }
    }
}
