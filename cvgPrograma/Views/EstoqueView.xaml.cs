using Microsoft.Win32;
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
    /// Interação lógica para EstoqueView.xam
    /// </summary>
    public partial class EstoqueView : UserControl
    {
        public EstoqueView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        private void OnShowModalClick(object sender, RoutedEventArgs e)
        {
            modalTeste.IsOpen = true;
        }

        private void OnCloseModalClick(object sender, RoutedEventArgs e)
        {
            modalTeste.IsOpen = false;

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

        
    }
}
