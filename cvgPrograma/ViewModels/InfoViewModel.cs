using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using LiveChartsCore.Defaults;
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
            Somas();            
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
      
        public double[] Somas()
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataReader dr;
            try
            {
                conexao.Open();
                string selectVenda = "SELECT sum(totalvenda) AS total_vendido_somado FROM venda;";
                string selectServico = "SELECT SUM(totalservico) AS total_somado FROM servico;";
                using (MySqlCommand comandoVenda = new MySqlCommand(selectVenda, conexao))
                {
                    using (dr = comandoVenda.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            double valorObtido = Convert.ToDouble(dr["total_vendido_somado"]);
                            TotalVendas = valorObtido;
                        }
                    }
                }
                using (MySqlCommand comandoServico = new MySqlCommand(selectServico, conexao))
                {
                    using (dr = comandoServico.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            double valorObtidos = Convert.ToDouble(dr["total_somado"]);
                            TotalServico = valorObtidos;
                        }
                    }
                }
                double[] array = { TotalVendas, TotalServico };
                return array;
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

        public SeriesCollection ColecaoPiechart { get; set; }
        public double TotalVendas { get; set; }
        public double TotalServico { get; set; }




    

    }
}
