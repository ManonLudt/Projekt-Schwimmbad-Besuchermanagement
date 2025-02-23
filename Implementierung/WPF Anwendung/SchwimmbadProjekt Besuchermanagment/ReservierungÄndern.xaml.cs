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

namespace SchwimmbadProjekt_Besuchermanagment
{
    /// <summary>
    /// Interaktionslogik für ReservierungÄndern.xaml
    /// </summary>
    public partial class ReservierungÄndern : Window
    {
        public ReservierungÄndern()
        {
            InitializeComponent();
        }
        private void ReservierungÄndern_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReservierungID.Text))
            {
                ErrorAusgabe.Text = "Bitte eine ReservierungID eingeben!";
                return;
            }

            int reservierungID;
            if (!int.TryParse(txtReservierungID.Text, out reservierungID))
            {
                ErrorAusgabe.Text = "Ungültige ReservierungID!";
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
            try
            {
                // SQL-Verbindung öffnen
                using (SqlConnection con = new SqlConnection(sqlSb.ConnectionString))
                {
                    con.Open();

                    // Hole die aktuelle Reservierung, um die bestehenden Daten abzurufen
                    string queryReservierung = "SELECT * FROM Reservierung WHERE ID_Reservierung = @ReservierungID";
                    SqlCommand cmdReservierung = new SqlCommand(queryReservierung, con);
                    cmdReservierung.Parameters.AddWithValue("@ReservierungID", reservierungID);
                    SqlDataReader readerReservierung = cmdReservierung.ExecuteReader();

                    if (!readerReservierung.Read())
                    {
                        ErrorAusgabe.Text = "Reservierung nicht gefunden!";
                        return;
                    }

                    // Vorhandene Daten speichern
                    string vorname = readerReservierung["Vorname"].ToString();
                    string nachname = readerReservierung["Nachname"].ToString();
                    string status = readerReservierung["Status"].ToString();
                    string ticketBezeichnung = readerReservierung["Ticket"].ToString();
                    int rabatt = Convert.ToInt32(readerReservierung["Rabatt"]);
                    readerReservierung.Close();

                    // Falls KundenID eingegeben wurde, die entsprechende Kundendaten aus der Datenbank holen
                    if (!string.IsNullOrWhiteSpace(txtKundenID.Text))
                    {
                        int kundenID = Convert.ToInt32(txtKundenID.Text);
                        string queryKunde = "SELECT * FROM Besucher WHERE ID_Besucher = @KundenID";
                        SqlCommand cmdKunde = new SqlCommand(queryKunde, con);
                        cmdKunde.Parameters.AddWithValue("@KundenID", kundenID);
                        SqlDataReader readerKunde = cmdKunde.ExecuteReader();

                        if (!readerKunde.Read())
                        {
                            ErrorAusgabe.Text = "Kunde nicht gefunden!";
                            return;
                        }

                        vorname = readerKunde["Vorname"].ToString();
                        nachname = readerKunde["Nachname"].ToString();
                        status = readerKunde["Status"].ToString();
                        readerKunde.Close();
                    }

                    // Falls TicketID eingegeben wurde, die entsprechende Ticketbezeichnung aus der Datenbank holen
                    if (!string.IsNullOrWhiteSpace(txtTicketID.Text))
                    {
                        int ticketID = Convert.ToInt32(txtTicketID.Text);
                        string queryTicket = "SELECT Bezeichnung FROM Ticket WHERE ID_Ticket = @TicketID";
                        SqlCommand cmdTicket = new SqlCommand(queryTicket, con);
                        cmdTicket.Parameters.AddWithValue("@TicketID", ticketID);
                        SqlDataReader readerTicket = cmdTicket.ExecuteReader();

                        if (!readerTicket.Read())
                        {
                            ErrorAusgabe.Text = "Ticket nicht gefunden!";
                            return;
                        }

                        ticketBezeichnung = readerTicket["Bezeichnung"].ToString();
                        readerTicket.Close();
                    }

                    // Berechne den Rabatt basierend auf dem Kundenstatus, falls dieser geändert wurde
                    if (status == "Schüler")
                    {
                        rabatt = 10;
                    }
                    else if (status == "Student")
                    {
                        rabatt = 5;
                    }

                    // Update der Reservierung mit den neuen Werten
                    string updateQuery = "UPDATE Reservierung SET " +
                                         "Vorname = @Vorname, " +
                                         "Nachname = @Nachname, " +
                                         "Status = @Status, " +
                                         "Ticket = @Ticket, " +
                                         "Rabatt = @Rabatt " +
                                         "WHERE ID_Reservierung = @ReservierungID";

                    SqlCommand cmdUpdate = new SqlCommand(updateQuery, con);
                    cmdUpdate.Parameters.AddWithValue("@Vorname", vorname);
                    cmdUpdate.Parameters.AddWithValue("@Nachname", nachname);
                    cmdUpdate.Parameters.AddWithValue("@Status", status);
                    cmdUpdate.Parameters.AddWithValue("@Ticket", ticketBezeichnung);
                    cmdUpdate.Parameters.AddWithValue("@Rabatt", rabatt);
                    cmdUpdate.Parameters.AddWithValue("@ReservierungID", reservierungID);

                    // Update ausführen
                    cmdUpdate.ExecuteNonQuery();

                    MessageBox.Show("Reservierung erfolgreich geändert!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler: {ex.Message}");
            }
        }
    }
}