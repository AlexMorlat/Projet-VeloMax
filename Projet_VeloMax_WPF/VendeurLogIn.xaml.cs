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
    /// Logique d'interaction pour VendeurLogIn.xaml
    /// </summary>
    public partial class VendeurLogIn : Window
    {
        public VendeurLogIn()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (id.Text == "bozo" & mdp.Password == "bozo")
            {
                Connexion connexion = new Connexion();
                connexion.stringconnexionGenerale = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo2;PASSWORD=bozo2;";
                /// Gestion des privilège qui ne marche pas
                /*
                Connexion stringconnexion = new Connexion();
                string stringconnection2 = stringconnexion.stringConnexion;
                MySqlConnection connection = new MySqlConnection(stringconnection2);

                string delete = "DROP USER IF EXISTS 'bozo1';";
                MySqlCommand userFirstDelete = new MySqlCommand(delete, connection);
                connection.Open();
                MySqlDataReader read1 = userFirstDelete.ExecuteReader();
                read1.Close();
                connection.Close();

                string user1 = "CREATE USER 'bozo1' IDENTIFIED BY 'password'; ";
                MySqlCommand usercmd = new MySqlCommand(user1, connection);

                connection.Open();
                MySqlDataReader read3 = usercmd.ExecuteReader();
                read3.Close();
                string userprivi = "GRANT All privileges  ON *.* TO 'bozo1'  With grant option;  ";
                MySqlCommand userprivicmd = new MySqlCommand(userprivi, connection);

                MySqlDataReader read4 = userprivicmd.ExecuteReader();
                read4.Close();
                MySqlConnection connection1 = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=veloMax;UID=bozo1;PASSWORD=password;");
                connection1.Open();
                string q = "REVOKE ALL PRIVILEGES, GRANT OPTION FROM 'bozo' ;  ";
                MySqlCommand s = new MySqlCommand(q, connection1);

                MySqlDataReader read = s.ExecuteReader();
                read.Close();
                string q1 = "GRANT Select, grant option ON `VeloMax`.* TO 'bozo' WITH GRANT OPTION;  ";
                MySqlCommand s1 = new MySqlCommand(q1, connection1);
                connection.Close();
                */



                EspaceVendeur next = new EspaceVendeur();
                

                next.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Problème d'identification veuillez réassayer");
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
