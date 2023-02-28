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
    /// Logique d'interaction pour EspaceVendeur.xaml
    /// </summary>
    public partial class EspaceVendeur : Window
    {
        public EspaceVendeur()
        {
            InitializeComponent();
        }
        private void CheckBox_Particulier(object sender, RoutedEventArgs e)
        {
            Check_entreprise.IsChecked = false;
            List<Client> LClient_particulier = ListeClient_Particulier();
            InfoClient.ItemsSource = LClient_particulier;
            string nbParticulier = Nbclient(1);
            AffichageNbClient.Text = "Nombre de client particulier = " + nbParticulier;


        }

        private void Client_Entreprise(object sender, RoutedEventArgs e)
        {
            Check_particulier.IsChecked = false;
            List<Client_entreprise> LClient_Entreprise = ListeClient_Entreprise();
            InfoClient.ItemsSource = LClient_Entreprise;
            string nbEntreprise = Nbclient(2);
            AffichageNbClient.Text = "Nombre de client entreprise = " + nbEntreprise;
        }
        private List<Client> ListeClient_Particulier()
        {
            List<Client> LClient_particulier = new List<Client>();
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo2;PASSWORD=bozo2;";
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
                Client F = new Client(tableauinfo[0], tableauinfo[1], tableauinfo[2], tableauinfo[3], tableauinfo[4], tableauinfo[5], tableauinfo[6]);
                LClient_particulier.Add(F);

            }
            connection1.Close();
            return LClient_particulier;
        }
        private List<Client_entreprise> ListeClient_Entreprise()
        {
            List<Client_entreprise> LClient_entreprise = new List<Client_entreprise>();
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo2;PASSWORD=bozo2;";
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
                Client_entreprise F = new Client_entreprise(tableauinfo[0], tableauinfo[1], tableauinfo[5], tableauinfo[2], tableauinfo[3], tableauinfo[4]);
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
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo2;PASSWORD=bozo2;";
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
    }
}
