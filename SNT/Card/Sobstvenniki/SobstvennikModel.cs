using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class SobstvennikModel : SecondModel
    {       
        public SobstvennikModel(object idSobstvennik)
        {                        
            sqlSelect =
                "SELECT ID, FAMILIYA, IMYA, OTCHESTVO, PHONE, EMAIL, ADDRESS " +
                "FROM sobstvennik WHERE ID = " + idSobstvennik;
            TableInit();
        }
                
        public object Familiya
        {
            get { return table.Rows[0]["FAMILIYA"]; }
            set
            {
                // Чтобы передать в БД значение NULL при "", присваивается DBNull.Value
                // (целесообразно использовать, когда свойство - object)
                table.Rows[0]["FAMILIYA"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Familiya");
            }
        }
        public object Imya
        {
            get { return table.Rows[0]["IMYA"]; }
            set
            {
                table.Rows[0]["IMYA"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Imya");
            }
        }
        public object Otchestvo
        {
            get { return table.Rows[0]["OTCHESTVO"]; }
            set
            {
                table.Rows[0]["OTCHESTVO"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Otchestvo");
            }
        }
        public object Phone
        {
            get { return table.Rows[0]["PHONE"]; }
            set
            {
                table.Rows[0]["PHONE"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Phone");
            }
        }
        public object Email
        {
            get { return table.Rows[0]["EMAIL"]; }
            set
            {
                table.Rows[0]["EMAIL"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Email");
            }
        }
        public object Address
        {
            get { return table.Rows[0]["ADDRESS"]; }
            set
            {
                table.Rows[0]["ADDRESS"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Address");
            }
        }        
    }
}
