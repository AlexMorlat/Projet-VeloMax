using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Client
    {

        #region Attributs
        private string id_client;      //clé primaire
        private string nom_client;
        private string prenom_client;
        private string date_adhesion_programme;
        private string numero_programme;
        private string courriel;
        private string telephone;
        private string adresse_client;
        #endregion Attributs

       
        public Client(string id_client, string nom_client, string prenom_client, string date_adhesion_programme, string courriel, string telephone, string numero_programme)
        {
            this.id_client = id_client;
            this.nom_client = nom_client;
            this.prenom_client = prenom_client;
            this.date_adhesion_programme = date_adhesion_programme;
            this.courriel = courriel;
            this.telephone = telephone;
            this.numero_programme = numero_programme;

        }
        public Client()
        {

        }
        public string Id_client
        {
            get { return id_client; }
            set { id_client = value; }
        }
        public string Numero_programme
        {
            get { return numero_programme; }
            set { numero_programme = value; }
        }

        public string Nom_client
        {
            get { return nom_client; }
            set { nom_client = value; }
        }
        public string Prenom_client
        {
            get { return prenom_client; }
            set { prenom_client = value; }
        }

        public string Date_adhesion_programme
        {
            get { return date_adhesion_programme; }
            set { date_adhesion_programme = value; }
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
            Console.WriteLine("Le client s'appelle " + Nom_client );
        }
    }
    
}
