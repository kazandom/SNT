using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace SNT
{
    class GlobalParamsViewModel : NotifyPropertyChanged
    {        
        public GlobalParamsModel GlobalParams { get; set; }

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

        // список глобальных параметров 
        public ObservableCollection<FirstModel.DictEntry> Dictionary { get; set; }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        GlobalParams.Save();
                    }, (obj) => GlobalParams.Changed == true));
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
                        GlobalParams.Add();
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
                        GlobalParams.Delete(SelectedRow.Row);
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
                        GlobalParams.Show((bool)obj);
                    }));
            }
        }
        
        public GlobalParamsViewModel()
        {
            GlobalParams = new GlobalParamsModel();            
            DataView = GlobalParams.Table.DefaultView;
            Dictionary = GlobalParams.GetDictionary();           
        }        
    }   
}
