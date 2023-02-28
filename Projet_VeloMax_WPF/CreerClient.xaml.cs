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
    /// Logique d'interaction pour CreerClient.xaml
    /// </summary>
    public partial class CreerClient : Window
    {
        public CreerClient()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nomclient = nom.Text;
            string prenomclient = prenom.Text;
            string adresseclient = adresse.Text;
            string courrielclient = courriel.Text;
            string numeroclient = numero.Text;
            Random R = new Random();
            int id_number = R.Next(20, 100);
            string id = "cp_" + id_number;
            DateTime date = DateTime.Now ;
            string ajd = date.ToString("yyyy-MM-dd");


            ///D'abord création de l'adresse puis du client
            string requete_creaadresse;
            string requete_creaclient = "INSERT INTO client_particulier VALUES " +
                        "('" +id+"','"+nomclient + "','" + prenomclient + "','" +ajd + "','" + courrielclient + "','" + numeroclient+ "'," + " null,"+0+");" ;
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);
            connection1.Open();
            MySqlCommand command = connection1.CreateCommand();
            command.CommandText = requete_creaclient;
            try
            {
                command.ExecuteNonQuery();
            }
            catch(MySqlException erreur)
            {
                Console.WriteLine(erreur);
            }
            connection1.Close();
            this.Close();
        }

        private void courriel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
