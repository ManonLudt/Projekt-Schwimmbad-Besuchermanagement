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

namespace Schwimmbad_Besuchermanagment
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
            // Überprüfen, ob die IDs eingegeben wurde und gültig ist
            if (string.IsNullOrWhiteSpace(txtReservierungID.Text))
            {
                MessageBox.Show("Bitte eine ReservierungID eingeben!");
                return;
            }

            int reservierungID;
            if (!int.TryParse(txtReservierungID.Text, out reservierungID))
            {
                MessageBox.Show("Ungültige ReservierungID!");
                return;
            }

            if (reservierungID == 0)
            {
                MessageBox.Show("Die ReservierungsID darf nicht 0 sein!");
                return;
            }

            int? kundenID = null;
            int? ticketID = null;
            if (!string.IsNullOrWhiteSpace(txtKundenID.Text))
            {
                if (!int.TryParse(txtKundenID.Text, out int tempKundenID) || !int.TryParse(txtTicketID.Text, out int tempTicketID))
                {
                    MessageBox.Show("Ungültige ID(s)!");
                    return;
                }
                kundenID = tempKundenID;
                ticketID = tempTicketID;
            }

            // Verbindung zur Datenbank aufbauen
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
                using (SqlConnection con = new SqlConnection(sqlSb.ConnectionString))
                {
                    con.Open();

                    string queryReservierung = "SELECT * FROM Reservierung WHERE ID_Reservierung = @ReservierungID";
                    SqlCommand cmdReservierung = new SqlCommand(queryReservierung, con);
                    cmdReservierung.Parameters.AddWithValue("@ReservierungID", reservierungID);
                    SqlDataReader readerReservierung = cmdReservierung.ExecuteReader();

                    if (!readerReservierung.Read())
                    {
                        MessageBox.Show("Reservierung nicht gefunden!");
                        return;
                    }

                    string vorname = readerReservierung["Vorname"].ToString();
                    string nachname = readerReservierung["Nachname"].ToString();
                    string status = readerReservierung["Status"].ToString();
                    string ticketBezeichnung = readerReservierung["Ticket"].ToString();
                    int rabatt = Convert.ToInt32(readerReservierung["Rabatt"]);
                    readerReservierung.Close();

                    if (kundenID.HasValue)
                    {
                        string queryKunde = "SELECT * FROM Besucher WHERE ID_Besucher = @KundenID";
                        SqlCommand cmdKunde = new SqlCommand(queryKunde, con);
                        cmdKunde.Parameters.AddWithValue("@KundenID", kundenID.Value);
                        SqlDataReader readerKunde = cmdKunde.ExecuteReader();

                        if (!readerKunde.Read())
                        {
                            MessageBox.Show("Kunde nicht gefunden!");
                            return;
                        }

                        vorname = readerKunde["Vorname"].ToString();
                        nachname = readerKunde["Nachname"].ToString();
                        status = readerKunde["Status"].ToString();
                        readerKunde.Close();
                    }

                    if (ticketID.HasValue)
                    {
                        string queryTicket = "SELECT Bezeichnung FROM Ticket WHERE ID_Ticket = @TicketID";
                        SqlCommand cmdTicket = new SqlCommand(queryTicket, con);
                        cmdTicket.Parameters.AddWithValue("@TicketID", ticketID.Value);
                        SqlDataReader readerTicket = cmdTicket.ExecuteReader();

                        if (!readerTicket.Read())
                        {
                            MessageBox.Show("Ticket nicht gefunden!");
                            return;
                        }

                        ticketBezeichnung = readerTicket["Bezeichnung"].ToString();
                        readerTicket.Close();
                    }

                    //Rabatte für Status
                    if (status == "Schüler")
                    {
                        rabatt = 10;
                    }
                    else if (status == "Student")
                    {
                        rabatt = 5;
                    }

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

                    cmdUpdate.ExecuteNonQuery();

                    MessageBox.Show("Reservierung erfolgreich geändert!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler: {ex.Message}");
            }
        }
    }
}
