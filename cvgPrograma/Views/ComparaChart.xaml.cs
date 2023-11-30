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
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using cvgPrograma.ViewModels;

namespace cvgPrograma.Views
{
    /// <summary>
    /// Interação lógica para ComparaChart.xam
    /// </summary>
    public partial class ComparaChart : UserControl
    {
        double[] RecebeTotal { get; set; }
        double valorVenda { get; set; }
        double valorServico { get; set; }

        public ComparaChart()
        {
            InitializeComponent();
            InfoViewModel info = new InfoViewModel();
            RecebeTotal = info.Somas();
            valorVenda = RecebeTotal[0];
            valorServico = RecebeTotal[1];

            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Vendas",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(valorVenda) },
                    DataLabels = true,
                    Fill = Brushes.Gray
                },
                new PieSeries
                {
                    Title = "Serviços",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(valorServico) },
                    DataLabels = true,
                    Fill = Brushes.LightSlateGray
                }
            };

            //adding values or series will update and animate the chart automatically
            //SeriesCollection.Add(new PieSeries());
            //SeriesCollection[0].Values.Add(5);

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
    }
}
