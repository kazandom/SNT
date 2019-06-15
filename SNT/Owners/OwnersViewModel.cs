using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace SNT
{
    class OwnersViewModel : NotifyPropertyChanged
    {        
        public OwnersModel Owners { get; set; }

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

        // сопоставление собственников с участками
        public ObservableCollection<FirstModel.DictEntry> Dictionary { get; set; }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        Owners.Save();
                    }, (obj) => Owners.Changed == true));
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
                        Owners.Add();
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
                        Owners.Delete(SelectedRow.Row);
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
                        Owners.Show((bool)obj);
                    }));
            }
        }

        public OwnersViewModel()
        {
            Owners = new OwnersModel();            
            DataView = Owners.Table.DefaultView;
            Dictionary = Owners.GetDictionary();
        }        
    }   
}
