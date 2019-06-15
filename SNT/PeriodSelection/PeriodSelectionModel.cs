using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class PeriodSelectionModel : FirstModel
    {
        public PeriodSelectionModel() : base(false)
        {
            firstSqlSelect =
                "SELECT PERIOD, OPERATING FROM period_selection ORDER BY PERIOD DESC";            
            TableInit();
            // столбец, значение которого добавляется при вставке строки
            columnName = "OPERATING";
        }

        public new void Save()
        {
            // установка всех строк в False, так как адаптер сначала вставляет True и только            
            // потом обновляет бывшую True на False, в результате - 2 True и ошибка...
            dataAccess.ExecuteQuery("UPDATE period_selection SET OPERATING = FALSE");
            base.Save();
        }
    }
}
