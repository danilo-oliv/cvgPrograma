using cvgPrograma.Commands;
using cvgPrograma.Models;
using cvgPrograma.Views;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace cvgPrograma.ViewModels
{
    public class EstoqueViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand AtualizarCollection => new RelayCommand(execute => AtualizarMetodo(), canExecute => { return true; });
        public RelayCommand UpdateProduto => new RelayCommand(execute => AlterarProduto(), canExecute => { return true; });
        public RelayCommand AddProdCommand => new RelayCommand(execute => AddProdHelper(), canExecute => { return true; });      
        public RelayCommand ImportandoImagem => new RelayCommand(execute => AdicionarImagem(), canExecute => { return true; });
        public RelayCommand ExcluindoImagem => new RelayCommand(execute => OffImagem(), canExecute => { return true; });

        public void AddProdHelper()
        {
            
            AtualizarMetodo();
        }


        private ObservableCollection<Produto> _produto;
        public ObservableCollection<Produto> Produtos
        {
            get { return _produto; }
            set
            {
                _produto = value;
                OnPropertyChanged(nameof(Produtos));
            }
        }
        #region Valores das TextBox

        private string _txbxNomeProduto;
        public string txbxNomeProduto
        {
            get { return _txbxNomeProduto; }
            set
            {
                if (_txbxNomeProduto != value)
                {
                    _txbxNomeProduto = value;
                    OnPropertyChanged(nameof(txbxNomeProduto));
                }
            }
        }

        private decimal _txtPrecoProduto;
        public decimal txtPrecoProduto
        {
            get { return _txtPrecoProduto; }
            set 
            { 
                if (_txtPrecoProduto != value)
                {
                    _txtPrecoProduto = value;
                    OnPropertyChanged(nameof(txtPrecoProduto));
                }
            }
        }

        private int _txtQuantidadeProduto;
        public int txtQuantidadeProduto
        {
            get { return _txtQuantidadeProduto; }
            set 
            { 
                if (_txtQuantidadeProduto != value)
                {
                    _txtQuantidadeProduto = value;
                    OnPropertyChanged(nameof(txtQuantidadeProduto));
                }
            }
        }

        #endregion


        public EstoqueViewModel()
        {
            Produto produto = new Produto();
            Produtos = produto.ConsultarProduto();
        }

        public void AtualizarMetodo()
        {
            Produto produto = new Produto();
            Produtos = produto.ConsultarProduto();
        }


        private string _connectionString = "Server=localhost;Database=casadovideogame=root;Pwd=;";
        
        public void AlterarProduto()
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            try
            {
                conexao.Open();
                string updateProduto = "UPDATE produto SET NomeProd = @NovoNome, PrecoProd = @NovoPreco WHERE ProdId = @ProdId;";
                string updateProdutoEstoque = "UPDATE estoque SET QuantidadeProduto = @Quantidade where ProdId = @ProdId;";
                using (MySqlCommand comandoUpdateProduto = new MySqlCommand(updateProduto, conexao))
                {
                    comandoUpdateProduto.Parameters.AddWithValue("@NovoNome", "textbox_update_nomeproduto"); //ALTERAR TEXTBOX
                    comandoUpdateProduto.Parameters.AddWithValue("@NovoPreco", "textbox_update_precoproduto"); //ALTERAR TEXTBOX
                    comandoUpdateProduto.Parameters.AddWithValue("@ProdId", "prodId_do_card_clicado"); //PEGAR ID
                    comandoUpdateProduto.ExecuteNonQuery();
                    using (MySqlCommand comandoUpdateProdutoEstoque = new MySqlCommand(updateProdutoEstoque, conexao))
                    {
                        comandoUpdateProduto.Parameters.AddWithValue("@Quantidade", "textbox_update_nomeproduto"); //ALTERAR TEXTBOX
                        comandoUpdateProduto.Parameters.AddWithValue("@ProdId", "prodId_do_card_clicado"); //PEGAR ID
                        comandoUpdateProduto.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
        private BitmapImage _imagemExibicao1;
        private BitmapImage _imagemExibicao2;
        private string _produtoNome;
        private string _preco;

        public BitmapImage ImagemExibicao1
        {
            get { return _imagemExibicao1; }
            set
            {
                _imagemExibicao1 = value;
                OnPropertyChanged(nameof(ImagemExibicao1));
            }
        }

        public BitmapImage ImagemExibicao2
        {
            get { return _imagemExibicao2; }
            set
            {
                _imagemExibicao2 = value;
                OnPropertyChanged(nameof(ImagemExibicao2));
            }
        }

        public string ProdutoNome
        {
            get { return _produtoNome; }
            set
            {
                _produtoNome = value;
                OnPropertyChanged(nameof(ProdutoNome));
            }
        }

        public string Preco
        {
            get { return _preco; }
            set
            {
                _preco = value;
                OnPropertyChanged(nameof(Preco));
            }
        }

        public void AdicionarImagem()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos os Arquivos|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(openFileDialog.FileName));

                ImagemExibicao1 = bitmapImage;
                ImagemExibicao2 = bitmapImage;
            }
        }

        public void OffImagem()
        {
            ImagemExibicao1 = null;
            ImagemExibicao2 = null;
        }
    }

    }

