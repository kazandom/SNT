using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class NachislenieModel : FirstModel
    {        
        public NachislenieModel(object id, object period) : base(id)
        {            
            firstSqlSelect =
                "SELECT ID, ID_UCHASTOK, ID_SERVICE, PERIOD, IZMENENIE, NACHIS, K_OPLATE FROM nachislenie " +
                "WHERE ID_UCHASTOK = " + id + " AND PERIOD = " + period;            
            dictionarySqlSelect = "SELECT ID, NAME FROM service";
            TableInit();
            // columnName null, т.к. строки не будут добавляться вручную
        }
    }
}
