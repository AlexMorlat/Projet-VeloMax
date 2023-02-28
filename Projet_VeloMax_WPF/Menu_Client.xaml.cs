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
using System.IO;
using WpfApp1.Classes;
using System.Data;
using Newtonsoft.Json;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Menu_Client.xaml
    /// </summary>
    public partial class Menu_Client : Window
    {
        
        public Menu_Client()
        {
            InitializeComponent();
            
        }


        private void CheckBox_Particulier(object sender, RoutedEventArgs e)
        {
            Check_entreprise.IsChecked = false;
            List<Client> LClient_particulier = ListeClient_Particulier();
            InfoClient.ItemsSource = LClient_particulier;
            string nbParticulier = Nbclient(1);
            AffichageNbClient.Text = "Nombre de client particulier = "+nbParticulier;


        }

        private void Client_Entreprise(object sender, RoutedEventArgs e)
        {
            Check_particulier.IsChecked = false;
            List<Client_entreprise> LClient_Entreprise = ListeClient_Entreprise();
            InfoClient.ItemsSource = LClient_Entreprise;
            string nbEntreprise = Nbclient(2);
            AffichageNbClient.Text = "Nombre de client entreprise = "+nbEntreprise;
        }
        private List<Client> ListeClient_Particulier()
        {
            List<Client> LClient_particulier = new List<Client>();
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            ///Connexion co = new Connexion();
            ///string connectionString1 = co.stringconnexionGenerale;
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = "SELECT * FROM client_particulier;";
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
                Client F = new Client(tableauinfo[0], tableauinfo[1], tableauinfo[2], tableauinfo[3], tableauinfo[4],tableauinfo[5], tableauinfo[6] );
                LClient_particulier.Add(F);
                
            }
            connection1.Close();
            return LClient_particulier;
        }
        private List<Client_entreprise> ListeClient_Entreprise()
        {
            List<Client_entreprise> LClient_entreprise = new List<Client_entreprise>();
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = "SELECT * FROM client_entreprise;";
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
                Client_entreprise F = new Client_entreprise(tableauinfo[0], tableauinfo[1], tableauinfo[5], tableauinfo[2], tableauinfo[3],tableauinfo[4]);
                LClient_entreprise.Add(F);

            }
            connection1.Close();
            return LClient_entreprise;
        }
        private string Nbclient(int choix)
        {
            string nb = "";
            string requete = "";
            if (choix == 1)
            {
                requete = "SELECT count(*) from client_particulier;";
            }
            if (choix == 2)
            {
                requete = "SELECT count(*) from client_entreprise;";
            }
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);
            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = requete;
            MySqlDataReader reader0;
            reader0 = command1.ExecuteReader();

            while (reader0.Read())
            {
                nb = reader0.GetValue(0).ToString();
            }
            return nb;
        }

        private void Creer(object sender, RoutedEventArgs e)
        {
            if (Check_particulier.IsChecked == true)
            {
                CreerClient next = new CreerClient();
                next.Show();
                Label1.Text = "Création du nouveau client efféctuée";
                /// Rafraichir les données
                List<Client> LClient_particulier = ListeClient_Particulier();
                InfoClient.ItemsSource = LClient_particulier;
                string nbParticulier = Nbclient(1);
                AffichageNbClient.Text = "Nombre de client particulier = " + nbParticulier;
            }
            if (Check_entreprise.IsChecked == true)
            {
                CreerClient_entreprise next = new CreerClient_entreprise();
                next.Show();
                Label1.Text = "Création du nouveau client efféctuée";
                /// Rafraichir les données
                List<Client_entreprise> LClient_entreprise = ListeClient_Entreprise();
                InfoClient.ItemsSource = LClient_entreprise;
                string nbEntreprise = Nbclient(2);
                AffichageNbClient.Text = "Nombre de client entreprise = " + nbEntreprise;
            }
        }

        private void Modifier(object sender, RoutedEventArgs e)
        {
            if (Check_particulier.IsChecked == true)
            {
                Client modifClient = new Client();
                try
                {
                    modifClient = (Client)InfoClient.SelectedItem;
                }
                catch (InvalidCastException erreur)
                {
                    MessageBoxResult msg = MessageBox.Show(erreur + "\n Modification Impossible ", "Confirmation", MessageBoxButton.YesNo);

                }
                ///String name = InfoClient.Columns[InfoClient.CurrentCell.ColumnIndex].Name;
                if (modifClient != null)
                {
                    Console.WriteLine("LAAAAAAAAAAAAAAAAAAAA" + (Client)InfoClient.SelectedItem);
                    string update_nom = "UPDATE client_particulier SET nom_client_particulier ='" + modifClient.Nom_client + "' WHERE ID_client_particulier ='" + modifClient.Id_client + "';";
                    string update_prenom = "UPDATE client_particulier SET prenom_client_particulier ='" + modifClient.Prenom_client + "' WHERE id_client_particulier ='" + modifClient.Id_client + "';";
                    string update_courriel = "UPDATE client_particulier SET courriel_particulier ='" + modifClient.Courriel + "' WHERE id_client_particulier ='" + modifClient.Id_client + "';";
                    string update_telephone = "UPDATE client_particulier SET telephone_particulier ='" + modifClient.Telephone + "' WHERE id_client_particulier ='" + modifClient.Id_client + "';";
                    string update_programme = "UPDATE client_particulier SET numero_programme ='" + modifClient.Numero_programme + "' WHERE id_client_particulier ='" + modifClient.Id_client + "';";


                    Modification_client(update_nom);
                    Modification_client(update_prenom);
                    Modification_client(update_courriel);
                    Modification_client(update_telephone);
                    Modification_client(update_programme);
                    Label1.Text = "Modification du client  " + modifClient.Id_client + " efféctuée";
                }
                else
                {
                    MessageBoxResult msg = MessageBox.Show("\n Vous n'avez pas selectionner de client à modifier", "Erreur", MessageBoxButton.YesNo);

                }
                ///Rafraichir les données
                List<Client> LClient_particulier = ListeClient_Particulier();
                InfoClient.ItemsSource = LClient_particulier;
                string nbParticulier = Nbclient(1);
                AffichageNbClient.Text = "Nombre de client particulier = " + nbParticulier;
            }
            if (Check_entreprise.IsChecked == true)
            {
                Client_entreprise modifClient_entreprise = new Client_entreprise();
                try
                {
                    modifClient_entreprise = (Client_entreprise)InfoClient.SelectedItem;
                }
                catch (InvalidCastException erreur)
                {
                    MessageBoxResult msg = MessageBox.Show(erreur + "\n Modification Impossible ", "Confirmation", MessageBoxButton.YesNo);

                }
                if (modifClient_entreprise != null)
                {
                    ///Console.WriteLine("LAAAAAAAAAAAAAAAAAAAA" + (Client)InfoClient.SelectedItem);
                    string update_nom = "UPDATE client_entreprise SET nom_client_entreprise ='" + modifClient_entreprise.Nom_client + "' WHERE ID_client_entreprise ='" + modifClient_entreprise.Id_client + "';";
                    string update_prenom = "UPDATE client_entreprise SET nom_contact_entreprise ='" + modifClient_entreprise.Contact_client + "' WHERE ID_client_entreprise ='" + modifClient_entreprise.Id_client + "';";
                    string update_courriel = "UPDATE client_entreprise SET courriel_entreprise ='" + modifClient_entreprise.Courriel + "' WHERE ID_client_entreprise ='" + modifClient_entreprise.Id_client + "';";
                    string update_telephone = "UPDATE client_entreprise SET telephone_entreprise ='" + modifClient_entreprise.Telephone + "' WHERE ID_client_entreprise ='" + modifClient_entreprise.Id_client + "';";
                    string update_programme = "UPDATE client_entreprise SET remise_client_entreprise ='" + modifClient_entreprise.Remise + "' WHERE ID_client_entreprise ='" + modifClient_entreprise.Id_client + "';";


                    Modification_client(update_nom);
                    Modification_client(update_prenom);
                    Modification_client(update_courriel);
                    Modification_client(update_telephone);
                    Modification_client(update_programme);
                    Label1.Text = "Modification du client  " + modifClient_entreprise.Id_client + " efféctuée";
                }
                else
                {
                    MessageBoxResult msg = MessageBox.Show("\n Vous n'avez pas selectionner de client à modifier", "Erreur", MessageBoxButton.YesNo);

                }
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

        private void Supprimer(object sender, RoutedEventArgs e)
        {
            if (Check_particulier.IsChecked == true)
            {
                Client modifClient = new Client();
                try
                {
                    modifClient = (Client)InfoClient.SelectedItem;
                }
                catch (InvalidCastException erreur)
                {
                    MessageBoxResult msg = MessageBox.Show(erreur + "\n Voulez ne pouvez pas modifier de client entreprise ", "Confirmation", MessageBoxButton.YesNo);

                }
                if (modifClient != null)
                {
                    string sup_client = "DELETE FROM client_particulier WHERE ID_client_particulier ='" + modifClient.Id_client + "';";
                    Modification_client(sup_client);
                    ///Rafraichir les données
                    List<Client> LClient_particulier = ListeClient_Particulier();
                    InfoClient.ItemsSource = LClient_particulier;
                    string nbParticulier = Nbclient(1);
                    AffichageNbClient.Text = "Nombre de client particulier = " + nbParticulier;
                }
                else
                {
                    MessageBoxResult msg = MessageBox.Show("\n Vous n'avez pas selectionner de Client à Supprimer", "Erreur", MessageBoxButton.YesNo);

                }
            }
            if (Check_entreprise.IsChecked == true)
            {
                Client_entreprise modifClient_entreprise = new Client_entreprise();
                try
                {
                    modifClient_entreprise = (Client_entreprise)InfoClient.SelectedItem;
                }
                catch (InvalidCastException erreur)
                {
                    MessageBoxResult msg = MessageBox.Show(erreur + "\n Voulez ne pouvez pas modifier ce client ", "Confirmation", MessageBoxButton.OK);

                }
                if (modifClient_entreprise != null)
                {
                    string sup_client = "DELETE FROM client_entreprise WHERE ID_client_entreprise ='" + modifClient_entreprise.Id_client + "';";
                    Modification_client(sup_client);
                    ///Rafraichir les données
                    List<Client_entreprise> LClient_entreprise = ListeClient_Entreprise();
                    InfoClient.ItemsSource = LClient_entreprise;
                    string nbParticulier = Nbclient(2);
                    AffichageNbClient.Text = "Nombre de client entreprise = " + nbParticulier;
                }
                else
                {
                    MessageBoxResult msg = MessageBox.Show("\n Vous n'avez pas selectionner de Client à Supprimer", "Erreur", MessageBoxButton.YesNo);

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            serialiseJson();
        }
        public static int relanceClient = 1;
        public void serialiseJson()
        {
            List<Client> liste = ListeClient_Particulier();
            List<Client> listeFiltrée = new List<Client>();
            List<Client> listeFinal = new List<Client>();
            
            
            for (int i = 0; i < liste.Count; i++)
            {
                Client ligne = liste[i];
                
                DateTime dateDebut = Convert.ToDateTime(ligne.Date_adhesion_programme);
                DateTime datefin = new DateTime();
                //Console.WriteLine("LAAAAAAAAAAAAAAAAAA"+Convert.ToDateTime(ligne.Date_adhesion_programme).ToString());
                Console.WriteLine(ligne.Numero_programme);
                if (ligne.Numero_programme == "1" || ligne.Numero_programme == "2" ||ligne.Numero_programme == "3" ||ligne.Numero_programme == "1")
                {
                    if (Convert.ToInt32(ligne.Numero_programme) == 4)
                    {
                        datefin = Convert.ToDateTime(ligne.Date_adhesion_programme).AddDays(365 * 3);
                    }
                    if (Convert.ToInt32(ligne.Numero_programme) == 3 || Convert.ToInt32(ligne.Numero_programme) == 2)
                    {
                        datefin = Convert.ToDateTime(ligne.Date_adhesion_programme).AddDays(365 * 2);
                    }
                    if (Convert.ToInt32(ligne.Numero_programme) == 1)
                    {
                        datefin = Convert.ToDateTime(ligne.Date_adhesion_programme).AddDays(365);
                    }
                }
                if (ligne.Numero_programme != null)
                {
                    int deltaJour = (datefin - dateDebut).Days;
                    if (deltaJour < 60)
                    {
                        listeFiltrée.Add( ligne);
                    }
                }
            }
            foreach (Client ligne in listeFiltrée)
            {
                

                    string ID_client = ligne.Id_client;
                string nom_client_particulier = ligne.Nom_client;
                    string prenom_client_particulier = ligne.Prenom_client;
                    string date_adhesion_programme = ligne.Date_adhesion_programme;
                    string courriel = ligne.Courriel;
                    string telephone = ligne.Telephone;
                string programme = ligne.Numero_programme ;
                    Client c = new Client(
                        ID_client,
                        nom_client_particulier,
                        prenom_client_particulier,
                        date_adhesion_programme,
                        
                        courriel,
                        telephone,
                        programme);

                    listeFinal.Add(c);
                
            }
            string fileToWrite = "relance_clients" + relanceClient.ToString() + ".json";
            relanceClient += 1;
            StreamWriter fileWriter = new StreamWriter(fileToWrite);
            JsonTextWriter jsonWriter = new JsonTextWriter(fileWriter);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, listeFinal);
            jsonWriter.Close();
            fileWriter.Close();
            MessageBox.Show("Succès de l'export de : " + fileToWrite + " !");
            //Cette fonction fais 2 choses en une, elle vérifie quels clients ont un programme de fidélité 
            //arrivant à terme d'ici 2 mois et elle les sérialise au format .json
        }
    }
}
