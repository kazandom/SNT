﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SNT
{
    /// <summary>
    /// Логика взаимодействия для VznosDisableView.xaml
    /// </summary>
    public partial class VznosDisableView : UserControl
    {
        public VznosDisableView(object id)
        {
            InitializeComponent();
            DataContext = new VznosDisableViewModel(id);
        }
    }
}
