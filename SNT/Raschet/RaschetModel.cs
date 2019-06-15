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
    public class RaschetModel : NotifyPropertyChanged
    {
        protected DataAccess dataAccess;        

        public RaschetModel()
        {
            dataAccess = new DataAccess();
        }

        public ObservableCollection<object> GetPeriods()
        {
            DataTableReader reader = dataAccess.GetReader("SELECT PERIOD FROM period_selection " +
                "WHERE PERIOD <= (SELECT PERIOD FROM period_selection WHERE OPERATING IS TRUE) " +
                "ORDER BY PERIOD DESC");
            ObservableCollection<object> periods = new ObservableCollection<object>();
            if (reader != null)
            {
                while (reader.Read())
                {
                    periods.Add(reader["PERIOD"]);
                }
            }
            return periods;
        }

        public void Raschet(object period, object idUchastok = null)
        {
            if (dataAccess.ExecuteRaschetProcedure(period, idUchastok) == true)
                MessageBox.Show("Успешное выполнение!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
