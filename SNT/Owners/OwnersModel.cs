using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class OwnersModel : FirstModel
    {        
        public OwnersModel() : base()
        {
            firstSqlSelect =
                "SELECT ID, FAMILIYA, IMYA, OTCHESTVO, PHONE, EMAIL, ADDRESS FROM sobstvennik ";
            secondAllSqlSelect = 
                "ORDER BY FAMILIYA";
            secondFilterSqlSelect =
                "WHERE ID IN (SELECT ID_SOBSTVENNIK FROM sobstv_uch WHERE DATA_OUT IS NULL) " +
                "ORDER BY FAMILIYA";
            dictionarySqlSelect =
                "SELECT ID_SOBSTVENNIK, LIST(N || ' / ' || COALESCE(ALLEYA, '-'), ', ') AS N " +
                "FROM sobstv_uch LEFT OUTER JOIN uchastok ON ID_UCHASTOK = uchastok.ID " +
                "GROUP BY ID_SOBSTVENNIK";
            TableInit();            
        }        
    }
}
