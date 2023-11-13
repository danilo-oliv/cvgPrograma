using cvgPrograma.Commands;
using cvgPrograma.Models;
using cvgPrograma.Views;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using LiveChartsCore.Drawing;

namespace cvgPrograma.ViewModels
{
    public class EstoqueViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand AtualizarCollection => new RelayCommand(execute => AtualizarMetodo(), canExecute => { return true; });
        public RelayCommand JanelaNovo => new RelayCommand(execute => AbrirNovo(), canExecute => { return true; });



        public void AbrirNovo()
        {
            NovoView novo = new NovoView();
            novo.tabControlNovo.SelectedIndex = 2;
            novo.Show();
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
        #region Valores das TextBox

        private long _Idproduto;

        public long Idproduto
        {
            get { return _Idproduto; }
            set { _Idproduto = value; OnPropertyChanged(nameof(Idproduto)); }
        }



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
        
        public ICommand DeletCommand { get; set; }

        public EstoqueViewModel()
        {
            Produto produto = new Produto();
            Produtos = produto.ConsultarProduto();
            DeletCommand = new RelayCommand(DelProdHelper);
        }

        public void AtualizarMetodo()
        {
            Produto produto = new Produto();
            Produtos = produto.ConsultarProduto();
        }

        public void DelProdHelper(object parameter )
        {
            Produto produto = new Produto();
            if (parameter is long Produto_Id )
            {
                produto.DeletarProduto(Produto_Id);
                AtualizarMetodo();
            }
        }

        private string _connectionString = "Server=localhost;Database=casadovideogame;Uid=root;Pwd=;";

        public object MaisVendidos()
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataReader dr;
            ObservableCollection<Produto> maisVendidos = new ObservableCollection<Produto>();
            try
            {
                conexao.Open();

                string pegaMaisVendidos = "SELECT p.NomeProd, SUM(pv.QuantVenda) AS total_vendido" +
                    "FROM produto p JOIN produtovenda pv ON p.ProdId = pv.ProdId GROUP BY p.ProdId, p.NomeProd, p.PrecoProd" +
                    "ORDER BY total_vendido DESC LIMIT 3;";

                using (MySqlCommand comandoMaisVendidos = new MySqlCommand(pegaMaisVendidos, conexao))
                {
                    using (dr = comandoMaisVendidos.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Produto maisvendido = new Produto
                            {
                                NomeProduto = dr["NomeProd"].ToString(),
                                QuantVenda = Convert.ToInt32(dr["total_vendido"]),
                            };
                            maisVendidos.Add(maisvendido);
                        }
                    }
                }
                return maisVendidos;
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





        #region Grafico



        // Mais Vendido
        public ISeries[] Series { get; set; } =
     {
        new ColumnSeries<double>
        {
            // Conseguir Nome String
            Name = "P1",
            // Conseguir Quantidade Vendida Int
            Values = new double[] { 2 } 
        },
        new ColumnSeries<double>
        {
            DataPadding = new LvcPoint(0, 0),
            Name = "P2",
            Values = new double[] { 3 }
        },
        new ColumnSeries<double>
        {
            DataPadding = new LvcPoint(0, 0),
            Name = "P3",
            Values = new double[] { 4 }
        }
    };

        public Axis[] XAxes { get; set; } =
        {
        new Axis
        {
            Labels = new string[] { "Top 1", "Top 2", "Top 3" },
            LabelsRotation = 0,
            SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
            SeparatorsAtCenter = false,
            TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
            TicksAtCenter = true,
        }
    };


        #endregion



    }
}
