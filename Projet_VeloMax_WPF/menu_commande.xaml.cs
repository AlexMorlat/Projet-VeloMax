using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour menu_commande.xaml
    /// </summary>
    public partial class menu_commande : Window
    {

        public menu_commande()
        {
            InitializeComponent();
            List<string> Client = new List<string>();
            Client = ListeClient();
            ClientBox.ItemsSource = Client;
            ///ClientBox.SelectedIndex = 0;
            List<string> Velo = new List<string>();
            Velo = ListeVelo();
            VeloBox.ItemsSource = Velo;
            List<string> Piece = new List<string>();
            Piece = ListePiece();
            PieceBox.ItemsSource = Piece;


        }
        
        List<string> panierPiece = new List<string>();
        List<string> panierVelo = new List<string>();

        
        List<int> panierPieceQuantite = new List<int>();
        List<int> panierVeloQuantite = new List<int>();

        private static DataTable ConvertToDatatable(List<string> list, List<int> listQt)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Panier");
            dt.Columns.Add("Quantite");
            int x = 0;
            foreach (var item in list)
            {
                var row = dt.NewRow();

                row["Panier"] = item;
                row["Quantite"] = listQt[x];

                dt.Rows.Add(row);
                x++;
            }
            
            

            return dt;
        }
        public List<string> ListeClient()
        {
            List<string> Client = new List<string>();

            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = "SELECT nom_client_particulier,prenom_client_particulier FROM client_particulier;";
            MySqlDataReader reader0;
            reader0 = command1.ExecuteReader();

            while (reader0.Read())
            {
                string dataline = "";
                for (int i = 0; i < reader0.FieldCount; i++)
                {
                    string valueAsString = reader0.GetValue(i).ToString();
                    dataline += valueAsString + " ";
                }
                  Client.Add(dataline);

            }
            connection1.Close();
            connection1.Open();
           
            command1.CommandText = "SELECT nom_client_entreprise FROM client_entreprise;";
            MySqlDataReader reader2;
            reader2 = command1.ExecuteReader();

            while (reader2.Read())
            {
                string dataline = "";
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    string valueAsString = reader2.GetValue(i).ToString();
                    dataline += valueAsString + " ";
                }
                Client.Add(dataline);

            }
            connection1.Close();

            return Client;
        }
        public List<string> ListeVelo()
        {
            List<string> Velo = new List<string>();

            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = "SELECT DISTINCT nom_velo FROM modele_velo ;";
            MySqlDataReader reader0;
            reader0 = command1.ExecuteReader();

            while (reader0.Read())
            {
                string dataline = "";
                for (int i = 0; i < reader0.FieldCount; i++)
                {
                    string valueAsString = reader0.GetValue(i).ToString();
                    dataline += valueAsString + " ";
                }
                Velo.Add(dataline);

            }
            connection1.Close();
            return Velo;
        }
        public List<string> ListePiece()
        {
            List<string> Piece = new List<string>();

            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = "SELECT DISTINCT numero_piece_catalogue FROM modele_piece ;";
            MySqlDataReader reader0;
            reader0 = command1.ExecuteReader();

            while (reader0.Read())
            {
                string dataline = "";
                for (int i = 0; i < reader0.FieldCount; i++)
                {
                    string valueAsString = reader0.GetValue(i).ToString();
                    dataline += valueAsString + " ";
                }
                Piece.Add(dataline);

            }
            connection1.Close();
            return Piece;
        }
        private void ClientBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AffichageClient.Text = "Commander au Fournisseur : "+ClientBox.SelectedItem;
        }
        private void VeloBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void AjoutVelo_Click(object sender, RoutedEventArgs e)
        {
            bool Present = false;
            int i = 0;
            int index = 0;
            foreach (string elem in panierVelo)
            {
                if (VeloBox.SelectedItem.ToString() == elem) { Present = true; index = i; }
                i++;
            }
            if(Present==true)
            {
                panierVeloQuantite[index] = panierVeloQuantite[index] + 1;
                DataTable dataT = ConvertToDatatable(panierVelo, panierVeloQuantite);
                CommandGridVelo.ItemsSource = dataT.DefaultView;
            }
            else
            {
                panierVelo.Add(VeloBox.SelectedItem.ToString());
                panierVeloQuantite.Add(1);
                DataTable dataT = ConvertToDatatable(panierVelo, panierVeloQuantite);
                CommandGridVelo.ItemsSource = dataT.DefaultView;

            }
            
            
            
            

            ///DataTable dataT = ConvertToDatatable(panierVelo,panierVeloQuantite);
            
            
        }

        private void AjoutPiece_Click(object sender, RoutedEventArgs e)
        {
            bool Present = false;
            int i = 0;
            int index = 0;
            foreach (string elem in panierPiece)
            {
                if (PieceBox.SelectedItem.ToString() == elem) { Present = true; index = i; }
                i++;
            }
            if (Present == true)
            {
                panierPieceQuantite[index] = panierPieceQuantite[index] + 1;
                DataTable dataT = ConvertToDatatable(panierPiece, panierPieceQuantite);
                CommandGridPiece.ItemsSource = dataT.DefaultView;
            }
            else
            {
                try
                {
                    panierPiece.Add(PieceBox.SelectedItem.ToString());
                }
                catch (NullReferenceException erreur)
                {
                    MessageBoxResult msg3 = System.Windows.MessageBox.Show(erreur.ToString(),"OK ? ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    
                }

                panierPieceQuantite.Add(1);
                DataTable dataT = ConvertToDatatable(panierPiece, panierPieceQuantite);
                CommandGridPiece.ItemsSource = dataT.DefaultView;

            }
        }
        private void Validation_Click(object sender, RoutedEventArgs e)
        {
            #region Alerte sur la commande
            string Alerte = "Attention : \n";
            bool valider = true;
            List<string> PieceManquante = new List<string>();
            for (int i = 0; i < panierPiece.Count; i++)
            {

                string numeroPiece = panierPiece[i].Trim();
                int quantite = panierPieceQuantite[i];
                
                string StockRequete = "SELECT stock_piece FROM modele_piece WHERE numero_piece_catalogue = '"+numeroPiece+"' ;";
                int Stock = Find(StockRequete);
                Console.WriteLine(Stock);
                int Reste = Stock - quantite;

                if (Reste <= 0) 
                /// Dans ce cas la pièce n'est pas en stock
                /// Il faut alors la commander
                {
                    PieceManquante.Add(panierPiece[i]);
                    Alerte = Alerte + "Piece " + numeroPiece + " : Stock insuffisant pour effectuer la commande \n"; valider = false; 
                }
                if (Reste <= 10 && Reste>0) { Alerte = Alerte + "Piece " + numeroPiece + " : Stock Faible    ( "+Reste+"pièces restantes après commande) \n"; }
                if (Reste >  10) { Alerte = Alerte + "Piece " + numeroPiece + " : Stock Suffisant ( " + Reste + "pièces restantes après commande)\n"; }

            }
            if(panierPiece.Count != 0) { AlerteText.Text = Alerte; }

            Fermeture.Visibility = Visibility.Visible;
            Validation.Visibility = Visibility.Collapsed;
            

            #endregion
            /// Dans le cas d'une commande valider toute les pièces sont en stock
            
            if (valider == true)
            {
                #region Création commande
                ///Recupération du plus grand "numero_commande" commandes
                int anciennum = RecupMaxNumCommande();
                int nouveaunum = anciennum + 1;

                DateTime ajd = DateTime.Now;
                /// Les pièces étant toutes en stock le déalai est de deux Jours
                DateTime Datelivraison = ajd.AddDays(2);
                string ajdstr = ajd.ToString("yyyy-MM-dd");
                string datelivr = Datelivraison.ToString("yyyy-MM-dd");
                
                string txt = "La commande est prête à être efectuée \n date de livraison estimée: "+datelivr;
                MessageBoxResult msg = System.Windows.MessageBox.Show(txt+"\n Voulez vous confirmer la commande?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (msg == MessageBoxResult.No)
                {  }
                string[] info = new string[2];
                if(ClientBox.SelectedItem != null)
                {
                    info = ClientBox.SelectedItem.ToString().Split();
                }
                else
                {
                    MessageBoxResult msg8 = System.Windows.MessageBox.Show("\n Voulez n'avez pas sélectionné de Client", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                }
                string nom = info[0];
                string prenom = info[1];
                List<string> numClient = FindClientParticulier(nom,prenom);
                string cp = numClient[0].ToString();
                string ce = numClient[2].ToString();
                int id_adresse = Convert.ToInt32(numClient[1]);

                string requete = "";
                if (ce == "null")
                {
                    requete = "INSERT INTO commande VALUES(" + nouveaunum + ", '" + ajdstr + "', '" + datelivr + "'," + id_adresse + ", " + ce + ", '" + cp + "',null);";
                }
                else
                {
                    requete = "INSERT INTO commande VALUES(" + nouveaunum + ", '" + ajdstr + "', '" + datelivr + "'," + id_adresse + ", '" + ce + "', " + cp + ",null);";
                }
                Console.WriteLine(requete);
                InsertRequete(requete);
                #endregion
                #region Création ligne de commande Velo et piece
                requete = "";

                for (int i = 0; i < panierVelo.Count; i++)
                {
                    int numeroVelo = FindVelo(panierVelo[i].Trim());
                    int quantite = panierVeloQuantite[i];
                    ///Console.WriteLine(numeroVelo);
                    requete = "INSERT INTO liste_velo_commande VALUES (" + nouveaunum + "," + numeroVelo + "," + quantite + ");";
                    InsertRequete(requete);
                }
                requete = "";

                for (int i = 0; i < panierPiece.Count; i++)
                {
                    string numeroPiece = panierPiece[i].Trim();
                    int quantite = panierPieceQuantite[i];
                    Console.WriteLine(numeroPiece);
                    requete = "INSERT INTO liste_piece_commande VALUES ('" + numeroPiece + "'," + nouveaunum + "," + quantite + ");";
                    InsertRequete(requete);
                }
                #endregion
                #region Modification des stocks
                for (int i = 0; i < panierPiece.Count; i++)
                {
                    string numeroPiece = panierPiece[i].Trim();
                    int quantite = panierPieceQuantite[i];
                    string StockRequete = "SELECT stock_piece FROM modele_piece WHERE numero_piece_catalogue = '" + numeroPiece + "' ;";
                    int Stock = Find(StockRequete);
                    int Reste = Stock - quantite;
                    requete = "UPDATE modele_piece SET stock_piece = "+Reste+" WHERE numero_piece_catalogue = '"+numeroPiece+"';";
                    InsertRequete(requete);
                }
                #endregion
                #region Prix 
                string Prix = PrixCommande(nouveaunum);
                MessageBoxResult msg11 = System.Windows.MessageBox.Show("La commande totale représente un montant de "+Prix+" €", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                #endregion


            }
            ///Si la commande n'est pas valide
            else
            {
                ///On doit commander les pieces manquante
                ///On récupère le déali de livraison 
                List<int> delaiLivraison = new List<int>();
                string strpieces = "";
                for (int i =0; i<PieceManquante.Count;i++)
                {
                    strpieces = strpieces + PieceManquante[i] + " ";
                    string piece = PieceManquante[i].Trim();
                    string requete = "SELECT delai_approvisionnement_piece FROM modele_piece WHERE numero_piece_catalogue = '"+piece+"' ;";
                    int delai = Find(requete);
                    delaiLivraison.Add(delai);
                }
                int delaiMax = delaiLivraison.Max();
                DateTime ajd = DateTime.Now;
                DateTime Datelivraison = ajd.AddDays(2+delaiMax);
                string Final = "Une commande des pièces "+strpieces+" est nécessaire. \n En considérant la commande au fournisseur, la date de livraison estimée pour le client : " + Datelivraison;
                MessageBoxResult msg2 = System.Windows.MessageBox.Show(Final, "Voulez vous effectuer une commande fournisseur maintenant?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (msg2 == MessageBoxResult.Yes)
                {

                    bool rep = true;
                    commande_Fournisseur next2 = new commande_Fournisseur();
                    next2.Show();

                }
                
            }
        }
        private int Find(string requete)
        {
            
            
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = requete;
            MySqlDataReader reader0;
            reader0 = command1.ExecuteReader();
            int numero = 0;
            while (reader0.Read())
            {
                numero = Convert.ToInt32(reader0.GetValue(0));
            }
            reader0.Close();
            return numero;
        }
        private int FindVelo(string nom)
        {
            ///Console.WriteLine(nom);
            string requete = "SELECT numero_velo FROM modele_velo WHERE nom_velo LIKE '%" + nom + "%'";
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = requete;
            MySqlDataReader reader0;
            reader0 = command1.ExecuteReader();
            int numero = 0;
            while (reader0.Read())
            {
                numero = Convert.ToInt32(reader0.GetValue(0));
            }
            reader0.Close();
            return numero;
        }

        private void InsertRequete(string requete)
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
        private List<string> FindClientParticulier(string nom,string prenom)
        {
            List<string>  numClientParticulier = new List<string>();
            string requete = "SELECT ID_client_particulier,ID_adresse_client_particulier FROM client_particulier WHERE nom_client_particulier = '"+nom+ "' AND prenom_client_particulier = '" + prenom + "' ;";
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = requete;
            MySqlDataReader reader0;
            reader0 = command1.ExecuteReader();
            while (reader0.Read())
            {
                for(int i =0; i<reader0.FieldCount;i++)
                {
                    numClientParticulier.Add(reader0.GetValue(i).ToString());
                }
            }
            reader0.Close();
            command1.Dispose();

            if (numClientParticulier.Count ==0)
            {
                numClientParticulier.Add("null");
                string requete2 = "SELECT ID_adresse_client_entreprise,ID_client_entreprise FROM client_entreprise WHERE nom_client_entreprise = '" + nom + "' ;";
                string connectionString2 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";

                MySqlConnection connection2 = new MySqlConnection(connectionString1);
                connection2.Open();
                MySqlCommand command2 = connection1.CreateCommand();
                command2.CommandText = requete2;
                MySqlDataReader reader2;
                reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    for (int i = 0; i < reader2.FieldCount; i++)
                    {
                        numClientParticulier.Add(reader2.GetValue(i).ToString());
                    }
                }
                reader2.Close();

            }
            else
            {
                numClientParticulier.Add("null");
            }
            return numClientParticulier;
        }
        
        private int RecupMaxNumCommande()
        {
            string requete = "SELECT max(numero_commande) FROM Commande ;";
            string connectionString1 = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);


            connection1.Open();
            MySqlCommand command1 = connection1.CreateCommand();
            command1.CommandText = requete;
            MySqlDataReader reader0;
            reader0 = command1.ExecuteReader();
            int anciennum = 0;
            while (reader0.Read())
            {
                anciennum = Convert.ToInt32(reader0.GetValue(0));
            }
            reader0.Close();
            return anciennum;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private string PrixCommande(int numero_commande)
        {
            string prix = "";
            string requete = "SELECT numero_commande , sum(prix_commande_piece) FROM((SELECT numero_commande, prix_commande_piece " +
                "FROM commande " +
                "JOIN(SELECT numero_commande_piece, SUM((quantite_piece_commande * prix_piece)) as prix_commande_piece " +
                "FROM liste_piece_commande JOIN modele_piece " +
                "ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue " +
                "GROUP BY numero_commande_piece) my_req " +
                "ON commande.numero_commande = my_req.numero_commande_piece) UNION ALL " +
                "(SELECT numero_commande, prix_commande_velo FROM commande JOIN(SELECT numero_commande_velo, sum(quantite_velo_commande* prix_velo) as prix_commande_velo " +
                "FROM liste_velo_commande JOIN modele_velo " +
                "ON liste_velo_commande.numero_velo_commande = modele_velo.numero_velo " +
                "GROUP BY numero_commande_velo) my_req " +
                "ON commande.numero_commande = my_req.numero_commande_velo) ) alias WHERE numero_commande = "+numero_commande+" GROUP BY numero_commande; ";
            string[] Res = Execute(requete).Split(';');

            string Res2 = "";
            foreach (string elem in Res)
            {
                string[] Finish = elem.Split(',');
                 prix = Finish[1];
                
               

               
            }
            return prix;
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
    }
}
