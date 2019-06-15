using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class ParametersModel : FirstModel
    {        
        public ParametersModel() : base(false)
        {
            firstSqlSelect =
                "SELECT ID, NAME, GLOBAL_ FROM parametr ORDER BY NAME";            
            TableInit();
            columnName = "GLOBAL_";
        }        
    }
}
