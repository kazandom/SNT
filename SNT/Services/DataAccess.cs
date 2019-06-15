using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SNT
{
    public class DataAccess
    {
        public string connectionString { get; private set; }
        FbDataAdapter adapter;
        bool isEmbedded;

        public DataAccess()
        {
            string connectionName = "DefaultConnection"; // "EmbeddedConnection", если используется встроенное соединение
            // получаем строку подключения из app.config
            connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            // дополняем строку логином и паролем с помощью построителя строк подключения
            FbConnectionStringBuilder builder = new FbConnectionStringBuilder(connectionString);                        
            builder.UserID = "SYSDBA";
            builder.Password = "password";
            connectionString = builder.ToString();
            isEmbedded = connectionString.Contains("server type=1");
        }

        // решение проблемы с кодировкой исключений при встроенном соединений
        string Unicode(string message)
        {
            if (isEmbedded == true)
                // декодирует строку "windows-1251" в байты, байты кодирует в строку "utf-8"
                return Encoding.UTF8.GetString(Encoding.GetEncoding(1251).GetBytes(message));
            else
                return message;
        }

        public void FillTable(string sql, DataTable table)
        {                     
            FbConnection connection = null;
            try
            {
                connection = new FbConnection(connectionString);
                connection.Open();
                adapter = new FbDataAdapter(sql, connection);
                table.Clear();
                adapter.Fill(table);               
            }
            catch (Exception ex)
            {
                MessageBox.Show(Unicode(ex.Message), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);                
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
        
        public bool UpdateDB(DataTable table)
        {
            FbCommandBuilder commandBuilder = new FbCommandBuilder(adapter);
            try
            {
                adapter.Update(table);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Unicode(ex.Message), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public DataTableReader GetReader(string sql)
        {           
            FbConnection connection = null;
            try
            {
                connection = new FbConnection(connectionString);
                connection.Open();
                FbCommand command = new FbCommand(sql, connection);
                FbDataReader reader = command.ExecuteReader();
                // с объектом FbDataReader можно работать только когда подключение открыто,
                // поэтому для передачи его во вне помещаем его в подходящий контейнер
                DataTable table = new DataTable();
                table.Load(reader);
                //reader.Close();
                return table.CreateDataReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Unicode(ex.Message), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public object ExecuteQuery(string sql, object first = null, object second = null,
            object third = null, object fourth = null, object fifth = null, object sixth = null)
        {
            FbConnection connection = null;
            try
            {
                connection = new FbConnection(connectionString);
                connection.Open();
                FbCommand command = new FbCommand(sql, connection);
                command.Parameters.Add(new FbParameter("@p1", first));
                command.Parameters.Add(new FbParameter("@p2", second));
                command.Parameters.Add(new FbParameter("@p3", third));
                command.Parameters.Add(new FbParameter("@p4", fourth));
                command.Parameters.Add(new FbParameter("@p5", fifth));
                command.Parameters.Add(new FbParameter("@p6", sixth));
                FbParameter outParam = new FbParameter
                {
                    ParameterName = "@id",
                    FbDbType = FbDbType.Integer,
                    Direction = ParameterDirection.Output // параметр выходной
                };
                command.Parameters.Add(outParam);
                command.ExecuteNonQuery();                
                return outParam.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Unicode(ex.Message), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);                
                return null;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public bool ExecuteRaschetProcedure(object period, object idUchastok = null, object idService = null)
        {
            FbConnection connection = null;
            try
            {
                connection = new FbConnection(connectionString);
                connection.Open();                
                FbCommand command = new FbCommand("RASCHET", connection);                
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new FbParameter("@period", period));
                FbParameter two = new FbParameter { ParameterName = "@id_uchastok" };
                if (idUchastok != null)
                    two.Value = idUchastok;
                else
                    two.IsNullable = true;
                command.Parameters.Add(two);
                FbParameter three = new FbParameter { ParameterName = "@id_service" };
                if (idService != null)
                    three.Value = idService; 
                else
                    three.IsNullable = true;
                command.Parameters.Add(three);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Unicode(ex.Message), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
    }
}
