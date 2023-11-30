using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;

namespace cvgPrograma.ViewModels
{
    public class InfoViewModel
    {
        public InfoViewModel()
        {
            QuantidadesVendidas = new ChartValues<int> { };
            NomesVendidos = new List<string> { };
            ConsegueTudo();
            ResultadoSomas();
        }



        public ChartValues<int> QuantidadesVendidas { get; set; }
        public List<string> NomesVendidos { get; set; }

        private string _connectionString = "Server=localhost;Database=casadovideogame;Uid=root;Pwd=Amorinha 24;";

        public void ConsegueTudo()
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataReader dr;
            try
            {
                conexao.Open();
                string stringSelectNomes = "SELECT p.NomeProd, SUM(pv.QuantVenda) AS total_vendido " +
                    "FROM produto p JOIN produtovenda pv ON p.ProdId = pv.ProdId " +
                    "GROUP BY p.ProdId, p.NomeProd, p.PrecoProd ORDER BY total_vendido DESC LIMIT 5;";

                using (MySqlCommand comandoConsegueNomes = new MySqlCommand(stringSelectNomes, conexao))
                {
                    using (dr = comandoConsegueNomes.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string stringObtida = dr["NomeProd"].ToString();
                            int valorObtido = Convert.ToInt32(dr["total_vendido"]);

                            if (stringObtida != null)
                                NomesVendidos.Add(stringObtida);

                            if (valorObtido != null)
                                QuantidadesVendidas.Add(valorObtido);

                        }
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


        public int SomaServico { get; set; }
        public int SomaVendas { get; set; }

        public void ResultadoSomas()
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataReader dr;
            try
            {
                conexao.Open();
                string selectServico = "SELECT SUM(totalservico) AS total_somado FROM servico;";
                using (MySqlCommand comandoServico = new MySqlCommand(selectServico, conexao))
                {
                    using (dr = comandoServico.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SomaServico = Convert.ToInt32(dr["total_somado"]);
                        }

                    }
                }
                string selectVenda = "SELECT sum(totalvenda) AS total_vendido_somado FROM venda;";
                using (MySqlCommand comandoVenda = new MySqlCommand(selectVenda, conexao))
                {
                    using (dr = comandoVenda.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SomaVendas = Convert.ToInt32(dr["total_vendido_somado"]);
                        }
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
