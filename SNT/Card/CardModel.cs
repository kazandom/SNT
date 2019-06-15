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
    public class CardModel : RaschetModel
    {        
        string sqlSelect;

        public CardModel(object id)
        {            
            sqlSelect =
                "SELECT('Участок № ' || N || ', Аллея/Улица ' || COALESCE(ALLEYA, '-') || ', ' || S || ' кв.м., ' || " +
                "LIST(FAMILIYA || ' ' || COALESCE(IMYA, '') || ' ' || COALESCE(OTCHESTVO, ''), ', ')) AS INFO " +
                "FROM sobstvennik JOIN sobstv_uch ON sobstvennik.ID = ID_SOBSTVENNIK " +
                "RIGHT OUTER JOIN uchastok ON ID_UCHASTOK = uchastok.ID " +
                "WHERE uchastok.ID = " + id + " AND DATA_OUT IS NULL " +
                "GROUP BY N, ALLEYA, S";
            GetCardInfo();
        }

        object cardInfo;
        public object CardInfo
        {
            get { return cardInfo; }
            set
            {
                cardInfo = value;
                OnPropertyChanged("CardInfo");
            }
        }

        void GetCardInfo()
        {
            DataTableReader reader = dataAccess.GetReader(sqlSelect);
            if (reader != null)
            {
                while (reader.Read())
                {
                    CardInfo = reader["INFO"];
                }                
            }
        }        
    }
}
