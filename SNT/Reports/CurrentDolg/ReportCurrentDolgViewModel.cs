using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms.Integration;

namespace SNT
{
    public class ReportCurrentDolgViewModel : NotifyPropertyChanged
    {
        WindowsFormsHost windowsFormsHost;
        ReportViewer reportViewer;
        DataAccess dataAccess;

        public WindowsFormsHost WindowsFormsHost
        {
            get { return windowsFormsHost; }
            set
            {
                windowsFormsHost = value;
                OnPropertyChanged("WindowsFormsHost");
            }
        }

        public ReportCurrentDolgViewModel()
        {
            // загружает нативные библиотеки из папки SqlServerType (для отчетов, но работает и без данных библиотек)
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);

            dataAccess = new DataAccess();
            WindowsFormsHost = new WindowsFormsHost();
            reportViewer = new ReportViewer();
            WindowsFormsHost.Child = reportViewer;            

            ReportDataSource reportDataSource = new ReportDataSource();
            DataSet dataset = new DataSet();

            dataset.BeginInit();

            reportDataSource.Name = "CurrentDolg"; // имя датасета отчета в .RDLC файле
            reportDataSource.Value = dataset.CURRENT_DOLG;
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
            reportViewer.LocalReport.ReportEmbeddedResource = "SNT.Reports.CurrentDolg.ReportCurrentDolg.rdlc";

            dataset.EndInit();

            // заполнение данных адаптером
            DataSetTableAdapters.CURRENT_DOLGTableAdapter currentDolgTableAdapter = new DataSetTableAdapters.CURRENT_DOLGTableAdapter();
            // замена строки подключения на строку из dataAccess 
            currentDolgTableAdapter.Connection.ConnectionString = dataAccess.connectionString;
            currentDolgTableAdapter.ClearBeforeFill = true;
            currentDolgTableAdapter.Fill(dataset.CURRENT_DOLG);

            reportViewer.RefreshReport();
        }
    }
}
