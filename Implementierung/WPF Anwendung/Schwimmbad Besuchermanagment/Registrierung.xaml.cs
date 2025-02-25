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
            string benutzername = txtBenutzername.Text; // Angenommene Textbox für den Benutzernamen
            string passwort = pwdPasswort.Password; // Angenommene Textbox für das Passwort
            string passwortAgain = pwdPasswortAgain.Password;

            // Überprüfen, ob alle Felder ausgefüllt sind
            if (string.IsNullOrEmpty(benutzername) || string.IsNullOrEmpty(passwort) || string.IsNullOrEmpty(passwortAgain))
            {
                MessageBox.Show("Bitte füllen Sie alle Felder aus.");
                return; // Abbruch, wenn ein Feld leer ist
            }

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

                        // SQL-Befehl, um den neuen Benutzer hinzuzufügen
                        string sqlInsertUser = "INSERT INTO Benutzer (Benutzername, Passwort) VALUES (@username, @password)";

                        using (SqlCommand command = new SqlCommand(sqlInsertUser, con))
                        {
                            // Parameter für den SQL-Befehl hinzufügen
                            command.Parameters.AddWithValue("@username", benutzername);
                            command.Parameters.AddWithValue("@password", passwort);

                            // Befehl ausführen, um den Benutzer hinzuzufügen
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
                        // Fehlerbehandlung
                        MessageBox.Show("Fehler: " + ex.Message);
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
