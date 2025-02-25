using Microsoft.Data.SqlClient;
using Schwimmbad_Besuchermanagment.Models;
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
    /// Interaktionslogik für TicketAnlegen.xaml
    /// </summary>
    public partial class TicketAnlegen : Window
    {
        public TicketAnlegen()
        {
            InitializeComponent();
        }

        private void TicketAnlegen_Click(object sender, RoutedEventArgs e)
        {
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
            if (string.IsNullOrEmpty(txtTicketID.Text) || string.IsNullOrEmpty(bezeichnung) || preis == null || anzahl == null)
            {
                MessageBox.Show("Bitte füllen Sie alle Felder aus!");
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

                    // Setze IDENTITY_INSERT auf ON, um explizit einen Wert für die IDTicket-Spalte zu erlauben
                    string setIdentityOnQuery = "SET IDENTITY_INSERT Ticket ON;";
                    using (SqlCommand setIdentityCommand = new SqlCommand(setIdentityOnQuery, con))
                    {
                        setIdentityCommand.ExecuteNonQuery();
                    }

                    // SQL-Anweisung zum Einfügen des Tickets
                    string query = "INSERT INTO Ticket (Id_Ticket, Bezeichnung, Preis, Anzahl) VALUES (@IdTicket, @Bezeichnung, @Preis, @Anzahl)";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Parameter für die SQL-Anweisung hinzufügen
                        command.Parameters.AddWithValue("@IdTicket", (object)idTicket ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Bezeichnung", bezeichnung);
                        command.Parameters.AddWithValue("@Preis", (object)preis ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Anzahl", (object)anzahl ?? DBNull.Value);

                        // Ausführen der SQL-Anweisung
                        int result = command.ExecuteNonQuery();

                        // Prüfen, ob die Einfügung erfolgreich war
                        if (result > 0)
                        {
                            MessageBox.Show("Ticket erfolgreich erstellt!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Fehler beim Erstellen des Tickets.");
                        }
                    }

                    // Setze IDENTITY_INSERT wieder auf OFF, um die automatische Inkrementierung der ID zu ermöglichen
                    string setIdentityOffQuery = "SET IDENTITY_INSERT Ticket OFF;";
                    using (SqlCommand setIdentityOffCommand = new SqlCommand(setIdentityOffQuery, con))
                    {
                        setIdentityOffCommand.ExecuteNonQuery();
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
