using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class SchetchikModel : FirstModel
    {        
        public SchetchikModel(object id) : base(id)
        {
            firstSqlSelect =
                "SELECT ID, ID_UCHASTOK, ID_SERVICE, N, PERV_POKAZ, NACHALO, KONEC FROM schetchik ";
            secondAllSqlSelect =
                "WHERE ID_UCHASTOK = " + id + " ORDER BY ID_SERVICE, NACHALO DESC";
            secondFilterSqlSelect =
                "WHERE ID_UCHASTOK = " + id + " AND KONEC IS NULL ORDER BY ID_SERVICE, NACHALO DESC";
            dictionarySqlSelect = "SELECT ID, NAME FROM service WHERE SCHETCHIK_ IS TRUE";
            TableInit();
            // столбец, значение которого добавляется при вставке строки
            columnName = "ID_UCHASTOK";
        }
    }
}
