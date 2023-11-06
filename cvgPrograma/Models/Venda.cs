using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cvgPrograma.Models
{
    public class Venda
    {
        private string _connectionString = "Server=localhost;Database=casadovideogame;Uid=root;Pwd=;";


        public int VendaId { get; set; }
        public DateOnly DataVenda { get ; set; }
        public decimal TotalVenda { get; set; }

        public long ProdId { get; set; }
        public string? NomeProd { get; set; }
        public decimal PrecoProd { get; set; }

        public long ProdVendaId { get; set; }
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


        public void InserirVenda(string NomeProduto, DateOnly DataVenda, int QuantVenda, decimal TotalVenda,string TipoPagamento)
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            try
            {
                string encontraProduto = "SELECT ProdId from produto WHERE NomeProd = \"@NomeProd\";";
                using (MySqlCommand comandoEncontraProduto = new MySqlCommand(encontraProduto, conexao))
                {
                    comandoEncontraProduto.Parameters.AddWithValue("@NomeProd", NomeProduto);
                    object resultado = comandoEncontraProduto.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        int ProdId = Convert.ToInt32(resultado);

                        string insereVenda = "INSERT into venda (DataVenda, QuantVenda, TotalVenda, ProdId, CodMetodo) " +
    "VALUES (@DataVenda, @QuantVenda, @TotalVenda, @ProdId, @CodMetodo)";

                        using (MySqlCommand comandoInsereVenda = new MySqlCommand(insereVenda, conexao))
                        {
                            comandoInsereVenda.Parameters.AddWithValue("@DataVenda", DataVenda);
                            comandoInsereVenda.Parameters.AddWithValue("@TotalVenda", TotalVenda);
                            comandoInsereVenda.Parameters.AddWithValue("@CodMetodo", TipoPagamentoHelper(TipoPagamento));
                            comandoInsereVenda.ExecuteNonQuery();
                        }
                        using(MySqlCommand comandoInsereProdutoVenda = new MySqlCommand(insereVenda, conexao))
                        {
                            comandoInsereProdutoVenda.Parameters.AddWithValue("@QuantVenda", QuantVenda);

                            comandoInsereProdutoVenda.Parameters.AddWithValue("@ProdId", ProdId);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Produto não encontrado");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);

            }
            finally
            {

            }
        }

        public int TipoPagamentoHelper(string TipoPagamento)
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            try
            {
                string encontraTipo = "SELECT CodMetodo FROM metodopagamento WHERE TipoPagamento = \"@TipoPagamento\"";
                using (MySqlCommand comandoEncontraTipo = new MySqlCommand(encontraTipo, conexao))
                {
                    comandoEncontraTipo.Parameters.AddWithValue("@TipoPagamento", TipoPagamento);
                    object resultado = comandoEncontraTipo.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        int ProdutoId = Convert.ToInt32(resultado);
                        return ProdutoId;
                    }
                    else
                    {
                        MessageBox.Show("Opção de pagamento não encontrada.");
                        return 0;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                return 0;
            }
            
        }

    }
}
