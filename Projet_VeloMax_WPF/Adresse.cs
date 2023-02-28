using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    class Adresse
    {
        #region Attributs
        private int ID_adresse;
        private string rue_adresse;
        private string ville_adresse;
        private string code_postal_adresse;
        private string province_adresse;
        #endregion Attributs

        #region Constructeurs
        public Adresse(int ID_adresse, string rue_adresse, string ville_adresse,
            string code_postal_adresse, string province_adresse)
        {
            this.ID_adresse = ID_adresse;
            this.rue_adresse = rue_adresse;
            this.ville_adresse = ville_adresse;
            this.code_postal_adresse = code_postal_adresse;
            this.province_adresse = province_adresse;
        }
        #endregion Constructeurs

        #region Accesseurs
        public int ID_Adresse
        {
            get { return ID_adresse; }
            set { ID_adresse = value; }
        }
        public string Rue
        {
            get { return rue_adresse; }
            set { rue_adresse = value; }
        }
        public string Ville
        {
            get { return ville_adresse; }
            set { ville_adresse = value; }
        }
        public string Code_postal
        {
            get { return code_postal_adresse; }
            set { code_postal_adresse = value; }
        }
        public string Province_adresse
        {
            get { return province_adresse; }
            set { province_adresse = value; }
        }
        #endregion Accesseurs
    }
}
