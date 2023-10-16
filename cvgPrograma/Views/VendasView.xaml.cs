﻿using cvgPrograma.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cvgPrograma.Views
{
    /// <summary>
    /// Interação lógica para VendasView.xam
    /// </summary>
    public partial class VendasView : UserControl
    {
        public VendasView()
        {
            InitializeComponent();
            DataContext = new VendasViewModel();
        }
    }

}
