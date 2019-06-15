using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class SumItogModel : FirstModel
    {        
        public SumItogModel(object id) : base(id)
        {
            firstSqlSelect =
                "SELECT ID_UCHASTOK, PERIOD, DOLG, K_OPLATE, ITOGO_K_OPLATE, OPLACHENO " +
                "FROM sum_itog(" + id + ")";            
            secondFilterSqlSelect =
                "FETCH FIRST 4 ROWS ONLY";
            TableInit();
            // columnName null, т.к. строки не будут добавляться вручную
        }
    }
}
