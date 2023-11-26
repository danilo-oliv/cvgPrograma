using cvgPrograma.Models;
using cvgPrograma.Views;
using cvgPrograma.Commands;
using MaterialDesignThemes.Wpf;
using Microsoft.VisualBasic;
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
        public RelayCommand Imagem => new RelayCommand(execute => EscolherImagem(), canExecute => { return true; });


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

        private string _upcaminhoDoArquivo;

        public string upcaminhoDoArquivo
        {
            get { return _upcaminhoDoArquivo; }
            set { _upcaminhoDoArquivo = value; OnPropertyChanged(nameof(upcaminhoDoArquivo)); }
        }

        private string _updateNome;

        public string updateNome
        {
            get { return _updateNome; }
            set { _updateNome = value; OnPropertyChanged(nameof(updateNome)); }
        }

        private int _updateQuant;

        public int updateQuant
        {
            get { return _updateQuant; }
            set { _updateQuant = value; OnPropertyChanged(nameof(updateQuant)); }
        }


        private decimal _updatePreco;

        public decimal updatePreco
        {
            get { return _updatePreco; }
            set { _updatePreco = value; OnPropertyChanged(nameof(updatePreco)); }
        }

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
        public ICommand UpdateDoProduto { get; set; }

        public EstoqueViewModel()
        {
            Produto produto = new Produto();
            Produtos = produto.ConsultarProduto();
            DeletCommand = new RelayCommand(DelProdHelper);
            UpdateDoProduto = new RelayCommand(UpdateProdHelper);
        }

        public void UpdateProdHelper(object parameter)
        {
            try
            {
                if (parameter is long ProdutoId)
                    UpdateProduto(ProdutoId, updateNome, updatePreco, updateQuant, upcaminhoDoArquivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {

            }
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
        public void UpdateProduto(long ProdutoId, string updateNome, decimal updatePreco, int updateQuant, string upcaminhoDoArquivo)
        {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            try
            {
                conexao.Open();
                string updateProduto = "UPDATE estoque SET QuantidadeProduto = @QuantidadeProduto WHERE ProdId = @ProdId;";
                string updateProduto2 = "UPDATE produto SET NomeProd = @NomeProd, CaminhoImagem = @CaminhoImagem, PrecoProd = @PrecoProd WHERE ProdId = @ProduId;";

                using (MySqlCommand comandoUpdateProduto = new MySqlCommand(updateProduto, conexao))
                {

                    comandoUpdateProduto.Parameters.AddWithValue("@QuantidadeProduto", updateQuant);
                    comandoUpdateProduto.Parameters.AddWithValue("@ProdId", ProdutoId);
                    comandoUpdateProduto.ExecuteNonQuery();

                    using (MySqlCommand comandoUpdateProduto2 = new MySqlCommand(updateProduto2, conexao))
                    {
                        comandoUpdateProduto2.Parameters.AddWithValue("@NomeProd", updateNome);
                        comandoUpdateProduto2.Parameters.AddWithValue("@PrecoProd", updatePreco);
                        comandoUpdateProduto2.Parameters.AddWithValue("@ProduId", ProdutoId);
                        comandoUpdateProduto2.Parameters.AddWithValue("@CaminhoImagem", upcaminhoDoArquivo);
                        comandoUpdateProduto2.ExecuteNonQuery();
                    }
            
                }
            
           

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }


        private void EscolherImagem()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.Title = "Escolher uma Imagem";

            if (openFileDialog.ShowDialog() == true)
            {
                upcaminhoDoArquivo = openFileDialog.FileName;
                BitmapImage caminho = new BitmapImage(new Uri(upcaminhoDoArquivo));

            }
        }







    }
}
