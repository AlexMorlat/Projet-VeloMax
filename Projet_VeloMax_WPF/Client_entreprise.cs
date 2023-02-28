using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Client_entreprise
    {

        #region Attributs
        private string id_client;      //clé primaire
        private string nom_client;
        private string contact_client;
        private string remise;
        private string courriel;
        private string telephone;
        private string adresse_client;
        #endregion Attributs


        public Client_entreprise(string id_client, string nom_client, string contact_client, 
            string remise, string courriel, string telephone)
        {
            this.id_client = id_client;
            this.nom_client = nom_client;
            this.contact_client = contact_client;
            this.remise = remise;
            this.courriel = courriel;
            this.telephone = telephone;

        }
        public Client_entreprise()
        {

        }
        public string Id_client
        {
            get { return id_client; }
            set { id_client = value; }
        }

        public string Nom_client
        {
            get { return nom_client; }
            set { nom_client = value; }
        }
        public string Contact_client
        {
            get { return contact_client; }
            set { contact_client = value; }
        }

        public string Remise
        {
            get { return remise; }
            set { remise = value; }
        }

        public string Courriel
        {
            get { return courriel; }
            set { courriel = value; }
        }
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }
        public void info()
        {
            Console.WriteLine("Le client s'appelle " + Nom_client);
        }
    }
}
