﻿using cvgPrograma.Commands;
using cvgPrograma.Models;
using cvgPrograma.Views;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace cvgPrograma.ViewModels
{
    public class VendasViewModel : INotifyPropertyChanged
    {

        private DataTable _dataTableVenda;

        public DataTable dataTableVenda
        {
            get { return _dataTableVenda; }
            set
            {
                _dataTableVenda = value;
                OnPropertyChanged(nameof(dataTableVenda));
            }
        }

        #region TEXTBOXES
        private decimal _txtTotalVenda;

        public decimal txtTotalVenda
        {
            get { return _txtTotalVenda; }
            set 
            { 
                _txtTotalVenda = value;
                OnPropertyChanged(nameof(txtTotalVenda));
            }
        }

        private int _txtQuantidadeVendida;

        public int txtQuantidadeVendida
        {
            get { return _txtQuantidadeVendida; }
            set
            { 
                _txtQuantidadeVendida = value;
                OnPropertyChanged(nameof(txtQuantidadeVendida));
            }
        }


        #endregion




        public VendasViewModel()
        {
            Venda venda = new Venda();
            dataTableVenda = venda.ConsultarVenda();            
        }

        


        public RelayCommand AtualizarVendas => new RelayCommand(execute => AtualizarTabela(), canExecute => { return true; });
        public RelayCommand JanelaNovo => new RelayCommand(execute => AbrirNovo(), canExecute => { return true;  } );



        public void AbrirNovo()
        {
            NovoView novo = new NovoView();
            novo.tabControlNovo.SelectedIndex = 3;
            novo.Show();
        }

        public void AtualizarTabela()
        {
            Venda venda = new Venda();
            dataTableVenda = venda.ConsultarVenda();
        }

        private string _connectionString = "Server=localhost;Database=casadovideogame; Uid=root;Pwd=Amorinha 24;";
        public void UpdateVenda(int VendaId, decimal NovoTotal)
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            try
            {
                conexao.Open();
                string updateTabelaVenda = "UPDATE venda SET TotalVenda = @NovoTotal, ProdId = @NovoProdId " +
                    "WHERE VendaId = @VendaId";

                string updateTabelaProdutoVenda = "UPDATE produtovenda SET QuantVenda = @NovaQuantidade WHERE VendaId = @VendaId";

                using(MySqlCommand comandoUpdateTabelaVenda = new MySqlCommand(updateTabelaVenda, conexao)) 
                {
                    // if date_textbox !null
                    comandoUpdateTabelaVenda.Parameters.AddWithValue("@NovoTotal", NovoTotal);                    
                    comandoUpdateTabelaVenda.Parameters.AddWithValue("@VendaId", VendaId);
                    comandoUpdateTabelaVenda.Parameters.AddWithValue("@NovoId", 5);
                    comandoUpdateTabelaVenda.ExecuteNonQuery();
                }
                using(MySqlCommand comandoUpdateTabelaProdutoVenda = new MySqlCommand(updateTabelaProdutoVenda, conexao))
                {
                    comandoUpdateTabelaProdutoVenda.Parameters.AddWithValue("@NovaQuantidade", 2);
                    comandoUpdateTabelaProdutoVenda.Parameters.AddWithValue("@VendaId", VendaId);
                    comandoUpdateTabelaProdutoVenda.ExecuteNonQuery();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        #region SISTEMA DE BUSCA

        public ObservableCollection<Produto> ListaProdutos { get; set; }
        private string _termoDeBusca;
        private ObservableCollection<Produto> _itensFiltrados;

        public string TermoDeBusca
        {
            get { return _termoDeBusca; }
            set
            {
                _termoDeBusca = value;
                FiltrarItens();
                OnPropertyChanged(nameof(_termoDeBusca));
            }
        }

        public ObservableCollection<Produto> ItensFiltrados
        {
            get { return _itensFiltrados; }
            set
            {
                _itensFiltrados = value;
                OnPropertyChanged(nameof(ItensFiltrados));
            }
        }

        private void FiltrarItens()
        {
            Produto produto = new Produto();
            ListaProdutos = produto.ConsultarProduto();

            if (_termoDeBusca is null || _termoDeBusca == "")
            {             
                ItensFiltrados = new ObservableCollection<Produto>(ListaProdutos);
            }
            else
            {
                ItensFiltrados = new ObservableCollection<Produto>(
                    ListaProdutos.Where(produto => produto.NomeProduto.IndexOf(_termoDeBusca, StringComparison.OrdinalIgnoreCase) >= 0)

                );
            }
        }
        #endregion



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
