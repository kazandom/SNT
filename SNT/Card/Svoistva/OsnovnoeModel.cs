using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class OsnovnoeModel : SecondModel
    {                
        public OsnovnoeModel(object id)
        {                        
            sqlSelect =
                "SELECT ID, N, ALLEYA, S, KADASTR, NACHALO, KONEC FROM uchastok WHERE ID = " + id;
            TableInit();
        }

        public object N
        {
            get { return table.Rows[0]["N"]; }
            set
            {
                // Чтобы передать в БД значение NULL при "", присваивается DBNull.Value
                // (целесообразно использовать, когда свойство - object)
                table.Rows[0]["N"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("N");
            }
        }
        public object Alleya
        {
            get { return table.Rows[0]["ALLEYA"]; }
            set
            {                
                table.Rows[0]["ALLEYA"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Alleya");
            }
        }
        public object S
        {
            get { return table.Rows[0]["S"]; }
            set
            {
                table.Rows[0]["S"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("S");                
            }
        }
        public object Kadastr
        {
            get { return table.Rows[0]["KADASTR"]; }
            set
            {
                table.Rows[0]["KADASTR"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Kadastr");
            }
        }
        public object Nachalo
        {
            get { return table.Rows[0]["NACHALO"]; }
            set
            {
                table.Rows[0]["NACHALO"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Nachalo");
            }
        }
        public object Konec
        {
            get { return table.Rows[0]["KONEC"]; }
            set
            {                
                table.Rows[0]["KONEC"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Konec");
            }
        }                
    }
}
