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
    /// Interaktionslogik für TicketLöschen.xaml
    /// </summary>
    public partial class TicketLöschen : Window
    {
        public TicketLöschen()
        {
            InitializeComponent();
        }

        private void TicketLöschen_Click(object sender, RoutedEventArgs e)
        {
            // Überprüfen, ob die TicketID leer ist oder ein ungültiger Wert (0 oder String) eingegeben wurde
            if (string.IsNullOrEmpty(txtTicketID.Text))
            {
                MessageBox.Show("Bitte eine TicketID eingeben!");
                return;
            }

            if (!int.TryParse(txtTicketID.Text, out int idTicket))
            {
                MessageBox.Show("Ungültige TicketID!");
                return;  // Rückgabe, falls ID ungültig
            }

            if(idTicket == 0)
            {
                MessageBox.Show("Die TicketID darf nicht 0 sein!");
                return;  // Rückgabe, falls ID ungültig
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

                    // SQL-Anweisung zum Löschen des Tickets
                    string query = "DELETE FROM Ticket WHERE Id_Ticket = @IdTicket";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Parameter für die SQL-Anweisung hinzufügen
                        command.Parameters.AddWithValue("@IdTicket", idTicket);

                        // Ausführen der SQL-Anweisung
                        int result = command.ExecuteNonQuery();

                        // Prüfen, ob das Ticket erfolgreich gelöscht wurde
                        if (result > 0)
                        {
                            MessageBox.Show("Ticket erfolgreich gelöscht!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ticket mit dieser ID existiert nicht.");
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
