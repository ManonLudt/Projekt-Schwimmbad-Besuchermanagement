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
    /// Interaktionslogik für PasswortÄndern.xaml
    /// </summary>
    public partial class PasswortÄndern : Window
    {
        public PasswortÄndern()
        {
            InitializeComponent();
        }

        private void PasswortÄndernClick(object sender, RoutedEventArgs e)
        {
            string benutzername = txtBenutzername.Text; // Benutzername aus der Textbox
            string Passwort = pwdPasswort.Password; // Neues Passwort aus der PasswordBox
            string PasswortAgain = pwdPasswortAgain.Password;

            if (string.IsNullOrEmpty(benutzername) || string.IsNullOrEmpty(Passwort))
            {
                MessageBox.Show("Bitte Benutzername und Passwort eingeben.");
                return;
            }

            else if (Passwort == PasswortAgain)
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

                        // SQL-Abfrage, um das Passwort für den Benutzer zu aktualisieren
                        string sqlUpdatePasswort = "UPDATE Benutzer SET Passwort = @password WHERE Benutzername = @username";

                        using (SqlCommand command = new SqlCommand(sqlUpdatePasswort, con))
                        {
                            // Parameter für den SQL-Befehl hinzufügen
                            command.Parameters.AddWithValue("@username", benutzername);
                            command.Parameters.AddWithValue("@password", Passwort);

                            // Befehl ausführen, um das Passwort zu ändern
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Passwort erfolgreich geändert.", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Benutzer nicht gefunden oder Fehler beim Ändern des Passworts.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Fehlerbehandlung
                        MessageBox.Show("Fehler: " + ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            else
            {
                pwdPasswort.BorderBrush = Brushes.Red;
                pwdPasswortAgain.BorderBrush = Brushes.Red;
                ErrorAusgabe.Text = "Passwort stimmt nicht überein";
            }
        }

    }
    
}
