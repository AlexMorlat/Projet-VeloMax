using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour commande_Fournisseur.xaml
    /// </summary>
    public partial class commande_Fournisseur : Window,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));

            // With C# 6 this can be replaced with
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

        public commande_Fournisseur()
        {
            InitializeComponent();
            List<string> Client = new List<string>();
            ListeFournisseur();
            List<string> Piece = new List<string>();
            Piece = ListePiece();
            PieceBox.ItemsSource = Piece;
           
        }
        private string choixfourni;
        public string Choixfourni
        {
            get { return choixfourni; 
            }
            set { choixfourni = value; OnPropertyChanged("ChoixFourni"); ListeFournisseur(); }
            
        }
        List<int> panierPieceQuantite = new List<int>();
        List<string> panierPiece = new List<string>();
        private List<string> sumFourni = new List<string>();
        public List<string> SumFourni
        { get { return sumFourni; } set { sumFourni = value; OnPropertyChanged("SumFourni"); } }
        private void AjoutPiece_Click(object sender, RoutedEventArgs e)
        {
            bool Present = false;
            int i = 0;
            int index = 0;
            foreach (string elem in panierPiece)
            {
                if (PieceBox.SelectedItem.ToString() == elem) { Present = true; index = i; }
                i++;
            }
            if (Present == true)
            {
                panierPieceQuantite[index] = panierPieceQuantite[index] + 1;
                DataTable dataT = ConvertToDatatable(panierPiece, panierPieceQuantite);
                CommandGridPiece.ItemsSource = dataT.DefaultView;
            }
            else
            {
                panierPiece.Add(PieceBox.SelectedItem.ToString());
                panierPieceQuantite.Add(1);
                DataTable dataT = ConvertToDatatable(panierPiece, panierPieceQuantite);
                CommandGridPiece.ItemsSource = dataT.DefaultView;

            }
        }
        private static DataTable ConvertToDatatable(List<string> list, List<int> listQt)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Panier");
            dt.Columns.Add("Quantite");
            int x = 0;
            foreach (var item in list)
            {
                var row = dt.NewRow();

                row["Panier"] = item;
                row["Quantite"] = listQt[x];

                dt.Rows.Add(row);
                x++;
            }



            return dt;
        }
        private void ClientBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AffichageClient.Text = "Commander au Fournisseur : " + ClientBox.SelectedItem;
        }
        private void Piece_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            choixfourni = PieceBox.SelectedItem.ToString().Trim();
            string choixpiece = PieceBox.SelectedItem.ToString();
            ///Console.WriteLine("LAAAAAAAAAAAAAAAA"+Choixfourni);
            ClientBox.ItemsSource = ListeFournisseur();

        }
        public List<string> ListeFournisseur()
        {
            List<string> Client = new List<string>();

            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);

            if (TestForNullOrEmpty(Choixfourni) == true)
            {
                connection1.Open();
                MySqlCommand command1 = connection1.CreateCommand();
                command1.CommandText = "SELECT nom_fournisseur FROM Fournisseur ;";
                MySqlDataReader reader0;
                reader0 = command1.ExecuteReader();

                while (reader0.Read())
                {
                    string dataline = "";
                    for (int i = 0; i < reader0.FieldCount; i++)
                    {
                        string valueAsString = reader0.GetValue(i).ToString();
                        dataline += valueAsString + " ";
                    }
                    Client.Add(dataline);

                }
                connection1.Close();
            }
            else
            {
                
                connection1.Open();
                MySqlCommand command1 = connection1.CreateCommand();
                
                string requete = "SELECT nom_fournisseur,qualite_fournisseur FROM Fournisseur NATURAL JOIN catalogue WHERE numero_piece_catalogue LIKE '" + Choixfourni +"';";
                

                command1.CommandText = requete;
                MySqlDataReader reader0;
                reader0 = command1.ExecuteReader();

                while (reader0.Read())
                {
                    
                    string dataline = "";
                    ///for (int i = 0; i < reader0.FieldCount; i++)
                    //{
                        
                        string valueAsString = reader0.GetValue(0).ToString();
                        string valueAsString2 = reader0.GetValue(1).ToString();
                        dataline = valueAsString + " - " + valueAsString2;
                        
                    ///}
                    Client.Add(dataline);

                }
                connection1.Close();
            }
            return Client;
            
        }
        public List<string> ListePiece()
        {
            List<string> Piece = new List<string>();

            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = "SELECT DISTINCT numero_piece_catalogue FROM modele_piece ;";
            MySqlDataReader reader0;
            reader0 = command1.ExecuteReader();

            while (reader0.Read())
            {
                string dataline = "";
                for (int i = 0; i < reader0.FieldCount; i++)
                {
                    string valueAsString = reader0.GetValue(i).ToString();
                    dataline += valueAsString + " ";
                }
                Piece.Add(dataline);

            }
            connection1.Close();
            return Piece;
        }
        bool TestForNullOrEmpty(string s)
        {
            bool result;
            result = s == null || s == string.Empty;
            return result;
        }
        private void Validation_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < panierPiece.Count; i++)
            {

                string numeroPiece = panierPiece[i].Trim();
                int quantite = panierPieceQuantite[i];
                string StockRequete = "SELECT stock_piece FROM modele_piece WHERE numero_piece_catalogue = '" + numeroPiece + "' ;";
                int Stock = Find(StockRequete);
                int Reste = Stock + quantite;
                string requete = "UPDATE modele_piece SET stock_piece = " + Reste + " WHERE numero_piece_catalogue = '" + numeroPiece + "';";
                InsertRequete(requete);
            }
            AlerteText.Text = "Commande efféctuée";
            Fermeture.Visibility = Visibility.Visible;
            Validation.Visibility = Visibility.Collapsed;
            MessageBoxResult msg = MessageBox.Show("\n Voulez vous confirmer la commande?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

        }
        private int Find(string requete)
        {


            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = requete;
            MySqlDataReader reader0;
            reader0 = command1.ExecuteReader();
            int numero = 0;
            while (reader0.Read())
            {
                numero = Convert.ToInt32(reader0.GetValue(0));
            }
            reader0.Close();
            return numero;
        }
        private void InsertRequete(string requete)
        {
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);
            connection1.Open();
            MySqlCommand command = connection1.CreateCommand();
            command.CommandText = requete;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException erreur)
            {
                Console.WriteLine(erreur);
            }
            connection1.Close();

        }

        private void Fermeture_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
