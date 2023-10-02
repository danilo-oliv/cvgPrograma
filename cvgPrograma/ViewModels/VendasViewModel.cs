using cvgPrograma.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public VendasViewModel()
        {
            Venda venda = new Venda();
            dataTableVenda = venda.ConsultarVenda();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
