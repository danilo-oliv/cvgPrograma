using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;


namespace cvgPrograma.Models
{
    public class Servico
    {
        private string _connectionString = "Server=localhost;Database=casadovideogame;Uid=root;Pwd=;";

        public int ServicoId { get; set; }
        public string? DescSevico { get; set; }
        public DateTime DataEntradaServico { get; set; }
        public DateTime DataSaidaServico { get; set; }
        public bool ProntoServico { get; set; }
        public decimal TotalServico { get; set; }
        public int ClienteId { get; set; }
        public int CodMetodo { get; set; }
        public int CodServico { get; set; }

        public void InserirCard(string DescricaoServ, DateTime SaidaServ, decimal TotalServ, string NomeCliente, string TelefoneCliente, string TipoPagamento)
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataReader dr;
            ObservableCollection<Servico> servicos = new ObservableCollection<Servico>();

            int IdMetodo = EncontrarCodMetodo(TipoPagamento);


            try
            {
                conexao.Open();
                string inserirServico = "INSERT INTO servico " +
                        "(DescServico, DataEntradaServico, DataSaidaServico, ProntoServico, TotalServico, NomeCliente, TelefoneCliente, CodMetodo) " +
                        "VALUES (@DescServico, @DataEntradaServico, @DataSaidaServico, false, @TotalServico, @NomeCliente, @TelefoneCliente, @CodMetodo);";


                using (MySqlCommand comandoInserirServico = new MySqlCommand(inserirServico, conexao))
                {
                    comandoInserirServico.Parameters.AddWithValue("@DescServico", DescricaoServ);
                    comandoInserirServico.Parameters.AddWithValue("@DataEntradaServico", DateTime.Now);
                    comandoInserirServico.Parameters.AddWithValue("@DataSaidaServico", SaidaServ);
                    comandoInserirServico.Parameters.AddWithValue("@TotalServico", TotalServ);
                    comandoInserirServico.Parameters.AddWithValue("@NomeCliente", NomeCliente);
                    comandoInserirServico.Parameters.AddWithValue("@TelefoneCliente", TelefoneCliente);
                    comandoInserirServico.Parameters.AddWithValue("@CodMetodo", IdMetodo);
                    comandoInserirServico.ExecuteNonQuery();
                }

                MessageBox.Show("Inserção concluída com sucesso.");
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

        public void DeletarProduto(long ServicoId)
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);

            try
            {
                conexao.Open();

                string deletarServicoSql = "DELETE from servico WHERE ServicoId = @ServicoId;" +
                    "DELETE from servico WHERE ServicoId = @ServicoId;";

                using (MySqlCommand comandoDeletarServico = new MySqlCommand(deletarServicoSql, conexao))
                {
                    comandoDeletarServico.Parameters.AddWithValue("@ServicoId", ServicoId);
                    comandoDeletarServico.ExecuteNonQuery();
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
