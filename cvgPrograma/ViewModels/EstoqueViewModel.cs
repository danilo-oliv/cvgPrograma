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

        public RelayCommand AddProdCommand => new RelayCommand(execute => AddProdHelper(), canExecute => { return true; });
        public RelayCommand AtualizarCollection => new RelayCommand(execute => AtualizarMetodo(), canExecute => { return true; });
        public RelayCommand EditandoCard => new RelayCommand(execute => EditarCard(), canExecute => { return true; });
        public RelayCommand SalvandoCard => new RelayCommand(execute => SalvarCard(), canExecute => { return true; });
        public RelayCommand ImportandoImagem => new RelayCommand(execute => AdicionarImagem(), canExecute => { return true; });
        public RelayCommand ExcluindoImagem => new RelayCommand(execute => OffImagem(), canExecute => { return true; });

        public void AddProdHelper()
        {
            InserirProduto(txbxNomeProduto, txtPrecoProduto, txtQuantidadeProduto);
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





        private string _connectionString = "Server=localhost;Database=casadovideogame;Uid=root;Pwd=;";
        public void InserirProduto(string NomeProduto, decimal PrecoProduto, int QuantidadeEstoque)
        {
            
                MySqlConnection conexao = new MySqlConnection(_connectionString);

                try
                {
                    conexao.Open();

                    // Query para cadastrar na tabela produto
                    // @Nome e @Preco são chaves para parâmetros 
                    string inserirProdutoSql = "INSERT INTO produto (NomeProd, PrecoProd) VALUES (@Nome, @Preco);";
                    using (MySqlCommand comandoInserirProduto = new MySqlCommand(inserirProdutoSql, conexao))
                    {
                        // "@Chave", valor de um campo input
                        comandoInserirProduto.Parameters.AddWithValue("@Nome", NomeProduto);
                        comandoInserirProduto.Parameters.AddWithValue("@Preco", PrecoProduto); // NOMEAR AS TEXTBOX E TROCAR
                        comandoInserirProduto.ExecuteNonQuery();
                    }

                    // Pega o maior id de produto (ultimo adicionado) para fazer o match no estoque
                    string consultarMaiorIdSql = "SELECT MAX(ProdId) FROM produto;";
                    using (MySqlCommand comandoConsultarMaiorId = new MySqlCommand(consultarMaiorIdSql, conexao))
                    {
                        object resultado = comandoConsultarMaiorId.ExecuteScalar();

                        if (resultado != null && resultado != DBNull.Value)
                        {
                            int maiorId = Convert.ToInt32(resultado);

                            // Com o ID obtido, insere no estoque
                            string inserirEstoqueSql = "INSERT INTO estoque (QuantidadeProduto, ProdId) VALUES (@Quantidade, @IdProduto);";
                            using (MySqlCommand comandoInserirEstoque = new MySqlCommand(inserirEstoqueSql, conexao))
                            {
                                comandoInserirEstoque.Parameters.AddWithValue("@Quantidade", QuantidadeEstoque); // TROCAR!
                                comandoInserirEstoque.Parameters.AddWithValue("@IdProduto", maiorId);
                                comandoInserirEstoque.ExecuteNonQuery();
                            }

                            MessageBox.Show("Inserção concluída com sucesso.");                            
                        }
                        else
                        {
                            MessageBox.Show("Nenhum registro encontrado na tabela 'produto'.");
                        }
                    }
                }
                catch (Exception ex)
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


        private string _IsVisible;
        public string IsVisible
        {
            get { return _IsVisible; }
            set
            {
                _IsVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }
        private string _IsVisible2;
        public string IsVisible2
        {
            get { return _IsVisible2; }
            set
            {
                _IsVisible2 = value;
                OnPropertyChanged(nameof(IsVisible2));
            }
        }

        private object _prodID;

        public object prodID
        {
            get { return _prodID; }
            set { _prodID = value; }
        }

        private string  _InsiraId;

        public string InsiraID
        {
            get { return _InsiraId; }
            set { _InsiraId = value; OnPropertyChanged(nameof(InsiraID)); }
        }

        public void EditarCard()
        {

                IsVisible = "Visible";
                IsVisible2 = "Collapsed";            
        }

        public void SalvarCard()
        {
            IsVisible = "Collapsed";   
            IsVisible2 = "Visible";  
        }






    }
}
