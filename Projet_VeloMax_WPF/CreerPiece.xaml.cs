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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour CreerPiece.xaml
    /// </summary>
    public partial class CreerPiece : Window
    {
        public CreerPiece()
        {
            InitializeComponent();
            List<string> DescriptionPiece = ListeDescriptionPiece();
            DescriptionBox.ItemsSource = DescriptionPiece;
            List<string> FournisseurPiece = ListeFournisseur();
            FournisseurBox.ItemsSource = FournisseurPiece;
        }

        public List<string> ListeDescriptionPiece()
        {
            List<string> Piece = new List<string>();

            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = "SELECT DISTINCT description_piece FROM modele_piece ;";
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
        public List<string> ListeFournisseur()
        {
            List<string> Client = new List<string>();

            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);

            
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
            
            
            return Client;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string num_piece = numero_piece.Text;
            string num_piece_catalogue = numero_piece_catalogue.Text;
            string description_piece = DescriptionBox.Text;
            string date_intro = DateTime.Now.ToString("yyyy-MM-dd");
            string prix_piece = prix.Text;
            int delai_livr = Convert.ToInt32(delai.Text);
            string nomFournisseur = FournisseurBox.Text.Trim();


            Random R = new Random();
            int id_number = R.Next(20, 100);
            string id = "cp_" + id_number;
            DateTime date = DateTime.Now;
            string ajd = date.ToString("yyyy-MM-dd");


           
            
            string requete_creaPiece = "INSERT INTO modele_piece VALUES " +
                        "('" + num_piece + "','" + num_piece_catalogue + "','" + description_piece + "'," +
                        "'" + date_intro + "','" + null + "'," + prix_piece + "," + delai_livr + ","+0+");";
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);
            connection1.Open();
            MySqlCommand command = connection1.CreateCommand();
            command.CommandText = requete_creaPiece;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException erreur)
            {
                Console.WriteLine(erreur);
            }
            
            connection1.Close();
            Console.WriteLine("-" + nomFournisseur + "-");
            string requete_creacatalogue = "INSERT INTO catalogue VALUES('"+num_piece+"', (SELECT siret_fournisseur FROM Fournisseur WHERE nom_fournisseur = '"+ nomFournisseur + "'));";
            string connectionString2 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection2 = new MySqlConnection(connectionString2);
            connection2.Open();
            MySqlCommand command2 = connection2.CreateCommand();
            command2.CommandText = requete_creacatalogue;
            try
            {
                command2.ExecuteNonQuery();
            }
            catch (MySqlException erreur)
            {
                Console.WriteLine(erreur);
            }

            connection2.Close();
            this.Close();

        }
    }
}
