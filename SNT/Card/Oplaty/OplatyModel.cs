using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class OplatyModel : FirstModel
    {        
        public OplatyModel(object id) : base(id)
        {            
            firstSqlSelect =
                "SELECT ID, ID_UCHASTOK, SUMMA, PERIOD, DATA_ FROM oplata ";
            secondAllSqlSelect =
                "WHERE ID_UCHASTOK = " + id + " ORDER BY PERIOD DESC, DATA_ DESC";
            secondFilterSqlSelect =
                "WHERE ID_UCHASTOK = " + id + " AND " +
                "PERIOD >= (SELECT MAX(PERIOD) FROM oplata WHERE ID_UCHASTOK = " + id + ") - 5" +
                "ORDER BY PERIOD DESC, DATA_ DESC";
            // выбрано 2 столбца, чтобы соответствовало функции base.GetDictionary()
            dictionarySqlSelect = "SELECT PERIOD, OPERATING FROM period_selection " +
                "WHERE PERIOD <= (SELECT PERIOD FROM period_selection WHERE OPERATING IS TRUE) " +
                "ORDER BY PERIOD DESC";
            TableInit();
            columnName = "ID_UCHASTOK";            
        }        
    }
}
