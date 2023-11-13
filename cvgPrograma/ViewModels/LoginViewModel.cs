using cvgPrograma.Commands;
using cvgPrograma.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cvgPrograma.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _connectionString = "Server=localhost;Database=casadovideogame;Uid=root;Pwd=root;";

        public RelayCommand BotaoLogar => new RelayCommand(execute => AcaoLogar(), canExecute => { return true; });
        private string _txbxcampoUsuario;
        public string txbxcampoUsuario
        {
            get { return _txbxcampoUsuario; }
            set
            {
                if (_txbxcampoUsuario != value)
                {
                    _txbxcampoUsuario = value;
                    OnPropertyChanged(nameof(txbxcampoUsuario));
                }
            }
        }

        private string _txbxcampoSenha;

        

        public string txbxcampoSenha
        {
            get { return _txbxcampoSenha; }
            set
            {
                if (_txbxcampoSenha != value)
                {
                    _txbxcampoSenha = value;
                    OnPropertyChanged(nameof(txbxcampoSenha));
                }
            }
        }
        public bool sucesso { get; set; }
        public void AcaoLogar()
        {
            sucesso = Logar(txbxcampoUsuario, txbxcampoSenha);
            if (sucesso is true)
            {
                Login login = new Login();
                login.Close();

                MainWindow main = new MainWindow();
                main.Show();
            }

            else
            {
                MessageBox.Show("Erro: ");
            }
        }

        public bool Logar(string txbxcampoUsuario, string txbxcampoSenha) {
            MySqlConnection conexao = new MySqlConnection(_connectionString);
            MySqlDataReader dr;
            ObservableCollection<Produto> produtos = new ObservableCollection<Produto>();
            try
            {
                conexao.Open();
                string login = "SELECT * FROM usuario WHERE usuario=@Usuario and senha=@Senha";
                using (MySqlCommand logar = new MySqlCommand(login, conexao))
                {
                    logar.Parameters.AddWithValue("@Usuario", txbxcampoUsuario);
                    logar.Parameters.AddWithValue("@Senha", txbxcampoUsuario);
                    dr = logar.ExecuteReader();
                    if (dr.Read() == true)
                    {
                        return true;
                    }
                    else { return false; }
                }
            }
            catch
            {
                MessageBox.Show("Erro: ");
                return false;
            }
            finally { conexao.Close(); }
            }
            }
    
}
