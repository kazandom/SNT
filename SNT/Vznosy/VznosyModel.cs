using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class VznosyModel : FirstModel
    {        
        public VznosyModel() : base(false, false)
        {            
            firstSqlSelect =
                "SELECT ID, NAME, NACHALO, KONEC, SCHETCHIK_, S_UCH, ID_PARAMETR_1, ID_PARAMETR_2, " +
                "ID_PARAMETR_3, ID_PARAMETR_4, OPERATOR_1, OPERATOR_2, OPERATOR_3, OPERATOR_4 FROM service ";
            secondAllSqlSelect =
                "ORDER BY NAME";
            secondFilterSqlSelect =
                "WHERE KONEC IS NULL OR KONEC >= (SELECT PERIOD FROM period_selection WHERE OPERATING IS TRUE) " +
                "ORDER BY NAME";            
            TableInit();
            columnName = "SCHETCHIK_";
            columnNameAdditional = "S_UCH";
        }

        public new ObservableCollection<DictEntry> GetDictionary()
        {
            dictionarySqlSelect = "SELECT ID, (NAME || ' (глобальный)') FROM parametr WHERE GLOBAL_ IS TRUE";
            var dictionary = base.GetDictionary();
            dictionarySqlSelect = "SELECT ID, (NAME || ' (по участку)') FROM parametr WHERE GLOBAL_ IS FALSE";
            foreach (DictEntry dictEntry in base.GetDictionary())
                dictionary.Add(dictEntry);
            return dictionary;
        }

        public ObservableCollection<object> GetOperators()
        {            
            ObservableCollection<object> operators = new ObservableCollection<object>
            {
                // запаковка значений в Int16 во избежание ошибки при преобразовании,                
                // т.к. по умолчанию числа пакуются в object Int32, а в БД хранится Int16 (SmallInt) 
                new DictEntry { Id = (short)1, Name = "+" },
                new DictEntry { Id = (short)2, Name = "-" },
                new DictEntry { Id = (short)3, Name = "*" },
                new DictEntry { Id = (short)4, Name = "/" }
            };            
            return operators;
        }
    }
}
