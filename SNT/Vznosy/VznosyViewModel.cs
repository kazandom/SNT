using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace SNT
{
    class VznosyViewModel : NotifyPropertyChanged
    {        
        public VznosyModel Vznosy { get; set; }

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

        // список глобальных и неглобальных параметров 
        public ObservableCollection<FirstModel.DictEntry> Dictionary { get; set; }

        // список арифметических операторов 
        public ObservableCollection<object> Operators { get; set; }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        Vznosy.Save();
                    }, (obj) => Vznosy.Changed == true));
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
                        Vznosy.Add();
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
                        Vznosy.Delete(SelectedRow.Row);
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
                        Vznosy.Show((bool)obj);
                    }));
            }
        }
        
        public VznosyViewModel()
        {
            Vznosy = new VznosyModel();            
            DataView = Vznosy.Table.DefaultView;
            Dictionary = Vznosy.GetDictionary();
            Operators = Vznosy.GetOperators();
        }        
    }   
}
