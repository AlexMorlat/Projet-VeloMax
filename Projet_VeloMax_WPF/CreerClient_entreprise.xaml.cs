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
    /// Logique d'interaction pour CreerClient_entreprise.xaml
    /// </summary>
    public partial class CreerClient_entreprise : Window
    {
        public CreerClient_entreprise()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nomclient = nom.Text;
            string contactclient = contact.Text;
            string adresseclient = adresse.Text;
            string courrielclient = courriel.Text;
            string telephoneclient = telephone.Text;
            string remiseclient = remise.Text;
            /*
            try
            {
                remiseclient = float.Parse(remise.Text);
            }
            catch(System.FormatException erreur)
            {
                MessageBoxResult msg = MessageBox.Show(erreur.ToString());
            }
            */
            Random R = new Random();
            int id_number = R.Next(20, 100);
            string id = "ce_" + id_number;
            DateTime date = DateTime.Now;
            string ajd = date.ToString("yyyy-MM-dd");


            ///D'abord création de l'adresse puis du client
            string requete_creaadresse;
            string requete_creaclient = "INSERT INTO client_entreprise VALUES " +
                        "('" + id + "','" + nomclient + "'," + remiseclient + ",'" + courrielclient + "','" + telephoneclient + "','" + contactclient + "'," + 0 + ");";
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MessageBoxResult msg2 = MessageBox.Show(requete_creaclient.ToString());
            MySqlConnection connection1 = new MySqlConnection(connectionString1);
            connection1.Open();
            MySqlCommand command = connection1.CreateCommand();
            command.CommandText = requete_creaclient;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException erreur)
            {
                MessageBoxResult msg3 = MessageBox.Show(erreur.ToString());
            }
            connection1.Close();
            this.Close();
        }

        
    }
}
