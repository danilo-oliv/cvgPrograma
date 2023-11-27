using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using cvgPrograma.Commands;
using cvgPrograma.Models;
<<<<<<< HEAD
using Microsoft.Win32;
=======
using MySql.Data.MySqlClient;
>>>>>>> a170577a96776ba23dd4de15ca7c8df4b22d87ee

namespace cvgPrograma.ViewModels
{
    public class NovoCadastroViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NovoCadastroViewModel()
        {
            Produto produto = new Produto();
            Produtos = produto.ConsultarProduto();
            Pagamentos = MetodosPagamento();
            diaHoje = DateTime.Now;
        }


        #region Metodo de Pagamento

        public class Pagamento
        {
            public long CodPagamento { get; set; }
            public string TipoPagamento { get; set; }
        }

        private ObservableCollection<Pagamento> _pagamento;

        public ObservableCollection<Pagamento> Pagamentos
        {
            get { return _pagamento; }
            set { _pagamento = value; OnPropertyChanged(nameof(Pagamentos)); }
        }

        private Pagamento _PagamentoSelecionado;
        public Pagamento PagamentoSelecionado
        {
            get { return _PagamentoSelecionado; }
            set
            {
                if (_PagamentoSelecionado != value)
                {
                    _PagamentoSelecionado = value;
                    OnPropertyChanged(nameof(PagamentoSelecionado));
                }
            }
        }

        private Pagamento _PagamentoSelecionadoServico;
        public Pagamento PagamentoSelecionadoServico
        {
            get { return _PagamentoSelecionadoServico; }
            set
            {
                if (_PagamentoSelecionadoServico != value)
                {
                    _PagamentoSelecionadoServico = value;
                    OnPropertyChanged(nameof(PagamentoSelecionadoServico));
                }
            }
        }

        private string _connectionString = "Server=localhost;Database=casadovideogame;User=root;Password=;";
        public ObservableCollection<Pagamento> MetodosPagamento()
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            try
            {
                conexao.Open();
                MySqlDataReader dr;
                ObservableCollection<Pagamento> pagamentos2 = new ObservableCollection<Pagamento>();

                string stringSelecao = "select * from metodopagamento;";

                using (MySqlCommand comandoSelecionar = new MySqlCommand(stringSelecao, conexao))
                {
                    using (dr = comandoSelecionar.ExecuteReader())
                        while (dr.Read())
                        {
                            Pagamento pagamentos = new Pagamento
                            {
                                CodPagamento = Convert.ToInt64(dr["CodMetodo"]),
                                TipoPagamento = dr["TipoPagamento"].ToString()
                            };
                            pagamentos2.Add(pagamentos);
                        }
                }
                return pagamentos2;
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
        #endregion

        #region Produto
        #region Valores das TextBox - Produto

<<<<<<< HEAD
        private string _txbxcaminhoDoArquivo;
        public string txbxcaminhoDoArquivo
        {
            get { return _txbxcaminhoDoArquivo; }
            set
            {
                if (_txbxcaminhoDoArquivo != value)
                {
                    _txbxcaminhoDoArquivo = value;
                    OnPropertyChanged(nameof(txbxcaminhoDoArquivo));
=======
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
>>>>>>> a170577a96776ba23dd4de15ca7c8df4b22d87ee
                }
            }
        }

<<<<<<< HEAD
=======
        public decimal PrecoProdSelecionado
        {
            get { return ProdSelecionado?.PrecoProduto ?? 0; }
        }


>>>>>>> a170577a96776ba23dd4de15ca7c8df4b22d87ee
        private string _txbxNomeProduto;
        public string txbxNomeProduto
        {
            get { return _txbxNomeProduto; }
            set
            {
                if (_txbxNomeProduto != value)
                {
                    _txbxNomeProduto = value;
                    OnPropertyChanged(nameof(txbxNomeProduto));
                }
            }
        }

        private decimal _txtPrecoProduto;
        public decimal txtPrecoProduto
        {
            get { return _txtPrecoProduto; }
            set
            {
                if (_txtPrecoProduto != value)
                {
                    _txtPrecoProduto = value;
                    OnPropertyChanged(nameof(txtPrecoProduto));
                }
            }
        }

        private int _txtQuantidadeProduto;
        public int txtQuantidadeProduto
        {
            get { return _txtQuantidadeProduto; }
            set
            {
                if (_txtQuantidadeProduto != value)
                {
                    _txtQuantidadeProduto = value;
                    OnPropertyChanged(nameof(txtQuantidadeProduto));
                }
            }
        }

        #endregion

        public RelayCommand AddProdCommand => new RelayCommand(execute => AddProdHelper(), canExecute => AddProdValida());
        public RelayCommand ImagemEscolha => new RelayCommand(execute => EscolhaImagem(), canExecute => { return true; });

        public void AddProdHelper()
        {
            Produto produto = new Produto();
            try
            {
                produto.InserirProduto(txbxNomeProduto, txtPrecoProduto, txtQuantidadeProduto, txbxcaminhoDoArquivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                txbxNomeProduto = "";
                txtPrecoProduto = 0;
                txtQuantidadeProduto = 0;
                txbxcaminhoDoArquivo = "";
            }
        }


        public bool AddProdValida()
        {
            if (txbxNomeProduto != null && txtPrecoProduto > 0 && txtQuantidadeProduto > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void EscolhaImagem()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.Title = "Escolher uma Imagem";

            if (openFileDialog.ShowDialog() == true)
            {
                txbxcaminhoDoArquivo = openFileDialog.FileName;
                BitmapImage caminho = new BitmapImage(new Uri(txbxcaminhoDoArquivo));

            }
        }
        #endregion

        #region Serviço
        #region Valores das TextBox - Serviço

        private DateTime _diaHoje;

        public DateTime diaHoje
        {
            get { return _diaHoje; }
            set { _diaHoje = DateTime.Now; OnPropertyChanged(nameof(diaHoje)); }
        }



        private string _txtDesc;

        public string txtDesc
        {
            get { return _txtDesc; }
            set
            {
                _txtDesc = value;
                OnPropertyChanged(nameof(txtDesc));
            }
        }

        private decimal _txtPrecoServico;

        public decimal txtPrecoServico
        {
            get { return _txtPrecoServico; }
            set { _txtPrecoServico = value; OnPropertyChanged(nameof(txtPrecoServico)); }
        }

        private DateTime _txtDataEntrega;

        public DateTime txtDataEntrega
        {
            get { return _txtDataEntrega; }
            set { _txtDataEntrega = value; OnPropertyChanged(nameof(txtDataEntrega)); }
        }

        private string _txtMetodoPg;

        public string txtMetodoPg
        {
            get { return _txtMetodoPg; }
            set { _txtMetodoPg = value; OnPropertyChanged(nameof(txtMetodoPg)); }
        }

        private string _txtCliente;

        public string txtCliente
        {
            get { return _txtCliente; }
            set { _txtCliente = value; OnPropertyChanged(nameof(txtCliente)); }
        }

        private string _txtContato;

        public string txtContato
        {
            get { return _txtContato; }
            set { _txtContato = value; OnPropertyChanged(nameof(txtContato)); }
        }
        #endregion

        public RelayCommand AddServCommand => new RelayCommand(execute => AddServHelper(), canExecute => AddServValida());

        public void AddServHelper()
        {
            Servico servico = new Servico();
            try
            {
                servico.InserirCard(txtDesc, txtDataEntrega, txtPrecoServico, txtCliente, txtContato, txtMetodoPg);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                //txtDesc = "";
                //txtPrecoServico = 0;
                //txtCliente = "";
                //txtContato = "";
                //txtMetodoPg = "";
            }
        }

        public bool AddServValida()
        {
            if (txtDesc != null && txtPrecoServico > 0 && txtMetodoPg != null && txtCliente != null && txtContato != null)
            {
                return true;
            }
            else
                return false;
        }
        #endregion

        #region Venda
        #region Valores das TextBox - Venda
        private string _txtComboProduto;

        public string txtComboProduto
        {
            get { return _txtComboProduto; }
            set { _txtComboProduto = value; OnPropertyChanged(nameof(txtComboProduto)); }
        }

        private decimal _txtPrecoVenda;

        public decimal txtPrecoVenda
        {
            get { return _txtPrecoVenda; }
            set { _txtPrecoVenda = value; OnPropertyChanged(nameof(txtPrecoVenda)); }
        }

        private int _txtQuantidadeVenda;

        public int txtQuantidadeVenda
        {
            get { return _txtQuantidadeVenda; }
            set
            {
                if (_txtQuantidadeVenda != value)
                {
                    _txtQuantidadeVenda = value;
                    OnPropertyChanged(nameof(txtQuantidadeVenda));
                    CalculaTotal();
                }
            }
        }

        private decimal _totalVenda;

        public decimal totalVenda
        {
            get { return _totalVenda; }
            set
            {
                if (_totalVenda != value)
                {
                    _totalVenda = value;
                    OnPropertyChanged(nameof(totalVenda));                    
                }
            }
        }

        private string _txtMetodoPgVenda;

        public string txtMetodoPgVenda
        {
            get { return _txtMetodoPgVenda; }
            set { _txtMetodoPgVenda = value; OnPropertyChanged(nameof(txtMetodoPgVenda)); }
        }





        #endregion

        public void CalculaTotal()
        {
           totalVenda = PrecoProdSelecionado * txtQuantidadeVenda;
        }

        public RelayCommand AddVendaCommand => new RelayCommand(execute => AddVendaHelper(), canExecute => AddVendaValida());

        public void AddVendaHelper()
        {
            Venda venda = new Venda();
            try
            {
                venda.InserirVenda(ProdSelecionado.ToString(), txtQuantidadeVenda, totalVenda, txtMetodoPgVenda);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                //txtComboProduto = "";
                //txtQuantidadeVenda = 0;
                //totalVenda = 0;
                //txtMetodoPgVenda = "";
            }
        }

        public bool AddVendaValida()
        {
            if (PagamentoSelecionado != null && totalVenda > 0 && ProdSelecionado != null)
                return true;
            else
                return false;
        }


        #endregion



    }
}
