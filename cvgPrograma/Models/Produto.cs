using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.ObjectModel;
using cvgPrograma.ViewModels;
using System.ComponentModel;

namespace cvgPrograma.Models
{
    public class Produto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _connectionString = "Server=localhost;Database=casadovideogame;Uid=root;Pwd=;";        

        public long ProdutoId { get; set;  }

        private long _retornaProdutoId;

        public long retornaProdutoId
        {
            get { return _retornaProdutoId; }
            set { _retornaProdutoId = ProdutoId; }
        }

        private string _atributoVisibilidade;

        public string atributoVisibilidade
        {
            get { return _atributoVisibilidade; }
            set { _atributoVisibilidade = value; OnPropertyChanged(nameof(atributoVisibilidade)); }
        }



        public string? NomeProduto { get; set; }
        public decimal PrecoProduto { get; set; }

        public long EstoqueId { get; set; }
        public int QuantidadeEstoque { get; set; }

        public ObservableCollection<Produto> ConsultarProduto()
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataReader dr;
            ObservableCollection<Produto> produtos = new ObservableCollection<Produto>();
            try 
            {
                conexao.Open();

                string consultarProduto = "SELECT * FROM PRODUTO as p INNER JOIN estoque as e on p.ProdId = e.ProdId";
                using (MySqlCommand comandoConsultarProduto = new MySqlCommand(consultarProduto, conexao))
                {
                    using (dr = comandoConsultarProduto.ExecuteReader()) 
                    { 
                        while (dr.Read()) 
                        {
                            Produto produto = new Produto
                            {
                                ProdutoId = Convert.ToInt32(dr["ProdId"]),
                                NomeProduto = dr["NomeProd"].ToString(),
                                PrecoProduto = Convert.ToDecimal(dr["PrecoProd"]),
                                EstoqueId = Convert.ToInt32(dr["EstoqueId"]),
                                QuantidadeEstoque = Convert.ToInt32(dr["QuantidadeProduto"]),
                            };
                            produtos.Add(produto);
                        }
                    }
                }
                return produtos;
            } 
            catch(Exception ex)
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
