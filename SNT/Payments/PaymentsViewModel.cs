using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace SNT
{
    class PaymentsViewModel : NotifyPropertyChanged
    {        
        public PaymentsModel Payments { get; set; }

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

        // в DataRowView уже реализован INotifyPropertyChanged
        public DataRowView SelectedRow { get; set; }

        // список участков 
        public ObservableCollection<FirstModel.DictEntry> Dictionary { get; set; }

        // список периодов 
        public ObservableCollection<FirstModel.DictEntry> Periods { get; set; }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        Payments.Save();
                    }, (obj) => Payments.Changed == true));
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
                        Payments.Add();
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
                        Payments.Delete(SelectedRow.Row);
                    }, (obj) => SelectedRow != null));
            }
        }

        private RelayCommand showCommand;
        public RelayCommand ShowCommand
        {
            get
            {
                return showCommand ??
                    (showCommand = new RelayCommand(obj =>
                    {
                        Payments.Show((bool)obj);
                    }));
            }
        }
        
        public PaymentsViewModel()
        {
            Payments = new PaymentsModel();            
            DataView = Payments.Table.DefaultView;
            Dictionary = Payments.GetDictionary(0);
            Periods = Payments.GetDictionary(1);
        }        
    }   
}
