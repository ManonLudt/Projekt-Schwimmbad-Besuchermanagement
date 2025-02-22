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
using Microsoft.Data.SqlClient;

namespace SchwimmbadProjekt_Besuchermanagment
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
            // Eingabewerte aus den Textboxen holen
            int? idTicket = string.IsNullOrEmpty(txtTicketID.Text) ? (int?)null : Convert.ToInt32(txtTicketID.Text);
            string bezeichnung = txtTicketBezeichnung.Text;
            decimal? preis = string.IsNullOrEmpty(txtTicketPreis.Text) ? (decimal?)null : Convert.ToDecimal(txtTicketPreis.Text);
            int? anzahl = string.IsNullOrEmpty(txtTicketAnzahl.Text) ? (int?)null : Convert.ToInt32(txtTicketAnzahl.Text);

            // Überprüfen, ob alle Eingabefelder ausgefüllt sind
            //if (string.IsNullOrEmpty(idKartenText) || string.IsNullOrEmpty(bezeichnungText) ||
            //    string.IsNullOrEmpty(preisText) || string.IsNullOrEmpty(anzahlText))
            //{
            //    ErrorAusgabe.Text = "Bitte füllen Sie alle Felder aus.";
            //    txtTicketID.BorderBrush = Brushes.Red;
            //    txtTicketBezeichnung.BorderBrush = Brushes.Red;
            //    txtTicketPreis.BorderBrush = Brushes.Red;
            //    txtTicketAnzahl.BorderBrush = Brushes.Red;
            //}

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

