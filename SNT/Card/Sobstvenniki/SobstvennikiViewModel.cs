using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace SNT
{
    public class SobstvennikiViewModel : NotifyPropertyChanged
    {        
        public SobstvUchModel SobstvUch { get; set; }
        
        // с OnPropertyChanged, т.к. имеется привязка
        SobstvennikModel sobstvennik;
        public SobstvennikModel Sobstvennik
        {
            get { return sobstvennik; }
            set
            {
                sobstvennik = value;
                OnPropertyChanged("Sobstvennik");
            }
        }

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
        
        DataRowView selectedRow;       
        public DataRowView SelectedRow
        {
            get { return selectedRow; }
            set
            {
                selectedRow = value;
                // при selectedRow = null - передается null
                RefreshSobstvennik(selectedRow?.Row["ID_SOBSTVENNIK"]);
                // в DataRowView уже реализован INotifyPropertyChanged
            }
        }

        void RefreshSobstvennik (object idSobstvennik)
        {
            // проверка не передается ли "" или null (is int)
            if (idSobstvennik is int)                         
                Sobstvennik = new SobstvennikModel(idSobstvennik);            
        }

        int selected;
        public int Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }

        // список собственников 
        public ObservableCollection<FirstModel.DictEntry> Dictionary { get; set; }
        
        private RelayCommand saveSobstvUchCommand;
        public RelayCommand SaveSobstvUchCommand
        {
            get
            {
                return saveSobstvUchCommand ??
                    (saveSobstvUchCommand = new RelayCommand(obj =>
                    {
                        SobstvUch.Save();
                        Selected = 0;
                    }, (obj) => SobstvUch.Changed == true));
            }
        }

        private RelayCommand addSobstvUchCommand;
        public RelayCommand AddSobstvUchCommand
        {
            get
            {
                return addSobstvUchCommand ??
                    (addSobstvUchCommand = new RelayCommand(obj =>
                    {
                        SobstvUch.Add();
                        Selected = 0;
                    }));
            }
        }

        private RelayCommand deleteSobstvUchCommand;
        public RelayCommand DeleteSobstvUchCommand
        {
            get
            {
                return deleteSobstvUchCommand ??
                    (deleteSobstvUchCommand = new RelayCommand(obj =>
                    {
                        SobstvUch.Delete(SelectedRow.Row);
                        Selected = 0;
                    }, (obj) => SelectedRow != null));
            }
        }

        private RelayCommand showSobstvUchCommand;
        public RelayCommand ShowSobstvUchCommand
        {
            get
            {
                return showSobstvUchCommand ??
                    (showSobstvUchCommand = new RelayCommand(obj =>
                    {
                        SobstvUch.Show((bool)obj);
                        Selected = 0;
                    }));
            }
        }

        private RelayCommand saveSobstvennikCommand;
        public RelayCommand SaveSobstvennikCommand
        {
            get
            {
                return saveSobstvennikCommand ??
                    (saveSobstvennikCommand = new RelayCommand(obj =>
                    {
                        Sobstvennik?.Save(); // ? на всякий случай
                    }, // проверка на null, т.к. Sobstvennik создается не в конструкторе
                    (obj) => Sobstvennik?.Changed == true));
            }
        }

        public SobstvennikiViewModel(object id)
        {
            SobstvUch = new SobstvUchModel(id);            
            DataView = SobstvUch.Table.DefaultView;
            if (DataView.Count > 0)
                SelectedRow = DataView[0];
            Dictionary = SobstvUch.GetDictionary();
        }
    }
}
