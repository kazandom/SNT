using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace SNT
{
    public class CountersViewModel : NotifyPropertyChanged
    {        
        public SchetchikModel Schetchik { get; set; }
        public PokazanieModel Pokazanie { get; set; }

        DataView dataViewSchetchik;
        public DataView DataViewSchetchik
        {
            get { return dataViewSchetchik; }
            set
            {
                dataViewSchetchik = value;
                OnPropertyChanged("DataViewSchetchik");
            }
        }

        DataView dataViewPokazanie;
        public DataView DataViewPokazanie
        {
            get { return dataViewPokazanie; }
            set
            {
                dataViewPokazanie = value;
                OnPropertyChanged("DataViewPokazanie");
            }
        }
        
        DataRowView selectedRowSchetchik;       
        public DataRowView SelectedRowSchetchik
        {
            get { return selectedRowSchetchik; }
            set
            {
                selectedRowSchetchik = value;                
                RefreshPokazanie(selectedRowSchetchik?.Row["ID"]);
                // в DataRowView уже реализован INotifyPropertyChanged
            }
        }

        void RefreshPokazanie(object idSchetchik)
        {
            // проверка не передается ли "" или null (is int)
            if (idSchetchik is int)
            {                
                Pokazanie = new PokazanieModel(idSchetchik);
                IsCheckedPokazanie = false;
                DataViewPokazanie = Pokazanie.Table.DefaultView;               
            }               
        }

        int selectedSchetchik;
        public int SelectedSchetchik
        {
            get { return selectedSchetchik; }
            set
            {
                selectedSchetchik = value;
                OnPropertyChanged("SelectedSchetchik");
            }
        }

        public DataRowView SelectedRowPokazanie { get; set; }

        // список взносов, рассчитываемых по счетчику
        public ObservableCollection<FirstModel.DictEntry> Dictionary { get; set; }

        // для смены состояния чекбоса в RefreshPokazanie
        bool isCheckedPokazanie;
        public bool IsCheckedPokazanie
        {
            get { return isCheckedPokazanie; }
            set
            {
                isCheckedPokazanie = value;
                OnPropertyChanged("IsCheckedPokazanie");
            }
        }

        // команды счетчиков

        private RelayCommand saveSchetchikCommand;
        public RelayCommand SaveSchetchikCommand
        {
            get
            {
                return saveSchetchikCommand ??
                    (saveSchetchikCommand = new RelayCommand(obj =>
                    {
                        Schetchik.Save();
                        SelectedSchetchik = 0;
                    }, (obj) => Schetchik.Changed == true));
            }
        }

        private RelayCommand addSchetchikCommand;
        public RelayCommand AddSchetchikCommand
        {
            get
            {
                return addSchetchikCommand ??
                    (addSchetchikCommand = new RelayCommand(obj =>
                    {
                        Schetchik.Add();
                        SelectedSchetchik = 0;
                    }));
            }
        }

        private RelayCommand deleteSchetchikCommand;
        public RelayCommand DeleteSchetchikCommand
        {
            get
            {
                return deleteSchetchikCommand ??
                    (deleteSchetchikCommand = new RelayCommand(obj =>
                    {
                        Schetchik.Delete(SelectedRowSchetchik.Row);
                        SelectedSchetchik = 0;
                    }, (obj) => SelectedRowSchetchik != null));
            }
        }

        private RelayCommand showSchetchikCommand;
        public RelayCommand ShowSchetchikCommand
        {
            get
            {
                return showSchetchikCommand ??
                    (showSchetchikCommand = new RelayCommand(obj =>
                    {
                        Schetchik.Show((bool)obj);
                        SelectedSchetchik = 0;
                    }));
            }
        }

        // команды показаний

        private RelayCommand savePokazanieCommand;
        public RelayCommand SavePokazanieCommand
        {
            get
            {
                return savePokazanieCommand ??
                    (savePokazanieCommand = new RelayCommand(obj =>
                    {
                        Pokazanie?.Save(); // ? на всякий случай
                    }, // проверка на null, т.к. Pokazanie создается не в конструкторе
                    (obj) => Pokazanie?.Changed == true));
            }
        }

        private RelayCommand addPokazanieCommand;
        public RelayCommand AddPokazanieCommand
        {
            get
            {
                return addPokazanieCommand ??
                    (addPokazanieCommand = new RelayCommand(obj =>
                    {
                        Pokazanie?.Add();                        
                    }, (obj) => Pokazanie != null));
            }
        }

        private RelayCommand deletePokazanieCommand;
        public RelayCommand DeletePokazanieCommand
        {
            get
            {
                return deletePokazanieCommand ??
                    (deletePokazanieCommand = new RelayCommand(obj =>
                    {
                        Pokazanie?.Delete(SelectedRowPokazanie.Row);                        
                    }, (obj) => SelectedRowPokazanie != null));
            }
        }
        
        private RelayCommand showPokazanieCommand;
        public RelayCommand ShowPokazanieCommand
        {
            get
            {
                return showPokazanieCommand ??
                    (showPokazanieCommand = new RelayCommand(obj =>
                    {                        
                        Pokazanie?.Show(IsCheckedPokazanie);                        
                    }, (obj) => Pokazanie != null));
            }
        }        

        public CountersViewModel(object id)
        {
            Schetchik = new SchetchikModel(id);            
            DataViewSchetchik = Schetchik.Table.DefaultView;
            if (DataViewSchetchik.Count > 0)
                SelectedRowSchetchik = DataViewSchetchik[0];
            Dictionary = Schetchik.GetDictionary();
        }
    }
}
