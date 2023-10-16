using cvgPrograma.Interfaces;
using cvgPrograma.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace cvgPrograma.ViewModels
{
    public class EstoqueViewModel : ObservableObject
    {        
        private DataTable _dataTable;

        public DataTable dataTable
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                OnPropertyChanged(nameof(dataTable));
            }
        }

        public EstoqueViewModel()
        {
            Produto produto = new Produto();
            dataTable = produto.ConsultarProduto();
        }           
    }
}
