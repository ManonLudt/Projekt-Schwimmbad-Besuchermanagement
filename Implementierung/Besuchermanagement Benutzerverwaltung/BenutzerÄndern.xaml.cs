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

namespace Besuchermanagement_Benutzerverwaltung
{
    /// <summary>
    /// Interaktionslogik für BenutzerÄndern.xaml
    /// </summary>
    public partial class BenutzerÄndern : Window
    {
        public BenutzerÄndern()
        {
            InitializeComponent();
        }

        private void BenutzerÄndern_Click(object sender, RoutedEventArgs e)
        {
            string alterBenutzername = txtBenutzername.Text.Trim();
            string neuerBenutzername = txtNeuerBenutzernamen.Text.Trim();
            string email = txtEMail.Text.Trim();
            string telefon = txtTelefon.Text.Trim();

            // Verbindungsaufbau zur Datenbank
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

                    // Prüfen, ob der Benutzer existiert
                    string checkUserQuery = "SELECT COUNT(*) FROM Benutzer WHERE Benutzername = @Benutzername";
                    SqlCommand checkCmd = new SqlCommand(checkUserQuery, con);
                    checkCmd.Parameters.AddWithValue("@Benutzername", alterBenutzername);
                    int userExists = (int)checkCmd.ExecuteScalar();

                    if (userExists > 0)
                    {
                        // Wenn der Benutzer existiert, aktualisiere die Daten
                        string updateQuery = "UPDATE Benutzer SET ";

                        // Dynamische Abfrage, um nur die Felder zu aktualisieren, die auch neue Werte haben
                        List<SqlParameter> parameters = new List<SqlParameter>();
                        if (!string.IsNullOrEmpty(neuerBenutzername))
                        {
                            updateQuery += "Benutzername = @NeuerBenutzername, ";
                            parameters.Add(new SqlParameter("@NeuerBenutzername", neuerBenutzername));
                        }
                        if (!string.IsNullOrEmpty(email))
                        {
                            updateQuery += "EMail = @EMail, ";
                            parameters.Add(new SqlParameter("@EMail", email));
                        }
                        if (!string.IsNullOrEmpty(telefon))
                        {
                            updateQuery += "Telefon = @Telefon, ";
                            parameters.Add(new SqlParameter("@Telefon", telefon));
                        }

                        // Entferne das letzte Komma und füge WHERE-Klausel hinzu
                        updateQuery = updateQuery.TrimEnd(',', ' ') + " WHERE Benutzername = @Benutzername";
                        parameters.Add(new SqlParameter("@Benutzername", alterBenutzername));

                        // Aktualisieren der Daten
                        SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                        updateCmd.Parameters.AddRange(parameters.ToArray());
                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Benutzerdaten wurden erfolgreich geändert.");

                            // Benutzerdaten für die BenutzerAusgabe-Seite vorbereiten
                            BenutzerAusgabe benutzerAusgabe = new BenutzerAusgabe();
                            benutzerAusgabe.UpdateBenutzerdaten(neuerBenutzername, email, telefon);
                        }
                        else
                        {
                            MessageBox.Show("Keine Änderungen vorgenommen.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Benutzer mit dem angegebenen Benutzernamen existiert nicht.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler: {ex.Message}");
                }
            }
        }
    }
}

