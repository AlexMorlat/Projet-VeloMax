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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Xml;
using Newtonsoft.Json;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TimeNow.Text = $"Date : {DateTime.UtcNow}";
        }
        private void Profil_Administrateur(object sender, RoutedEventArgs e)
        {
            AdminLogIn next = new AdminLogIn();
            next.Show();
            this.Close();
        }


        
        private void Profil_Vendeur(object sender, RoutedEventArgs e)
        {
            VendeurLogIn next = new VendeurLogIn();
            next.Show();
            this.Close();
        }

        
        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            
            string requete_nbclientparticulier = "SELECT count(*) from client_particulier;" ;
            string requete_nbcliententreprise = "SELECT count(*) from client_particulier;";
            string requete_prix_commande_client_particulier = "SELECT nom_client_particulier,sum(prix_commande_piece) FROM client_particulier NATURAL JOIN Commande NATURAL JOIN (SELECT numero_commande,prix_commande_piece FROM " +
                "((SELECT numero_commande, prix_commande_piece FROM commande " +
                "JOIN(SELECT numero_commande_piece, SUM((quantite_piece_commande * prix_piece)) as prix_commande_piece " +
                "FROM liste_piece_commande JOIN modele_piece " +
                "ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue " +
                "GROUP BY numero_commande_piece) my_req " +
                "ON commande.numero_commande = my_req.numero_commande_piece) UNION ALL (SELECT numero_commande, prix_commande_velo FROM commande " +
                "JOIN(SELECT numero_commande_velo, sum(quantite_velo_commande* prix_velo) as prix_commande_velo " +
                "FROM liste_velo_commande JOIN modele_velo " +
                "ON liste_velo_commande.numero_velo_commande = modele_velo.numero_velo " +
                "GROUP BY numero_commande_velo) my_req ON commande.numero_commande = my_req.numero_commande_velo)) req2)req3 group by nom_client_particulier; ";
            string requete_prix_commande_client_entreprise = "SELECT nom_client_entreprise,sum(prix_commande_piece) FROM client_entreprise NATURAL JOIN Commande NATURAL JOIN (SELECT numero_commande,prix_commande_piece FROM " +
                "((SELECT numero_commande, prix_commande_piece FROM commande " +
                "JOIN(SELECT numero_commande_piece, SUM((quantite_piece_commande * prix_piece)) as prix_commande_piece " +
                "FROM liste_piece_commande JOIN modele_piece " +
                "ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue " +
                "GROUP BY numero_commande_piece) my_req " +
                "ON commande.numero_commande = my_req.numero_commande_piece) UNION ALL (SELECT numero_commande, prix_commande_velo FROM commande " +
                "JOIN(SELECT numero_commande_velo, sum(quantite_velo_commande* prix_velo) as prix_commande_velo " +
                "FROM liste_velo_commande JOIN modele_velo " +
                "ON liste_velo_commande.numero_velo_commande = modele_velo.numero_velo " +
                "GROUP BY numero_commande_velo) my_req ON commande.numero_commande = my_req.numero_commande_velo)) req2)req3 group by nom_client_entreprise; ";
            string requete_stock_faible = "SELECT DISTINCT numero_piece_catalogue,stock_piece FROM modele_piece WHERE stock_piece <=2 ;";
            string requete_piece_fournisseur = "SELECT nom_fournisseur,sum(stock_piece) FROM Fournisseur NATURAL JOIN Catalogue NATURAL JOIN modele_piece GROUP BY siret_fournisseur; ";

            string chaine = "";
            for (int i = 0; i < 11; i++)
            {
                chaine = chaine + Demo(i);
            }
            Affichage.Text = chaine; 



            /*
            string[] Res = Execute(requete_piece_fournisseur).Split(';');

            string Res2 = "";
            foreach (string elem in Res)
            {
                string[] Finish = elem.Split(',');
                string piece = Finish[0];
                string quantite = Finish[1];
                string chaine = "Nom Fournisseur " + piece + " : " + quantite + " en stock. \n";

                Res2 = Res2 + chaine;
            }
            string chaine = "";
            #region Stock Faible
            string[] Res = Execute(requete_stock_faible).Split(';');

            string Res2 = "";
            foreach (string elem in Res)
            {
                string[] Finish = elem.Split(',');
                string piece = Finish[0];
                string quantite = Finish[1];
                string chainex = "La pièce " + piece + " est en stock faible ( " + quantite + " ) \n";

                Res2 = Res2 + chainex;
            }
            */

            
            //string[] Res9 = Execute(mean_velo).Split(';');
            /*
            string Res92 = "";
            foreach (string elem in Res9)
            {
                string[] Finish = elem.Split(',');

                string quantite = Finish[0];
                string chaine2 = "Moyenne de velo commandé par commande " + quantite + " \n";

                Res92 = Res92 + chaine2;
            }
            chaine = chaine + Res92;
            */
        }
        public string Demo(int i )
        {
            TheScroll.Visibility = Visibility.Visible;
            #region Requetes
            string requete_nbclientparticulier = "SELECT count(*) from client_particulier;";
            string requete_nbcliententreprise = "SELECT count(*) from client_entreprise;";
            string requete_prix_commande_client_particulier = "SELECT nom_client_particulier,sum(prix_commande_piece) FROM client_particulier NATURAL JOIN Commande NATURAL JOIN (SELECT numero_commande,prix_commande_piece FROM " +
                "((SELECT numero_commande, prix_commande_piece FROM commande " +
                "JOIN(SELECT numero_commande_piece, SUM((quantite_piece_commande * prix_piece)) as prix_commande_piece " +
                "FROM liste_piece_commande JOIN modele_piece " +
                "ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue " +
                "GROUP BY numero_commande_piece) my_req " +
                "ON commande.numero_commande = my_req.numero_commande_piece) UNION ALL (SELECT numero_commande, prix_commande_velo FROM commande " +
                "JOIN(SELECT numero_commande_velo, sum(quantite_velo_commande* prix_velo) as prix_commande_velo " +
                "FROM liste_velo_commande JOIN modele_velo " +
                "ON liste_velo_commande.numero_velo_commande = modele_velo.numero_velo " +
                "GROUP BY numero_commande_velo) my_req ON commande.numero_commande = my_req.numero_commande_velo)) req2)req3 group by nom_client_particulier; ";
            string requete_prix_commande_client_entreprise = "SELECT nom_client_entreprise,sum(prix_commande_piece) FROM client_entreprise NATURAL JOIN Commande NATURAL JOIN (SELECT numero_commande,prix_commande_piece FROM " +
                "((SELECT numero_commande, prix_commande_piece FROM commande " +
                "JOIN(SELECT numero_commande_piece, SUM((quantite_piece_commande * prix_piece)) as prix_commande_piece " +
                "FROM liste_piece_commande JOIN modele_piece " +
                "ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue " +
                "GROUP BY numero_commande_piece) my_req " +
                "ON commande.numero_commande = my_req.numero_commande_piece) UNION ALL (SELECT numero_commande, prix_commande_velo FROM commande " +
                "JOIN(SELECT numero_commande_velo, sum(quantite_velo_commande* prix_velo) as prix_commande_velo " +
                "FROM liste_velo_commande JOIN modele_velo " +
                "ON liste_velo_commande.numero_velo_commande = modele_velo.numero_velo " +
                "GROUP BY numero_commande_velo) my_req ON commande.numero_commande = my_req.numero_commande_velo)) req2)req3 group by nom_client_entreprise; ";
            string requete_stock_faible = "SELECT DISTINCT numero_piece_catalogue,stock_piece FROM modele_piece WHERE stock_piece <=2 ;";
            string requete_piece_fournisseur = "SELECT nom_fournisseur,sum(stock_piece) FROM Fournisseur NATURAL JOIN Catalogue NATURAL JOIN modele_piece GROUP BY siret_fournisseur; ";
            string requete_jointure = "SELECT c1.nom_client_particulier,c1.prenom_client_particulier FROM client_particulier c1 , client_particulier c2 WHERE c1.nom_client_particulier = c2.nom_client_particulier AND (c1.prenom_client_particulier>c2.prenom_client_particulier OR c1.prenom_client_particulier<c2.prenom_client_particulier );";
            string requete_synchro = "SELECT v.nom_velo FROM modele_velo v WHERE v.prix_velo < (SELECT avg(v1.prix_velo) FROM modele_velo v1 WHERE v1.ligne_produit_velo = v.ligne_produit_velo)  ORDER BY v.prix_velo; ";
            string requete_union = "SELECT nom_client_particulier FROM client_particulier union SELECT nom_client_entreprise FROM client_entreprise; ";
            #endregion
            string chaine = "";
            switch (i)
            {
                case (0):
                    ///Nombre de clients particuliers
                    ///chaine = chaine + "Nombre de clients particuliers : \n";
                    string Res = Execute2(requete_nbcliententreprise);

                    
                        string chainey = "Nombre de clients entreprise : "+Res+". \n";

                    
                    chaine = chaine + chainey;
                    break;
                case (1):
                    ///Nombre de clients entreprise
                    ///string[] Res = Execute(requete_nbclientparticulier).Split(';');
                    string Res2 = Execute2(requete_nbclientparticulier);


                    string chainey2 = "Nombre de clients particuliers : " + Res2 + ". \n";


                    chaine = chaine + chainey2;
                    break;
                case (2):
                    /// Nom des clients particuliers avec montant des commandes en euro
                    chaine = chaine + "\n Nom des clients particuliers avec montant des commandes en euro \n";
                    string[] Res3 = Execute(requete_prix_commande_client_particulier).Split(';');

                    string Res33 = "";
                    foreach (string elem in Res3)
                    {
                        string[] Finish = elem.Split(',');
                        string nom = Finish[0];
                        string quantite = Finish[1];
                        string chainex = "Le client " + nom + " à commandé pour un total de  ( " + quantite + " ) € \n";

                        Res33 = Res33 + chainex;
                    }
                    chaine = chaine + Res33;
                    break;
                case (3):
                    /// Nom des entreprises avec montant des commandes en euro
                    chaine = chaine + "\n Nom des entreprises avec montant des commandes en euro \n";
                    string[] Res3bis = Execute(requete_prix_commande_client_entreprise).Split(';');

                    string Res33bis = "";
                    foreach (string elem in Res3bis)
                    {
                        string[] Finish = elem.Split(',');
                        string nom = Finish[0];
                        string quantite = Finish[1];
                        string chainex = "Le client " + nom + " à commandé pour un total de  ( " + quantite + " ) € \n";

                        Res33bis = Res33bis + chainex;
                    }
                    chaine = chaine + Res33bis;
                    break;
                case (4):
                    /// Quantite en en stock avec quantite inferieure à 2 
                    chaine = chaine + "\n Piece en stock Faible \n";
                    string[] Res4 = Execute(requete_stock_faible).Split(';');

                    string Res44 = "";
                    foreach (string elem in Res4)
                    {
                        string[] Finish = elem.Split(',');
                        string piece = Finish[0];
                        string quantite = Finish[1];
                        string chainex = "La pièce " + piece + " est en stock faible ( " + quantite + " ) \n";

                        Res44 = Res44 + chainex;
                    }
                    chaine = chaine + Res44;
                    break;
                case (5):
                    /// Nombre de pièce fourni par fournisseur
                    chaine = chaine + "\n Nombre de pièce fourni par fournisseur \n";
                    string[] Res5 = Execute(requete_piece_fournisseur).Split(';');

                    string Res55 = "";
                    foreach (string elem in Res5)
                    {
                        string[] Finish = elem.Split(',');
                        string Fournisseur = Finish[0];
                        string Piece = Finish[1];
                        string chainex = "Le fournisseur " + Fournisseur + " fournit à VeloMax  " + Piece + " pièces \n";

                        Res55 = Res55 + chainex;
                    }
                    chaine = chaine + Res55;
                    break;
                case (6):
                    /// Export en XML et JSON de la Table Programme Fidelio
                    ExportJsonFidelio();
                    chaine = chaine + " \n Fichier Fidelio.json dans bin->Debug créé";
                    ExportXML();
                    chaine = chaine + " \n Fichier Fidelio.xml dans bin->Debug créé ";
                    break;
                case (7):
                    chaine = chaine + "\n \n Requêtes Supplémentaires \n Requête Synchronisé : Liste des vélos ayant un prix unitaire \n inférieure à la moyenne des prix des vélos de même catégorie ";
                    string[] Res7 = Execute(requete_synchro).Split(';');
                    string Res77 = "\n Liste des Velos : \n";
                    foreach (string elem in Res7)
                    {
                        string[] Finish = elem.Split(',');
                        string Client = Finish[0];

                        string chainex = Client + ",";

                        Res77 = Res77 + chainex;
                    }
                    chaine = chaine + Res77.Trim(',');
                    break;
                case (8):
                    chaine = chaine + "\n \n Requête en Auto-Jointure : Liste des Clients qui ont le même nom : ";
                    string[] Res8 = Execute(requete_jointure).Split(';');
                    string Res88 = "";
                    foreach (string elem in Res8)
                    {
                        string[] Finish = elem.Split(',');
                        string Client = Finish[0];
                        string Clientprenom = Finish[1];

                        string chainex = Clientprenom +" " +Client + ",";

                        Res88 = Res88 + chainex;
                    }
                    chaine = chaine + Res88.Trim(',');
                    break;
                case (9):
                    chaine = chaine + " \n \n Requête avec Union : Liste de tous les clients (particuliers puis entreprises) ";
                    string[] Res9 = Execute(requete_union).Split(';');
                    string Res99 = "\n Liste des Clients : \n";
                    foreach (string elem in Res9)
                    {
                        string[] Finish = elem.Split(',');
                        string Client = Finish[0];
                        
                        string chainex = Client + ",";

                        Res99 = Res99 + chainex;
                    }
                    chaine = chaine + Res99.Trim(',');
                    break;


            }
            
            return chaine;
        }
        private string Execute(string requete)
        {
            string chaine = "";

            string stringconnexion = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection = new MySqlConnection(stringconnexion);
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
        private string Execute2(string requete)
        {
            string chaine = "";

            string stringconnexion = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection = new MySqlConnection(stringconnexion);
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
                    currentRowAsString += valueAsString  ;
                }
                chaine = chaine + currentRowAsString ;
            }
            connection.Close();
            return chaine;
        }
        public static void ExportJsonFidelio()
        {
            Console.WriteLine("Ecriture de Fidelio.json");
            string monFichier = "Fidelio.json";

            //informations sur les programmes
            string[] id = { "1", "2", "3", "4" };
            string[] desc = { "Fidélio", "Fidélio Or", "Fidélio Platine", "Fidélio Max" };
            string[] prix = { "15", "25", "60", "100" };
            string[] duree = { "1", "2", "2", "3" };
            string[] rabais = { "5", "8", "10", "12" };

            //instanciation des "writer"
            StreamWriter writer = new StreamWriter(monFichier);
            JsonTextWriter jwriter = new JsonTextWriter(writer);

            //debut du fichier Json
            jwriter.WriteStartObject();

            //debut du tableau Json
            jwriter.WritePropertyName("Fidelio");
            jwriter.WriteStartArray();
            for (int i = 0; i < id.Length; i++)
            {
                jwriter.WriteStartObject();
                jwriter.WritePropertyName("idFidelio");
                jwriter.WriteValue(id[i]);
                jwriter.WritePropertyName("description");
                jwriter.WriteValue(desc[i]);
                jwriter.WritePropertyName("prix");
                jwriter.WriteValue(prix[i]);
                jwriter.WritePropertyName("duree");
                jwriter.WriteValue(duree[i]);
                jwriter.WritePropertyName("rabais");
                jwriter.WriteValue(rabais[i]);
                jwriter.WriteEndObject();
            }
            jwriter.WriteEndArray();
            jwriter.WriteEndObject();

            //fermeture des "writer"
            jwriter.Close();
            writer.Close();

           
        }
        public static void ExportXML()
        {
            XmlDocument docXml = new XmlDocument();

            // Création de l'élément racine
            XmlElement racine = docXml.CreateElement("Fidelio");
            docXml.AppendChild(racine);

            // création de l'en-tête XML
            XmlDeclaration decla = docXml.CreateXmlDeclaration("1.0", "UTF-8", "no");
            docXml.InsertBefore(decla, racine);


            // Création d'un élément... qu'on ajoute à un autre élément (en tant que sous-élément)
            XmlElement idFidelio = docXml.CreateElement("idFidelio");
            idFidelio.InnerText = "1";
            racine.AppendChild(idFidelio);
            // Création des sous-éléments du dernier élément créé
            XmlElement desc = docXml.CreateElement("description");
            desc.InnerText = "Fidélio";
            idFidelio.AppendChild(desc);

            XmlElement prix = docXml.CreateElement("prix");
            prix.InnerText = "15";
            idFidelio.AppendChild(prix);

            XmlElement duree = docXml.CreateElement("duree");
            duree.InnerText = "1";
            idFidelio.AppendChild(duree);

            XmlElement rabais = docXml.CreateElement("rabais");
            rabais.InnerText = "5";
            idFidelio.AppendChild(rabais);


            // Nouvel élément
            idFidelio = docXml.CreateElement("idFidelio");
            idFidelio.InnerText = "2";
            racine.AppendChild(idFidelio);
            // Avec ses sous-éléments
            desc = docXml.CreateElement("description");
            desc.InnerText = "Fidélio Or";
            idFidelio.AppendChild(desc);

            prix = docXml.CreateElement("prix");
            prix.InnerText = "25";
            idFidelio.AppendChild(prix);

            duree = docXml.CreateElement("duree");
            duree.InnerText = "2";
            idFidelio.AppendChild(duree);

            rabais = docXml.CreateElement("rabais");
            rabais.InnerText = "8";
            idFidelio.AppendChild(rabais);


            // Nouvel élément
            idFidelio = docXml.CreateElement("idFidelio");
            idFidelio.InnerText = "3";
            racine.AppendChild(idFidelio);
            // Avec ses sous-éléments
            desc = docXml.CreateElement("description");
            desc.InnerText = "Fidélio Platine";
            idFidelio.AppendChild(desc);

            prix = docXml.CreateElement("prix");
            prix.InnerText = "60";
            idFidelio.AppendChild(prix);

            duree = docXml.CreateElement("duree");
            duree.InnerText = "2";
            idFidelio.AppendChild(duree);

            rabais = docXml.CreateElement("rabais");
            rabais.InnerText = "10";
            idFidelio.AppendChild(rabais);

            // Nouvel élément
            idFidelio = docXml.CreateElement("idFidelio");
            idFidelio.InnerText = "4";
            racine.AppendChild(idFidelio);
            // Avec ses sous-éléments
            desc = docXml.CreateElement("description");
            desc.InnerText = "Fidélio Max";
            idFidelio.AppendChild(desc);

            prix = docXml.CreateElement("prix");
            prix.InnerText = "100";
            idFidelio.AppendChild(prix);

            duree = docXml.CreateElement("duree");
            duree.InnerText = "3";
            idFidelio.AppendChild(duree);

            rabais = docXml.CreateElement("rabais");
            rabais.InnerText = "12";
            idFidelio.AppendChild(rabais);

            // enregistrement du document XML dans bin\Debug
            docXml.Save("FidelioXML.xml");
            Console.WriteLine("Fichier Fidelio.xml dans bin->Debug créé");
        }
    }
}
