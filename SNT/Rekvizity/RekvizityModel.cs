using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class RekvizityModel : SecondModel
    {                
        public RekvizityModel()
        {
            sqlSelect =
                "SELECT NAZVANIE, PREDSEDATEL, BUHGALTER, INN, KPP, BANK, BIK, " +
                "KORRES_SCHET, RASCH_SCHET FROM rekvizity " +
                "FETCH FIRST ROW ONLY";
            TableInit();
        }

        public object Nazvanie
        {
            get { return table.Rows[0]["NAZVANIE"]; }
            set
            {
                // Чтобы передать в БД значение NULL при "", присваивается DBNull.Value
                // (целесообразно использовать, когда свойство - object)
                table.Rows[0]["NAZVANIE"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Nazvanie");
            }
        }
        public object Predsedatel
        {
            get { return table.Rows[0]["PREDSEDATEL"]; }
            set
            {                
                table.Rows[0]["PREDSEDATEL"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Predsedatel");
            }
        }
        public object Buhgalter
        {
            get { return table.Rows[0]["BUHGALTER"]; }
            set
            {
                table.Rows[0]["BUHGALTER"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Buhgalter");                
            }
        }
        public object Inn
        {
            get { return table.Rows[0]["INN"]; }
            set
            {
                table.Rows[0]["INN"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Inn");
            }
        }
        public object Kpp
        {
            get { return table.Rows[0]["KPP"]; }
            set
            {
                table.Rows[0]["KPP"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Kpp");
            }
        }
        public object Bank
        {
            get { return table.Rows[0]["BANK"]; }
            set
            {                
                table.Rows[0]["BANK"] = value as string == "" ? DBNull.Value : value;                
                OnPropertyChanged("Bank");
            }
        }

        public object Bik
        {
            get { return table.Rows[0]["BIK"]; }
            set
            {
                table.Rows[0]["BIK"] = value as string == "" ? DBNull.Value : value;
                OnPropertyChanged("Bik");
            }
        }

        public object KorresSchet
        {
            get { return table.Rows[0]["KORRES_SCHET"]; }
            set
            {
                table.Rows[0]["KORRES_SCHET"] = value as string == "" ? DBNull.Value : value;
                OnPropertyChanged("KorresSchet");
            }
        }

        public object RaschSchet
        {
            get { return table.Rows[0]["RASCH_SCHET"]; }
            set
            {
                table.Rows[0]["RASCH_SCHET"] = value as string == "" ? DBNull.Value : value;
                OnPropertyChanged("RaschSchet");
            }
        }
    }
}
