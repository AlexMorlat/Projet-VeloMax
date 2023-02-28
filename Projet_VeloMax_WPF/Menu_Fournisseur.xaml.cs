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
using WpfApp1.Classes;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Menu_Fournisseur.xaml
    /// </summary>
    public partial class Menu_Fournisseur : Window
    {
        public Menu_Fournisseur()
        {
            InitializeComponent();
            List<Fournisseur> Lfournisseurs = ListeFournisseur();
            InfoFournisseur.ItemsSource = Lfournisseurs;
            
        }
        private List<Fournisseur> ListeFournisseur()
        {
            List<Fournisseur> Lfournisseurs = new List<Fournisseur>();



            //Console.WriteLine("ICCCCCCCCCCCCCIIIIIIIIIIIIIIII" + Connexion.stringconnexionGenerale + "ICCCCCCCCCCCCCIIIIIIIIIIIIIIII");
            MySqlConnection connection1 = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;");

          
            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = "SELECT * FROM fournisseur;"; 
            MySqlDataReader reader0;
            reader0 = command1.ExecuteReader();

            while (reader0.Read())
            {
                string dataline = "";
                for (int i = 0; i < reader0.FieldCount; i++)
                {
                    string valueAsString = reader0.GetValue(i).ToString();
                    dataline += valueAsString + ";";
                }
                string[] tableauinfo = dataline.Split(';');
                Fournisseur F = new Fournisseur(tableauinfo[0], tableauinfo[1], tableauinfo[2], tableauinfo[3], tableauinfo[4]);
                Lfournisseurs.Add(F);
                
            }
            connection1.Close();
            return Lfournisseurs;
        }

        

        private void Creer(object sender, RoutedEventArgs e)
        {
            CreerFournisseur next = new CreerFournisseur();
            next.Show();
            this.Close();
        }

        private void Modifier(object sender, RoutedEventArgs e)
        {

            Fournisseur modifClient = (Fournisseur)InfoFournisseur.SelectedItem;

            if (modifClient != null)
            {
                string update_nom = "UPDATE Fournisseur SET nom_fournisseur ='" + modifClient.nom_fournisseur + "' WHERE siret_fournisseur ='" + modifClient.Siret_Fournisseur + "';";
                string update_prenom = "UPDATE Fournisseur SET qualite_fournisseur ='" + modifClient.Qualite_fournisseur + "' WHERE siret_fournisseur ='" + modifClient.Siret_Fournisseur + "';";
                string update_courriel = "UPDATE Fournisseur SET nom_contact_fournisseur ='" + modifClient.Nom_contact_fournisseur + "' WHERE siret_fournisseur ='" + modifClient.Siret_Fournisseur + "';";
                ///string update_siret = "UPDATE Fournisseur SET siret_fournisseur ='" + modifClient.Siret_Fournisseur + "' WHERE siret_fournisseur ='" + modifClient.Siret_Fournisseur + "';";


                Modification_client(update_nom);
                Modification_client(update_prenom);
                Modification_client(update_courriel);
                ///Modification_client(update_siret);
            }
            else
            {
                MessageBoxResult msg = MessageBox.Show("\n Vous n'avez pas selectionner de Fournisseur à modifier", "Erreur", MessageBoxButton.YesNo);

            }

        }

        private void Supprimer(object sender, RoutedEventArgs e)
        {
            Fournisseur modifClient = (Fournisseur)InfoFournisseur.SelectedItem;
            if (modifClient != null)
            {
                string sup_client = "DELETE FROM Fournisseur WHERE siret_fournisseur ='" + modifClient.siret_fournisseur + "';";
                Modification_client(sup_client);
                List<Fournisseur> LClient_particulier = ListeFournisseur();
                InfoFournisseur.ItemsSource = LClient_particulier;
            }
            else
            {
                MessageBoxResult msg = MessageBox.Show("\n Vous n'avez pas selectionner de Fournisseur à Supprimer", "Erreur", MessageBoxButton.YesNo);

            }
        }
        private void Modification_client(string requete)
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
    }
}
