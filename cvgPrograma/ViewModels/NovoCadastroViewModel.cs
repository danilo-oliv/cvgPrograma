using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using cvgPrograma.Commands;
using cvgPrograma.Models;

namespace cvgPrograma.ViewModels
{
    public class NovoCadastroViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Produto
        #region Valores das TextBox - Produto

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

        public void AddProdHelper()
        {
            Produto produto = new Produto();
            try
            {
                produto.InserirProduto(txbxNomeProduto, txtPrecoProduto, txtQuantidadeProduto);
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
        #endregion

        #region Serviço
        #region Valores das TextBox - Serviço
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

        private DateOnly _txtDataEntrega;

        public DateOnly txtDataEntrega
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
            // método no model
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
            set { _txtQuantidadeVenda = value; OnPropertyChanged(nameof(txtQuantidadeVenda)); }
        }

        private decimal _totalVenda;

        public decimal totalVenda
        {
            get { return _totalVenda; }
            set { _totalVenda = txtPrecoVenda * txtQuantidadeVenda; OnPropertyChanged(nameof(totalVenda)); }
        }

        private string _txtMetodoPgVenda;

        public string txtMetodoPgVenda
        {
            get { return _txtMetodoPgVenda; }
            set { _txtMetodoPgVenda = value; OnPropertyChanged(nameof(txtMetodoPgVenda)); }
        }
        #endregion

        public RelayCommand AddVendaCommand => new RelayCommand(execute => AddVendaHelper(), canExecute => AddVendaValida());

        public void AddVendaHelper()
        {
            Venda venda = new Venda();
            try
            {
                venda.InserirVenda(txtComboProduto, txtDataEntrega, txtQuantidadeVenda, totalVenda, txtMetodoPgVenda);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                txtComboProduto = "";
                txtQuantidadeVenda = 0;
                totalVenda = 0;
                txtMetodoPgVenda = "";
            }
        }

        public bool AddVendaValida()
        {
            if (txtComboProduto != null && totalVenda > 0 && txtMetodoPgVenda != null)
                return true;
            else
                return false;
        }


        #endregion



    }
}
