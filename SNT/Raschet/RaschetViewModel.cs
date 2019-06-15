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
    public class RaschetViewModel : NotifyPropertyChanged
    {
        RaschetModel raschet;
        public RaschetModel Raschet
        {
            get { return raschet; }
            set
            {
                raschet = value;
                OnPropertyChanged("Raschet");
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
                        Raschet.Raschet(SelectedPeriod);
                    }));
            }
        }

        public RaschetViewModel()
        {
            Raschet = new RaschetModel();
            Periods = Raschet.GetPeriods();
            if (Periods.Count > 0)
                SelectedPeriod = Periods[0];            
        }
    }
}
