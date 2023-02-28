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
    /// Logique d'interaction pour menu_commande2.xaml
    /// </summary>
    public partial class menu_commande2 : Window
    {
        public menu_commande2()
        {
            InitializeComponent();
            List<Commande> Lfournisseurs = ListeFournisseur();
            DataGridCommande.ItemsSource = Lfournisseurs;
        }
        private List<Commande> ListeFournisseur()
        {
            List<Commande> Lfournisseurs = new List<Commande>();
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = "SELECT * FROM commande;";
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
                Commande F = new Commande(Convert.ToInt32(tableauinfo[0]), tableauinfo[1], tableauinfo[2], Convert.ToInt32(tableauinfo[3]),tableauinfo[6], tableauinfo[4],tableauinfo[5]);
                Lfournisseurs.Add(F);

            }
            connection1.Close();
            return Lfournisseurs;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            menu_commande next = new menu_commande();
            next.Show();
            this.Close();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            commande_Fournisseur next = new commande_Fournisseur();
            next.Show();
            this.Close();
        }
    }
}
