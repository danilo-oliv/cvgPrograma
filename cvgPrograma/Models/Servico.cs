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


        // as que nao tem no banco de dados servico
        public string NomeCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string TipoPagamento { get; set; }
        public string TipoServico { get; set; }



        public ObservableCollection<Servico> ConsultarCard()
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataReader dr;
            ObservableCollection<Servico> servicos = new ObservableCollection<Servico>();
            try
            {
                conexao.Open();

                string consultarCard = "SELECT m.CodMetodo, m.TipoPagamento, s.ServicoId, s.NomeCliente, " +
                    "s.TelefoneCliente, s.DescServico, s.DataEntradaServico, s.DataSaidaServico, s.ProntoServico, " +
                    "s.TotalServico FROM MetodoPagamento m INNER JOIN Servico s ON m.CodMetodo = s.CodMetodo where s.ProntoServico = false;";

                using (MySqlCommand comandConsultarCard = new MySqlCommand(consultarCard, conexao))
                {
                    using (dr = comandConsultarCard.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Servico servico = new Servico
                            {
                                NomeCliente = dr["NomeCliente"].ToString(),
                                TelefoneCliente = dr["TelefoneCliente"].ToString(),
                                CodMetodo = Convert.ToInt32(dr["CodMetodo"]),
                                TipoPagamento = dr["TipoPagamento"].ToString(),
                                ServicoId = Convert.ToInt32(dr["ServicoId"]),
                                DescSevico = dr["DescServico"].ToString(),
                                DataEntradaServico = Convert.ToDateTime(dr["DataEntradaServico"]),
                                DataSaidaServico =Convert.ToDateTime(dr["DataSaidaServico"]),
                                ProntoServico = Convert.ToBoolean(dr["ProntoServico"]),
                                TotalServico = Convert.ToDecimal(dr["TotalServico"])
                            };
                            servicos.Add(servico);
                        }

                    }
                    return servicos;
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

        public void DeletarServico(long ServicoId)
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

        public void UpdateServico(string nome, string tel, string desc, decimal total, int idServ)
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            try
            {
                conexao.Open();
                string updateServ = "UPDATE servico SET NomeCliente = @nome, TelefoneCliente = @tel, DescServico = @desc, TotalServico = @total WHERE ServicoId = @servId;";
                using (MySqlCommand comandoUpdate = new MySqlCommand(updateServ, conexao))
                {
                    comandoUpdate.Parameters.AddWithValue("@nome", nome);
                    comandoUpdate.Parameters.AddWithValue("@tel", tel);
                    comandoUpdate.Parameters.AddWithValue("@desc", desc);
                    comandoUpdate.Parameters.AddWithValue("@total", total);
                    comandoUpdate.Parameters.AddWithValue("@servId", idServ);
                    comandoUpdate.ExecuteNonQuery();
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
