using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class VznosDisableModel : FirstModel
    {        
        public VznosDisableModel(object id) : base(id, true)
        {            
            firstSqlSelect =
                "SELECT ID, ID_UCHASTOK, ID_SERVICE, NACHALO, DISABLE, CHANGED, NOTE FROM serv_uch_disable ";
            secondAllSqlSelect =
                "WHERE ID_UCHASTOK = " + id + "ORDER BY ID_SERVICE, NACHALO DESC";
            secondFilterSqlSelect =
                "WHERE ID IN (WITH cte AS " +
                "(SELECT ID_SERVICE, MAX(NACHALO) AS NACHALO FROM serv_uch_disable WHERE ID_UCHASTOK = " + id +
                " AND NACHALO <= (SELECT PERIOD FROM period_selection WHERE OPERATING IS TRUE) " +
                "GROUP BY ID_SERVICE) " +
                "SELECT ID FROM cte JOIN serv_uch_disable ON cte.ID_SERVICE = serv_uch_disable.ID_SERVICE AND " +
                "cte.NACHALO = serv_uch_disable.NACHALO WHERE ID_UCHASTOK = " + id + ") " +
                "ORDER BY ID_SERVICE, NACHALO DESC";
            dictionarySqlSelect = "SELECT ID, NAME FROM service";
            TableInit();
            columnName = "ID_UCHASTOK";
            columnNameAdditional = "DISABLE";
        }

        public ObservableCollection<object> GetBooleans()
        {
            ObservableCollection<object> booleans = new ObservableCollection<object>
            {                 
                new DictEntry { Id = true, Name = "Да" },
                new DictEntry { Id = false, Name = "Нет" }                
            };
            return booleans;
        }
    }
}
