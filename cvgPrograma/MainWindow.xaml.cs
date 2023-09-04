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

            List<User> users = new List<User>();
            users.Add(new User() { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23) });
            users.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) });
            users.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) });

            dgSimple.ItemsSource = users;

            List<Service> services = new List<Service>();
            services.Add(new Service() { Id = 4, Cliente = "Maria", ContatoCliente = 38212222, Desc = "Troca de tela", Pagamento = "Pix", Preco = 100.00f });
            services.Add(new Service() { Id = 4, Cliente = "João", ContatoCliente = 38212223, Desc = "Troca de cabo", Pagamento = "Cartão", Preco = 50.00f });
            services.Add(new Service() { Id = 4, Cliente = "José", ContatoCliente = 38212224, Desc = "Limpeza", Pagamento = "Dinheiro", Preco = 29.99f });

            dgService.ItemsSource = services;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
