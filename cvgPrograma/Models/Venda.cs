﻿using MySql.Data.MySqlClient;
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
    }
}
