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
        private string _connectionString = "Server=localhost;Database=casadovideogame;User=root;Password=;";
        
        public int ServicoId { get; set; }
        public string? DescSevico { get; set; }
        public DateOnly DataEntradaServico { get; set; }
        public DateOnly DataSaidaServico { get; set; }
        public bool ProntoServico { get; set; }
        public decimal TotalServico { get; set; }
        public int ClienteId { get; set; }
        public int CodMetodo { get; set; }
        public int CodServico { get; set; }

        public void InserirCard(string DescricaoServ, DateOnly SaidaServ, decimal TotalServ, string NomeCliente, string TelefoneCliente, string TipoPagamento)
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataReader dr;
            ObservableCollection<Servico> servicos = new ObservableCollection<Servico>();

            try
            {
                conexao.Open();
                string inserirServico = "INSERT INTO servico " +
                        "(DescServico, DataEntradaServico, DataSaidaServico, ProntoServico, TotalServico, NomeCliente, TelefoneCliente, CodMetodo) " +
                        "VALUES (@DescServico, @DataEntradaServico, @DataSaidaServico, false, @TotalServico, @NomeCliente, @TelefoneCliente, @CodMetodo);";

                Venda venda = new Venda();

                using (MySqlCommand comandoInserirServico = new MySqlCommand(inserirServico, conexao))
                {
                        comandoInserirServico.Parameters.AddWithValue("@DescServico", DescricaoServ);
                        comandoInserirServico.Parameters.AddWithValue("@DataEntradaServico", DateTime.Now);
                        comandoInserirServico.Parameters.AddWithValue("@DataSaidaServico", SaidaServ);
                        comandoInserirServico.Parameters.AddWithValue("@TotalServico", TotalServ); 
                        comandoInserirServico.Parameters.AddWithValue("@NomeCliente", NomeCliente);
                        comandoInserirServico.Parameters.AddWithValue("@TelefoneCliente", TelefoneCliente);
                        comandoInserirServico.Parameters.AddWithValue("@CodMetodo", venda.TipoPagamentoHelper(TipoPagamento));
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
    }

}
