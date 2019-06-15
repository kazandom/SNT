using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class AddUchastokModel : NotifyPropertyChanged
    {
        DataAccess dataAccess;        

        public AddUchastokModel()
        {
            dataAccess = new DataAccess();
        }
       
        object n;
        public object N
        {
            get { return n; }
            set
            {
                n = value;
                OnPropertyChanged("N");
            }
        }
        object alleya;
        public object Alleya
        {
            get { return alleya; }
            set
            {
                alleya = value;
                OnPropertyChanged("Alleya");
            }
        }
        object s;
        public object S
        {
            get { return s; }
            set
            {
                s = value;
                OnPropertyChanged("S");                
            }
        }
        object kadastr;
        public object Kadastr
        {
            get { return kadastr; }
            set
            {
                kadastr = value;
                OnPropertyChanged("Kadastr");
            }
        }
        object nachalo;
        public object Nachalo
        {
            get { return nachalo; }
            set
            {
                nachalo = value;                
                OnPropertyChanged("Nachalo");
            }
        }

        public object Add()
        {
            string sql = "INSERT INTO uchastok(N, ALLEYA, S, KADASTR, NACHALO) " +
                "VALUES (@p1, @p2, @p3, @p4, @p5) RETURNING ID";
            object id = dataAccess.ExecuteQuery(sql, N, Alleya, S, Kadastr, Nachalo);
            if (id != null)
                return id;
            else
                return null;
        }
    }
}
