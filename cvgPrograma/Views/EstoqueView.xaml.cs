using cvgPrograma.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Interação lógica para EstoqueView.xam
    /// </summary>
    public partial class EstoqueView : UserControl
    {

        public ObservableCollection<ItemModel> Itens { get; set; }
        public EstoqueView()
        {
            InitializeComponent();
            Itens = new ObservableCollection<ItemModel>
            {
                new ItemModel { Nome = "Item 1" },
                new ItemModel { Nome = "Item 2" },
                new ItemModel { Nome = "Item 3" }
            };
            meuItemControl.ItemsSource = Itens;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                imgDynamic.Source = new BitmapImage(fileUri);
            }
        }

        private void BtnLimparImagem(object sender, RoutedEventArgs e)
        {
            imgDynamic.Source = null;
        }

        private void btnEditarCard_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void virarCard(object sender, RoutedEventArgs e)
        //{
        //    var itemIndex = 0;
        //    var itemContainer = listagemCards.ItemContainerGenerator.ContainerFromIndex(itemIndex) as FrameworkElement;
        //    if (itemContainer != null)
        //    {
        //        itemContainer.Visibility = Visibility.Collapsed;
        //    }
        //}

        private void AzulButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button?.DataContext as ItemModel;
            if (item != null)
            {
                item.Cor = Brushes.Blue;
                item.Visibilidade = "Hidden";
            }
        }

        private void VermelhoButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button?.DataContext as ItemModel;
            if (item != null)
            {
                item.Cor = Brushes.Red;
                item.Visibilidade = "Visible";
            }
        }

    }

    public class ItemModel : INotifyPropertyChanged
    {
        private string _visibilidade;

        public string Visibilidade
        {
            get { return _visibilidade; }
            set { _visibilidade = value; OnPropertyChanged(nameof(Visibilidade)); }
        }



        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set
            {
                if (_nome != value)
                {
                    _nome = value;
                    OnPropertyChanged(nameof(Nome));
                }
            }
        }

        private Brush _cor = Brushes.Black;
        public Brush Cor
        {
            get { return _cor; }
            set
            {
                if (_cor != value)
                {
                    _cor = value;
                    OnPropertyChanged(nameof(Cor));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    }
