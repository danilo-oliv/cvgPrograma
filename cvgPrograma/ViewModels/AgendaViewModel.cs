using cvgPrograma.Commands;
using cvgPrograma.Models;
using cvgPrograma.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;


namespace cvgPrograma.ViewModels
{
    public class AgendaViewModel : INotifyPropertyChanged
    {

        private DataTable _dataTableServico;

        public DataTable dataTableServico
        {
            get { return _dataTableServico; }
            set
            {
                _dataTableServico = value;
                OnPropertyChanged(nameof(dataTableServico));
            }
        }

        /*public AgendaViewModel()
        {
            Servico Servico = new Servico();
            dataTableServico = Servico.InserirCard();
        }*/

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RelayCommand AtualizarCollection => new RelayCommand(execute => AtualizarMetodo(), canExecute => { return true; });

        private ObservableCollection<Servico> _servico;
        public ObservableCollection<Servico> Servicos
        {
            get { return _servico; }
            set
            {
                _servico = value;
                OnPropertyChanged(nameof(Servicos));
            }
        }

       #region Valores das TextBox

        private int _ServicoId;

        public int ServicoId
        {
            get { return _ServicoId; }
            set { _ServicoId = value; OnPropertyChanged(nameof(ServicoId)); }
        }



        private string _txbxDescServico;
        public string txbxDescServico
        {
            get { return _txbxDescServico; }
            set
            {
                if (_txbxDescServico != value)
                {
                    _txbxDescServico = value;
                    OnPropertyChanged(nameof(txbxDescServico));
                }
            }
        }

        private string _txbxProntoServico;
        public string txbxProntoServico
        {
            get { return _txbxProntoServico; }
            set
            {
                if (_txbxProntoServico != value)
                {
                    _txbxProntoServico = value;
                    OnPropertyChanged(nameof(txbxProntoServico));
                }
            }
        }

        private decimal _txtTotalServico;
        public decimal txtTotalServico
        {
            get { return _txtTotalServico; }
            set
            {
                if (_txtTotalServico != value)
                {
                    _txtTotalServico = value;
                    OnPropertyChanged(nameof(txtTotalServico));
                }
            }
        }

        private int _txtClientId;
        public int txtClientId
        {
            get { return _txtClientId; }
            set
            {
                if (_txtClientId != value)
                {
                    _txtClientId = value;
                    OnPropertyChanged(nameof(txtClientId));
                }
            }
        }

        private int _txtCodMetodo;
        public int txtCodMetodo
        {
            get { return _txtCodMetodo; }
            set
            {
                if (_txtCodMetodo != value)
                {
                    _txtCodMetodo = value;
                    OnPropertyChanged(nameof(txtCodMetodo));
                }
            }
        }

        private int _txtCodServico;
        public int txtCodServico
        {
            get { return _txtCodServico; }
            set
            {
                if (_txtCodServico != value)
                {
                    _txtCodServico = value;
                    OnPropertyChanged(nameof(txtCodServico));
                }
            }
        }

        #endregion

        public ICommand DeletCommand { get; set; }

        public AgendaViewModel()
        {
            Servico Servico = new Servico();
            Servicos = Servico.ConsultarCard();
            DeletCommand = new RelayCommand(DelProdHelper);
        }
        public void AtualizarMetodo()
        {
            Servico Servico = new Servico();
            Servicos = Servico.ConsultarCard();
        }

        public void DelProdHelper(object parameter )
        {
            Servico servico = new Servico();
            if ( parameter is long Servico_Id) 
            {
                servico.DeletarServico(Servico_Id);
                AtualizarMetodo();
            }
        }
        public RelayCommand JanelaNovo => new RelayCommand(execute => AbrirNovo(), canExecute => { return true; });

        public void AbrirNovo()
        {
            NovoView novo = new NovoView();
            novo.tabControlNovo.SelectedIndex = 1;
            novo.Show();
        }









    }



    //public BuscarCard()
}