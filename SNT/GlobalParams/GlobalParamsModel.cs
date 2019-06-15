using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class GlobalParamsModel : FirstModel
    {        
        public GlobalParamsModel() : base()
        {
            firstSqlSelect =
                "SELECT ID, ID_PARAMETR, NACHALO, ZNACHENIE, CHANGED, NOTE FROM param_global ";
            secondAllSqlSelect =
                "ORDER BY ID_PARAMETR, NACHALO DESC";
            secondFilterSqlSelect =
                "WHERE ID IN (WITH cte AS " +
                "(SELECT ID_PARAMETR, MAX(NACHALO) AS NACHALO FROM param_global " +
                "WHERE NACHALO <= (SELECT PERIOD FROM period_selection WHERE OPERATING IS TRUE) " +
                "GROUP BY ID_PARAMETR) " +
                "SELECT ID FROM cte JOIN param_global ON cte.ID_PARAMETR = param_global.ID_PARAMETR AND " +
                "cte.NACHALO = param_global.NACHALO) " +
                "ORDER BY ID_PARAMETR, NACHALO DESC";
            dictionarySqlSelect = "SELECT ID, NAME FROM parametr WHERE GLOBAL_ IS TRUE";
            TableInit();
        }        
    }
}
