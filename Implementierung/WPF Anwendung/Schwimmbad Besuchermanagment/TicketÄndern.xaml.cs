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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Schwimmbad_Besuchermanagment
{
    /// <summary>
    /// Interaktionslogik für TicketÄndern.xaml
    /// </summary>
    public partial class TicketÄndern : Window
    {
        public TicketÄndern()
        {
            InitializeComponent();
        }

        private void TicketÄndern_Click(object sender, RoutedEventArgs e)
        {
            // Eingabewerte aus den Textboxen holen
            if (!int.TryParse(txtTicketID.Text, out int parsedTicketID) && !string.IsNullOrEmpty(txtTicketID.Text))
            {
                MessageBox.Show("Ungültige TicketID!");
                return;
            }

            // Überprüfen, ob der Preis eine gültige Zahl ist
            if (!decimal.TryParse(txtTicketPreis.Text, out decimal parsedPreis) && !string.IsNullOrEmpty(txtTicketPreis.Text))
            {
                MessageBox.Show("Ungültige Preis!");
                return;
            }

            // Überprüfen, ob die Anzahl eine gültige Zahl ist
            if (!int.TryParse(txtTicketAnzahl.Text, out int parsedAnzahl) && !string.IsNullOrEmpty(txtTicketAnzahl.Text))
            {
                MessageBox.Show("Ungültige Anzahl!");
                return;
            }

            // Eingabewerte aus den Textboxen holen
            int? idTicket = string.IsNullOrEmpty(txtTicketID.Text) ? (int?)null : Convert.ToInt32(txtTicketID.Text);
            string bezeichnung = txtTicketBezeichnung.Text;
            decimal? preis = string.IsNullOrEmpty(txtTicketPreis.Text) ? (decimal?)null : Convert.ToDecimal(txtTicketPreis.Text);
            int? anzahl = string.IsNullOrEmpty(txtTicketAnzahl.Text) ? (int?)null : Convert.ToInt32(txtTicketAnzahl.Text);

            // Überprüfen, ob alle Felder ausgefüllt sind
            if (string.IsNullOrEmpty(txtTicketID.Text) && string.IsNullOrEmpty(bezeichnung) && preis == null && anzahl == null)
            {
                MessageBox.Show("Bitte füllen Sie mindestens ein Felder aus!");
                return;
            }

            // Verhindern, dass die TicketID, Preis oder Anzahl 0 sind oder ungültige Werte eingegeben werden
            if (idTicket == 0)
            {
                MessageBox.Show("Die TicketID darf nicht 0 sein!");
                return;
            }

            if (preis == 0)
            {
                MessageBox.Show("Der Preis darf nicht 0 sein!");
                return;
            }

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

                    // Erstelle die SQL-Anweisung zum Aktualisieren des Tickets
                    string query = "UPDATE Ticket SET ";
                    List<string> updateFields = new List<string>();

                    // Wenn Bezeichnung eingegeben wurde, zum Update-Statement hinzufügen
                    if (!string.IsNullOrEmpty(bezeichnung))
                    {
                        updateFields.Add("Bezeichnung = @Bezeichnung");
                    }

                    // Wenn Preis eingegeben wurde, zum Update-Statement hinzufügen
                    if (preis.HasValue)
                    {
                        updateFields.Add("Preis = @Preis");
                    }

                    // Wenn Anzahl eingegeben wurde, zum Update-Statement hinzufügen
                    if (anzahl.HasValue)
                    {
                        updateFields.Add("Anzahl = @Anzahl");
                    }

                    // Wenn es Felder gibt, die aktualisiert werden sollen
                    if (updateFields.Count > 0)
                    {
                        query += string.Join(", ", updateFields) + " WHERE Id_Ticket = @IdTicket";
                    }
                    else
                    {
                        MessageBox.Show("Bitte füllen Sie mindestens ein Feld aus, um Änderungen vorzunehmen.");
                        return; // Keine Änderungen, wenn keine Eingabe erfolgt
                    }

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Parameter für die SQL-Anweisung hinzufügen
                        command.Parameters.AddWithValue("@IdTicket", idTicket);

                        if (!string.IsNullOrEmpty(bezeichnung))
                            command.Parameters.AddWithValue("@Bezeichnung", bezeichnung);
                        if (preis.HasValue)
                            command.Parameters.AddWithValue("@Preis", preis.Value);
                        if (anzahl.HasValue)
                            command.Parameters.AddWithValue("@Anzahl", anzahl.Value);

                        // Ausführen der SQL-Anweisung
                        int result = command.ExecuteNonQuery();

                        // Prüfen, ob die Aktualisierung erfolgreich war
                        if (result > 0)
                        {
                            MessageBox.Show("Ticket erfolgreich aktualisiert!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Fehler beim Aktualisieren des Tickets oder Ticket nicht gefunden.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler: " + ex.Message);
                }
            }
        }

    }

}
