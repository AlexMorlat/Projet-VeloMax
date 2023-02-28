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
    /// Logique d'interaction pour EspaceAdmin.xaml
    /// </summary>
    public partial class EspaceAdmin : Window
    {
        public EspaceAdmin()
        {
            InitializeComponent();
        }

        private void Menu_Fournisseur_Click(object sender, RoutedEventArgs e)
        {
            Menu_Fournisseur next = new Menu_Fournisseur();
            next.Show();
            ///this.Close();
        }
        private void Menu_Client_Click(object sender, RoutedEventArgs e)
        {
            Menu_Client next = new Menu_Client();
            next.Show();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            menu_Stock next = new menu_Stock();
            string requete = "SELECT DISTINCT numero_piece_catalogue,stock_piece FROM modele_piece WHERE stock_piece <=5 ;";
            Execute(requete);
            string[] Res = Execute(requete).Split(';');

            string Res2 = "";
            foreach (string elem in Res)
            {
                string[] Finish = elem.Split(',');
                string piece = Finish[0];
                string quantite = Finish[1];
                string chaine = "La pièce " + piece + " est en stock faible ( " + quantite + " ) \n";

                Res2 = Res2 + chaine;
            }
            string StockFaible = "";
            MessageBoxResult msg = MessageBox.Show(Res2 + "\n Voulez vous effectuer une commande aupès des fournisseurs?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.Yes)
            {

                bool rep = true;
                commande_Fournisseur next2 = new commande_Fournisseur();
                next2.Show();
                
            }
            else
            {
                bool rep = false;

                next.Show();
            }
            
            
        }
        public string Module_Statistique(int num)
        {
            string chaine = "";
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection = new MySqlConnection(connectionString);

           switch (num)
            {
                case 1:
                    #region Manip1
                    string requete = "SELECT numero_piece_catalogue, description_piece, sum(quantite_piece_commande) FROM modele_piece JOIN liste_piece_commande ON modele_piece.numero_piece_catalogue = liste_piece_commande.numero_piece_catalogue_commande GROUP BY numero_piece_catalogue;";
                    
                    string[] Res = Execute(requete).Split(';');

                    string Res2 = "";
                    foreach (string elem in Res)
                    {
                        string[] Finish = elem.Split(',');
                        string piece = Finish[0];
                        string description = Finish[1];
                        string quantite = Finish[2];
                        string chaine2 = "Nom de la pièce " + piece + " ( "+description+" ) : " + quantite + " unitées vendus. \n";

                        Res2 = Res2 + chaine2;
                    }
                    chaine = Res2;
                    #endregion
                    break;
                case 2:
                    #region Manip2
                    /// MANIPULATION 2
                    connection.Open();
                    MySqlCommand command0 = connection.CreateCommand();
                    command0.CommandText = "SELECT numero_programme,nom_client_particulier,prenom_client_particulier FROM client_particulier NATURAL JOIN programme_Fidelio;"; // exemple de requete bien-sur !
                    MySqlDataReader reader0;
                    reader0 = command0.ExecuteReader();
                    /* Création d'un dictionnaire contenant les programmes de fidelités */
                    Dictionary<int, string> ClientProg = new Dictionary<int, string>();

                   
                    ClientProg.Add(1, "");
                    ClientProg.Add(2, "");
                    ClientProg.Add(3, "");
                    ClientProg.Add(4, "");
                    while (reader0.Read())                           // parcours ligne par ligne
                    {
                        int numProg = Convert.ToInt32(reader0.GetValue(0).ToString());
                        ClientProg[numProg] = ClientProg[numProg] + reader0.GetValue(1).ToString() + " " + reader0.GetValue(2).ToString() + ",";
                    }
                    connection.Close();
                    foreach (KeyValuePair<int, string> ligne in ClientProg)
                    {
                        chaine += "Numéro Programme: " + ligne.Key + ", Liste des adhérents: " + ligne.Value.TrimEnd(',') + "\n";
                    }
                    #endregion
                    break;
                case 3:
                    #region Manip3
                    connection.Open();
                    command0 = connection.CreateCommand();
                    command0.CommandText = "SELECT numero_programme,nom_client_particulier,prenom_client_particulier,date_adhesion_programme FROM client_particulier NATURAL JOIN programme_Fidelio;";
                    reader0 = command0.ExecuteReader();
                    chaine = chaine + "Nom, Date Adhésion, Date Expiration \n";
                    while (reader0.Read())                           // parcours ligne par ligne
                    {
                        int numProg = Convert.ToInt32(reader0.GetValue(0).ToString());
                        string nom = reader0.GetValue(1).ToString();
                        string prenom = reader0.GetValue(2).ToString();
                        DateTime dateInit = DateTime.Parse(reader0.GetValue(3).ToString());
                        DateTime dateExp = new DateTime();
                        if (numProg == 1) { dateExp = dateInit.AddYears(1); }
                        if (numProg == 2) { dateExp = dateInit.AddYears(2); }
                        if (numProg == 3) { dateExp = dateInit.AddYears(2); }
                        if (numProg == 4) { dateExp = dateInit.AddYears(3); }
                        chaine += nom + " " + dateInit.ToString("dd/MM/yyyy") + " " + dateExp.ToString("dd/MM/yyyy") + "\n";
                    }
                    connection.Close();
                    #endregion
                    break;
                case 4:
                    #region Manip4
                    string cp_maxpiece = "SELECT nom_client_particulier,prenom_client_particulier,sum(quantite_piece_commande) FROM commande JOIN client_particulier USING(ID_client_particulier)" +
                        "JOIN liste_piece_commande WHERE commande.numero_commande = liste_piece_commande.numero_commande_piece " +
                        "GROUP BY ID_client_entreprise ORDER BY sum(quantite_piece_commande) DESC LIMIT 1; ";
                    string ce_maxpiece = "SELECT nom_client_entreprise,sum(quantite_piece_commande) FROM commande JOIN client_entreprise USING(ID_client_entreprise) " +
                        "JOIN liste_piece_commande WHERE commande.numero_commande = liste_piece_commande.numero_commande_piece " +
                        "GROUP BY ID_client_entreprise ORDER BY sum(quantite_piece_commande) DESC LIMIT 1; ";
                    string cp_maxprix = "SELECT nom_client_particulier,prenom_client_particulier,sum((quantite_piece_commande*prix_piece*(1-programme_fidelio.rabais_programme)))as prixtotavecreduc " +
                        "FROM liste_piece_commande JOIN modele_piece " +
                        "ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue " +
                        "JOIN commande ON liste_piece_commande.numero_commande_piece = commande.numero_commande " +
                        "JOIN client_particulier ON commande.ID_client_particulier = client_particulier.ID_client_particulier " +
                        "JOIN programme_fidelio ON client_particulier.numero_programme = programme_fidelio.numero_programme " +
                        "GROUP BY nom_client_particulier ORDER BY prixtotavecreduc DESC LIMIT 1 ; ";
                    string ce_maxprix = "SELECT nom_client_entreprise, sum((quantite_piece_commande * prix_piece * (1 - remise_client_entreprise))) as prixavecremise" +
                        " FROM liste_piece_commande JOIN modele_piece ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue " +
                        "JOIN commande ON liste_piece_commande.numero_commande_piece = commande.numero_commande " +
                        "JOIN client_entreprise ON client_entreprise.ID_client_entreprise = commande.ID_client_entreprise " +
                        "GROUP BY nom_client_entreprise ORDER BY prixavecremise DESC LIMIT 1 ; ";


                    string[] Res4 = Execute(cp_maxpiece).Split(';');
                    chaine = chaine + "Client particulier ayant commander le plus de pièces : \n";
                    string Res42 = "";
                    foreach (string elem in Res4)
                    {
                        string[] Finish = elem.Split(',');
                        string Nom = Finish[0];
                        string Prenom = Finish[1];
                        string quantite = Finish[2];
                        string chaine2 = "Nom du client ; " + Nom + "  " + Prenom + "  : " + quantite + " pieces commandées. \n";

                        Res42 = Res42 + chaine2;
                    }
                    chaine = chaine + Res42;

                    string[] Res5 = Execute(ce_maxpiece).Split(';');
                    chaine = chaine + "Client entreprise ayant commander le plus de pièces : \n";
                    string Res52 = "";
                    foreach (string elem in Res5)
                    {
                        string[] Finish = elem.Split(',');
                        string Nom = Finish[0];
                        
                        string quantite = Finish[1];
                        string chaine2 = "Nom du client ; " + Nom + "   : " + quantite + " pieces commandées. \n";

                        Res52 = Res52 + chaine2;
                    }
                    chaine = chaine + Res52;

                    string[] Res6 = Execute(cp_maxprix).Split(';');
                    chaine = chaine + "Client particulier ayant commandé le plus gros montant : \n";
                    string Res62 = "";
                    foreach (string elem in Res6)
                    {
                        string[] Finish = elem.Split(',');
                        string Nom = Finish[0];
                        string Prenom = Finish[1];
                        string quantite = Finish[2];
                        string chaine2 = "Nom du client ; " + Nom + "  " + Prenom + "  : " + quantite + " € de commandes. \n";

                        Res62 = Res62 + chaine2;
                    }
                    chaine = chaine + Res62;

                    string[] Res7 = Execute(ce_maxprix).Split(';');
                    chaine = chaine + "Client entreprise ayant commander le plus gros montant : \n";
                    string Res72 = "";
                    foreach (string elem in Res7)
                    {
                        string[] Finish = elem.Split(',');
                        string Nom = Finish[0];

                        string quantite = Finish[1];
                        string chaine2 = "Nom du client ; " + Nom + "   : " + quantite + " € de commandes. \n";

                        Res72 = Res72 + chaine2;
                    }
                    chaine = chaine + Res72;
                    #endregion
                    break;
                case 5:
                    #region Manip5
                    string mean_velo = "SELECT avg(nombre_velo) FROM( SELECT sum(quantite_velo_commande) as nombre_velo FROM liste_velo_commande JOIN modele_velo " +
                        "ON liste_velo_commande.numero_velo_commande = modele_velo.numero_velo GROUP BY numero_commande_velo) nb; ";
                    string mean_piece = "SELECT avg(nombre_piece) FROM( SELECT sum(quantite_piece_commande) as nombre_piece" +
                        " FROM liste_piece_commande JOIN modele_piece ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue " +
                        "GROUP BY numero_commande_piece) nb; ";
                    string mean_prix = "SELECT avg(prix_commande) as prix_moyen FROM (SELECT numero_commande,sum(prix_commande_piece) as prix_commande FROM " +
                        "((SELECT numero_commande, prix_commande_piece FROM commande JOIN(SELECT numero_commande_piece, SUM((quantite_piece_commande * prix_piece)) as prix_commande_piece FROM liste_piece_commande JOIN modele_piece " +
                        "ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue GROUP BY numero_commande_piece) my_req " +
                        "ON commande.numero_commande = my_req.numero_commande_piece) UNION ALL (SELECT numero_commande, prix_commande_velo FROM commande JOIN(SELECT numero_commande_velo, sum(quantite_velo_commande* prix_velo) as prix_commande_velo FROM liste_velo_commande JOIN modele_velo  ON liste_velo_commande.numero_velo_commande = modele_velo.numero_velo GROUP BY numero_commande_velo) my_req ON commande.numero_commande = my_req.numero_commande_velo)) req2 " +
                        "" +
                        "GROUP BY numero_commande)alias; ";

                    chaine = "";
                    string[] Res9 = Execute(mean_velo).Split(';');

                    string Res92 = "";
                    foreach (string elem in Res9)
                    {
                        string[] Finish = elem.Split(',');
                        
                        string quantite = Finish[0];
                        string chaine2 = "Moyenne de velo commandé par commande : "+quantite+" \n";

                        Res92 = Res92 + chaine2;
                    }
                    chaine = chaine + Res92;

                    string[] Res10 = Execute(mean_piece).Split(';');

                    string Res102 = "";
                    foreach (string elem in Res10)
                    {
                        string[] Finish = elem.Split(',');

                        string quantite = Finish[0];
                        string chaine2 = "Moyenne de piece commandé par commande : " + quantite + " \n";

                        Res102 = Res102 + chaine2;
                    }
                    chaine = chaine + Res102;

                    string[] Res14 = Execute(mean_prix).Split(';');
                    string Res104 = "";
                    foreach (string elem in Res14)
                    {
                        string[] Finish = elem.Split(',');

                        string quantite = Finish[0];
                        string chaine2 = "Moyenne d'euro dépensés par commande : " + quantite + " € \n";

                        Res104 = Res104 + chaine2;
                    }
                    chaine = chaine + Res104;


                    #endregion 
                    break;
            }


            return chaine;
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
                chaine = chaine + currentRowAsString + ";";
            }
            connection.Close();
            return chaine.Trim(';');
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ///Bouton Statistique : Active les Radio Butons
            TextStatistiques.Visibility = Visibility.Visible;
            Rapport_quantite.Visibility = Visibility.Visible;
            Liste_membre_prog.Visibility = Visibility.Visible;
            Expiration.Visibility = Visibility.Visible;
            Meilleur_client.Visibility = Visibility.Visible;
            Analyse_commande.Visibility = Visibility.Visible;


            Adhésion.Visibility = Visibility.Collapsed;
            Stat.Visibility = Visibility.Visible;
            ///Stat.Text = Module_Statistique();
        }
        private void Rapport_quantite_Checked(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(Rapport_quantite.IsChecked))
            {
                Adhésion.Visibility = Visibility.Collapsed;
                Stat.Visibility = Visibility.Visible;
                string text = Module_Statistique(1);
                Stat.Text = text;
            }
        }

        private void Liste_membre_prog_Checked(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(Liste_membre_prog.IsChecked))
            {
                Adhésion.Visibility = Visibility.Collapsed;
                Stat.Visibility = Visibility.Visible;
                string text = Module_Statistique(2);

                Stat.Text = text;
            }
        }

        private void Expiration_Checked(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(Expiration.IsChecked))
            {
                Adhésion.Visibility = Visibility.Collapsed;
                Stat.Visibility = Visibility.Visible;
                string text = Module_Statistique(3);
                Stat.Text = text;
            }
        }

        private void Meilleur_client_Checked(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(Meilleur_client.IsChecked))
            {
                Adhésion.Visibility = Visibility.Collapsed;
                Stat.Visibility = Visibility.Visible;
                string text = Module_Statistique(4);
                Stat.Text = text;
            }
        }

        private void Analyse_commande_Checked(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(Analyse_commande.IsChecked))
            {
                Stat.Visibility = Visibility.Visible;
                string text = Module_Statistique(5);
                Stat.Text = text;
            }
        }

        private void Menu_commande_Click(object sender, RoutedEventArgs e)
        {
            menu_commande2 next = new menu_commande2();
            next.Show();
        }
       
    }
}
