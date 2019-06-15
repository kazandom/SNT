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
using System.Windows.Shapes;

namespace SNT
{
    /// <summary>
    /// Логика взаимодействия для RaschetView.xaml
    /// </summary>
    public partial class RaschetView : Window
    {
        public RaschetView()
        {
            InitializeComponent();
            DataContext = new RaschetViewModel();
        }

        // вынужденная мера, т.к. свойство Window.DialogResult не является 
        // dependency property и поэтому Binding с ним не работает
        private void Accept(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
