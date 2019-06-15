using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SNT
{
    public class UchastkiViewModel : NotifyPropertyChanged
    {                
        public UchastkiModel Uchastki { get; set; }

        AddUchastokModel addUchastok;
        public AddUchastokModel AddUchastok
        {
            get { return addUchastok; }
            set
            {
                addUchastok = value;                
                OnPropertyChanged("AddUchastok");
            }
        }

        DataView dataView;       
        public DataView DataView
        {
            get { return dataView; }
            set
            {
                dataView = value;
                // Необходимо, чтобы в привязке учитывались изменения данного свойства 
                // (не нужно, если свойство - ObservableCollection или DataRowView)
                OnPropertyChanged("DataView");
            }
        }

        // в DataRowView уже реализован INotifyPropertyChanged
        public DataRowView SelectedRow { get; set; }

        // для поиска, так как сам TextBox обновлять не нужно INotifyPropertyChanged не нужен
        public object Fio { get; set; }
        public object N { get; set; }
        
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        AddUchastok = new AddUchastokModel();
                        // циклический вызов диалогового окна в случае ошибки
                        // с сохранением введенных данных
                        while (true)
                        {
                            AddUchastokView addUchastokView = new AddUchastokView(this);
                            if (addUchastokView.ShowDialog() == true)
                            {
                                object id = AddUchastok.Add();
                                if (id != null)
                                {
                                    MessageBox.Show("Успешное добавление!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information); 
                                    MainViewModel.OpenTab?.Invoke("Участок №", AddUchastok.N, id);
                                    Uchastki.Refresh(N, Fio);
                                    break;
                                }
                            }
                            else
                                // нажата кнопка отмена
                                break;
                        }                       
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
                        Uchastki.Delete(SelectedRow.Row);
                    }, (obj) => SelectedRow != null));
            }
        }

        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                    (openCommand = new RelayCommand(obj =>
                    {
                        object n = SelectedRow.Row["N"];
                        object id = SelectedRow.Row["ID"];
                        // вызов статического делегата с проверкой на null
                        MainViewModel.OpenTab?.Invoke("Участок №", n, id);
                    }, (obj) => SelectedRow != null));
            }
        }
        
        private RelayCommand findCommand;
        public RelayCommand FindCommand
        {
            get
            {
                return findCommand ??
                    (findCommand = new RelayCommand(obj =>
                    {
                        Uchastki.Refresh(N, Fio);
                    }));
            }
        }

        public UchastkiViewModel()
        {
            Uchastki = new UchastkiModel();
            DataView = Uchastki.Table.DefaultView;            
        }
    }
}
