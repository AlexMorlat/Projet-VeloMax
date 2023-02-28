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
    /// Logique d'interaction pour AdminLogIn.xaml
    /// </summary>
    public partial class AdminLogIn : Window
    {
        public AdminLogIn()
        {
            InitializeComponent();
            
      }
                
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (id.Text == "root" & mdp.Password == "root")
            {
                Connexion my_connexion = new Connexion();
                my_connexion.stringconnexionGenerale = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo;";
                ///Console.WriteLine(Connexion.stringconnexionGenerale);
                
                ///Console.WriteLine("LAAAAAAAAAAAAAAAAAAAAAAAAAA" + Connexion.StringconnexionGenerale);
                EspaceAdmin next = new EspaceAdmin();
                next.Show();
                this.Close();
                
            }
            else
            {
                MessageBox.Show("Problème d'identification veuillez réassayer");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        static void Definition_privilege()
        {
        }
    }
}
