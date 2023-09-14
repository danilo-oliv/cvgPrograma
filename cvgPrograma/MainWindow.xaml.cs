using cvgPrograma.Models;
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

namespace cvgPrograma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AgendaView_Loaded(object sender, RoutedEventArgs e)
        {

        }

  //      private void MenuTabClick(object sender, SelectionChangedEventArgs e)
     //   {

      //  }

        //private void MenuTabClick(object sender, RoutedEventArgs e)
        //{
        //    if (menuTab.SelectedItem != null)
        //    {

        //        string selectedTabName = ((TabItem)menuTab.SelectedItem).Name;

        //        switch (selectedTabName)
        //        {
        //            case "agendaTab":
        //                Console.WriteLine("Agenda");
        //                break;
        //            case "esoqueTab":
        //                Console.WriteLine("Estoque");
        //                break;
        //            case "vendasTab":
        //                Console.WriteLine("Vendas");
        //                break;
        //            case "infoTab":
        //                Console.WriteLine("Info");
        //                break;
        //        }
        //    }
        //}

        private void testeDoubleClick(object sender, MouseButtonEventArgs e)
        {
            menuTab.SelectedItem = 1;
            agendaView.Visibility = Visibility.Visible;
        }

        private void testeDoubleClick2(object sender, MouseButtonEventArgs e)
        {
            menuTab.SelectedItem = 2;
            agendaView.Visibility = Visibility.Hidden;
        }
    }
}
