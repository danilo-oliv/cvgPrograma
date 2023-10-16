using cvgPrograma.Commands;
using cvgPrograma.Models;
using cvgPrograma.Views;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace cvgPrograma.ViewModels
{
    public class EstoqueViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand AddProdCommand => new RelayCommand(execute => InserirProduto(txbxNomeProduto, txtPrecoProduto, txtQuantidadeProduto), canExecute => { return true; });



        private DataTable _dataTable;

        public DataTable dataTable
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                OnPropertyChanged(nameof(dataTable));
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
            dataTable = produto.ConsultarProduto();
        }


        private string _connectionString = "Server=localhost;Database=cvgtestedois;Uid=root;Pwd=;";
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

    }
}
