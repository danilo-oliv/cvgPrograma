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
        string connectionString = "Server=localhost;Database=casadovideogame;User=root;Password=;30012005I.p";
        private string _connectionString;
        public int ServicoId { get; set; }
        public string? DescSevico { get; set; }
        public DateOnly DataEntradaServico { get; set; }
        public DateOnly DataSaidaServico { get; set; }
        public bool ProntoServico { get; set; }
        public decimal TotalServico { get; set; }
        public int ClienteId { get; set; }
        public int CodMetodo { get; set; }
        public int CodServico { get; set; }

        public ObservableCollection<Servico> InserirCard()
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataReader dr;
            ObservableCollection<Servico> servicos = new ObservableCollection<Servico>();

            try
            {
                conexao.Open();

                // Query para cadastrar na tabela produto
                // @Nome e @Preco são chaves para parâmetros 
                /*string BuscarCard = "SELECT ServicoId, DescServico, DataEntradaServico, DataSaidaServico, ProntoServico, TotalServico, NomeCliente, TelefoneCliente, TipoPagamento, TipoServ  FROM servico as s \r\nINNER JOIN cliente as c ON s.ClienteId =c.ClienteId\r\nINNER JOIN metodopagamento as mp ON s.CodMetodo = mp.CodMetodo\r\nINNER JOIN tiposervico as tp ON s.CodServico = tp.CodServico;"
                using (MySqlCommand InserirCard = new MySqlCommand(InserirCardSql, conexao))
                {
                    InserirCard.Parameters.AddWithValue("@DescServico", textBoxDescServico.Text); // Substitua textBoxDescServico.Text pelo campo correspondente
                    InserirCard.Parameters.AddWithValue("@DataEntradaServico", DateTime.Parse(textBoxDataEntrada.Text)); // Substitua textBoxDataEntrada.Text pelo campo correspondente
                    InserirCard.Parameters.AddWithValue("@DataSaidaServico", DateTime.Parse(textBoxDataSaida.Text)); // Substitua textBoxDataSaida.Text pelo campo correspondente
                    InserirCard.Parameters.AddWithValue("@ProntoServico", checkBoxPronto.Checked); // Substitua checkBoxPronto.Checked pelo campo correspondente
                    InserirCard.Parameters.AddWithValue("@TotalServico", decimal.Parse(textBoxTotal.Text)); // Substitua textBoxTotal.Text pelo campo correspondente
                    InserirCard.Parameters.AddWithValue("@ClienteId", int.Parse(textBoxClienteId.Text)); // Substitua textBoxClienteId.Text pelo campo correspondente
                    InserirCard.Parameters.AddWithValue("@CodMetodo", int.Parse(textBoxCodMetodo.Text)); // Substitua textBoxCodMetodo.Text pelo campo correspondente
                    InserirCard.Parameters.AddWithValue("@CodServico", int.Parse(textBoxCodServico.Text)); // Substitua textBoxCodServico.Text pelo campo correspondente
                    comandoInserirCard.ExecuteNonQuery();
                }*/
                // {
                // "@Chave", valor de um campo input
                // comandoBuscarCard.Parameters.AddWithValue("@ServicoId", 1);
                // comandoBuscarCard.Parameters.AddWithValue("@ClienteId", 1); // NOMEAR AS TEXTBOX E TROCAR
                // comandoBuscarCard.ExecuteNonQuery();
                //  }

                string InserirCardSql = "SELECT ServicoId, DescServico, DataEntradaServico, DataSaidaServico, ProntoServico, TotalServico, NomeCliente, TelefoneCliente, TipoPagamento, TipoServ  FROM servico as s \r\nINNER JOIN cliente as c ON s.ClienteId =c.ClienteId\r\nINNER JOIN metodopagamento as mp ON s.CodMetodo = mp.CodMetodo\r\nINNER JOIN tiposervico as tp ON s.CodServico = tp.CodServico";
                using (MySqlCommand comandoInserirCard = new MySqlCommand(InserirCardSql, conexao))
                {
                    conexao.Open();

                    // 1. Inserir na tabela Cliente
                    string inserirClienteSql = "INSERT INTO cliente (NomeCliente, TelefoneCliente) VALUES (@NomeCliente, @TelefoneCliente);";
                    using (MySqlCommand comandoInserirCliente = new MySqlCommand(inserirClienteSql, conexao))
                    using (dr = comandoInserirCard.ExecuteReader()) 
                    {
                        comandoInserirCliente.Parameters.AddWithValue("@NomeCliente", "Nome do Cliente"); // Substitua pelo nome do cliente
                        comandoInserirCliente.Parameters.AddWithValue("@TelefoneCliente", "123456789"); // Substitua pelo telefone do cliente
                        comandoInserirCliente.ExecuteNonQuery();
                    }

                    // 2. Obter o ClienteId inserido
                    string consultarClienteIdSql = "SELECT LAST_INSERT_ID();";
                    int clienteId;
                    using (MySqlCommand comandoConsultarClienteId = new MySqlCommand(consultarClienteIdSql, conexao))
                    {
                        clienteId = Convert.ToInt32(comandoConsultarClienteId.ExecuteScalar());
                    }

                    // 3. Inserir na tabela MetodoPagamento
                    string inserirMetodoPagamentoSql = "INSERT INTO metodopagamento (CodMetodo, TipoPagamento) VALUES (@CodMetodo, @TipoPagamento);";
                    using (MySqlCommand comandoInserirMetodoPagamento = new MySqlCommand(inserirMetodoPagamentoSql, conexao))
                    {
                        comandoInserirMetodoPagamento.Parameters.AddWithValue("@CodMetodo", 1); // Substitua pelo código do método de pagamento
                        comandoInserirMetodoPagamento.Parameters.AddWithValue("@TipoPagamento", "Tipo de Pagamento"); // Substitua pelo tipo de pagamento
                        comandoInserirMetodoPagamento.ExecuteNonQuery();
                    }

                    // 4. Obter o CodMetodo inserido
                    string consultarCodMetodoSql = "SELECT LAST_INSERT_ID();";
                    int codMetodo;
                    using (MySqlCommand comandoConsultarCodMetodo = new MySqlCommand(consultarCodMetodoSql, conexao))
                    {
                        codMetodo = Convert.ToInt32(comandoConsultarCodMetodo.ExecuteScalar());
                    }

                    // 5. Inserir na tabela TipoServico
                    string inserirTipoServicoSql = "INSERT INTO tiposervico (CodServico, TipoServ) VALUES (@CodServico, @TipoServ);";
                    using (MySqlCommand comandoInserirTipoServico = new MySqlCommand(inserirTipoServicoSql, conexao))
                    {
                        comandoInserirTipoServico.Parameters.AddWithValue("@CodServico", 1); // Substitua pelo código do tipo de serviço
                        comandoInserirTipoServico.Parameters.AddWithValue("@TipoServ", "Tipo de Serviço"); // Substitua pelo tipo de serviço
                        comandoInserirTipoServico.ExecuteNonQuery();
                    }

                    // 6. Obter o CodServico inserido
                    string consultarCodServicoSql = "SELECT LAST_INSERT_ID();";
                    int codServico;
                    using (MySqlCommand comandoConsultarCodServico = new MySqlCommand(consultarCodServicoSql, conexao))
                    {
                        codServico = Convert.ToInt32(comandoConsultarCodServico.ExecuteScalar());
                    }

                    // 7. Inserir na tabela Servico
                    string inserirServicoSql = "INSERT INTO servico (DescServico, DataEntradaServico, DataSaidaServico, ProntoServico, TotalServico, ClienteId, CodMetodo, CodServico) VALUES (@DescServico, @DataEntradaServico, @DataSaidaServico, @ProntoServico, @TotalServico, @ClienteId, @CodMetodo, @CodServico);";
                    using (MySqlCommand comandoInserirServico = new MySqlCommand(inserirServicoSql, conexao))
                    {
                        comandoInserirServico.Parameters.AddWithValue("@DescServico", "Descrição do Serviço"); // Substitua pela descrição do serviço
                        comandoInserirServico.Parameters.AddWithValue("@DataEntradaServico", DateTime.Now); // Substitua pela data de entrada do serviço
                        comandoInserirServico.Parameters.AddWithValue("@DataSaidaServico", DateTime.Now.AddDays(1)); // Substitua pela data de saída do serviço
                        comandoInserirServico.Parameters.AddWithValue("@ProntoServico", true); // Substitua pelo status de pronto do serviço
                        comandoInserirServico.Parameters.AddWithValue("@TotalServico", 100.0); // Substitua pelo total do serviço
                        comandoInserirServico.Parameters.AddWithValue("@ClienteId", clienteId); // Usando o ClienteId obtido anteriormente
                        comandoInserirServico.Parameters.AddWithValue("@CodMetodo", codMetodo); // Usando o CodMetodo obtido anteriormente
                        comandoInserirServico.Parameters.AddWithValue("@CodServico", codServico); // Usando o CodServico obtido anteriormente
                        comandoInserirServico.ExecuteNonQuery();
                    }

                    MessageBox.Show("Inserção concluída com sucesso.");
                    return null;
                }
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
    }

}
