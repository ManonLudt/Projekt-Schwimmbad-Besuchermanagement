using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Schwimmbad_Besuchermanagment.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaktionslogik für ReservierungAnlegen.xaml
    /// </summary>
    public partial class ReservierungAnlegen : Window
    {
        public ReservierungAnlegen()
        {
            InitializeComponent();
        }

        private void ReservierungAnlegen_Click(object sender, RoutedEventArgs e)
        {
            // Fehlerbehandlung
            if (string.IsNullOrWhiteSpace(txtReservierungID.Text))
            {
                MessageBox.Show("Bitte eine ReservierungID eingeben!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKundenID.Text) || string.IsNullOrWhiteSpace(txtTicketID.Text))
            {
                MessageBox.Show("Bitte beide IDs eingeben!");
                return;
            }

            int kundenID;
            int ticketID;
            int reservierungsID = 0; // Standardwert für ReservierungsID, falls sie manuell eingegeben wird.

            if (!int.TryParse(txtKundenID.Text, out kundenID) || !int.TryParse(txtTicketID.Text, out ticketID))
            {
                MessageBox.Show("Ungültige ID(s)!");
                return;
            }

            // Versuche, die ReservierungsID aus dem Textfeld zu lesen, falls der Benutzer diese manuell eingibt.
            if (!string.IsNullOrWhiteSpace(txtReservierungID.Text) && !int.TryParse(txtReservierungID.Text, out reservierungsID))
            {
                MessageBox.Show("Ungültige ReservierungsID!");
                return;
            }

            if (reservierungsID == 0)
            {
                MessageBox.Show("Die ReservierungsID darf nicht 0 sein!");
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

                    string setIdentityOnQuery = "SET IDENTITY_INSERT Reservierung ON;";
                    using (SqlCommand setIdentityCommand = new SqlCommand(setIdentityOnQuery, con))
                    {
                        setIdentityCommand.ExecuteNonQuery();
                    }

                    // Hole die Kundendaten
                    string queryKunde = "SELECT * FROM Besucher WHERE ID_Besucher = @KundenID";
                    SqlCommand cmdKunde = new SqlCommand(queryKunde, con);
                    cmdKunde.Parameters.AddWithValue("@KundenID", kundenID);
                    SqlDataReader readerKunde = cmdKunde.ExecuteReader();

                    if (!readerKunde.Read())
                    {
                        MessageBox.Show("Kunde nicht gefunden!");
                        return;
                    }

                    string vorname = readerKunde["Vorname"].ToString();
                    string nachname = readerKunde["Nachname"].ToString();
                    string status = readerKunde["Status"].ToString();
                    readerKunde.Close();

                    // Hole die Ticketdaten
                    string queryTicket = "SELECT * FROM Ticket WHERE ID_Ticket = @TicketID";
                    SqlCommand cmdTicket = new SqlCommand(queryTicket, con);
                    cmdTicket.Parameters.AddWithValue("@TicketID", ticketID);
                    SqlDataReader readerTicket = cmdTicket.ExecuteReader();

                    if (!readerTicket.Read())
                    {
                        MessageBox.Show("Ticket nicht gefunden!");
                        return;
                    }

                    string ticketBezeichnung = readerTicket["Bezeichnung"].ToString();
                    readerTicket.Close();

                    // Berechne den Rabatt basierend auf dem Kundenstatus
                    int rabatt = 0;
                    if (status == "Schüler")
                    {
                        rabatt = 10;
                    }
                    else if (status == "Student")
                    {
                        rabatt = 5;
                    }

                    // Erstelle die SQL-Anweisung zum Einfügen der neuen Reservierung
                    string insertQuery = "INSERT INTO Reservierung (ID_Reservierung, Vorname, Nachname, Status, Ticket, Rabatt, Anwesend) " +
                                         "VALUES (@ID_Reservierung, @Vorname, @Nachname, @Status, @Ticket, @Rabatt, 0)"; // Anwesend bleibt null

                    SqlCommand cmdInsert = new SqlCommand(insertQuery, con);
                    cmdInsert.Parameters.AddWithValue("@ID_Reservierung", reservierungsID == 0 ? DBNull.Value : (object)reservierungsID); // Wenn ReservierungsID 0, dann Null in die Datenbank
                    cmdInsert.Parameters.AddWithValue("@Vorname", vorname);
                    cmdInsert.Parameters.AddWithValue("@Nachname", nachname);
                    cmdInsert.Parameters.AddWithValue("@Status", status);
                    cmdInsert.Parameters.AddWithValue("@Ticket", ticketBezeichnung);
                    cmdInsert.Parameters.AddWithValue("@Rabatt", rabatt);

                    // Führe das Insert aus
                    cmdInsert.ExecuteNonQuery();

                    MessageBox.Show("Die Reservierung wurde erfolgreich erstellt!");


                    string setIdentityOffQuery = "SET IDENTITY_INSERT Reservierung OFF;";
                    using (SqlCommand setIdentityOffCommand = new SqlCommand(setIdentityOffQuery, con))
                    {
                        setIdentityOffCommand.ExecuteNonQuery();
                    }

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
