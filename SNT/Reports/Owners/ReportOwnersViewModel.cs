using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms.Integration;

namespace SNT
{
    public class ReportOwnersViewModel : NotifyPropertyChanged
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

        public ReportOwnersViewModel()
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

            reportDataSource.Name = "Owners"; // имя датасета отчета в .RDLC файле
            reportDataSource.Value = dataset.OWNERS;
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
            reportViewer.LocalReport.ReportEmbeddedResource = "SNT.Reports.Owners.ReportOwners.rdlc";

            dataset.EndInit();

            // заполнение данных адаптером
            DataSetTableAdapters.OWNERSTableAdapter ownersTableAdapter = new DataSetTableAdapters.OWNERSTableAdapter();
            // замена строки подключения на строку из dataAccess 
            ownersTableAdapter.Connection.ConnectionString = dataAccess.connectionString;
            ownersTableAdapter.ClearBeforeFill = true;
            ownersTableAdapter.Fill(dataset.OWNERS);

            reportViewer.RefreshReport();
        }
    }
}
