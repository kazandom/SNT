using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace SNT
{
    public class TabViewModel : NotifyPropertyChanged
    {
        private string tabName;
        private ContentControl content;       

        public string TabName
        {
            get { return tabName; }
            set
            {
                tabName = value;
                OnPropertyChanged("TabName");
            }
        }

        public ContentControl Content
        {
            get { return content; }
            set
            {
                content = value;
                OnPropertyChanged("Content");
            }
        }        
    }
}
