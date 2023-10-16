using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Data;

namespace cvgPrograma.Models
{
    public class Produto
    {
        private string _connectionString = "Server=localhost;Database=cvgtestedois;Uid=root;Pwd=;";
        MySqlDataAdapter da;

        public int ProdutoId { get; }
        public string? NomeProduto { get; set; }
        public decimal PrecoProduto { get; set; }

        public int EstoqueId { get; }
        public int QuantidadeEstoque { get; set; }

        //public void InserirProduto( string NomeProduto, decimal PrecoProduto ,int QuantidadeEstoque) 
        //{
        //    MySqlConnection conexao = new MySqlConnection(_connectionString);

        //    try
        //    {
        //        conexao.Open();

        //        // Query para cadastrar na tabela produto
        //        // @Nome e @Preco são chaves para parâmetros 
        //        string inserirProdutoSql = "INSERT INTO produto (NomeProd, PrecoProd) VALUES (@Nome, @Preco);";
        //        using (MySqlCommand comandoInserirProduto = new MySqlCommand(inserirProdutoSql, conexao))
        //        {
        //            // "@Chave", valor de um campo input
        //            comandoInserirProduto.Parameters.AddWithValue("@Nome", NomeProduto);
        //            comandoInserirProduto.Parameters.AddWithValue("@Preco", PrecoProduto); // NOMEAR AS TEXTBOX E TROCAR
        //            comandoInserirProduto.ExecuteNonQuery();
        //        }

        //        // Pega o maior id de produto (ultimo adicionado) para fazer o match no estoque
        //        string consultarMaiorIdSql = "SELECT MAX(ProdId) FROM produto;";
        //        using (MySqlCommand comandoConsultarMaiorId = new MySqlCommand(consultarMaiorIdSql, conexao))
        //        {
        //            object resultado = comandoConsultarMaiorId.ExecuteScalar();

        //            if (resultado != null && resultado != DBNull.Value)
        //            {
        //                int maiorId = Convert.ToInt32(resultado);

        //                // Com o ID obtido, insere no estoque
        //                string inserirEstoqueSql = "INSERT INTO estoque (QuantidadeProduto, ProdId) VALUES (@Quantidade, @IdProduto);";
        //                using (MySqlCommand comandoInserirEstoque = new MySqlCommand(inserirEstoqueSql, conexao))
        //                {
        //                    comandoInserirEstoque.Parameters.AddWithValue("@Quantidade", QuantidadeEstoque); // TROCAR!
        //                    comandoInserirEstoque.Parameters.AddWithValue("@IdProduto", maiorId);
        //                    comandoInserirEstoque.ExecuteNonQuery();
        //                }

        //                MessageBox.Show("Inserção concluída com sucesso.");                        
        //                MessageBox.Show("Inserção concluída com sucesso.");                        
        //            }
        //            else
        //            {
        //                MessageBox.Show("Nenhum registro encontrado na tabela 'produto'.");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erro: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conexao.Close();
        //    }
        //}

        public DataTable ConsultarProduto()
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();
            try 
            {
                conexao.Open();

                string consultarProduto = "SELECT * FROM PRODUTO as p INNER JOIN estoque as e on p.ProdId = e.ProdId";
                using (MySqlCommand comandoConsultarProduto = new MySqlCommand(consultarProduto, conexao))
                {
                    da.SelectCommand = comandoConsultarProduto;
                    da.Fill(dataTable);
                }
                return dataTable;
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

    }
}
