using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace SNT
{
    class SvoistvaViewModel : NotifyPropertyChanged
    {
        // с OnPropertyChanged, т.к. имеется привязка
        OsnovnoeModel osnovnoe;
        public OsnovnoeModel Osnovnoe
        {
            get { return osnovnoe; }
            set
            {
                osnovnoe = value;
                OnPropertyChanged("Osnovnoe");
            }
        }

        public ParamUchModel ParamUch { get; set; }

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

        // список неглобальных параметров 
        public ObservableCollection<FirstModel.DictEntry> Dictionary { get; set; }
        
        private RelayCommand saveOsnovnoeCommand;
        public RelayCommand SaveOsnovnoeCommand
        {
            get
            {
                return saveOsnovnoeCommand ??
                    (saveOsnovnoeCommand = new RelayCommand(obj =>
                    {
                        Osnovnoe.Save();
                    }, (obj) => Osnovnoe.Changed == true));
            }
        }

        private RelayCommand saveParamUchCommand;
        public RelayCommand SaveParamUchCommand
        {
            get
            {
                return saveParamUchCommand ??
                    (saveParamUchCommand = new RelayCommand(obj =>
                    {
                        ParamUch.Save();                        
                    }, (obj) => ParamUch.Changed == true));
            }
        }

        private RelayCommand addParamUchCommand;
        public RelayCommand AddParamUchCommand
        {
            get
            {
                return addParamUchCommand ??
                    (addParamUchCommand = new RelayCommand(obj =>
                    {
                        ParamUch.Add();
                    }));
            }
        }

        private RelayCommand deleteParamUchCommand;
        public RelayCommand DeleteParamUchCommand
        {
            get
            {
                return deleteParamUchCommand ??
                    (deleteParamUchCommand = new RelayCommand(obj =>
                    {
                        ParamUch.Delete(SelectedRow.Row);
                    }, (obj) => SelectedRow != null));
            }
        }

        private RelayCommand showParamUchCommand;
        public RelayCommand ShowParamUchCommand
        {
            get
            {
                return showParamUchCommand ??
                    (showParamUchCommand = new RelayCommand(obj =>
                    {
                        ParamUch.Show((bool)obj);
                    }));
            }
        }
        
        public SvoistvaViewModel(object id)
        {                        
            Osnovnoe = new OsnovnoeModel(id);
            ParamUch = new ParamUchModel(id);
            DataView = ParamUch.Table.DefaultView;
            Dictionary = ParamUch.GetDictionary();
        }        
    }   
}
