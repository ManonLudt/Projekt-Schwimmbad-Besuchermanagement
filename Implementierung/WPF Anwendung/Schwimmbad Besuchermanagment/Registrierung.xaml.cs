using Microsoft.Data.SqlClient;
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

namespace Schwimmbad_Besuchermanagment
{
    /// <summary>
    /// Interaktionslogik für Registrierung.xaml
    /// </summary>
    public partial class Registrierung : Window
    {
        public Registrierung()
        {
            InitializeComponent();
        }

        private void BenutzerRegestrierenClick(object sender, RoutedEventArgs e)
        {
            string benutzername = txtBenutzername.Text; 
            string passwort = pwdPasswort.Password; 
            string passwortAgain = pwdPasswortAgain.Password;

            // Überprüft Eingaben
            if (string.IsNullOrEmpty(benutzername) || string.IsNullOrEmpty(passwort) || string.IsNullOrEmpty(passwortAgain))
            {
                MessageBox.Show("Bitte füllen Sie alle Felder aus.");
                return;
            }

            if (!int.TryParse(passwort, out int passwortInt))
            {
                MessageBox.Show("Das Passwort darf nur Zahlen enthalten.");
                return; 
            }

            if (passwort.Length < 4)
            {
                MessageBox.Show("Das Passwort muss mindestens 4 Zeichen lang sein.");
                return; 
            }

            if (passwort.Length > 10)
            {
                MessageBox.Show("Das Passwort darf maximal 10 Zeichen lang sein.");
                return;
            }

            // Überprüft, ob die Passwörter übereinstimmen
            if (passwort == passwortAgain)
            {
                // Verbindung zur Datenbank aufbauen
                SqlConnectionStringBuilder sqlSb = new SqlConnectionStringBuilder
                {
                    DataSource = @"(LocalDb)\MSSQLLocalDB",
                    InitialCatalog = "BesuchermanagmentDatenbank",
                    IntegratedSecurity = true,
                    MultipleActiveResultSets = true
                };

                // SQL-Verbindung verwenden
                using (SqlConnection con = new SqlConnection(sqlSb.ConnectionString))
                {
                    try
                    {
                        con.Open();

                        // Benutzer wird hinzuzufügt
                        string sqlInsertUser = "INSERT INTO Benutzer (Benutzername, Passwort) VALUES (@username, @password)";

                        using (SqlCommand command = new SqlCommand(sqlInsertUser, con))
                        {
                            command.Parameters.AddWithValue("@username", benutzername);
                            command.Parameters.AddWithValue("@password", passwortInt); 

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Benutzer erfolgreich hinzugefügt.");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Fehler beim Hinzufügen des Benutzers.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fehler: " + ex.Message);
                    }
                }
            }

            else
            {
                // Falls die Passwörter nicht übereinstimmen
                pwdPasswort.BorderBrush = Brushes.Red;
                pwdPasswortAgain.BorderBrush = Brushes.Red;
                ErrorAusgabe.Text = "Passwort stimmt nicht überein";
            }
        }

        private void ShowPasswortCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            txtPasswort.Text = pwdPasswort.Password;

            txtPasswort.Visibility = Visibility.Visible;
        }

        private void ShowPasswortCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPasswort.Visibility = Visibility.Collapsed;
        }

        private void ShowPasswortAgainCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            txtPasswortAgain.Text = pwdPasswortAgain.Password;

            txtPasswortAgain.Visibility = Visibility.Visible;
        }

        private void ShowPasswortAgainCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPasswortAgain.Visibility = Visibility.Collapsed;
        }
    }
}
