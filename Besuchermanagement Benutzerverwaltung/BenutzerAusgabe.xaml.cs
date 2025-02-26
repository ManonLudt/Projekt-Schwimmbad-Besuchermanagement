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

namespace Besuchermanagement_Benutzerverwaltung
{
    /// <summary>
    /// Interaktionslogik für BenutzerAusgabe.xaml
    /// </summary>
    public partial class BenutzerAusgabe : Page
    {
        public BenutzerAusgabe()
        {
            InitializeComponent();
        }

        public void UpdateBenutzerdaten(string benutzername, string email, string telefon, string profilbildPfad)
        {
            // Daten in die Textblöcke setzen
            txtBenutzername.Text = benutzername;
            txtBenutzerEMail.Text = email;
            txtBenutzerTelefon.Text = telefon;

            // Profilbild aktualisieren
            if (!string.IsNullOrEmpty(profilbildPfad))
            {
                myImage.Source = new BitmapImage(new Uri(profilbildPfad));
            }
        }

        private void BenutzerÄndern_Click(object sender, RoutedEventArgs e)
        {
            BenutzerÄndern BenutzerÄndernWindow = new BenutzerÄndern();
            BenutzerÄndernWindow.Show();
        }
    }
}
