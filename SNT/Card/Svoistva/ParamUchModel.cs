using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class ParamUchModel : FirstModel
    {        
        public ParamUchModel(object id) : base(id)
        {            
            firstSqlSelect =
                "SELECT ID, ID_UCHASTOK, ID_PARAMETR, NACHALO, ZNACHENIE, CHANGED, NOTE FROM param_uch ";
            secondAllSqlSelect =
                "WHERE ID_UCHASTOK = " + id + " ORDER BY ID_PARAMETR, NACHALO DESC";
            secondFilterSqlSelect =
                "WHERE ID IN (WITH cte AS " +
                "(SELECT ID_PARAMETR, MAX(NACHALO) AS NACHALO FROM param_uch WHERE ID_UCHASTOK = " + id +
                " AND NACHALO <= (SELECT PERIOD FROM period_selection WHERE OPERATING IS TRUE) " +
                "GROUP BY ID_PARAMETR) " +
                "SELECT ID FROM cte JOIN param_uch ON cte.ID_PARAMETR = param_uch.ID_PARAMETR AND " +
                "cte.NACHALO = param_uch.NACHALO WHERE ID_UCHASTOK = " + id + ") " +
                "ORDER BY ID_PARAMETR, NACHALO DESC";
            dictionarySqlSelect = "SELECT ID, NAME FROM parametr WHERE GLOBAL_ IS FALSE";
            TableInit();
            // столбец, значение которого добавляется при вставке строки
            columnName = "ID_UCHASTOK";
        }        
    }
}
