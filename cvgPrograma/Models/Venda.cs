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
        private string _connectionString = "Server=localhost;Database=casadovideogame;Uid=root;Pwd=Amorinha 24;";


        public int Id { get; set; }
        public string Produto { get; set; }
        public float Preco { get; set; }
        public string MetodoPagamento { get; set; }
        public string Cliente { get; set; }
        public string Contato { get; set; }

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

                string consultarVenda = "SELECT * FROM VENDA;";
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
    }
}
  