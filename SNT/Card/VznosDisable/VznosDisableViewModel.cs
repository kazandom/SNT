using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace SNT
{
    class VznosDisableViewModel : NotifyPropertyChanged
    {        
        public VznosDisableModel VznosDisable { get; set; }

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

        // список взносов 
        public ObservableCollection<FirstModel.DictEntry> Dictionary { get; set; }

        // да/нет
        public ObservableCollection<object> Booleans { get; set; }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        VznosDisable.Save();
                    }, (obj) => VznosDisable.Changed == true));
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
                        VznosDisable.Add();
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
                        VznosDisable.Delete(SelectedRow.Row);
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
                        VznosDisable.Show((bool)obj);
                    }));
            }
        }
        
        public VznosDisableViewModel(object id)
        {
            VznosDisable = new VznosDisableModel(id);            
            DataView = VznosDisable.Table.DefaultView;
            Dictionary = VznosDisable.GetDictionary();
            Booleans = VznosDisable.GetBooleans();
        }        
    }   
}
