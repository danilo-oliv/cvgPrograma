using cvgPrograma.Commands;
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
using System.Windows.Input;

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
            DeletCommand = new RelayCommand(DelProdHelper);
            UpdateCommand = new RelayCommand(UpdateVenda);
            Produto produto = new Produto();
            Produtos = produto.ConsultarProduto();
        }


        public ICommand UpdateCommand { get; set; }

        public void UpdateVenda(object parameter)
        {
            Venda venda = new Venda();
            int Venda_Id = Convert.ToInt32(parameter);
            string nome_produto = ProdSelecionado.ToString();
            venda.UpdateVenda(nome_produto, quantProd, Venda_Id, totalVenda);
            AtualizarTabela();
        }



        public RelayCommand AtualizarVendas => new RelayCommand(execute => AtualizarTabela(), canExecute => { return true; });
        public RelayCommand JanelaNovo => new RelayCommand(execute => AbrirNovo(), canExecute => { return true;  } );
        private ObservableCollection<Venda> _vendas; // alterei o nome da propriedade para _vendas

        public ObservableCollection<Venda> Vendas
        {
            get { return _vendas; }
            set
            {
                _vendas = value;
                OnPropertyChanged(nameof(Vendas));
            }
        }



        public void AbrirNovo()
        {
            NovoView novo = new NovoView();
            novo.tabControlNovo.SelectedIndex = 3;
            novo.Show();
        }

        public void AtualizarTabela()
        {
            Venda Venda = new Venda();
            dataTableVenda = Venda.ConsultarVenda();
        }

        private string _connectionString = "Server=localhost;Database=casadovideogame; Uid=root;Pwd=;";
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

        
        public ICommand DeletCommand { get; set; }
        
        public void DelProdHelper(object parameter )
        {
            Venda venda = new Venda();         
            venda.DeletarVenda(Convert.ToInt32(parameter));
            AtualizarTabela();
        }


        #region Box pro update

        private string _nomeProd;

        public string nomeProd
        {
            get { return _nomeProd; }
            set { _nomeProd = ProdSelecionado.ToString(); OnPropertyChanged(nameof(nomeProd)); }
        }

        private decimal _precoProd;

        public decimal precoProd
        {
            get { return _precoProd; }
            set { _precoProd = value; OnPropertyChanged(nameof(precoProd)); }
        }

        private int _quantProd;

        public int quantProd
        {
            get { return _quantProd; }
            set { _quantProd = value; OnPropertyChanged(nameof(quantProd)); CalculaTotal(); }
        }

        private decimal _totalVenda;

        public decimal totalVenda
        {
            get { return _totalVenda; }
            set { _totalVenda = value; OnPropertyChanged(nameof(totalVenda)); }
        }

        public void CalculaTotal()
        {
            totalVenda = PrecoProdSelecionado * quantProd;
        }

        private ObservableCollection<Produto> _produto;
        public ObservableCollection<Produto> Produtos
        {
            get { return _produto; }
            set
            {
                _produto = value;
                OnPropertyChanged(nameof(Produtos));
            }
        }

        private Produto _ProdSelecionado;
        public Produto ProdSelecionado
        {
            get { return _ProdSelecionado; }
            set
            {
                if (_ProdSelecionado != value)
                {
                    _ProdSelecionado = value;
                    OnPropertyChanged(nameof(ProdSelecionado));
                    OnPropertyChanged(nameof(PrecoProdSelecionado));
                }
            }
        }

        public decimal PrecoProdSelecionado
        {
            get { return ProdSelecionado?.PrecoProduto ?? 0; }
        }





        #endregion

    }

}
