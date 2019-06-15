using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class PokazanieModel : FirstModel
    {        
        public PokazanieModel(object idSchetchik) : base(idSchetchik)
        {            
            firstSqlSelect =
                "SELECT ID, ID_SCHETCHIK, POKAZ, PERIOD FROM pokazanie ";
            secondAllSqlSelect =
                "WHERE ID_SCHETCHIK = " + idSchetchik + " ORDER BY PERIOD DESC";
            secondFilterSqlSelect =
                "WHERE ID_SCHETCHIK = " + idSchetchik + " AND PERIOD = " +
                "(SELECT MAX(PERIOD) FROM pokazanie WHERE ID_SCHETCHIK = " + idSchetchik +
                " AND PERIOD <= (SELECT PERIOD FROM period_selection WHERE OPERATING IS TRUE))";            
            TableInit();
            // столбец, значение которого добавляется при вставке строки
            columnName = "ID_SCHETCHIK";
        }
    }
}
