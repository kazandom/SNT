using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public abstract class SecondModel : NotifyPropertyChanged
    {
        protected DataTable table;
        protected DataAccess dataAccess;
        protected string sqlSelect;

        public bool Changed { get; set; }

        public SecondModel()
        {
            table = new DataTable();
            dataAccess = new DataAccess();           
        }

        protected void TableInit()
        {
            dataAccess.FillTable(sqlSelect, table);
            // защита от ошибки в случае если запрос не вернул ни одной строки
            if (table.Rows.Count == 0)            
                table.Rows.InsertAt(table.NewRow(), 0);            
            // событие для активации/деактивации кнопки Сохранить
            table.RowChanged += (object sender, DataRowChangeEventArgs e) => Changed = true;
        }

        public void Save()
        {
            if (dataAccess.UpdateDB(table) == true)
            {
                MessageBox.Show("Успешное сохранение!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Changed = false;
            }
        }
    }
}
