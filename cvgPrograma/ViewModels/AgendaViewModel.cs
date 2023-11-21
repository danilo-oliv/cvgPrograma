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

        public AgendaViewModel()
        {
            Servico Servico = new Servico();

        }
        public void AtualizarMetodo()
        {
            Servico Servico = new Servico();

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