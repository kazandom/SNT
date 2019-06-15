using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace SNT
{
    class RekvizityViewModel : NotifyPropertyChanged
    {
        // с OnPropertyChanged, т.к. имеется привязка
        RekvizityModel rekvizity;
        public RekvizityModel Rekvizity
        {
            get { return rekvizity; }
            set
            {
                rekvizity = value;
                OnPropertyChanged("Rekvizity");
            }
        }        
        
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        Rekvizity.Save();
                    }, (obj) => Rekvizity.Changed == true));
            }
        }        
        
        public RekvizityViewModel()
        {
            Rekvizity = new RekvizityModel();            
        }        
    }   
}
