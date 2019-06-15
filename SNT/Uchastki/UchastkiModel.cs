using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class UchastkiModel
    {
        public DataTable Table { get; private set; }
        DataAccess dataAccess;
        object n;
        object fio;
        string sqlSelect;
        string searchNSql;
        string searchFioSql;

        public UchastkiModel()
        {
            Table = new DataTable();
            dataAccess = new DataAccess();           
            //Refresh(n, fio);
        }
        
        public void Refresh(object n, object fio)
        {
            // запоминание поисковых критериев, чтобы можно было обновляться с учетом данных критерий
            this.n = n;
            this.fio = fio;
            if (n != null && n as string != "")
                searchNSql = "AND N = '" + n + "' ";
            else
                searchNSql = null;
            if (fio != null && fio as string != "")
                searchFioSql = "HAVING LIST(FAMILIYA || ' ' || COALESCE(IMYA, '') || ' ' || COALESCE(OTCHESTVO, '')) " +
                    "CONTAINING '" + fio + "' ";
            else
                searchFioSql = null;
            sqlSelect =
                "SELECT uchastok.ID, N, ALLEYA, S, KADASTR, " +
                "LIST(FAMILIYA || ' ' || COALESCE(IMYA, '') || ' ' || COALESCE(OTCHESTVO, ''), ', ') AS NAME, " +
                "LIST(DOCUMENT, ', ') AS DOCUMENT, LIST(PHONE, ', ') AS PHONE FROM sobstvennik " +
                "JOIN sobstv_uch ON sobstvennik.ID = ID_SOBSTVENNIK " +
                "RIGHT OUTER JOIN uchastok ON ID_UCHASTOK = uchastok.ID " +
                "WHERE DATA_OUT IS NULL " + searchNSql +
                "GROUP BY uchastok.ID, N, ALLEYA, S, KADASTR " + searchFioSql +
                "ORDER BY ALLEYA, N";
            dataAccess.FillTable(sqlSelect, Table);            
        }

        public void Delete(DataRow dataRow)
        // переданный dataRow соответствует одной из строк Table
        {
            var result = MessageBox.Show("Действительно удалить выбранный участок?", "Подтверждение", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                dataRow.Delete();
                if (dataAccess.UpdateDB(Table) == true)
                    MessageBox.Show("Успешное удаление!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    Refresh(n, fio);                
            }            
        }        
    }
}
