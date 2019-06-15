using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public abstract class FirstModel
    {
        public DataTable Table { get; private set; }
        public bool Changed { get; set; }
        protected DataAccess dataAccess;
        protected string firstSqlSelect;
        protected string secondAllSqlSelect;
        protected string secondFilterSqlSelect;
        protected string currentSqlSelect;
        protected string dictionarySqlSelect;
        protected object columnValue;
        protected string columnName;
        protected object columnValueAdditional;
        protected string columnNameAdditional;
        protected bool isRowDeleted;

        public FirstModel(object columnValue = null, object columnValueAdditional = null)
        {
            this.columnValue = columnValue;
            this.columnValueAdditional = columnValueAdditional;
            Table = new DataTable();
            dataAccess = new DataAccess();            
        }

        protected void TableInit()
        {            
            currentSqlSelect = firstSqlSelect + secondFilterSqlSelect;
            dataAccess.FillTable(currentSqlSelect, Table);
            // события для активации/деактивации кнопки Сохранить
            Table.RowChanged += TableChanged;
            Table.RowDeleted += TableChanged;
        }

        private void TableChanged(object sender, DataRowChangeEventArgs e)
        {
            Changed = true;
        }

        public void Refresh()
        {
            dataAccess.FillTable(currentSqlSelect, Table);
            Changed = false;
        }

        public void Save()
        {
            if (dataAccess.UpdateDB(Table) == true)
            {
                MessageBox.Show("Успешное сохранение!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                // перезаполнение во избежание ошибки нарушения параллелизма БД 
                // при обновлении добавленной строки (при FbCommandBuilder)
                Refresh();
            }
            else
            {
                if (isRowDeleted == true)
                    Refresh();
            }                
            isRowDeleted = false;
        }

        public void Add()
        {
            DataRow newRow = Table.NewRow();
            if (columnValue != null && columnName != null)
            {
                newRow[columnName] = columnValue;
                if (columnValueAdditional != null && columnNameAdditional != null)
                    newRow[columnNameAdditional] = columnValueAdditional;
            }                
            // вставка сверху
            Table.Rows.InsertAt(newRow, 0);
        }

        public void Delete(DataRow dataRow)
        {
            dataRow.Delete();
            isRowDeleted = true;
        }

        public void Show(bool isChecked)
        {
            if (isChecked == false)
                currentSqlSelect = firstSqlSelect + secondFilterSqlSelect;
            else
                currentSqlSelect = firstSqlSelect + secondAllSqlSelect;

            Refresh();
        }

        public ObservableCollection<DictEntry> GetDictionary()
        {
            // поскольку в объекте dataAccess объект FbDataAdapter уже мониторит одну таблицу
            // на предмет изменений, получение данных происходит без помощи адаптера  
            DataTableReader reader = dataAccess.GetReader(dictionarySqlSelect);
            ObservableCollection<DictEntry> dictionary = new ObservableCollection<DictEntry>();
            if (reader != null)
            {
                while (reader.Read())
                {
                    DictEntry dictModel = new DictEntry()
                    {
                        Id = reader.GetValue(0),
                        Name = reader.GetValue(1)
                    };
                    dictionary.Add(dictModel);
                }
            }
            return dictionary;
        }
        
        public class DictEntry
        {
            public object Id { get; set; }
            public object Name { get; set; }
        }
    }
}
