using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class MainViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<TabViewModel> Items { get; set; }        
        private int selected;
        // статический делегат для открытия вкладок за пределами MainWindow
        public static Action<string, object, object> OpenTab;

        // команда открытия вкладки
        private RelayCommand openTabCommand;
        public RelayCommand OpenTabCommand
        {
            get
            {
                return openTabCommand ??
                  (openTabCommand = new RelayCommand(obj =>
                  {
                      string name = obj as string; // as выдаст null в случае неудачи преобразования
                      openTab(name);
                  }));
            }
        }

        // команда закрытия вкладки
        private RelayCommand closeTabCommand;
        public RelayCommand CloseTabCommand
        {
            get
            {
                return closeTabCommand ??
                  (closeTabCommand = new RelayCommand(obj =>
                  {
                      string name = obj as string;
                      foreach (TabViewModel i in Items)
                      {                        
                          if (i.TabName == name)
                          {
                              Items.Remove(i);
                              return; // иначе исключение - коллекция была изменена, невозможно выполнить перечисление
                          }
                      }                      
                  }));
            }
        }        

        public int Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }
        
        public MainViewModel()
        {
            Items = new ObservableCollection<TabViewModel>();            
            OpenTab = openTab;
        }
        
        // открывает вкладки как из MainWindow, так и с других UserControl благодаря статическому делегату
        void openTab(string firstName, object secondName = null, object id = null)
        {
            string name = firstName + secondName;
            foreach (TabViewModel i in Items)
            {
                if (i.TabName == name)
                {
                    Selected = Items.IndexOf(i); // активация вкладки
                    return;
                }
            }
            TabViewModel item = new TabViewModel
            { TabName = name, Content = UserControl(firstName, id) };
            Items.Add(item);
            Selected = Items.IndexOf(item);
        }

        ContentControl UserControl(string firstName, object id)
        {
            switch (firstName)
            {
                case "Выбор участков":
                    return new UchastkiView();
                case "Участок №":
                    return new CardView(id);
                case "Собственники":
                    return new OwnersView();
                case "Параметры":
                    return new ParametersView();
                case "Взносы":
                    return new VznosyView();
                case "Тарифы":
                    return new TarifyView();
                case "Глобальные параметры":
                    return new GlobalParamsView();
                case "Оплаты":
                    return new PaymentsView();
                case "Отчет Реестр собственнников":
                    return new ReportOwnersView();
                case "Отчет Начисления и оплаты":
                    return new ReportCurrentDolgView();
                case "Реквизиты":
                    return new RekvizityView();
                default:
                    return null;
            }
        }

        // открытие диалоговых окон

        private RelayCommand openRaschetCommand;
        public RelayCommand OpenRaschetCommand
        {
            get
            {
                return openRaschetCommand ??
                    (openRaschetCommand = new RelayCommand(obj =>
                    {                        
                        RaschetView raschetView = new RaschetView();
                        raschetView.ShowDialog();
                    }));
            }
        }

        private RelayCommand openPeriodSelectionCommand;
        public RelayCommand OpenPeriodSelectionCommand
        {
            get
            {
                return openPeriodSelectionCommand ??
                    (openPeriodSelectionCommand = new RelayCommand(obj =>
                    {
                        PeriodSelectionView periodSelectionView = new PeriodSelectionView();
                        periodSelectionView.ShowDialog();
                    }));
            }
        }
    }
}
