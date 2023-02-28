using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Commande
    {
        ///# numero_commande date_commande date_livraison_commande ID_adresse_commande ID_client_entreprise ID_client_particulier 
        public int numero_commande;
        public string date_commande;
        public string date_livraison_commande;
        public int ID_adresse_commande;
        public string ID_client_entreprise;
        public string ID_client_particulier;
        public string statut;
        public Commande(int numero_commande, string date_commande, string date_livraison_commande, int ID_adresse_commande, string statut, string ID_client_entreprise, string ID_client_particulier)
        {
            this.numero_commande = numero_commande;
            this.date_commande = date_commande;
            this.date_livraison_commande = date_livraison_commande;
            this.ID_adresse_commande = ID_adresse_commande;
            this.ID_client_entreprise = ID_client_entreprise;
            this.ID_client_particulier = ID_client_particulier;
            this.statut = statut;
        }
        public int Numero_commande
        {
            get { return numero_commande; }
            set { numero_commande = value; }
        }
        public string Date_commande
        {
            get { return date_commande; }
            set { date_commande = value; }
        }
        public string Date_livraison_commande
        {
            get { return date_livraison_commande; }
            set { date_livraison_commande = value; }
        }
        public int ID_Adresse_commande
        {
            get { return ID_adresse_commande; }
            set { ID_adresse_commande = value; }
        }
        public string ID_Client_entreprise
        {
            get { return ID_client_entreprise; }
            set { ID_client_entreprise = value; }
        }
        public string ID_Client_particulier
        {
            get { return ID_client_particulier; }
            set { ID_client_particulier = value; }
        }
        public string Statut
        {
            get { return statut; }
            set { statut = value; }
        }
    }
}
