using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace SNT
{
    public class PeriodSelectionViewModel : NotifyPropertyChanged
    {        
        public PeriodSelectionModel PeriodSelection { get; set; }

        DataView dataView;
        public DataView DataView
        {
            get { return dataView; }
            set
            {
                dataView = value;
                OnPropertyChanged("DataView");
            }
        }

        public DataRowView SelectedRow { get; set; }        

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        PeriodSelection.Save();
                    }, (obj) => PeriodSelection.Changed == true));
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        PeriodSelection.Add();
                    }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand(obj =>
                    {
                        PeriodSelection.Delete(SelectedRow.Row);
                    }, (obj) => SelectedRow != null));
            }
        }

        public PeriodSelectionViewModel()
        {
            PeriodSelection = new PeriodSelectionModel();
            DataView = PeriodSelection.Table.DefaultView;            
        }
    }
}
