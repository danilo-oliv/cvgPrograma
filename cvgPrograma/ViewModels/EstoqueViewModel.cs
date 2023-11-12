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

        private long _Idproduto;

        public long Idproduto
        {
            get { return _Idproduto; }
            set { _Idproduto = value; OnPropertyChanged(nameof(Idproduto)); }
        }



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
            DeletCommand = new RelayCommand(DelProdHelper);
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


        public void DelProdHelper(object parameter )
        {
            Produto produto = new Produto();
            if (parameter is long Produto_Id )
            {
                produto.DeletarProduto(Produto_Id);
                AtualizarMetodo();
            }
        }

        public ICommand DeletCommand { get; set; }
        



    }
}
