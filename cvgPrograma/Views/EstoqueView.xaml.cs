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
using cvgPrograma.Models;

namespace cvgPrograma.Views
{
    /// <summary>
    /// Interação lógica para EstoqueView.xam
    /// </summary>
    public partial class EstoqueView : UserControl
    {
        public EstoqueViewModel ViewModel { get; set; }


        public EstoqueView()
        {
            InitializeComponent();
<<<<<<< HEAD


            ViewModel = new EstoqueViewModel();
            DataContext = ViewModel;
=======
>>>>>>> 403245b8f674733afc58ae37c4a4c30db45218e6
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

<<<<<<< HEAD
        private void btnEditarCard_Click(object sender, RoutedEventArgs e)
        {

        }



        private void voltarCard(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is long ProdutoId)
            {

                funcaoVoltaCard(ProdutoId);
            }
            else
                MessageBox.Show("False");
        }

        private void virarCard(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is long ProdutoId)
            {

                funcaoViraCard(ProdutoId);
            }
            else
                MessageBox.Show("False");
        }


        private void funcaoVoltaCard(long valorDoId)
        {
            int index = -1;

            for (int i = 0; i < listagemCards.Items.Count; i++)
            {
                if (listagemCards.Items[i] is Produto produtos && produtos.ProdutoId.ToString() == valorDoId.ToString())
                {
                    index = i;                    
                    break;
                }

            }

            if (index != -1)
            {
                // Access the item at the found index and modify its properties
                Produto itemAtIndex = listagemCards.Items[index] as Produto;
                if (itemAtIndex != null)
                {                    
                    itemAtIndex.atributoVisibilidade = "Visible";
                }

            }


        }

        private void funcaoViraCard(long ProdutoId)
        {
            int index = -1;

            for (int i = 0; i < listagemCards.Items.Count; i++)
            {
                if (listagemCards.Items[i] is Produto produtos && produtos.ProdutoId.ToString() == ProdutoId.ToString())
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                // Access the item at the found index and modify its properties
                Produto itemAtIndex = listagemCards.Items[index] as Produto;
                if (itemAtIndex != null)
                {
                    itemAtIndex.atributoVisibilidade = "Hidden";
                }

            }




        }

    }


    }
=======
    }
}
>>>>>>> 403245b8f674733afc58ae37c4a4c30db45218e6
