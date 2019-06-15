using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class SobstvUchModel : FirstModel
    {        
        public SobstvUchModel(object id) : base(id)
        {            
            firstSqlSelect =
                "SELECT ID, ID_SOBSTVENNIK, ID_UCHASTOK, DOLYA, DOCUMENT, DATA_IN, DATA_OUT FROM sobstv_uch ";
            secondAllSqlSelect =
                "WHERE ID_UCHASTOK = " + id + " ORDER BY DATA_IN DESC";
            secondFilterSqlSelect =
                "WHERE ID_UCHASTOK = " + id + " AND DATA_OUT IS NULL ORDER BY DATA_IN DESC";
            dictionarySqlSelect = "SELECT ID, " +
                "(FAMILIYA || ' ' || COALESCE(IMYA, '') || ' ' || COALESCE(OTCHESTVO, '')) AS NAME FROM sobstvennik";
            TableInit();
            // столбец, значение которого добавляется при вставке строки
            columnName = "ID_UCHASTOK";
        }                         
    }
}
