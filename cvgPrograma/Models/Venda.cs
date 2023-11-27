using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cvgPrograma.Models
{
    public class Venda : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _connectionString = "Server=localhost;Database=casadovideogame;Uid=root;Pwd=Amorinha 24;";


        public int VendaId { get; set; }
        public DateOnly DataVenda { get; set; }
        public decimal TotalVenda { get; set; }

        public long ProdId { get; set; }
        public string? NomeProd { get; set; }
        public decimal PrecoProd { get; set; }

        public int ProdVendaId { get; set; }
        public int QuantVenda { get; set; }

        public string? TipoPagamento { get; set; }




        public int Quantidade { get; set; }
        public DateOnly Data { get; set; }

        public DataTable ConsultarVenda()
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dataTableVenda = new DataTable();
            try
            {
                conexao.Open();

                string consultarVenda = "SELECT v.VendaId, v.DataVenda, v.TotalVenda, mp.TipoPagamento, " +
                    "pv.ProdutoVendaId, pv.QuantVenda, p.NomeProd, p.PrecoProd from venda as v " +
                    "INNER JOIN metodopagamento as mp on v.CodMetodo = mp.CodMetodo " +
                    "INNER JOIN produtovenda as pv on v.VendaId = pv.VendaId " +
                    "INNER JOIN produto as p on pv.ProdId = p.ProdId;";
                using (MySqlCommand comandoConsultarVenda = new MySqlCommand(consultarVenda, conexao))
                {
                    da.SelectCommand = comandoConsultarVenda;
                    da.Fill(dataTableVenda);
                }
                return dataTableVenda;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                return null;
            }
            finally
            {
                conexao.Close();
            }
        }


        public void InserirVenda(string NomeProduto, int QuantVenda, decimal TotalVenda, string TipoPagamento)
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            int IdMetodo = EncontrarCodMetodo(TipoPagamento);
            if (IdMetodo != -1)
            {
                try
                {
                    conexao.Open();
                    int IdProduto = EncontrarProdId(NomeProduto);
                    string insereVenda = "INSERT into venda (DataVenda, TotalVenda, CodMetodo) " +
                                      "VALUES (@DataVenda, @TotalVenda, @CodMetodo);";
                    string selecionaVenda = "SELECT MAX(VendaId) FROM venda;";
                    using (MySqlCommand comandoInsereVenda = new MySqlCommand(insereVenda, conexao))
                    {
                        comandoInsereVenda.Parameters.AddWithValue("@DataVenda", DateTime.Now);
                        comandoInsereVenda.Parameters.AddWithValue("@TotalVenda", TotalVenda);
                        comandoInsereVenda.Parameters.AddWithValue("@CodMetodo", IdMetodo);
                        comandoInsereVenda.ExecuteNonQuery();
                    }

                    using (MySqlCommand comandoSelecionaVenda = new MySqlCommand(selecionaVenda, conexao))
                    {
                        object resultadoSelecao = comandoSelecionaVenda.ExecuteScalar();
                        if (resultadoSelecao != null && resultadoSelecao != DBNull.Value)
                        {
                            int VendaIdConvertida = Convert.ToInt32(resultadoSelecao);
                            string insereProdutoVenda = "INSERT into produtovenda (ProdId, VendaId, QuantVenda)" +
                                                "VALUES (@ProdId, @VendaId, @QuantVenda);";
                            using (MySqlCommand comandoInsereProdutoVenda = new MySqlCommand(insereProdutoVenda, conexao))
                            {
                                comandoInsereProdutoVenda.Parameters.AddWithValue("@QuantVenda", QuantVenda);
                                comandoInsereProdutoVenda.Parameters.AddWithValue("@VendaId", VendaIdConvertida);
                                comandoInsereProdutoVenda.Parameters.AddWithValue("@ProdId", IdProduto);
                                comandoInsereProdutoVenda.ExecuteNonQuery();
                            }
                        }
                    }
                    string reduzEstoque = "UPDATE estoque SET QuantidadeProduto = QuantidadeProduto - @QuantVenda WHERE ProdId = @IdProduto;";
                    using (MySqlCommand comandoReduzEstoque = new MySqlCommand(reduzEstoque, conexao))
                    {
                        comandoReduzEstoque.Parameters.AddWithValue("@QuantVenda", QuantVenda);
                        comandoReduzEstoque.Parameters.AddWithValue("@IdProduto", IdProduto);
                        comandoReduzEstoque.ExecuteNonQuery();
                    }
                    MessageBox.Show("Inserido com sucesso.");
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

        public int EncontrarCodMetodo(string tipoPagamento)
        {
            int codMetodo = -1;

            string encontraTipo = "SELECT CodMetodo FROM metodopagamento WHERE TipoPagamento = @TipoPagamentoP;";

            using (MySqlConnection conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();

                using (MySqlCommand comandoEncontraTipo = new MySqlCommand(encontraTipo, conexao))
                {
                    comandoEncontraTipo.Parameters.AddWithValue("@TipoPagamentoP", tipoPagamento);

                    object resultado = comandoEncontraTipo.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        codMetodo = Convert.ToInt32(resultado);
                    }
                }
                conexao.Close();
            }

            return codMetodo;
        }

        public int EncontrarProdId(string nomeProduto)
        {
            int prodId = -1;

            string query = "SELECT ProdId FROM produto WHERE NomeProd = @NomeProdD;";

            using (MySqlConnection conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();

                using (MySqlCommand command = new MySqlCommand(query, conexao))
                {
                    command.Parameters.AddWithValue("@NomeProdD", nomeProduto);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        prodId = Convert.ToInt32(result);
                    }
                }
                conexao.Close();
            }

            return prodId;

             
        }

        public void DeletarVenda(int Idvenda)
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);

            try
            {
                conexao.Open();

                string deletarVendaSql = "DELETE from venda WHERE VendaId = @VendaId;" +
                    "DELETE from venda WHERE VendaId = @VendaId;";

                using (MySqlCommand comandoDeletarVenda = new MySqlCommand(deletarVendaSql, conexao))
                {
                    comandoDeletarVenda.Parameters.AddWithValue("@VendaId", Idvenda);
                    comandoDeletarVenda.ExecuteNonQuery();
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
