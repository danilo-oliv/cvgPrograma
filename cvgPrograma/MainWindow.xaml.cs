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
            //menuTab.SelectedItem = 0;

        }

        //Botao agenda
        private void agendaTabClick(object sender, MouseButtonEventArgs e)
        {
            menuTab.SelectedItem = 0;
            loginView.Visibility = Visibility.Hidden;
            agendaView.Visibility = Visibility.Visible;
            estoqueView.Visibility = Visibility.Hidden;
            vendasView.Visibility = Visibility.Hidden;
            infoView.Visibility = Visibility.Hidden;
        }

        //Botao estoque
        private void estoqueTabClick(object sender, MouseButtonEventArgs e)
        {
            menuTab.SelectedItem = 1;
            loginView.Visibility = Visibility.Hidden;
            agendaView.Visibility = Visibility.Hidden;
            estoqueView.Visibility = Visibility.Visible;
            vendasView.Visibility = Visibility.Hidden;
            infoView.Visibility = Visibility.Hidden;
        }

        //Botao vendas
        private void vendasTabClick(object sender, MouseButtonEventArgs e)
        {
            menuTab.SelectedItem = 2;
            loginView.Visibility = Visibility.Hidden;
            agendaView.Visibility = Visibility.Hidden;
            estoqueView.Visibility = Visibility.Hidden;
            vendasView.Visibility = Visibility.Visible;
            infoView.Visibility = Visibility.Hidden;
        }

        //Botao info
        private void infoTabClick(object sender, MouseButtonEventArgs e)
        {
            menuTab.SelectedItem = 3;
            loginView.Visibility = Visibility.Hidden;
            agendaView.Visibility = Visibility.Hidden;
            estoqueView.Visibility = Visibility.Hidden;
            vendasView.Visibility = Visibility.Hidden;
            infoView.Visibility = Visibility.Visible;
        }

        private void loginTabClick(object sender, MouseButtonEventArgs e)
        {
            menuTab.SelectedItem = 4;
            loginView.Visibility = Visibility.Visible;
            agendaView.Visibility = Visibility.Hidden;
            estoqueView.Visibility = Visibility.Hidden;
            vendasView.Visibility = Visibility.Hidden;
            infoView.Visibility = Visibility.Hidden;
        }
    }
}
