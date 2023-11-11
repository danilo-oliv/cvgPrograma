using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.ObjectModel;
using cvgPrograma.ViewModels;
using System.ComponentModel;

namespace cvgPrograma.Models
{
    public class Produto : INotifyPropertyChanged
    {
<<<<<<< HEAD
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _connectionString = "Server=localhost;Database=casadovideogame;Uid=root;Pwd=;";        

        public long ProdutoId { get; set;  }

        private long _retornaProdutoId;

        public long retornaProdutoId
        {
            get { return _retornaProdutoId; }
            set { _retornaProdutoId = ProdutoId; }
        }

        private string _atributoVisibilidade;

        public string atributoVisibilidade
        {
            get { return _atributoVisibilidade; }
            set { _atributoVisibilidade = value; OnPropertyChanged(nameof(atributoVisibilidade)); }
        }



        public string? NomeProduto { get; set; }
=======
        private string _connectionString = "Server=localhost;Database=casadovideogame;Uid=root;Pwd=root;";        

        public long ProdutoId { get; set;  }
        public string NomeProduto { get; set; }
>>>>>>> 403245b8f674733afc58ae37c4a4c30db45218e6
        public decimal PrecoProduto { get; set; }

        public long EstoqueId { get; set; }
        public int QuantidadeEstoque { get; set; }


        public override string ToString()
        {
            return NomeProduto;
        }

        public ObservableCollection<Produto> ConsultarProduto()
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataReader dr;
            ObservableCollection<Produto> produtos = new ObservableCollection<Produto>();
            try 
            {
                conexao.Open();

                string consultarProduto = "SELECT * FROM PRODUTO as p INNER JOIN estoque as e on p.ProdId = e.ProdId";
                using (MySqlCommand comandoConsultarProduto = new MySqlCommand(consultarProduto, conexao))
                {
                    using (dr = comandoConsultarProduto.ExecuteReader()) 
                    { 
                        while (dr.Read()) 
                        {
                            Produto produto = new Produto
                            {
                                ProdutoId = Convert.ToInt32(dr["ProdId"]),
                                NomeProduto = dr["NomeProd"].ToString(),
                                PrecoProduto = Convert.ToDecimal(dr["PrecoProd"]),
                                EstoqueId = Convert.ToInt32(dr["EstoqueId"]),
                                QuantidadeEstoque = Convert.ToInt32(dr["QuantidadeProduto"]),
                            };
                            produtos.Add(produto);
                        }
                    }
                }
                return produtos;
            } 
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                return null;
            }
            finally
            {
                conexao.Close();
            }
        }

        public void InserirProduto(string NomeProduto, decimal PrecoProduto, int QuantidadeEstoque)
        {

            MySqlConnection conexao = new MySqlConnection(_connectionString);

            try
            {
                conexao.Open();

                string inserirProdutoSql = "INSERT INTO produto (NomeProd, PrecoProd) VALUES (@Nome, @Preco);";
                using (MySqlCommand comandoInserirProduto = new MySqlCommand(inserirProdutoSql, conexao))
                {
                    comandoInserirProduto.Parameters.AddWithValue("@Nome", NomeProduto);
                    comandoInserirProduto.Parameters.AddWithValue("@Preco", PrecoProduto);
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
                            comandoInserirEstoque.Parameters.AddWithValue("@Quantidade", QuantidadeEstoque);
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
