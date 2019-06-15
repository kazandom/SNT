using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace SNT
{
    public class NachisleniyaViewModel : NotifyPropertyChanged
    {        
        public SumItogModel SumItog { get; set; }
        public NachislenieModel Nachislenie { get; set; }

        DataView dataViewSumItog;
        public DataView DataViewSumItog
        {
            get { return dataViewSumItog; }
            set
            {
                dataViewSumItog = value;
                OnPropertyChanged("DataViewSumItog");
            }
        }

        DataView dataViewNachislenie;
        public DataView DataViewNachislenie
        {
            get { return dataViewNachislenie; }
            set
            {
                dataViewNachislenie = value;
                OnPropertyChanged("DataViewNachislenie");
            }
        }
        
        DataRowView selectedRowSumItog;       
        public DataRowView SelectedRowSumItog
        {
            get { return selectedRowSumItog; }
            set
            {
                selectedRowSumItog = value;                
                RefreshNachislenie(selectedRowSumItog?.Row["ID_UCHASTOK"], selectedRowSumItog?.Row["PERIOD"]);
                // в DataRowView уже реализован INotifyPropertyChanged
            }
        }

        void RefreshNachislenie(object id, object period)
        {
            // проверка не передается ли "" или null
            if (period is short)
            {                
                Nachislenie = new NachislenieModel(id, period);
                Dictionary = Nachislenie.GetDictionary();
                DataViewNachislenie = Nachislenie.Table.DefaultView;               
            }               
        }

        int selectedSumItog;
        public int SelectedSumItog
        {
            get { return selectedSumItog; }
            set
            {
                selectedSumItog = value;
                OnPropertyChanged("SelectedSumItog");
            }
        }        

        // список взносов
        public ObservableCollection<FirstModel.DictEntry> Dictionary { get; set; }

        private RelayCommand refreshSumItogCommand;
        public RelayCommand RefreshSumItogCommand
        {
            get
            {
                return refreshSumItogCommand ??
                    (refreshSumItogCommand = new RelayCommand(obj =>
                    {
                        SumItog.Refresh();
                        SelectedSumItog = 0;
                    }));
            }
        }

        private RelayCommand showSumItogCommand;
        public RelayCommand ShowSumItogCommand
        {
            get
            {
                return showSumItogCommand ??
                    (showSumItogCommand = new RelayCommand(obj =>
                    {
                        SumItog.Show((bool)obj);
                        SelectedSumItog = 0;
                    }));
            }
        }
       
        private RelayCommand saveNachislenieCommand;
        public RelayCommand SaveNachislenieCommand
        {
            get
            {
                return saveNachislenieCommand ??
                    (saveNachislenieCommand = new RelayCommand(obj =>
                    {
                        Nachislenie?.Save();
                        SumItog.Refresh();
                        SelectedSumItog = 0;
                    }, // проверка на null, т.к. Pokazanie создается не в конструкторе
                    (obj) => Nachislenie?.Changed == true));
            }
        }
        
        public NachisleniyaViewModel(object id)
        {
            SumItog = new SumItogModel(id);            
            DataViewSumItog = SumItog.Table.DefaultView;
            if (DataViewSumItog.Count > 0)
                SelectedRowSumItog = DataViewSumItog[0];
        }
    }
}
