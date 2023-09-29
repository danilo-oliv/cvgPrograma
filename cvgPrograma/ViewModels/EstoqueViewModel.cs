using cvgPrograma.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvgPrograma.ViewModels
{
    public class EstoqueViewModel
    {
        private Produto _produto;

        public EstoqueViewModel(Produto produto)
        {
            _produto = produto;
            DataTable dataTable = produto.ConsultarProduto();
            //insere o source do datagrid
        }

    }
}
