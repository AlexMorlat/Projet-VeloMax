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
    /// Logique d'interaction pour CreerFournisseur.xaml
    /// </summary>
    public partial class CreerFournisseur : Window
    {
        public CreerFournisseur()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string siretFournisseur = Siret.Text;
            string NomF = Nom.Text;
            string AdresseF = Adresse.Text;
            string Nom_contactF = Nom_contact.Text;
            string QualiteF = Qualite.Text;
            


            ///D'abord création de l'adresse puis du client
            string requete_creaadresse;
            string requete_creaclient = "INSERT INTO Fournisseur VALUES " +
                        "('" + siretFournisseur + "','" + NomF + "','" + QualiteF + "','" + Nom_contactF + "'," + 0 + ") ;";
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
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
                Console.WriteLine(erreur);
            }
            connection1.Close();
            this.Close();
        }
    }
}
