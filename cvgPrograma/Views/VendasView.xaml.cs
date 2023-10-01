using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cvgPrograma.Views
{
    /// <summary>
    /// Interação lógica para VendasView.xam
    /// </summary>
    public partial class VendasView : UserControl
    {
        public VendasView()
        {
            InitializeComponent();


            List<Venda> listaVendas = new List<Venda>
            {
  
                new Venda { ID="102030", Produto  ="Mouse", Preço="50,00", Pagamento="Cartão de credito", Cliente="Lays", Quantidade = 5,  Data = "01/09/2023" },
                new Venda { ID="102030", Produto="Mouse", Preço="50,00", Pagamento="Cartão de credito", Cliente="Lays", Quantidade = 5,  Data = "01/09/2023" },
                new Venda { ID="102030", Produto="Mouse", Preço="50,00", Pagamento="Cartão de credito", Cliente="Lays", Quantidade = 5,  Data = "01/09/2023" }
                // Adicione mais vendas conforme necessário
            };
            dataGridVendas.ItemsSource = listaVendas;
        }
    }
    public class Venda
    {
        public string ID { get; set; }
        public string Produto { get; set; }
        public string Preço { get; set; }
        public string Pagamento { get; set; }
        public string Cliente { get; set; }
        public int Quantidade { get; set; }
        public string Data { get; set; }
        
        
    }
}
