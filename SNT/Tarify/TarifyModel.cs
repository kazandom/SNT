using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class TarifyModel : FirstModel
    {        
        public TarifyModel() : base()
        {            
            firstSqlSelect =
                "SELECT ID, ID_SERVICE, NACHALO, ZNACHENIE, CHANGED, NOTE FROM tarif ";
            secondAllSqlSelect =
                "ORDER BY ID_SERVICE, NACHALO DESC";
            secondFilterSqlSelect =
                "WHERE ID IN (WITH cte AS " +
                "(SELECT ID_SERVICE, MAX(NACHALO) AS NACHALO FROM tarif WHERE " +
                "NACHALO <= (SELECT PERIOD FROM period_selection WHERE OPERATING IS TRUE) " +
                "GROUP BY ID_SERVICE) " +
                "SELECT ID FROM cte JOIN tarif ON cte.ID_SERVICE = tarif.ID_SERVICE AND " +
                "cte.NACHALO = tarif.NACHALO) " +
                "ORDER BY ID_SERVICE, NACHALO DESC";
            dictionarySqlSelect = "SELECT ID, NAME FROM service";
            TableInit();            
        }        
    }
}
