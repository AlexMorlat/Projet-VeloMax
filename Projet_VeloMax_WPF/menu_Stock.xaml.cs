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
    /// Logique d'interaction pour menu_Stock.xaml
    /// </summary>
    public partial class menu_Stock : Window
    {
        public menu_Stock()
        {
            InitializeComponent();
            
        }

        

        private void CheckBox_Fournisseur(object sender, RoutedEventArgs e)
        {
            Par_velo.IsChecked = false;
            Par_piece.IsChecked = false;
            Par_velo_Copy.IsChecked = false;
            string requete = "SELECT nom_fournisseur,sum(stock_piece) FROM Fournisseur NATURAL JOIN Catalogue NATURAL JOIN modele_piece GROUP BY siret_fournisseur; ";
            string[] Res = Execute(requete).Split(';');

            string Res2 = "";
            foreach (string elem in Res)
            {
                string[] Finish = elem.Split(',');
                string piece = Finish[0];
                string quantite = Finish[1];
                string chaine = "Nom Fournisseur " + piece + " : " + quantite + " en stock. \n";

                Res2 = Res2 + chaine;
            }
            ///TextBlock.Text = Res2;
            TextB.Text = Res2;
        }

        private void CheckBox_Piece(object sender, RoutedEventArgs e)
        {
            Par_velo.IsChecked = false;
            Par_velo_Copy.IsChecked = false;
            Par_fournisseur.IsChecked = false;
            string requete = "SELECT numero_piece_catalogue,sum(stock_piece) FROM modele_piece GROUP BY numero_piece_catalogue ;";
            string[] Res = Execute(requete).Split(';');
            
            string Res2 = "";
            foreach (string elem in Res)
            {
                string[] Finish = elem.Split(',');
                string piece = Finish[0];
                string quantite = Finish[1];
                string chaine = "Pièce numéro " + piece + " : " + quantite + " en stock. \n";

                Res2 = Res2 + chaine;
            }
            ///TextBlock.Text = Res2;
            TextB.Text = Res2;
        }

        private void CheckBox_Velo(object sender, RoutedEventArgs e)
        {
            Par_piece.IsChecked = false;
            Par_fournisseur.IsChecked = false;
            Par_velo_Copy.IsChecked = false;
            
            string requete = "SELECT nom_velo , COALESCE(StockPieceVelo,0),stock_velo FROM modele_velo NATURAL LEFT JOIN (SELECT numero_velo,min(stock_piece)  as StockPieceVelo FROM modele_velo NATURAL JOIN liste_assemblage NATURAL JOIN modele_piece GROUP BY numero_velo)alias;";
            string[] Res = Execute(requete).Split(';');

            string Res2 = "";
            foreach (string elem in Res)
            {
                string[] Finish = elem.Split(',');
                string piece = Finish[0];
                string quantite = (Convert.ToInt32(Finish[1])+ Convert.ToInt32(Finish[2])).ToString() ;
                string chaine = "Nom du velo " + piece + " : " + quantite + " en stock. \n";

                Res2 = Res2 + chaine;
            }
            ///TextBlock.Text = Res2;
            TextB.Text = Res2;
        }

        private void CheckBox_Categorie(object sender, RoutedEventArgs e)
        {
            Par_velo.IsChecked = false;
            Par_fournisseur.IsChecked = false;
            Par_piece.IsChecked = false;
            
            string requete = "SELECT ligne_produit_velo , sum(COALESCE(StockPieceVelo,0)),sum(stock_velo) FROM modele_velo NATURAL LEFT JOIN (SELECT numero_velo,min(stock_piece)  as StockPieceVelo FROM modele_velo NATURAL JOIN liste_assemblage NATURAL JOIN modele_piece GROUP BY numero_velo)alias GROUP BY ligne_produit_velo; ";
            string[] Res = Execute(requete).Split(';');

            string Res2 = "";
            foreach (string elem in Res)
            {
                string[] Finish = elem.Split(',');
                string piece = Finish[0];
                string quantite = (Convert.ToInt32(Finish[1]) + Convert.ToInt32(Finish[2])).ToString();
                string chaine = "Nom de la catégorie " + piece + " : " + quantite + " en stock. \n";

                Res2 = Res2 + chaine;
            }
            ///TextBlock.Text = Res2;
            TextB.Text = Res2;
        }
    
        private string Execute(string requete)
        {
            string chaine = "";
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            
            MySqlCommand command1 = connection.CreateCommand();
            command1.CommandText = requete;
            MySqlDataReader reader1;
            reader1 = command1.ExecuteReader();
            /* exemple de manipulation du resultat */
            while (reader1.Read())                           // parcours ligne par ligne
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader1.FieldCount; i++)    // parcours cellule par cellule
                {
                    string valueAsString = reader1.GetValue(i).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
                    currentRowAsString += valueAsString + ",";
                }
                chaine = chaine + currentRowAsString+";";
            }
            connection.Close();
            return chaine.Trim(';');
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreerPiece next = new CreerPiece();
            next.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msg = MessageBox.Show("Creation de Velo Indisponible pour le moment");
        }
    }
}
