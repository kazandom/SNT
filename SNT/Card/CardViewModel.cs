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
    public class CardViewModel : NotifyPropertyChanged
    {    
        // UserControl'ы вкладок

        ContentControl svoistva;
        public ContentControl Svoistva
        {
            get { return svoistva; }
            set
            {
                svoistva = value;
                OnPropertyChanged("Svoistva");
            }
        }

        ContentControl sobstvenniki;
        public ContentControl Sobstvenniki
        {
            get { return sobstvenniki; }
            set
            {
                sobstvenniki = value;
                OnPropertyChanged("Sobstvenniki");
            }
        }

        ContentControl counters;
        public ContentControl Counters
        {
            get { return counters; }
            set
            {
                counters = value;
                OnPropertyChanged("Counters");
            }
        }

        ContentControl nachisleniya;
        public ContentControl Nachisleniya
        {
            get { return nachisleniya; }
            set
            {
                nachisleniya = value;
                OnPropertyChanged("Nachisleniya");
            }
        }

        ContentControl oplaty;
        public ContentControl Oplaty
        {
            get { return oplaty; }
            set
            {
                oplaty = value;
                OnPropertyChanged("Oplaty");
            }
        }

        ContentControl vznosDisable;
        public ContentControl VznosDisable
        {
            get { return vznosDisable; }
            set
            {
                vznosDisable = value;
                OnPropertyChanged("VznosDisable");
            }
        }

        // взаимодействие в самом Card

        object id; // id участка

        CardModel card;
        public CardModel Card
        {
            get { return card; }
            set
            {
                card = value;
                OnPropertyChanged("Card");
            }
        }

        // список периодов 
        public ObservableCollection<object> Periods { get; set; }

        object selectedPeriod;
        public object SelectedPeriod
        {
            get { return selectedPeriod; }
            set
            {
                selectedPeriod = value;
                OnPropertyChanged("SelectedPeriod");
            }
        }

        private RelayCommand raschetCommand;
        public RelayCommand RaschetCommand
        {
            get
            {
                return raschetCommand ??
                    (raschetCommand = new RelayCommand(obj =>
                    {
                        Card.Raschet(SelectedPeriod, id);
                    }));
            }
        }

        public CardViewModel(object id)
        {
            this.id = id;
            Card = new CardModel(id);
            Periods = Card.GetPeriods();
            if (Periods.Count > 0)
                SelectedPeriod = Periods[0];
            Svoistva = new SvoistvaView(id);
            Sobstvenniki = new SobstvennikiView(id);
            Counters = new CountersView(id);
            Nachisleniya = new NachisleniyaView(id);
            Oplaty = new OplatyView(id);
            VznosDisable = new VznosDisableView(id);
        }
    }
}
